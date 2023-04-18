using RealState.Core.Application.ViewModels.Agents;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface IAgentService
    {
        Task<List<AgentViewModel>> ShowAgents();
        Task<List<AgentViewModel>> AgentWithFilter(string name);
    }
}