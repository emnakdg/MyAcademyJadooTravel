using JadooTravel.Dtos.BookingDtos;
using JadooTravel.Services.BookingServices;
using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JadooTravel.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IDestinationService _destinationService;

        public BookingController(IBookingService bookingService, IDestinationService destinationService)
        {
            _bookingService = bookingService;
            _destinationService = destinationService;
        }

        public async Task<IActionResult> BookingList()
        {
            var values = await _bookingService.GetAllBookingsAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateBooking()
        {
            var destinations = await _destinationService.GetAllDestinationAsync();
            ViewBag.Destinations = new SelectList(destinations, "DestinationId", "CityCountry");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
        {
            await _bookingService.CreateBookingAsync(createBookingDto);
            return RedirectToAction("BookingList");
        }

        public async Task<IActionResult> DeleteBooking(string id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return RedirectToAction("BookingList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBooking(string id)
        {
            var value = await _bookingService.GetBookingByIdAsync(id);
            var destinations = await _destinationService.GetAllDestinationAsync();
            ViewBag.Destinations = new SelectList(destinations, "DestinationId", "CityCountry");
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            await _bookingService.UpdateBookingAsync(updateBookingDto);
            return RedirectToAction("BookingList");
        }

        // API endpoint for homepage booking form
        [HttpPost]
        [Route("api/booking/create")]
        public async Task<IActionResult> CreateBookingApi([FromBody] CreateBookingDto createBookingDto)
        {
            try
            {
                await _bookingService.CreateBookingAsync(createBookingDto);
                return Ok(new { success = true, message = "Rezervasyonunuz başarıyla oluşturuldu!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
