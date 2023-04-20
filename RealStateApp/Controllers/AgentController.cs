
using RealState.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Helpers;
using RealState.Core.Application.Services;
using RealState.Core.Application.ViewModels.Properties;
using RealState.Core.Application.ViewModels.Users;
using RealState.Core.Application.Interfaces.Services;

namespace RealState.Controllers
{
    public class AgentController : Controller
    {
        private readonly IAdsService _adsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;
        private readonly IAdsTypeService _adsTypeService;
        private readonly IAdsUpgrateService _adsUpgrateService;
        private readonly ISaleService _saleService;




        public AgentController(IAdsService adsService, IHttpContextAccessor httpContextAccessor, IAdsTypeService adsTypeService, IAdsUpgrateService adsUpgrateService, ISaleService saleService)
        {
            _adsService = adsService;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _adsTypeService = adsTypeService;
            _adsUpgrateService = adsUpgrateService;
            _saleService = saleService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _adsService.AgentsAds(_userViewModel.Id));
        }

        public async Task<IActionResult> MantProperties ()
        {
            return View(await _adsService.AgentsAds(_userViewModel.Id));
        }

        public async Task <IActionResult> Add()
        {
            var sales = await _saleService.GetAllViewModel();
            var type = await _adsTypeService.GetAllViewModel();
            var upgrates = await _adsUpgrateService.GetAllViewModel();
            ViewBag.Action = false;
            ViewBag.Sales = sales;
            ViewBag.Type = type;
            ViewBag.Upgrates = upgrates;

            if (sales.Count == 0 || type.Count == 0 || upgrates.Count == 0)
            {
                ViewBag.Action = true;
            }

            ViewBag.Edit = 0;
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Add(SavePropertyViewModel viewModel)
        {
            var sales = await _saleService.GetAllViewModel();
            var type = await _adsTypeService.GetAllViewModel();
            var upgrates = await _adsUpgrateService.GetAllViewModel();
            ViewBag.Action = false;
            ViewBag.Sales = sales;
            ViewBag.Type = type;
            ViewBag.Upgrates = upgrates;


            ViewBag.Edit = 0;

            if (!ModelState.IsValid)
            {
                return View(viewModel);
                ViewBag.Action = false;
            }

            if (viewModel.Files != null)
            {
                if(viewModel.Files.Count > 4)
                {
                    ViewBag.Action = false;
                    return View(viewModel);
                    ModelState.AddModelError("UserValidation", "El limite de fotos es 4, porfavor sube meno de 5 fotos");
                }

            }

            viewModel.UserId = _userViewModel.Id;
            SavePropertyViewModel vm = await _adsService.AddAsync(viewModel);
            viewModel.Photos = UploadFile(viewModel.Files[0], vm.Id);
             await _adsService.UpdateAsync(vm, vm.Id);
            if (vm.Files.Count > 1)
            {
                for (int i = 1; i < vm.Files.Count; i++)
                {
                    UploadFile(vm.Files[i], vm.Id);
                }
            }


            return RedirectToRoute(new { Controller = "Agent", Action = "MantProperties" });

        }

        public async Task <IActionResult> Edit(int Id)
        {
            var sales = await _saleService.GetAllViewModel();
            var type = await _adsTypeService.GetAllViewModel();
            var upgrates = await _adsUpgrateService.GetAllViewModel();
            ViewBag.Action = false;
            ViewBag.Sales = sales;
            ViewBag.Type = type;
            ViewBag.Upgrates = upgrates;

            ViewBag.Edit = Id;
            SavePropertyViewModel viewModel = await _adsService.GetByIdSaveViewModel(Id);
            return View("Add", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePropertyViewModel viewModel)
        {
            var sales = await _saleService.GetAllViewModel();
            var type = await _adsTypeService.GetAllViewModel();
            var upgrates = await _adsUpgrateService.GetAllViewModel();
            ViewBag.Action = false;
            ViewBag.Sales = sales;
            ViewBag.Type = type;
            ViewBag.Upgrates = upgrates;
            ViewBag.Edit = viewModel.Id;

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            SavePropertyViewModel vm = await _adsService.GetByIdSaveViewModel(viewModel.Id);
            viewModel.Photos = UploadFile(vm.Files[0], vm.Id, true, vm.Photos);

            await _adsService.UpdateAsync(viewModel, viewModel.Id);

            return RedirectToRoute(new { Controller = "Agent", Action = "MantProperties" });
        }




        public async Task <IActionResult> Delete(int Id)
        {
            await _adsService.DeleteAsync(Id);

            string basePath = $"/Images/Products/{Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }


            return RedirectToRoute(new {Controller = "Agent", Action = "MantProperties" });
        }


        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Products/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> listOfStrings = new List<string>();
            int i = 0;


            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);


            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            listOfStrings.Add(fileName);


            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{listOfStrings[0].ToString()}";
        }

    }
}
