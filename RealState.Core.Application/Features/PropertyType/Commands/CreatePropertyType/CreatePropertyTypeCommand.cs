using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace RealState.Core.Application.Features.PropertyType.Commands.CreatePropertyType
{
    public class CreatePropertyTypeCommand : IRequest<int>
    {
        /// <example>Apartamento</example>
        [SwaggerParameter(Description = "Nombre del tipo de propiedad")]

        public string Name { get; set; }

        /// <example>Apartamento ubicado en... </example>
        [SwaggerParameter(Description = "Descripcion del tipo de propiedad")]
        public string Description { get; set; }
    }
    public class CreatePropertyTypeCommandHandler : IRequestHandler<CreatePropertyTypeCommand, int>
    {
        private readonly IAdsTypeRepository _adsTypeRepository;
        private readonly IMapper _mapper;
        public CreatePropertyTypeCommandHandler(IAdsTypeRepository adsTypeRepository, IMapper mapper)
        {
            _adsTypeRepository = adsTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            var ad = _mapper.Map<AdsType>(request);
            await _adsTypeRepository.AddAsync(ad);
            return ad.Id;
        }
    }
}
