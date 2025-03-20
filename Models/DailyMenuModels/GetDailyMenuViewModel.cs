using KarYemek.Models.DailyMenuFoodViewModels;

namespace KarYemek.Models.DailyMenuModels
{
    public class GetDailyMenuViewModel:BaseViewModel
    {
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public List<DailyMenuFoodViewModel> DailyMenuFoods { get; set; }
    }
}
