using RealState.Core.Application.ViewModels.Favourite;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface IFavouritePropertyService : IGenericService<SaveFavouriteViewModel, FavouriteViewModel, FavouriteProperties>
    {
        Task<List<FavouriteViewModel>> GetAllViewModel(string id);
    }
}
