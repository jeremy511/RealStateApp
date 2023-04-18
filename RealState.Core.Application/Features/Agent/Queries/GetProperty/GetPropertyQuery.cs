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

namespace RealState.Core.Application.Features.Agent.Queries.GetProperty
{
    public class GetPropertyQuery : IRequest<IEnumerable<PropertyViewModel>>
    {
        public string Id { get; set; }

    }

    public class GetPropertyQueryHandler : IRequestHandler<GetPropertyQuery, IEnumerable<PropertyViewModel>>
    {
        private readonly IAdsRepository _adsRepository;
        private readonly IMapper _mapper;

        public GetPropertyQueryHandler(IAdsRepository adsRepository, IMapper mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<PropertyViewModel>> Handle(GetPropertyQuery request, CancellationToken cancellationToken)
        {
            var propertyViewModels = await GetAllAgentProperty(request.Id);
            return propertyViewModels;
        }

        private async Task<List<PropertyViewModel>> GetAllAgentProperty(string id)
        {
            var adsList = await _adsRepository.GetAllWithIncludes(new List<string> { "AdsType,Sales" });

            return adsList.Where(w => w.UserId == id).Select(ads => new PropertyViewModel
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
