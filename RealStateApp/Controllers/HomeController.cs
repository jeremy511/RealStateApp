using RealState.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.Services;
using RealState.Core.Application.ViewModels.Properties;

namespace RealState.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly IAdsService _adsService;
        private readonly IAdsTypeService _adsTypeService;
        public HomeController(IAgentService agentService, IAdsService adsService, IAdsTypeService adsTypeService)
        {
            _agentService = agentService;
            _adsService = adsService;
            _adsTypeService = adsTypeService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PropertyList = await _adsTypeService.GetAllViewModel();
            return View(await _adsService.GetAllViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int TypeId, int min, int max, int bed, int bath, string identifier)
        {
            PropertyFilterViewModel propertyFilterViewModel = new();
            propertyFilterViewModel.TypeId = TypeId;
            propertyFilterViewModel.Min = min;  
            propertyFilterViewModel.Max = max;
            propertyFilterViewModel.BedRooms = bed;
            propertyFilterViewModel.BathRooms = bath;
            propertyFilterViewModel.Identifier = identifier;

            if ( TypeId == 0 &&  min == 0 &&  max == 0 &&  bed == 0 &&  bath == 0 &&  identifier == null) {

                return RedirectToRoute(new {Controller = "Home", Action = "Index"});
            }


            List<PropertyViewModel> viewModels = await _adsService.AdsWithFilter(propertyFilterViewModel);
            return RedirectToRoute(new { Controller = "Home", Action = "IndexWithFilter", viewModels});

            
        }

        public async Task<IActionResult> IndexWithFilter(List<PropertyViewModel> viewModels)
        {
            ViewBag.PropertyList = await _adsTypeService.GetAllViewModel();
            return View(viewModels);
        }


        public async Task<IActionResult> AdDetails(int Id)
        {
            ViewBag.Fotos = GetImagesNames(Id);
            
            return View(await _adsService.AdsDetail(Id));
        }

        public async Task <IActionResult> Agent()
        {
            ViewBag.Filter = false;
            return View(await _agentService.ShowAgents());
           
        }

        [HttpPost]
        public async Task<IActionResult> Agent(string name)
        {
            var agents = await _agentService.AgentWithFilter(name);
            if (agents.Count == 0)
            {
                ViewBag.Filter = true;
            }

            return View(await _agentService.AgentWithFilter(name));
        }

        public async Task <IActionResult> AgentAds(string Id)
        {
            return View( await _adsService.AgentsAds(Id));
        }

        private List<String> GetImagesNames(int id)
        {

            var dir = new System.IO.DirectoryInfo("wwwroot/Images/Products/" + id.ToString());

            System.IO.FileInfo[] files = dir.GetFiles("*.*");

            List<String> names = new List<String>();
            foreach (var file in files)
            {
                names.Add(file.Name);
            }

            return names;
        }



    }
}
