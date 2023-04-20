using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Core.Application.Features.Sales_.Queries.GetById
{
    public class GetSalesByIdQuery : IRequest<TypeViewModel>
    {
        /// <example>1 </example>
        [SwaggerParameter(Description = "Id del tipo de venta")]
        public int Id { get; set; }

    }

    public class GetSalesByIdQueryHandler : IRequestHandler<GetSalesByIdQuery, TypeViewModel>
    {
        private readonly ISaleRepository _adsRepository;
        private readonly IMapper _mapper;

        public GetSalesByIdQueryHandler(ISaleRepository adsRepository, IMapper mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;

        }

        public async Task<TypeViewModel> Handle(GetSalesByIdQuery request, CancellationToken cancellationToken)
        {
            var propertyViewModel = await GetByIdViewModel(request.Id);
            return propertyViewModel;
        }

        private async Task<TypeViewModel> GetByIdViewModel(int id)
        {

            var ad = await _adsRepository.GetViewModelById(id);

            TypeViewModel ads = new()
            {
                Id = ad.Id,
                Name = ad.Name,
                Description = ad.Description,

            };

            return ads;
        }
    }
}
