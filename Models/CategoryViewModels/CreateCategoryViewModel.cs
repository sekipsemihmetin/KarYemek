using KarYemek.Models.FoodCategoryViewModels;

namespace KarYemek.Models.CategoryViewModels
{
    public class CreateCategoryViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageURL { get; set; }
        public IFormFile? Photo { get; set; }

    }
}
