using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Core.Application.Features.Sales_.Commands.DeleteSalesById
{
    public class DeleteSalesByIdCommand : IRequest<int>
    {
        /// <example>1 </example>
        [SwaggerParameter(Description = "Id del tipo de venta")]
        public int Id { get; set; }
    }
    public class DeleteSalesByIdCommandHandler : IRequestHandler<DeleteSalesByIdCommand, int>
    {
        private readonly ISaleRepository _repository;
        public DeleteSalesByIdCommandHandler(ISaleRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(DeleteSalesByIdCommand command, CancellationToken cancellationToken)
        {
            var ad = await _repository.GetViewModelById(command.Id);
            if (ad == null) throw new Exception($"Sales Not Found.");
            await _repository.DeleteAsync(ad);
            return ad.Id;
        }
    }
}
