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

namespace RealState.Core.Application.Features.Ads.Queries.GetAllAds
{
    public class GetAllAdsQuery : IRequest<IEnumerable<PropertyViewModel>>
    {
    }

    public class GetAllAdsQueryHandler : IRequestHandler<GetAllAdsQuery, IEnumerable<PropertyViewModel>>
    {
        private readonly IAdsRepository _adsRepository;
        private readonly IMapper _mapper;

        public GetAllAdsQueryHandler(IAdsRepository adsRepository, IMapper mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<PropertyViewModel>> Handle(GetAllAdsQuery request, CancellationToken cancellationToken)
        {
            var propertyViewModel = await GetAllViewModelWithInclude();
            return propertyViewModel;
        }

        private async Task<List<PropertyViewModel>> GetAllViewModelWithInclude()
        {
            var adsList = await _adsRepository.GetAllWithIncludes(new List<string> { "AdsType", "Sales" });

            return adsList.Select(ads => new PropertyViewModel
            {
                Id = ads.Id,
                AgentId = ads.UserId,
                BathRooms = ads.BathRooms,
                BedRooms = ads.BedRooms,
                Size = ads.Size,
                Code = ads.Identifier,
                Description = ads.Description,
                Price = ads.Price,
                Upgrates = ads.Upgrates,
                SaleType = ads.Sales.Name,
                Type = ads.AdsType.Name,






            }).ToList();
        }
    }
}
