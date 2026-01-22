using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents
{
    public class _DefaultBookingFormComponentPartial : ViewComponent
    {
        private readonly IDestinationService _destinationService;

        public _DefaultBookingFormComponentPartial(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var destinations = await _destinationService.GetAllDestinationAsync();
            return View(destinations);
        }
    }
}
