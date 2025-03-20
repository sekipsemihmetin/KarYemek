using AutoMapper;
using BLL.AbstractServices.ICategoryServices;
using BLL.AllDTOS.CategoryDTOS;
using BLL.AllDTOS.FoodDTOS;
using DAL.Abstract_Repositories;
using DAL.Entities;
using KarYemek.Models.CategoryViewModels;
using KarYemek.Models.FoodViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BLL.ConcreteServices.CategoryServices
{
    public class CategoryController(ICategoryService _categoryService, IMapper _mapper) : Controller
    {



        public ActionResult Index()
        {

            try
            {
                var categoriesDto = _categoryService.GetAllCategory();
                var categoriesViewModel = _mapper.Map<List<CategoryViewModel>>(categoriesDto);
                return View(categoriesViewModel);
            }
            catch (Exception ex)
            {

                TempData["Error"] = "Kategoriler alınırken bir hata oluştu: " + ex.Message;
                return View();
            }
        }
        public IActionResult Details(string id)
        {
            var categoryDTO = _categoryService.GetByIdCategory(id);
            if (categoryDTO == null)
            {
                return NotFound();
            }

            var categoryViewModel  = _mapper.Map<GetCategoryViewModel>(categoryDTO);
            return View(categoryViewModel);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateCategory(CreateCategoryViewModel createCategoryViewModel)
        {

            if (createCategoryViewModel.Photo != null && createCategoryViewModel.Photo.Length > 0)
            {
                var uniqueFileName = $"{createCategoryViewModel.Photo.FileName}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", uniqueFileName);

                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    createCategoryViewModel.Photo.CopyTo(stream);
                }

                createCategoryViewModel.ImageURL = $"/images/{uniqueFileName}";
            }
            else
            {
                createCategoryViewModel.ImageURL = "/images/default-user.png";
            }


            _categoryService.AddCategory(_mapper.Map<CreateCategoryDTO>(createCategoryViewModel));
            TempData["Success"] = "Kategori başarıyla oluşturuldu.";
            return RedirectToAction("Index");



        }



        public ActionResult DeleteCategory(string id)
        {
            var categoryDto=_categoryService.GetByIdCategory(id);
            if (categoryDto == null)
            {
                return NotFound();
            }

            var categoryViewModel = _mapper.Map<GetCategoryViewModel>(categoryDto);
            return View(categoryViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            _categoryService.RemoveCategory(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult EditCategory(string id)
        {
          
            var categoryDTO = _categoryService.GetByIdCategory(id);
            if (categoryDTO == null)
            {
                return NotFound();
            }


            var categoryViewModel = _mapper.Map<UpdateCategoryViewModel>(categoryDTO);
            return View(categoryViewModel);
        }


        [HttpPost]
        public ActionResult EditCategory(string id, UpdateCategoryViewModel updateCategoryViewModel)
        {
            if (updateCategoryViewModel.Photo != null && updateCategoryViewModel.Photo.Length > 0)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{updateCategoryViewModel.Photo.FileName}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", uniqueFileName);

               
                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    updateCategoryViewModel.Photo.CopyTo(stream);
                }


                updateCategoryViewModel.ImageURL = $"/images/{uniqueFileName}";
            }


            if (string.IsNullOrEmpty(updateCategoryViewModel.ImageURL))
            {
                updateCategoryViewModel.ImageURL = "/images/default-food.png";
            }


     
            var categoryDTO = _mapper.Map<UpdateCategoryDTO>(updateCategoryViewModel);
            _categoryService.UpdateCategory(id, categoryDTO);

            return RedirectToAction("Details", new { Id = id });
        }
    }
}
