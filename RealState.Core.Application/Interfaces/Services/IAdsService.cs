using RealState.Core.Application.ViewModels.Properties;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface IAdsService : IGenericService<SavePropertyViewModel, PropertyViewModel, Ads>
    {
        Task<List<PropertyViewModel>> AgentsAds(string id);
        Task<PropertyDetailViewModel> AdsDetail(int id);

        Task<List<PropertyViewModel>> AdsWithFilter(PropertyFilterViewModel viewModel);
    }
}
