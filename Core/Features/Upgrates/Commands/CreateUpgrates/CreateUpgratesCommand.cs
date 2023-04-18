using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace RealState.Core.Application.Features.Upgrates.Commands.CreateUpgrates
{
    public class CreateUpgratesCommand : IRequest<int>
    {
        /// <example>Pintura </example>
        [SwaggerParameter(Description = "Nombre de la mejora")]
        public string Name { get; set; }

        /// <example> se pintara cada 6 meses </example>
        [SwaggerParameter(Description = "Descripcion de la mejora")]
        public string Description { get; set; }
    }
    public class CreateUpgratesCommandHandler : IRequestHandler<CreateUpgratesCommand, int>
    {
        private readonly IAdsUpgrateRepository _adsTypeRepository;
        private readonly IMapper _mapper;
        public CreateUpgratesCommandHandler(IAdsUpgrateRepository adsTypeRepository, IMapper mapper)
        {
            _adsTypeRepository = adsTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpgratesCommand request, CancellationToken cancellationToken)
        {
            var ad = _mapper.Map<AdsUpgrates>(request);
            await _adsTypeRepository.AddAsync(ad);
            return ad.Id;
        }
    }
}
