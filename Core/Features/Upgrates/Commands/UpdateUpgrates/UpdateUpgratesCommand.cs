using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Application.Features.PropertyType.Commands.UpdateCategory;
using RealState.Core.Domain.Entities;
using RealState.Core.Application.Features.Sales_.Commands.UpdateCategory;
using RealState.Core.Application.Features.Upgrates.Commands.UpdateCategory;
using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Core.Application.Features.Upgrates.Commands.UpdateCategory
{
    public class UpdateUpgratesCommand : IRequest<UpgratesUpdateResponse>
    {
        /// <example>1 </example>
        [SwaggerParameter(Description = "Id de la mejora")]
        public int Id { get; set; }

        /// <example>Pintura </example>
        [SwaggerParameter(Description = "Nombre de la mejora")]
        public string Name { get; set; }

        /// <example> se pintara cada 6 meses </example>
        [SwaggerParameter(Description = "Descripcion de la mejora")]
        public string? Description { get; set; }
    }
    public class UpdateUpgratesCommandHandler : IRequestHandler<UpdateUpgratesCommand, UpgratesUpdateResponse>
    {
        private readonly IAdsUpgrateRepository _repository;
        private readonly IMapper _mapper;

        public UpdateUpgratesCommandHandler(IAdsUpgrateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<UpgratesUpdateResponse> Handle(UpdateUpgratesCommand command, CancellationToken cancellationToken)
        {
            var category = await _repository.GetViewModelById(command.Id);

            if (category == null)
            {
                throw new Exception($" Not Found.");
            }
            else
            {
                category = _mapper.Map<AdsUpgrates>(command);
                await _repository.UpdateAsync(category,category.Id);
                var categoryVm = _mapper.Map<UpgratesUpdateResponse>(category);

                return categoryVm;
            }
        }
    }
}
