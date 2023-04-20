using RealState.Core.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Enums;
using RealState.Core.Application.Helpers;
using RealState.Core.Application.Services;
using RealState.Core.Application.ViewModels.Properties;
using RealState.Core.Application.ViewModels.Users;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Favourite;

namespace RealState.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
       

        private readonly IAdsService _adsService;
        private readonly IFavouritePropertyService _favouritePropertyService;
        private readonly IAdsTypeService _adsTypeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;

        public ClientController(IAdsService adsService, IFavouritePropertyService favouritePropertyService, IAdsTypeService adsTypeService, IHttpContextAccessor httpContextAccessor)
        {
            _adsService = adsService;
            _favouritePropertyService = favouritePropertyService;
            _adsTypeService = adsTypeService;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Favorites = await _favouritePropertyService.GetAllViewModel();
            ViewBag.PropertyList = await _adsTypeService.GetAllViewModel();
            return View(await _adsService.GetAllViewModel());
        }

        public async Task<IActionResult> Favourite()
        {
            return View(await _favouritePropertyService.GetAllViewModel(_userViewModel.Id));
        }

        public async Task<IActionResult> AddFavourite(int Id)
        {
            var favorite = await _favouritePropertyService.GetAllViewModel();
            favorite.Where(f => f.ProperyId == Id).Select(f => new FavouriteViewModel
            {
                ProperyId = f.ProperyId,
            }).ToList();

            if (favorite.Count != 0)
            {
                return RedirectToAction("Index");

            }

            SaveFavouriteViewModel saveFavouriteViewModel = new();
            saveFavouriteViewModel.PropetyId = Id;
            saveFavouriteViewModel.UserName = _userViewModel.Id;
            await _favouritePropertyService.AddAsync(saveFavouriteViewModel);
            return RedirectToAction("Index");
        }
    }
}
