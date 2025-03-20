using KarYemek.Models.DailyMenuFoodViewModels;

namespace KarYemek.Models.DailyMenuModels
{
    public class UpdateDailyMenuViewModel:BaseViewModel
    {
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public List<string> SelectedFoodIds { get; set; }
        public List<DailyMenuFoodViewModel> DailyMenuFoods { get; set; }
    }
}
