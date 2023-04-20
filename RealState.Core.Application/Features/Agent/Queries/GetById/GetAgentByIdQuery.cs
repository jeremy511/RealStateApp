using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Application.Features.Agent.Queries.GetById
{
    public class GetAgentByIdQuery : IRequest<AgentApiViewModel>
    {
        public string Id { get; set; }

    }

    public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, AgentApiViewModel>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IAdsService _adsService;


        public GetAgentByIdQueryHandler(IAccountService account, IMapper mapper, IAdsService adsService)
        {
            _accountService = account;
            _mapper = mapper;
            _adsService = adsService;
        }

        public async Task<AgentApiViewModel> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
        {
            var Agent = await GetByIdViewModel(request.Id);
            return Agent;
        }

        private async Task<AgentApiViewModel> GetByIdViewModel(string id)
        {
            var agent = await _accountService.GetUserById(id);
            var ads = await _adsService.GetAllViewModel();
            AgentApiViewModel result = new()
            {
                Id = agent.Id,
                Email = agent.Email,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                Properties = ads.Where(w => w.UserId == agent.Id).Count(),
                Tel = agent.Phone
            };

            return result;
        }
    }
}
