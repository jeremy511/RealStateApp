using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace RealState.Core.Application.Features.Sales_.Commands.CreateSales
{
    public class CreateSalesCommand : IRequest<int>
    {
        /// <example>Vende</example>
        [SwaggerParameter(Description = "Nombre del tipo de venta")]

        public string Name { get; set; }

        /// <example>Se vende esta propiedad</example>

        [SwaggerParameter(Description = "Descripcion del tipo de venta")]

        public string Description { get; set; }
    }
    public class CreateSalesCommandCommandHandler : IRequestHandler<CreateSalesCommand, int>
    {
        private readonly ISaleRepository _adsTypeRepository;
        private readonly IMapper _mapper;
        public CreateSalesCommandCommandHandler(ISaleRepository adsTypeRepository, IMapper mapper)
        {
            _adsTypeRepository = adsTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSalesCommand request, CancellationToken cancellationToken)
        {
            var ad = _mapper.Map<Sales>(request);
            await _adsTypeRepository.AddAsync(ad);
            return ad.Id;
        }
    }
}
