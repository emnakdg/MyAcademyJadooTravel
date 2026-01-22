using JadooTravel.Services.ServiceServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents
{
    public class _DefaultCategoryComponentPartial : ViewComponent
    {
        private readonly IServiceService _serviceService;

        public _DefaultCategoryComponentPartial(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var services = await _serviceService.GetAllServiceAsync();
            return View(services);
        }
    }
}
