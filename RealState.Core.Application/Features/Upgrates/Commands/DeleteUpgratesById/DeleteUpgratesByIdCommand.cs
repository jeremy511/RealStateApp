using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Core.Application.Features.Upgrates.Commands.DeleteCategoryById
{
    public class DeleteUpgratesByIdCommand : IRequest<int>
    {
        /// <example>1 </example>
        [SwaggerParameter(Description = "Id de la mejora")]
        public int Id { get; set; }
    }
    public class DeleteUpgratesByIdCommandHandler : IRequestHandler<DeleteUpgratesByIdCommand, int>
    {
        private readonly IAdsUpgrateRepository _repository;
        public DeleteUpgratesByIdCommandHandler(IAdsUpgrateRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(DeleteUpgratesByIdCommand command, CancellationToken cancellationToken)
        {
            var ad = await _repository.GetViewModelById(command.Id);
            if (ad == null) throw new Exception($"Upgrate Not Found.");
            await _repository.DeleteAsync(ad);
            return ad.Id;
        }
    }
}
