using KarYemek.Models.CategoryViewModels;
using KarYemek.Models.FoodViewModels;

namespace KarYemek.Models.FoodCategoryViewModels
{
    public class FoodCategoryViewModel : BaseViewModel
    {
        public string CategoryId { get; set; }
        public string FoodId { get; set; }
        public FoodViewModel Food { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}
