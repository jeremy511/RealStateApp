using RealState.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Dtos.Account;
using RealState.Core.Application.Enums;
using RealState.Core.Application.Helpers;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.Services;
using RealState.Core.Application.ViewModels.Agents;
using RealState.Core.Application.ViewModels.Properties;
using RealState.Core.Application.ViewModels.Users;

namespace RealState.Controllers
{
    public class AdminController : Controller
    {

        private readonly IUserService _userService;
        private readonly IAdsService _adsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;
        private readonly ISaleService _saleService;
        private readonly IAdsTypeService _adsTypeService;
        private readonly IAdsUpgrateService _adsUpgrateService;



        public AdminController(IUserService userService, IAdsService adsService, IHttpContextAccessor httpContextAccessor, ISaleService saleService, IAdsUpgrateService adsUpgrateService, IAdsTypeService adsTypeService)
        {
            _userService = userService;
            _adsService = adsService;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _saleService = saleService;
            _adsTypeService = adsTypeService;   
            _adsUpgrateService = adsUpgrateService;
        }   

        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAllInfo());
        }

        public async Task <IActionResult> Agent()
        {
            ViewBag.HaveMessage = false;
            var Ads = await _adsService.GetAllViewModel();
            List<UserViewModel> agents = new ();
            foreach(UserViewModel agent in await _userService.GetAllAdmin(Roles.Agent.ToString()))
            {
                agent.properties = Ads.Where(a => a.UserId == agent.Id).Count();
                agents.Add(agent);
            }
            return View(agents);
        }

        public async Task<IActionResult> DeleteAge(string Id)
        {

            return View(await _userService.GetUserById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAgent(string Id)
        {
            await _userService.DeleteAgent(Id);
            return RedirectToRoute(new { Controller = "Admin", Action = "Agent" });

        }


        public async Task<IActionResult> MantAdmin()
        {
            ViewBag.HaveMessage = false;
            return View( await _userService.GetAllAdmin(Roles.Admin.ToString()));
        }

        public IActionResult RegisterAdmin()
        {
            ViewBag.HasError = false;
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> RegisterAdmin(SaveAdminViewModel vm)
        {
            ViewBag.HasError = false;


            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.UserType = Roles.Admin.ToString();
            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userService.RegisterAdmin(vm, origin);


            if (response.HasError)
            {
                ViewBag.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }


            return RedirectToRoute(new { Controller = "Admin", Action = "MantAdmin" });

        }


        public IActionResult Confirm(string UserId, int fun, string de)
        {
            EmailConfirm confirm = new();
            confirm.UserId = UserId;
            confirm.Token = fun;
            confirm.De = de;


            if (UserId == _userViewModel.Id)
            {
                string message = "No puedes desactivar tu cuenta mientras estas logeado.";
                return RedirectToRoute(new { Controller = "Admin", Action = "MantAdmin", message });

            }

            return View(confirm);
        }

        [HttpPost]
        public IActionResult Confirm(EmailConfirm confirm)
        {

            return RedirectToRoute(new { Controller = "Admin", Action = "ConfirmEmail", confirm.UserId, confirm.Token, confirm.De });

        }

        public async Task<IActionResult> ConfirmEmail(string UserId, int fun, string de)
        {
            string message = "";
            if (UserId == _userViewModel.Id)
            {
                message = "No puedes activar ni desactivar mientras estes logeado a esta cuenta.";
                return RedirectToRoute(new { Controller = "Admin", Action = "MantAdmin", message });

            }

            await _userService.Activate_Disactivate(UserId, fun);

            
            if (fun == 0)
            {

                message = "Cuenta Activada!";
            }
            else
            {
                message = "Cuenta Desactivada!";

            }

            if(de == "admin")
            {
                return RedirectToRoute(new { Controller = "Admin", Action = "MantAdmin", message });

            }

            if(de == "developer")
            {
                return RedirectToRoute(new { Controller = "Admin", Action = "MantDeveloper", message });

            }

            return RedirectToRoute(new { Controller = "Admin", Action = "Agent", message });


        }


        public async Task <IActionResult> EditAdmin(string id)
        {
            
            ViewBag.HasMessage = false;

            return View(await _userService.EditId(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditAdmin(EditUserViewModel vm)
        {
            


            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            ViewBag.HasMessage = false;
            

            await _userService.EditAdmin(vm, vm.UserType);
            string message = "Datos Actualizados Correctamente!";

            if(vm.Role == Roles.Developer.ToString())
            {
                return RedirectToRoute(new { Controller = "Admin", Action = "MantDeveloper", message });

            }

            return RedirectToRoute(new { Controller = "Admin", Action = "MantAdmin", message });

        }

        public async Task<IActionResult> ChangePass(string id)
        {

            return View(await _userService.GetChengePassId(id));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePass(PassUserViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            PassUserViewModel passUser = await _userService.ChangePass(vm);
            string message = passUser.Error;


            return RedirectToRoute(new { Controller = "Admin", Action = "EditAdmin", passUser.Id, message });

        }

        public async Task<IActionResult> MantDeveloper()
        {

            ViewBag.HaveMessage = false;

            return View(await _userService.GetAllAdmin(Roles.Developer.ToString()));
        }


        public IActionResult RegisterDeveloper()
        {
            ViewBag.HasError = false;


            return View();
        }

        [HttpPost]

        public async Task<IActionResult> RegisterDeveloper(SaveAdminViewModel vm)
        {
            ViewBag.HasError = false;


            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.UserType = Roles.Developer.ToString();
            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userService.RegisterAdmin(vm, origin);


            if (response.HasError)
            {
                ViewBag.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }


            return RedirectToRoute(new { Controller = "Admin", Action = "MantDeveloper" });

        }


        public async Task<IActionResult> MantProperty()
        {
            ViewBag.HaveMessage = false;
            return View(await _adsTypeService.GetAllViewModel());
        }

        public async Task<IActionResult> CreateProperty()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(SavePropertyTypeViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            await _adsTypeService.AddAsync(vm);
            return RedirectToRoute(new { Controller = "Admin", Action = "MantProperty" });

        }

        public async Task<IActionResult> EditProperty(int Id)
        {
            SavePropertyTypeViewModel viewModel = await _adsTypeService.GetByIdSaveViewModel(Id);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProperty(SavePropertyTypeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _adsTypeService.UpdateAsync(vm, (int)vm.Id);
            return RedirectToRoute(new { Controller = "Admin", Action = "MantProperty" });

        }

        

        public async Task<IActionResult> DeleteProp(int Id)
        {
            return View(await _adsTypeService.GetByIdSaveViewModel(Id));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProperty(int Id)
        {
            await _adsTypeService.DeleteAsync(Id);
            return RedirectToRoute(new { Controller = "Admin", Action = "MantProperty" });


        }

        public async Task<IActionResult> MantSales()
        {
            ViewBag.HaveMessage = false;
            return View(await _saleService.GetAllViewModel());
        }

        public async Task<IActionResult> CreateSale()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(SaveSalesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _saleService.AddAsync(vm);
            return RedirectToRoute(new { Controller = "Admin", Action = "MantSales" });

        }

        public async Task<IActionResult> EditSales(int Id)
        {
            SaveSalesViewModel viewModel = await _saleService.GetByIdSaveViewModel(Id);
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditSales(SaveSalesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _saleService.UpdateAsync(vm,(int)vm.Id);
            return RedirectToRoute(new { Controller = "Admin", Action = "MantSales" });

        }


        public async Task<IActionResult> DeleteSal(int Id)
        {
            return View(await _saleService.GetByIdSaveViewModel(Id));
        }


        [HttpPost]

        public async Task<IActionResult> DeleteSales(int Id)
        {
            await _saleService.DeleteAsync(Id);
            return RedirectToRoute(new { Controller = "Admin", Action = "MantSales" });

        }

        public async Task<IActionResult> MantUpgrates()
        {
            ViewBag.HaveMessage = false;
            return View(await _adsUpgrateService.GetAllViewModel());
        }

        public async Task<IActionResult> CreateUpgrates()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpgrates(SaveUpgradeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _adsUpgrateService.AddAsync(vm);
            return RedirectToRoute(new { Controller = "Admin", Action = "MantUpgrates" });

           
        }

        public async Task<IActionResult> EditUpgrate(int Id)
        {
            SaveUpgradeViewModel viewModel = await _adsUpgrateService.GetByIdSaveViewModel(Id);
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditUpgrate(SaveUpgradeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _adsUpgrateService.UpdateAsync(vm,(int)vm.Id);
            return RedirectToRoute(new { Controller = "Admin", Action = "MantUpgrates" });


        }

        public async Task<IActionResult> DeleteUgre(int Id)
        {
            return View(await _adsUpgrateService.GetByIdSaveViewModel(Id));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUpgrate(int Id)
        {
            await _adsUpgrateService.DeleteAsync(Id);
            return RedirectToRoute(new { Controller = "Admin", Action = "MantUpgrates" });

        }
    }
}
