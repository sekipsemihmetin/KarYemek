using KarYemek.Models.DailyMenuFoodViewModels;

namespace KarYemek.Models.DailyMenuModels
{
    public class CreateDailyMenuViewModel
    {
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public List<string> SelectedFoodIds { get; set; } = new();
   
    }
}
