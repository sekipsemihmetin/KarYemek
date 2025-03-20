using Microsoft.AspNetCore.Mvc;

namespace KarYemek.Controllers
{
    using AutoMapper;
    using BLL.AbstractServices.ICategoryServices;
    using BLL.AbstractServices.IDailyMenuServices;
    using BLL.AbstractServices.IFoodCategoryServices;
    using BLL.AbstractServices.IFoodServices;
    using BLL.AllDTOS.DailyMenuDTOS;
    using BLL.AllDTOS.DailyMenuFoodDTOS;
    using DAL.Data;
    using DAL.Entities;
    using KarYemek.Models.CategoryViewModels;
    using KarYemek.Models.DailyMenuModels;
    using KarYemek.Models.FoodCategoryViewModels;
    using KarYemek.Models.FoodViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class DailyMenuController : Controller
    {
        private readonly IDailyMenuService _dailyMenuService;
        private readonly IMapper _mapper;
        private readonly IFoodCategoryService _foodCategoryService;
        private readonly IFoodService _foodService;
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;

        public DailyMenuController(IDailyMenuService dailyMenuService, IMapper mapper,IFoodCategoryService foodCategoryService,IFoodService foodService,ICategoryService categoryService,AppDbContext context)
        {
            _dailyMenuService = dailyMenuService;
            _mapper = mapper;
            _foodCategoryService = foodCategoryService;
           _foodService = foodService;
          _categoryService = categoryService;
           _context = context;
        }

        public IActionResult Index()
        {
            var dailyMenus = _dailyMenuService.GetAllDailyMenus();
            var viewModel = _mapper.Map<List<GetDailyMenuViewModel>>(dailyMenus);
            return View(viewModel);
        }

        public IActionResult Details(string id)
        {
            var dailyMenu = _dailyMenuService.GetDailyMenuById(id);
            if (dailyMenu == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<GetDailyMenuViewModel>(dailyMenu);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            var foods = _foodService.GetAllFood();
            var foodViewModels = _mapper.Map<List<GetFoodViewModel>>(foods);
            ViewBag.Food = foodViewModels;  

            var categories=_categoryService.GetAllCategory();
            var categoryViewModel=_mapper.Map<List<GetCategoryViewModel>>(categories);
            ViewBag.Categories = categoryViewModel;
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(CreateDailyMenuViewModel model)
        {
            if (ModelState.IsValid)
            {
              
                var createDailyMenuDTO = new CreateDailyMenuDTO
                {
                    Date = model.Date,
                    Note = model.Note
                };

                var selectedFoodIds = model.SelectedFoodIds;

                
                _dailyMenuService.CreateDailyMenu(createDailyMenuDTO, selectedFoodIds);

                
                return RedirectToAction("Index");
            }

           
            return View(model);
        }



        public IActionResult Edit(string id)
        {
            var dailyMenu = _dailyMenuService.GetDailyMenuById(id);
            if (dailyMenu == null)
            {
                return NotFound();
            }

            var viewModel = new UpdateDailyMenuViewModel
            {
                Id = dailyMenu.Id,
                Date = dailyMenu.Date,
                Note = dailyMenu.Note,
                SelectedFoodIds = dailyMenu.DailyMenuFoods.Select(df => df.FoodId.ToString()).ToList()
            };

            // Get all available foods and categories to display in the form
            var foods = _foodService.GetAllFood();
            var foodViewModels = _mapper.Map<List<GetFoodViewModel>>(foods);
            ViewBag.Food = foodViewModels;

            var categories = _categoryService.GetAllCategory();
            var categoryViewModel = _mapper.Map<List<GetCategoryViewModel>>(categories);
            ViewBag.Categories = categoryViewModel;

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Edit(string id, UpdateDailyMenuViewModel updateDailyMenuViewModel)
        {
           

           
            var dto = _mapper.Map<UpdateDailyMenuDTO>(updateDailyMenuViewModel);

          
            var selectedFoodIds = updateDailyMenuViewModel.SelectedFoodIds;

           
            dto.SelectedFoodIds = selectedFoodIds;


            _dailyMenuService.UpdateDailyMenu(id, dto);

           
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            var dailyMenu = _dailyMenuService.GetDailyMenuById(id);
            if (dailyMenu == null)
            {
                return NotFound();
            }

           
            var viewModel = _mapper.Map<GetDailyMenuViewModel>(dailyMenu);
            return View(viewModel);
        }

        
        [HttpPost, ActionName("Delete")]
      
        public IActionResult DeleteConfirmed(string id)
        {
            var dailyMenu = _dailyMenuService.GetDailyMenuById(id);
            if (dailyMenu == null)
            {
                return NotFound();
            }

            // Call the service to delete the DailyMenu
            _dailyMenuService.RemoveDailyMenu(id);

           
            return RedirectToAction(nameof(Index));
        }
    }
}
