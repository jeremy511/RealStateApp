using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Application.ViewModels.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Application.Features.Ads.Queries.GetByCode
{
    public class GetByCodeQuery : IRequest<PropertyViewModel>
    {
        public string Code { get; set; }
    }

    public class GetByCodeQueryHandler : IRequestHandler<GetByCodeQuery, PropertyViewModel>
    {
        private readonly IAdsRepository _adsRepository;
        private readonly IMapper _mapper;

        public GetByCodeQueryHandler(IAdsRepository adsRepository, IMapper mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;

        }

        public async Task<PropertyViewModel> Handle(GetByCodeQuery request, CancellationToken cancellationToken)
        {
            var propertyViewModel = await GetByCodeViewModel(request.Code);
            return propertyViewModel;
        }

        private async Task<PropertyViewModel> GetByCodeViewModel(string code)
        {
            var adsList = await _adsRepository.GetAllWithIncludes(new List<string> { "AdsType", "Sales" });
            var ad = adsList.FirstOrDefault(f => f.Identifier == code);

            PropertyViewModel ads = new()
            {
                Id = ad.Id,
                AgentId = ad.UserId,
                BathRooms = ad.BathRooms,
                BedRooms = ad.BedRooms,
                Size = ad.Size,
                Code = ad.Identifier,
                Description = ad.Description,
                Price = ad.Price,
                Upgrates = ad.Upgrates,
                SaleType = ad.Sales.Name,
                Type = ad.AdsType.Name
            };

            return ads;
        }
    }
}
