using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Core.Application.Features.PropertyType.Commands.DeleteCategoryById
{
    public class DeletePropertyTypeByIdCommand : IRequest<int>
    {
        /// <example>1  </example>
        [SwaggerParameter(Description = "Id del tipo de propiedad")]
        public int Id { get; set; }
    }
    public class DeletePropertyTypeByIdCommandCommandHandler : IRequestHandler<DeletePropertyTypeByIdCommand, int>
    {
        private readonly IAdsTypeRepository _repository;
        public DeletePropertyTypeByIdCommandCommandHandler(IAdsTypeRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(DeletePropertyTypeByIdCommand command, CancellationToken cancellationToken)
        {
            var ad = await _repository.GetViewModelById(command.Id);
            if (ad == null) throw new Exception($"AdsType Not Found.");
            await _repository.DeleteAsync(ad);
            return ad.Id;
        }
    }
}
