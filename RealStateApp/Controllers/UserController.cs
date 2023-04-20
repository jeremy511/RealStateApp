
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Dtos.Account;
using RealState.Core.Application.Enums;
using RealState.Core.Application.Helpers;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Agents;
using RealState.Core.Application.ViewModels.Users;

namespace RealState.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;


        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public IActionResult Register()
        {
            ViewBag.HasError = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            ViewBag.HasError = false;


            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"];
            vm.Photo = "sin foto";
            RegisterResponse response = await _userService.RegisterAsync(vm,origin);
            


            if(response.HasError)
            {
                ViewBag.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            SaveUserViewModel user = await _userService.GetUserByName(vm.UserName);
            user.Photo = UploadFile(vm.File, vm.UserName);
            await _userService.Edit(user, "New");

            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }

        public IActionResult Login()
        {
            ViewBag.HasError = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            ViewBag.HasError = false;
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);

            if(userVm.HasError)
            {
                ViewBag.HasError = true;
                vm.Error = userVm.Error;
                return View(vm);
            }

            HttpContext.Session.Set<AuthenticationResponse>("user", userVm);

            if (userVm.Roles.Any(a => a == Roles.Client.ToString()))
            {
                return RedirectToRoute(new { Controller = "Client", Action = "Index" });
            }

            if (userVm.Roles.Any(a => a == Roles.Admin.ToString()))
            {
                return RedirectToRoute(new { Controller = "Admin", Action = "Index" });
            }

            if (userVm.Roles.Any(a => a == Roles.Agent.ToString()))
            {
                return RedirectToRoute(new { Controller = "Agent", Action = "Index" });
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });



        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });

        }

        [Authorize] 
        public async Task<IActionResult> LogOut()
        {

            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });

        }


        public IActionResult EditAgent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditAgentl(SaveAgentViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            ViewBag.HasError = false;

            SaveUserViewModel user = await _userService.GetUserById(_userViewModel.Id);
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.Tel = viewModel.Photo;
            
            
            
            if (viewModel.ProfilePic != null)
            {
                user.Photo = UploadFile(viewModel.ProfilePic, viewModel.Id, true, user.Photo);
            }
            

             await _userService.Edit(user, Roles.Agent.ToString());
             return View();
        }

        private string UploadFile(IFormFile file, string id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/profile/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> listOfStrings = new List<string>();
            int i = 0;


            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);


            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            listOfStrings.Add(fileName);


            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{listOfStrings[0].ToString()}";
        }

    }
}
