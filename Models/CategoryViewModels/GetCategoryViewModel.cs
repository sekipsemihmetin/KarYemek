using KarYemek.Models.FoodCategoryViewModels;

namespace KarYemek.Models.CategoryViewModels
{
    public class GetCategoryViewModel:BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public List<FoodCategoryViewModel> FoodCategories { get; set; }

    }
}
