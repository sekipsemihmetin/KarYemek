using KarYemek.Models.CategoryViewModels;
using KarYemek.Models.FoodCategoryViewModels;

namespace KarYemek.Models.FoodViewModels
{
    public class CreateFoodViewModel:BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
        public string ImageURL { get; set; }
        public decimal Calorie { get; set; }
        public List<string> CategoryIds{ get; set; }

    }

}
