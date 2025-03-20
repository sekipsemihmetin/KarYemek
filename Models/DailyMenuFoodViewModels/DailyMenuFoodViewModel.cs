using BLL.AllDTOS.DailyMenuDTOS;
using BLL.AllDTOS.FoodDTOS;
using KarYemek.Models.DailyMenuModels;
using KarYemek.Models.FoodViewModels;

namespace KarYemek.Models.DailyMenuFoodViewModels
{
    public class DailyMenuFoodViewModel:BaseViewModel
    {
        public string FoodId { get; set; }
        public string DailyMenuId { get; set; }
        public DailyMenuViewModel DailyMenu { get; set; }
        public FoodViewModel Food { get; set; }
    }
}
