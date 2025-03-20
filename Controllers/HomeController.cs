using AutoMapper;
using BLL.AbstractServices.IDailyMenuServices;
using KarYemek.Models;
using KarYemek.Models.DailyMenuModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KarYemek.Controllers
{
    public class HomeController(ILogger<HomeController> _logger,IDailyMenuService _dailyMenuService,IMapper _mapper) : Controller
    {
   
        public IActionResult Index ()
        {
            return View ();
        }

        public IActionResult Privacy ()
        {
            return View ();
        }

        public IActionResult AboutUs ()
        {
            return View ();
        }

        public IActionResult Services ()
        {
            return View ();
        }

        public IActionResult Events ()
        {
            return View ();
        }

        public IActionResult Foods(DateTime? startOfWeek, DateTime? endOfWeek)
        {
            // If startOfWeek or endOfWeek is null, calculate them based on the current date
            if (!startOfWeek.HasValue || !endOfWeek.HasValue)
            {
                var currentDate = DateTime.Today;
                // Calculate the start of the week (Monday)
                startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday); // Monday of the current week
                                                                                                        // Calculate the end of the week (Sunday)
                endOfWeek = startOfWeek.Value.AddDays(6); // Sunday of the current week
            }

            // Retrieve the daily menus for the specified week (limit to 7)
            var weeklyMenus = _dailyMenuService.GetMenusForWeek(startOfWeek.Value, endOfWeek.Value)
                                                .Take(7) // Only take 7 items for the week
                                                .ToList();

            // Map weekly menus to view models
            var weeklyMenusViewModel = _mapper.Map<List<GetDailyMenuViewModel>>(weeklyMenus);

            // Retrieve all daily menus (unchanged part)
            var allDailyMenus = _dailyMenuService.GetAllDailyMenus();
            var allDailyMenusViewModel = _mapper.Map<List<GetDailyMenuViewModel>>(allDailyMenus);

            // Combine weekly menus and all daily menus without duplication, ensuring no missing data
            var combinedViewModel = weeklyMenusViewModel.Concat(allDailyMenusViewModel)
                .OrderBy(x => x.Date)  // Order by date to maintain sequence
                .ToList();

            // Pass the start and end week data for navigation in ViewData
            var startOfWeekNext = startOfWeek.Value.AddDays(7);
            var endOfWeekNext = endOfWeek.Value.AddDays(7);
            var startOfWeekPrev = startOfWeek.Value.AddDays(-7);
            var endOfWeekPrev = endOfWeek.Value.AddDays(-7);

            ViewData["StartOfWeekPrev"] = startOfWeekPrev;
            ViewData["EndOfWeekPrev"] = endOfWeekPrev;
            ViewData["StartOfWeekNext"] = startOfWeekNext;
            ViewData["EndOfWeekNext"] = endOfWeekNext;

            // Return the view with the combined menus
            return View(combinedViewModel);
        }









        public IActionResult Contact ()
        {
            return View ();
        }

        public IActionResult GetOffer ()
        {
            return View ();
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error ()
        {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
