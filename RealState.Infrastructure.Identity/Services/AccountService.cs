using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RealState.Core.Application.Dtos.Account;
using RealState.Core.Application.Enums;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Admin;
using RealState.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailServices _emailServices;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailServices emailServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailServices = emailServices;
        }


        #region Autentifica la cuenta de manera asincronica
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();
            var User = await _userManager.FindByNameAsync(request.UserName);


            if (User == null)
            {
                response.HasError = true;
                response.Error = $"Cuenta con usuario {request.UserName} no esta registrada.";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(User, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Datos de acceso no validos.";
                return response;
            }

            if (!User.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Usuario inactivo, comuniquese con un administrador.";
                return response;
            }

            response.Id = User.Id;
            response.Email = User.Email;
            response.UserName = User.UserName;
            //Consigue los roles
            var roleList = await _userManager.GetRolesAsync(User).ConfigureAwait(false);
            response.Roles = roleList.ToList();
            response.IsVerified = User.EmailConfirmed;

            return response;
        }
        #endregion

        #region Logout
        public async Task LogOut()
        {

            await _signInManager.SignOutAsync();

        }
        #endregion

        #region Registro de usuario
        public async Task<RegisterResponse> RegisterUser(RegisterRequest request)
        {
            RegisterResponse response = new();
            response.HasError = false;

            var SameUsers = await _userManager.FindByNameAsync(request.UserName);

            if (SameUsers != null)
            {
                response.HasError = true;
                response.Error = $"El nombre de usuario {request.UserName} ya esta tomado";
                return response;
            }



            var Users = new ApplicationUser
            {
                UserName = request.UserName,
                Nombre = request.FirstName,
                Apellido = request.LastName,
                PhoneNumber = request.Tel,
                Email = request.Email,
                Cedula = request.Cedula,
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(Users, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error mientras se registraba el usuraio.";
                return response;
            }
            else
            {
                if (request.TUser == Roles.Agente.ToString())
                {
                    await _userManager.AddToRoleAsync(Users, Roles.Agente.ToString());
                }
                if (request.TUser == Roles.Client.ToString())
                {
                    await _userManager.AddToRoleAsync(Users, Roles.Client.ToString());

                }

                if (request.TUser == Roles.Desarrollador.ToString())
                {
                    await _userManager.AddToRoleAsync(Users, Roles.Agente.ToString());
                    await _userManager.AddToRoleAsync(Users, Roles.Client.ToString());
                    await _userManager.AddToRoleAsync(Users, Roles.Desarrollador.ToString());
                }

                if (request.TUser == Roles.SuperAdmin.ToString())
                {
                    await _userManager.AddToRoleAsync(Users, Roles.Agente.ToString());
                    await _userManager.AddToRoleAsync(Users, Roles.Client.ToString());
                    await _userManager.AddToRoleAsync(Users, Roles.Desarrollador.ToString());
                    await _userManager.AddToRoleAsync(Users, Roles.SuperAdmin.ToString());


                }

            }

            return response;

        }
        #endregion

        #region Registro de un Usuario cualquiera o basico
        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.FirstName,
                Apellido = request.LastName,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                var verificationUri = await SendVerificationEmailUri(user, origin);
                await _emailServices.SendAssync(new Core.Application.Dtos.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this URL {verificationUri}",
                    Subject = "Confirm registration"
                });
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            return response;
        }
        #endregion

        #region Confirmar que la Cuenta esta sincronizada
        public async Task<string> ConfirmAccountAsync(string userId, int token)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return "Cuenta no registrada";
            }

            if (token == 0)
            {
                user.EmailConfirmed = true;
            }
            if (token == 1)
            {
                user.EmailConfirmed = false;
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return $"Cuenta activada/desactivada";
            }
            else
            {
                return $"Error mientras confirmaba la cuenta.";
            }



        }
        #endregion

        #region Enlista los usuarios administradores
        public async Task<List<AdminUserListViewModel>> GetAll()
        {
            var Users = await _userManager.Users.ToListAsync();
            var result = new List<AdminUserListViewModel>();


            foreach (var users in Users)
            {
                AdminUserListViewModel model = new();
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(users.Id);

                List<string> roles = (List<string>)await _userManager.GetRolesAsync(applicationUser);

                model.UserName = users.UserName;
                model.Id = users.Id;
                model.Name = users.Nombre;
                model.LastName = users.Apellido;
                model.Roles = roles;
                model.Status = users.EmailConfirmed;

                result.Add(model);

            }

            return result.ToList();
        }
        #endregion

        #region Enlista todos los usarios
        public async Task<List<GettingAllUsers>> GetAllUsers()
        {
            List<ApplicationUser> Users = await _userManager.Users.ToListAsync();
            List<GettingAllUsers> AllUsers = new();

            foreach (var user in Users)
            {
                GettingAllUsers gettingAll = new();
                gettingAll.IsActive = user.EmailConfirmed;
                gettingAll.Id = user.Id;
                AllUsers.Add(gettingAll);
            }
            return AllUsers.ToList();


        }
        #endregion

        #region Consigue los roles de los usario por medio de un Id
        public async Task<string> GetUserRoles(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            List<string> roles = (List<string>)await _userManager.GetRolesAsync(user);

            if (roles.Count == 4)
            {
                return Roles.SuperAdmin.ToString();
            }
            if (roles.Count == 3)
            {
                return Roles.Desarrollador.ToString();
            }
            if (roles.Count == 2)
            {
                return Roles.Client.ToString();
            }

            return null;
        }
        #endregion

        #region Confirmacion de Mails
        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
        #endregion

        #region Cambiar contraseña
        public async Task ChangePass(RegisterRequest request)
        {
            ApplicationUser User = await _userManager.FindByIdAsync(request.Id);
            var NewPassToken = await _userManager.GeneratePasswordResetTokenAsync(User);

            await _userManager.ResetPasswordAsync(User, NewPassToken, request.Password);
        }
        #endregion

        #region Editar un usuario
        public async Task Edit(RegisterRequest request, string role)
        {
            ApplicationUser User = await _userManager.FindByIdAsync(request.Id);


            if (role == Roles.SuperAdmin.ToString())
            {
                User.Nombre = request.FirstName;
                User.Apellido = request.LastName;
                User.Cedula = request.Cedula;
                User.Email = request.Email;
                User.UserName = request.UserName;
                User.PasswordHash = request.Password;
                await _userManager.UpdateAsync(User);

            }

            if (role == Roles.Desarrollador.ToString())
            {
                User.Nombre = request.FirstName;
                User.Apellido = request.LastName;
                User.Cedula = request.Cedula;
                User.Email = request.Email;
                User.UserName = request.UserName;
                User.PasswordHash = request.Password;
                await _userManager.UpdateAsync(User);

            }

        }
        #endregion

        #region Busca el usuario por el Id
        public async Task<RegisterRequest> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            RegisterRequest response = new();
            response.Id = user.Id;
            response.UserName = user.UserName;
            response.LastName = user.Apellido;
            response.FirstName = user.Nombre;
            response.Cedula = user.Cedula;
            response.Password = user.PasswordHash;
            response.Email = user.Email;

            return response;
        }
        #endregion

        #region Busca el usuario por el nombre
        public async Task<RegisterRequest> GetUserByName(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            RegisterRequest response = new();
            response.Id = user.Id;
            response.UserName = user.UserName;
            response.LastName = user.Apellido;
            response.FirstName = user.Nombre;
            response.TUser = user.TUser;
            response.Password = user.PasswordHash;
            response.Email = user.Email;

            return response;
        }
        #endregion

    }
}
