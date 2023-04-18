using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Application.Features.PropertyType.Commands.UpdateCategory;
using RealState.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using RealState.Core.Application.Features.Sales_.Commands.UpdateSales;

namespace RealState.Core.Application.Features.Sales_.Commands.UpdateCategory
{
    public class UpdateSalesCommand : IRequest<SalesUpdateResponse>
    {
        /// <example>1 </example>
        [SwaggerParameter(Description = "Id del tipo de venta")]
        public int Id { get; set; }

        /// <example>Alquila</example>
        [SwaggerParameter(Description = "Nuevo nombre")]
        public string Name { get; set; }

        /// <example>Se alquila... </example>
        [SwaggerParameter(Description = "Nueva descripcion ")]
        public string? Description { get; set; }
    }
    public class UpdateSalesCommandHandler : IRequestHandler<UpdateSalesCommand, SalesUpdateResponse>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public UpdateSalesCommandHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<SalesUpdateResponse> Handle(UpdateSalesCommand command, CancellationToken cancellationToken)
        {
            var category = await _repository.GetViewModelById(command.Id);

            if (category == null)
            {
                throw new Exception($" Not Found.");
            }
            else
            {
                category = _mapper.Map<Sales>(command);
                await _repository.UpdateAsync(category, category.Id);
                var categoryVm = _mapper.Map<SalesUpdateResponse>(category);

                return categoryVm;
            }
        }
    }
}
