using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Core.Application.Features.Upgrates.Queries.GetById
{
    public class GetUpgratesByIdQuery : IRequest<TypeViewModel>
    {
        /// <example>1 </example>
        [SwaggerParameter(Description = "Id de la mejora")]
        public int Id { get; set; }

    }

    public class GetUpgratesByIdQueryHandler : IRequestHandler<GetUpgratesByIdQuery, TypeViewModel>
    {
        private readonly IAdsUpgrateRepository _adsRepository;
        private readonly IMapper _mapper;

        public GetUpgratesByIdQueryHandler(IAdsUpgrateRepository adsRepository, IMapper mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;

        }

        public async Task<TypeViewModel> Handle(GetUpgratesByIdQuery request, CancellationToken cancellationToken)
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
