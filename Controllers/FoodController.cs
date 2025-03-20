using AutoMapper;
using BLL.AbstractServices.ICategoryServices;
using BLL.AbstractServices.IFoodServices;
using BLL.AllDTOS.FoodDTOS;
using KarYemek.Models.CategoryViewModels;
using KarYemek.Models.FoodViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

public class FoodController : Controller
{
    private readonly IFoodService _foodService;
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService; 

    public FoodController(IFoodService foodService, IMapper mapper, ICategoryService categoryService)
    {
        _foodService = foodService;
        _mapper = mapper;
        _categoryService = categoryService; 
    }

    [HttpGet]
    public IActionResult Index()
    {
        var foodDtos = _foodService.GetAllFood();  
        var foodViewModels = _mapper.Map<List<GetFoodViewModel>>(foodDtos);
        return View(foodViewModels);
    }
    public IActionResult Details(string id)
    {
        var foodDto = _foodService.GetByIdFood(id);
        if (foodDto == null)
        {
            return NotFound();
        }

        var foodViewModel = _mapper.Map<GetFoodViewModel>(foodDto);
        return View(foodViewModel);
    }



    public IActionResult Create()
    {
       
        var categories = _categoryService.GetAllCategory();

      
        var categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categories);

    
        ViewBag.Categories = categoryViewModels;

        return View();
    }



    [HttpPost]
    public IActionResult Create(CreateFoodViewModel foodViewModel)
    {


        if (foodViewModel.Photo != null && foodViewModel.Photo.Length > 0)
        {
        
            var uniqueFileName = $"{Guid.NewGuid()}_{foodViewModel.Photo.FileName}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", uniqueFileName);

           
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); 
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                foodViewModel.Photo.CopyTo(stream);
            }

          
            foodViewModel.ImageURL = $"/images/{uniqueFileName}";
        }
        else
        {
            foodViewModel.ImageURL = "/images/default-user.png";
        }


    
        var foodDto = new CreateFoodDTO
        {
            Name = foodViewModel.Name,
            Description = foodViewModel.Description,
            ImageURL = foodViewModel.ImageURL,
            Calorie = foodViewModel.Calorie,
            CategoryIds = foodViewModel.CategoryIds
        };

        _foodService.AddFood(foodDto);
        return RedirectToAction("Index");
    }




    public IActionResult Edit(string id)
    {

        var categories = _categoryService.GetAllCategory();


        var categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categories);


        ViewBag.Categories = categoryViewModels;

        var foodDto = _foodService.GetByIdFood(id); 
        if (foodDto == null)
        {
            return NotFound();
        }


        var foodViewModel = _mapper.Map<UpdateFoodViewModel>(foodDto);

     
 

        return View(foodViewModel);
    }


    [HttpPost]
    public IActionResult Edit(string id, UpdateFoodViewModel foodViewModel,List<string> FoodCategories)
    {
        if (foodViewModel.Photo != null && foodViewModel.Photo.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{foodViewModel.Photo.FileName}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", uniqueFileName);

            // Klasör yoksa oluştur
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Fotoğrafı kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                foodViewModel.Photo.CopyTo(stream);
            }

      
            foodViewModel.ImageURL = $"/images/{uniqueFileName}";
        }

        
        if (string.IsNullOrEmpty(foodViewModel.ImageURL))
        {
            foodViewModel.ImageURL = "/images/default-food.png";
        }

     
        foodViewModel.CategoryIds = FoodCategories;
        var foodDto = _mapper.Map<UpdateFoodDTO>(foodViewModel);
        _foodService.UpdateFood(id, foodDto);

        return RedirectToAction("Details", new { Id = id });
    }
   



    public IActionResult Delete(string id)
    {
        var foodDto = _foodService.GetByIdFood(id);  
        if (foodDto == null)
        {
            return NotFound();
        }

        var foodViewModel = _mapper.Map<GetFoodViewModel>(foodDto);
        return View(foodViewModel);
    }


    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(string id)
    {
        _foodService.RemoveFood(id);  
        return RedirectToAction("Index");
    }
}
