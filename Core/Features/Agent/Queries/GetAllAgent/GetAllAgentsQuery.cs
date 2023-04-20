using AutoMapper;
using MediatR;
using RealState.Core.Application.Enums;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Application.Features.Agent.Queries.GetAllAgent
{
    public class GetAllAgentsQuery : IRequest<IEnumerable<AgentApiViewModel>>
    {
    }

    public class GetAllAgentsQueryHandler : IRequestHandler<GetAllAgentsQuery, IEnumerable<AgentApiViewModel>>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IAdsService _adsService;

        public GetAllAgentsQueryHandler(IAccountService accountService, IMapper mapper, IAdsService adsService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _adsService = adsService;

        }

        public async Task<IEnumerable<AgentApiViewModel>> Handle(GetAllAgentsQuery request, CancellationToken cancellationToken)
        {
            var agent = await GetAllAgents();
            return agent;
        }

        private async Task<List<AgentApiViewModel>> GetAllAgents()
        {
            var Agents = await _accountService.GetAllAdmin(Roles.Agent.ToString());
            var ads = await _adsService.GetAllViewModel();

            return Agents.Select( agent => new AgentApiViewModel
            {
                Id = agent.Id,
                Email = agent.Email,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                Properties = ads.Where(w => w.UserId == agent.Id).Count(),
                Tel = agent.Phone
            


            }).ToList();
        }
    }
}
