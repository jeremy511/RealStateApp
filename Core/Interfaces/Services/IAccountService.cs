

using RealState.Core.Application.Dtos.Account;
using RealState.Core.Application.ViewModels.Agents;
using RealState.Core.Application.ViewModels.Users;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);

        Task<string> GetRole(string Id);
        Task DeleteAgent(string Id);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);

        Task SignOutAsync();

        Task<int> GetUserCount(string role, bool active);

        Task<RegisterRequest> GetUserByName(string name);

        Task Edit(RegisterRequest request, string role);

        Task<List<AgentViewModel>> GetAllAgents();

        Task<List<UserViewModel>> GetAllAdmin(string role);

        Task ChangePass(RegisterRequest request);
        Task<RegisterRequest> GetUserById(string id);

        Task<string> Activate_Desactivate(string userId, int action);
    }
}