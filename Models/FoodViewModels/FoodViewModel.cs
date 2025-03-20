using KarYemek.Models.DailyMenuFoodViewModels;
using KarYemek.Models.FoodCategoryViewModels;

namespace KarYemek.Models.FoodViewModels
{
    public class FoodViewModel:BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public decimal Calorie { get; set; }
        public List<DailyMenuFoodViewModel> DailyMenuFoods { get; set; }
        public List<FoodCategoryViewModel> FoodCategories { get; set; }
    }
}
