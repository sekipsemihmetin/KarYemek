using BLL.AllDTOS.FoodDTOS;
using KarYemek.Models.FoodCategoryViewModels;
using KarYemek.Models.FoodViewModels;

namespace KarYemek.Models.CategoryViewModels
{
    public class CategoryViewModel:BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public List<FoodCategoryViewModel> FoodCategories { get; set; }
    }
}
