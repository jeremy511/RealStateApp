using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Application.Features.Upgrates.Queries.GetAllUpgrates
{
    public class GetAllUpgratesQuerty : IRequest<IEnumerable<TypeViewModel>>
    {
    }

    public class GetAllUpgratesQuertyHandler : IRequestHandler<GetAllUpgratesQuerty, IEnumerable<TypeViewModel>>
    {
        private readonly IAdsUpgrateRepository _adsRepository;
        private readonly IMapper _mapper;

        public GetAllUpgratesQuertyHandler(IAdsUpgrateRepository adsRepository, IMapper mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<TypeViewModel>> Handle(GetAllUpgratesQuerty request, CancellationToken cancellationToken)
        {
            var propertyTypeViewModel = await GetAllViewModel();
            return propertyTypeViewModel;
        }

        private async Task<List<TypeViewModel>> GetAllViewModel()
        {
            var adsList = await _adsRepository.GetAllViewModel();

            return adsList.Select(ads => new TypeViewModel
            {
                Id = ads.Id,
                Name = ads.Name,
                Description = ads.Description,






            }).ToList();
        }
    }
}
