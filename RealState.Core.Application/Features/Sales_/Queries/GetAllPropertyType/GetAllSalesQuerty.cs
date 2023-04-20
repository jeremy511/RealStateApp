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

namespace RealState.Core.Application.Features.Sales_.Queries.GetAllSales
{
    public class GetAllSalesQuerty : IRequest<IEnumerable<TypeViewModel>>
    {
    }

    public class GetAllSalesQuertyHandler : IRequestHandler<GetAllSalesQuerty, IEnumerable<TypeViewModel>>
    {
        private readonly ISaleRepository _adsRepository;
        private readonly IMapper _mapper;

        public GetAllSalesQuertyHandler(ISaleRepository adsRepository, IMapper mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<TypeViewModel>> Handle(GetAllSalesQuerty request, CancellationToken cancellationToken)
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
