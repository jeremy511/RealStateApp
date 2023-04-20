
using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Dtos.Account;
using RealState.Core.Application.Helpers;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Properties;
using RealState.Core.Application.ViewModels.Users;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Services
{
    public class AdsService : GenericService<SavePropertyViewModel, PropertyViewModel, Ads>, IAdsService
    {
        private readonly IAdsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IAdsTypeRepository _adsTypeRepository;
        private readonly ISaleRepository _saleRepository;

        public AdsService(IAdsRepository repository, IMapper mapper, IAccountService accountService, IAdsTypeRepository adsTypeRepository, ISaleRepository saleRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _accountService = accountService;
            _adsTypeRepository = adsTypeRepository;
            _saleRepository = saleRepository;
        }


        public async Task<SavePropertyViewModel> AddAsync(SavePropertyViewModel viewModel)
        {
            string RandomID = RandomId.GenerateId();
            var CheckingUniqueID = await _repository.GetAllViewModel();
            viewModel.Photos = "sin fotos";
            for (int i = 0; i < 1; i++)
            {
                var CheckingId = CheckingUniqueID.Where(x => x.Identifier == RandomID).Select(x => new SavePropertyViewModel { Identifier = x.Identifier }).ToList();
                if (CheckingId.Count == 0)
                {
                    Ads ads = _mapper.Map<Ads>(viewModel);
                    ads.Identifier = RandomID;

                    await _repository.AddAsync(ads);


                }
                else
                {
                    RandomID = RandomId.GenerateId();
                    i = i - 1;
                }
            }


            var GettingID = await _repository.GetAllViewModel();
            viewModel.Identifier = RandomID;
            viewModel.Id = GettingID.FirstOrDefault(f => f.Identifier == RandomID).Id;
            return viewModel;
        }


        public async Task<PropertyDetailViewModel> AdsDetail(int id)
        {
            Ads ads = await _repository.GetViewModelById(id);
            PropertyDetailViewModel propertyDetailViewModel = new();
            var types = await _adsTypeRepository.GetAllViewModel();
            types.Where(d => d.Id == id).Select(s => new AdsType
            {
                Name = s.Name,
            }).ToList();

            var sales = await _saleRepository.GetAllViewModel();
            types.Where(d => d.Id == id).Select(s => new Sales
            {
                Name = s.Name,
            }).ToList();



            RegisterRequest user =await _accountService.GetUserById(ads.UserId);
            propertyDetailViewModel.Price = ads.Price;
            propertyDetailViewModel.Description = ads.Description;
            propertyDetailViewModel.BathRooms = ads.BathRooms;
            propertyDetailViewModel.BedRooms = ads.BedRooms;
            propertyDetailViewModel.Photos = ads.Photos;
            propertyDetailViewModel.Identifier = ads.Identifier;
            propertyDetailViewModel.Id = ads.Id;
            propertyDetailViewModel.Size = ads.Size;

            propertyDetailViewModel.Type = types.ElementAtOrDefault(0).Name;
            propertyDetailViewModel.SaleType = sales.ElementAtOrDefault(0).Name;



            propertyDetailViewModel.Upgrates = ads.Upgrates;
            propertyDetailViewModel.UserTel = user.Photo;
            propertyDetailViewModel.UserPhoto = user.Photo;
            propertyDetailViewModel.UserEmail = user.Email;
            propertyDetailViewModel.UserName = user.FirstName +" "+ user.LastName;

            return propertyDetailViewModel;





        }

        public async Task<List<PropertyViewModel>> AgentsAds(string id)
        {
            var ads = await _repository.GetAllWithIncludes(new List<string> {"AdsType", "Sales"});
            return ads.Where(a => a.UserId == id).Select(ad => new PropertyViewModel
            {
                Identifier = ad.Identifier,
                UserId = ad.UserId,
                BathRooms = ad.BathRooms,
                BedRooms = ad.BedRooms,
                Description = ad.Description,
                Id = ad.Id,
                Photos = ad.Photos,
                Price = ad.Price,
                SaleType = ad.Sales.Name,
                Size = ad.Size,
                Type = ad.AdsType.Name,
                Upgrates = ad.Upgrates
            }).ToList();
        }

        public async Task<List<PropertyViewModel>> AdsWithFilter(PropertyFilterViewModel viewModel)
        {


            var ads = await _repository.GetAllWithIncludes(new List<string> { "AdsType", "Sales" });

            if (viewModel.Identifier != null)
            {
                return ads.Where(a => a.Identifier == viewModel.Identifier).Select(ad => new PropertyViewModel
                {
                    Identifier = ad.Identifier,
                    UserId = ad.UserId,
                    BathRooms = ad.BathRooms,
                    BedRooms = ad.BedRooms,
                    Description = ad.Description,
                    Id = ad.Id,
                    Photos = ad.Photos,
                    Price = ad.Price,
                    SaleType = ad.Sales.Name,
                    Size = ad.Size,
                    Type = ad.AdsType.Name,
                    Upgrates = ad.Upgrates
                }).ToList<PropertyViewModel>();
            }

            if (viewModel.TypeId != 00)
            {
                if (viewModel.Max != 0 && viewModel.Min != 0 && viewModel.BedRooms == 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.Price < viewModel.Max && a.Price > viewModel.Min && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();
                }

                if (viewModel.Max != 0 && viewModel.Min == 0 && viewModel.BedRooms == 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.Price < viewModel.Max && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();
                }

                if (viewModel.Max == 0 && viewModel.Min != 0 && viewModel.BedRooms == 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.Price > viewModel.Min && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max == 0 && viewModel.Min == 0 && viewModel.BedRooms != 0 && viewModel.BathRooms !=0 )
                {
                    return ads.Where(a => a.BedRooms == viewModel.BedRooms && a.BathRooms == viewModel.BathRooms && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max == 0 && viewModel.Min == 0 && viewModel.BedRooms != 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.BedRooms == viewModel.BedRooms  && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max == 0 && viewModel.Min == 0 && viewModel.BedRooms == 0 && viewModel.BathRooms != 0)
                {
                    return ads.Where(a => a.BathRooms == viewModel.BathRooms && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max != 0 && viewModel.Min != 0 && viewModel.BedRooms != 0 && viewModel.BathRooms != 0)
                {
                    return ads.Where(a => a.BathRooms == viewModel.BathRooms && a.BedRooms == viewModel.BedRooms && a.Price > viewModel.Min && a.Price < viewModel.Max  && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max != 0 && viewModel.Min == 0 && viewModel.BedRooms == 0 && viewModel.BathRooms != 0)
                {
                    return ads.Where(a => a.BathRooms == viewModel.BathRooms &&  a.Price < viewModel.Max  && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max != 0 && viewModel.Min == 0 && viewModel.BedRooms != 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.BedRooms == viewModel.BedRooms && a.Price < viewModel.Max && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max == 0 && viewModel.Min != 0 && viewModel.BedRooms != 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.BedRooms == viewModel.BedRooms && a.Price > viewModel.Min && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max == 0 && viewModel.Min != 0 && viewModel.BedRooms == 0 && viewModel.BathRooms != 0)
                {
                    return ads.Where(a => a.BathRooms == viewModel.BathRooms && a.Price > viewModel.Min && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }


                return ads.Where(a => a.AdsType.Id ==  viewModel.TypeId ).Select(ad => new PropertyViewModel
                {
                    Identifier = ad.Identifier,
                    UserId = ad.UserId,
                    BathRooms = ad.BathRooms,
                    BedRooms = ad.BedRooms,
                    Description = ad.Description,
                    Id = ad.Id,
                    Photos = ad.Photos,
                    Price = ad.Price,
                    SaleType = ad.Sales.Name,
                    Size = ad.Size,
                    Type = ad.AdsType.Name,
                    Upgrates = ad.Upgrates
                }).ToList<PropertyViewModel>();

            }
            if (viewModel.TypeId == 00)
            {
                if (viewModel.Max != 0 && viewModel.Min != 0 && viewModel.BedRooms == 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.Price < viewModel.Max && a.Price > viewModel.Min && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();
                }

                if (viewModel.Max != 0 && viewModel.Min == 0 && viewModel.BedRooms == 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.Price < viewModel.Max && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();
                }

                if (viewModel.Max == 0 && viewModel.Min != 0 && viewModel.BedRooms == 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.Price > viewModel.Min && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max == 0 && viewModel.Min == 0 && viewModel.BedRooms != 0 && viewModel.BathRooms != 0)
                {
                    return ads.Where(a => a.BedRooms == viewModel.BedRooms && a.BathRooms == viewModel.BathRooms && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max == 0 && viewModel.Min == 0 && viewModel.BedRooms != 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.BedRooms == viewModel.BedRooms && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max == 0 && viewModel.Min == 0 && viewModel.BedRooms == 0 && viewModel.BathRooms != 0)
                {
                    return ads.Where(a => a.BathRooms == viewModel.BathRooms && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max != 0 && viewModel.Min != 0 && viewModel.BedRooms != 0 && viewModel.BathRooms != 0)
                {
                    return ads.Where(a => a.BathRooms == viewModel.BathRooms && a.BedRooms == viewModel.BedRooms && a.Price > viewModel.Min && a.Price < viewModel.Max && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max != 0 && viewModel.Min == 0 && viewModel.BedRooms == 0 && viewModel.BathRooms != 0)
                {
                    return ads.Where(a => a.BathRooms == viewModel.BathRooms && a.Price < viewModel.Max && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max != 0 && viewModel.Min == 0 && viewModel.BedRooms != 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.BedRooms == viewModel.BedRooms && a.Price < viewModel.Max && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max == 0 && viewModel.Min != 0 && viewModel.BedRooms != 0 && viewModel.BathRooms == 0)
                {
                    return ads.Where(a => a.BedRooms == viewModel.BedRooms && a.Price > viewModel.Min && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }

                if (viewModel.Max == 0 && viewModel.Min != 0 && viewModel.BedRooms == 0 && viewModel.BathRooms != 0)
                {
                    return ads.Where(a => a.BathRooms == viewModel.BathRooms && a.Price > viewModel.Min && viewModel.TypeId == a.AdsType.Id).Select(ad => new PropertyViewModel
                    {
                        Identifier = ad.Identifier,
                        UserId = ad.UserId,
                        BathRooms = ad.BathRooms,
                        BedRooms = ad.BedRooms,
                        Description = ad.Description,
                        Id = ad.Id,
                        Photos = ad.Photos,
                        Price = ad.Price,
                        SaleType = ad.Sales.Name,
                        Size = ad.Size,
                        Type = ad.AdsType.Name,
                        Upgrates = ad.Upgrates
                    }).ToList<PropertyViewModel>();

                }


                return ads.Select(ad => new PropertyViewModel
                {
                    Identifier = ad.Identifier,
                    UserId = ad.UserId,
                    BathRooms = ad.BathRooms,
                    BedRooms = ad.BedRooms,
                    Description = ad.Description,
                    Id = ad.Id,
                    Photos = ad.Photos,
                    Price = ad.Price,
                    SaleType = ad.Sales.Name,
                    Size = ad.Size,
                    Type = ad.AdsType.Name,
                    Upgrates = ad.Upgrates
                }).ToList<PropertyViewModel>();

            }




            return ads.Select(ad => new PropertyViewModel
            {
                Identifier = ad.Identifier,
                UserId = ad.UserId,
                BathRooms = ad.BathRooms,
                BedRooms = ad.BedRooms,
                Description = ad.Description,
                Id = ad.Id,
                Photos = ad.Photos,
                Price = ad.Price,
                SaleType = ad.Sales.Name,
                Size = ad.Size,
                Type = ad.AdsType.Name,
                Upgrates = ad.Upgrates
            }).ToList<PropertyViewModel>();
        }
    }
}
