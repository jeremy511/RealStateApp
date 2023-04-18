
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Agents;

namespace RealState.Core.Application.Services
{
    public class AgentService : IAgentService
    {
        private readonly IAccountService _accountService;
        public AgentService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<List<AgentViewModel>> ShowAgents()
        {
            return await _accountService.GetAllAgents();
        }

        public async Task<List<AgentViewModel>> AgentWithFilter(string name)
        {
            var agents = await _accountService.GetAllAgents();
            return agents.Where(a => a.FirstName == name ).Select(agent => new AgentViewModel
            {
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                Photo = agent.Photo,
                Id = agent.Id,
                
            }).ToList();
        }

       
    }
}
