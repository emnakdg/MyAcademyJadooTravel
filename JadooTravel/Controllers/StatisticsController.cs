using JadooTravel.Services.BookingServices;
using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IDestinationService _destinationService;

        public StatisticsController(IBookingService bookingService, IDestinationService destinationService)
        {
            _bookingService = bookingService;
            _destinationService = destinationService;
        }

        public async Task<IActionResult> Index()
        {
            //Son 5 Rezervasyon
            var latestBookings = await _bookingService.GetLatestBookingsAsync(5);
            ViewBag.LatestBookings = latestBookings;

            //Son Eklenen 4 Tur
            var allDestinations = await _destinationService.GetAllDestinationAsync();
            var latestTours = allDestinations.OrderByDescending(x => x.DestinationId).Take(4).ToList();
            ViewBag.LatestTours = latestTours;

            //Grafikler için veriler
            var allBookings = await _bookingService.GetAllBookingsAsync();

            //Turlar ve rezervasyon sayıları grafiği
            var tourBookingCounts = allBookings
                .GroupBy(b => b.DestinationName ?? "Bilinmeyen")
                .Select(g => new { TourName = g.Key, Count = g.Sum(b => b.NumberOfPeople) })
                .OrderByDescending(x => x.Count)
                .Take(6)
                .ToList();
            
            ViewBag.TourNames = tourBookingCounts.Select(x => x.TourName).ToList();
            ViewBag.TourCounts = tourBookingCounts.Select(x => x.Count).ToList();

            //Aylık rezervasyon grafiği
            var monthlyData = allBookings
                .GroupBy(b => b.BookingDate.Month)
                .OrderBy(g => g.Key)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToList();
            
            ViewBag.MonthlyLabels = monthlyData.Select(x => GetMonthName(x.Month)).ToList();
            ViewBag.MonthlyCounts = monthlyData.Select(x => x.Count).ToList();

            //Rezervasyon durum grafiği
            var statusCounts = allBookings
                .GroupBy(b => b.Status ?? "Pending")
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToList();
            
            ViewBag.StatusLabels = statusCounts.Select(x => x.Status).ToList();
            ViewBag.StatusCounts = statusCounts.Select(x => x.Count).ToList();

            return View();
        }

        private string GetMonthName(int month)
        {
            return month switch
            {
                1 => "Ocak",
                2 => "Şubat",
                3 => "Mart",
                4 => "Nisan",
                5 => "Mayıs",
                6 => "Haziran",
                7 => "Temmuz",
                8 => "Ağustos",
                9 => "Eylül",
                10 => "Ekim",
                11 => "Kasım",
                12 => "Aralık",
                _ => "Bilinmeyen"
            };
        }

        //Api endpiont
        [HttpGet]
        [Route("api/statistics/chart-data")]
        public async Task<IActionResult> GetChartData()
        {
            var allBookings = await _bookingService.GetAllBookingsAsync();
            
            var tourBookingCounts = allBookings
                .GroupBy(b => b.DestinationName ?? "Bilinmeyen")
                .Select(g => new { tourName = g.Key, count = g.Sum(b => b.NumberOfPeople) })
                .OrderByDescending(x => x.count)
                .Take(6)
                .ToList();

            return Json(tourBookingCounts);
        }
    }
}
