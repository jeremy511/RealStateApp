using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Core.Application.Features.PropertyType.Commands.UpdateCategory
{
    public class UpdatePropertyTypeCommand : IRequest<PropertyTypeUpdateResponse>
    {
        /// <example>1 </example>
        [SwaggerParameter(Description = "Id del tipo de propiedad")]
        public int Id { get; set; }

        /// <example>Casa</example>
        [SwaggerParameter(Description = "Nuevo nombre")]
        public string Name { get; set; }

        /// <example>Casa ubicado en... </example>
        [SwaggerParameter(Description = "Nueva descripcion ")]
        public string? Description { get; set; }
    }
    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, PropertyTypeUpdateResponse>
    {
        private readonly IAdsTypeRepository _repository;
        private readonly IMapper _mapper;

        public UpdatePropertyTypeCommandHandler(IAdsTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PropertyTypeUpdateResponse> Handle(UpdatePropertyTypeCommand command, CancellationToken cancellationToken)
        {
            var category = await _repository.GetViewModelById(command.Id);

            if (category == null)
            {
                throw new Exception($" Not Found.");
            }
            else
            {
                category = _mapper.Map<AdsType>(command);
                await _repository.UpdateAsync(category, category.Id);
                var categoryVm = _mapper.Map<PropertyTypeUpdateResponse>(category);

                return categoryVm;
            }
        }
    }
}
