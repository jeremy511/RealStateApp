using RealState.Core.Application.Dtos.Account;
using RealState.Core.Application.ViewModels.Admin;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task ChangePass(RegisterRequest request);
        Task<string> ConfirmAccountAsync(string userId, int token);
        Task Edit(RegisterRequest request, string role);
        Task<List<AdminUserListViewModel>> GetAll();
        Task<List<GettingAllUsers>> GetAllUsers();
        Task<RegisterRequest> GetUserById(string id);
        Task<RegisterRequest> GetUserByName(string name);
        Task<string> GetUserRoles(string id);
        Task LogOut();
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterUser(RegisterRequest request);
    }
}