using AutoMapper;
using MediatR;
using RealState.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Application.Features.Agent.Commands
{
    public class ChangeStatusCommand : IRequest<int>
    {
        /// <example>dsadsa-234-3dsadas</example>

        [SwaggerParameter(Description  = "Id del agente")]

        

        public string Id { get; set; }
        /// <example>true</example>
        [SwaggerParameter(Description = "True para activar / false para desactivar cuenta")]

        public bool Action { get; set; }


    }
    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, int>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public ChangeStatusCommandHandler(IAccountService account, IMapper mapper)
        {
            _accountService = account;
            _mapper = mapper;
        }

        public async Task<int> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.Action)
            {
                await _accountService.Activate_Desactivate(request.Id, 3);
            }
            else
            {
                await _accountService.Activate_Desactivate(request.Id, 4);
            }
            return 1;

        }
    }
}
