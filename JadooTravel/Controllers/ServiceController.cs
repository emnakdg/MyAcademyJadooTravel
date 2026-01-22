using JadooTravel.Dtos.ServiceDtos;
using JadooTravel.Services.ServiceServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<IActionResult> ServiceList()
        {
            var values = await _serviceService.GetAllServiceAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto createServiceDto)
        {
            await _serviceService.CreateServiceAsync(createServiceDto);
            return RedirectToAction("ServiceList");
        }

        public async Task<IActionResult> DeleteService(string id)
        {
            await _serviceService.DeleteServiceAsync(id);
            return RedirectToAction("ServiceList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateService(string id)
        {
            var value = await _serviceService.GetServiceByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateServiceDto updateServiceDto)
        {
            await _serviceService.UpdateServiceAsync(updateServiceDto);
            return RedirectToAction("ServiceList");
        }
    }
}
