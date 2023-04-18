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

namespace RealState.Core.Application.Features.Ads.Queries.GetById
{
    public class GetAdByIdQuery : IRequest<PropertyViewModel>
    {
        public int Id { get; set; }

    }

    public class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, PropertyViewModel>
    {
        private readonly IAdsRepository _adsRepository;
        private readonly IMapper _mapper;

        public GetAdByIdQueryHandler(IAdsRepository adsRepository, IMapper mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;

        }

        public async Task<PropertyViewModel> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            var propertyViewModel = await GetByIdViewModel(request.Id);
            return propertyViewModel;
        }

        private async Task<PropertyViewModel> GetByIdViewModel(int id)
        {
            var adsList = await _adsRepository.GetAllWithIncludes(new List<string> { "AdsType", "Sales" });
            var ad = adsList.FirstOrDefault(f => f.Id == id);

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
