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

namespace RealState.Core.Application.Features.PropertyType.Queries.GetAllPropertyType
{
    public class GetAllPropertyTypeQuerty : IRequest<IEnumerable<TypeViewModel>>
    {
    }

    public class GetAllPropertyTypeQuertyHandler : IRequestHandler<GetAllPropertyTypeQuerty, IEnumerable<TypeViewModel>>
    {
        private readonly IAdsTypeRepository _adsRepository;
        private readonly IMapper _mapper;

        public GetAllPropertyTypeQuertyHandler(IAdsTypeRepository adsRepository, IMapper mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<TypeViewModel>> Handle(GetAllPropertyTypeQuerty request, CancellationToken cancellationToken)
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
