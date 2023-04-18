
using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Favourite;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Services
{
    public class FavouritePropertyService : GenericService<SaveFavouriteViewModel, FavouriteViewModel, FavouriteProperties>, IFavouritePropertyService
    {
        private readonly IFavouritePretsRepository _repository;
        private readonly IMapper _mapper;


        public FavouritePropertyService(IFavouritePretsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FavouriteViewModel>> GetAllViewModel(string id)
        {
            var favorites = await _repository.GetAllWithIncludes(new List<string> { "Ads" });
            return favorites.Where(f => f.UserName == id).Select(f => new FavouriteViewModel
            {
                BathRooms = f.Ads.BathRooms,
                BedRooms = f.Ads.BedRooms,
                Description = f.Ads.Description,
                Id = f.Id,
                Photos = f.Ads.Photos,
                Price = f.Ads.Price,
               Identifier = f.Ads.Identifier,
                Size = f.Ads.Size,
                ProperyId = f.PropetyId,
                
                Upgrates = f.Ads.Upgrates,
            }).ToList();
        }




    }
}
