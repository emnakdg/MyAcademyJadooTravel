using JadooTravel.Dtos.ServiceDtos;
using JadooTravel.Dtos.TestimonialDtos;
using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Dtos.CategoryDtos;
using JadooTravel.Dtos.BookingDtos;
using JadooTravel.Services.ServiceServices;
using JadooTravel.Services.TestimonialServices;
using JadooTravel.Services.DestinationServices;
using JadooTravel.Services.CategoryServices;
using JadooTravel.Services.BookingServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class SeedController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly ITestimonialService _testimonialService;
        private readonly IDestinationService _destinationService;
        private readonly ICategoryService _categoryService;
        private readonly IBookingService _bookingService;

        public SeedController(
            IServiceService serviceService,
            ITestimonialService testimonialService,
            IDestinationService destinationService,
            ICategoryService categoryService,
            IBookingService bookingService)
        {
            _serviceService = serviceService;
            _testimonialService = testimonialService;
            _destinationService = destinationService;
            _categoryService = categoryService;
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> SeedAll()
        {
            var result = new List<string>();
            
            result.Add(await SeedServices());
            result.Add(await SeedTestimonials());
            result.Add(await SeedDestinations());
            result.Add(await SeedCategories());
            result.Add(await SeedBookings());
            
            return Content(string.Join("\n", result));
        }

        private async Task<string> SeedServices()
        {
            var existingServices = await _serviceService.GetAllServiceAsync();
            if (existingServices.Any()) return "Hizmetler: Zaten mevcut, atlandı.";

            var services = new List<CreateServiceDto>
            {
                new CreateServiceDto
                {
                    Title = "Havayolları",
                    Description = "En iyi uçuş seçenekleri için bizimle iletişime geçin.",
                    IconUrl = "/public/assets/img/category/icon1.png"
                },
                new CreateServiceDto
                {
                    Title = "Döviz Ofisi",
                    Description = "Seyahatiniz için en uygun döviz kurları.",
                    IconUrl = "/public/assets/img/category/icon2.png"
                },
                new CreateServiceDto
                {
                    Title = "Hastaneler",
                    Description = "Sağlık turizmi için en iyi hastaneler.",
                    IconUrl = "/public/assets/img/category/icon3.png"
                },
                new CreateServiceDto
                {
                    Title = "Özel Turlar",
                    Description = "Size özel tasarlanmış tur paketleri.",
                    IconUrl = "/public/assets/img/category/icon4.png"
                }
            };

            foreach (var service in services)
            {
                await _serviceService.CreateServiceAsync(service);
            }
            return "Hizmetler: 4 adet eklendi ✓";
        }

        private async Task<string> SeedTestimonials()
        {
            var existingTestimonials = await _testimonialService.GetAllTestimonialAsync();
            if (existingTestimonials.Any()) return "Referanslar: Zaten mevcut, atlandı.";

            var testimonials = new List<CreateTestimonialDto>
            {
                new CreateTestimonialDto
                {
                    NameSurname = "Ahmet Yılmaz",
                    Comment = "Harika bir seyahat deneyimi yaşadık. Her şey mükemmeldi!",
                    CityCountry = "İstanbul, Türkiye",
                    ImageUrl = "https://randomuser.me/api/portraits/men/32.jpg"
                },
                new CreateTestimonialDto
                {
                    NameSurname = "Ayşe Demir",
                    Comment = "Jadoo Travel ile unutulmaz bir tatil geçirdik. Kesinlikle tavsiye ederim.",
                    CityCountry = "Ankara, Türkiye",
                    ImageUrl = "https://randomuser.me/api/portraits/women/44.jpg"
                },
                new CreateTestimonialDto
                {
                    NameSurname = "Mehmet Kaya",
                    Comment = "Profesyonel ekip ve uygun fiyatlar. Tekrar tercih edeceğiz.",
                    CityCountry = "İzmir, Türkiye",
                    ImageUrl = "https://randomuser.me/api/portraits/men/45.jpg"
                },
                new CreateTestimonialDto
                {
                    NameSurname = "Fatma Öztürk",
                    Comment = "Ailecek çok güzel vakit geçirdik. Organizasyon harikaydı!",
                    CityCountry = "Bursa, Türkiye",
                    ImageUrl = "https://randomuser.me/api/portraits/women/68.jpg"
                }
            };

            foreach (var testimonial in testimonials)
            {
                await _testimonialService.CreateTestimonialAsync(testimonial);
            }
            return "Referanslar: 4 adet eklendi ✓";
        }

        private async Task<string> SeedDestinations()
        {
            var existingDestinations = await _destinationService.GetAllDestinationAsync();
            if (existingDestinations.Any()) return "Turlar: Zaten mevcut, atlandı.";

            var destinations = new List<CreateDestinationDto>
            {
                new CreateDestinationDto
                {
                    CityCountry = "Paris, Fransa",
                    Price = 15000,
                    DayNight = "5 Gün 4 Gece",
                    Capacity = 30,
                    Description = "Romantik Paris turu. Eyfel Kulesi, Louvre Müzesi ve daha fazlası.",
                    ImageUrl = "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=400"
                },
                new CreateDestinationDto
                {
                    CityCountry = "Roma, İtalya",
                    Price = 12000,
                    DayNight = "4 Gün 3 Gece",
                    Capacity = 25,
                    Description = "Tarihi Roma turu. Kolezyum, Vatikan ve antik kalıntılar.",
                    ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=400"
                },
                new CreateDestinationDto
                {
                    CityCountry = "Londra, İngiltere",
                    Price = 18000,
                    DayNight = "6 Gün 5 Gece",
                    Capacity = 20,
                    Description = "Kraliyet şehri Londra. Big Ben, Buckingham Sarayı ve Themse Nehri.",
                    ImageUrl = "https://images.unsplash.com/photo-1513635269975-59663e0ac1ad?w=400"
                },
                new CreateDestinationDto
                {
                    CityCountry = "Dubai, BAE",
                    Price = 22000,
                    DayNight = "5 Gün 4 Gece",
                    Capacity = 35,
                    Description = "Lüks Dubai turu. Burj Khalifa, çöl safarisi ve alışveriş.",
                    ImageUrl = "https://images.unsplash.com/photo-1512453979798-5ea266f8880c?w=400"
                }
            };

            foreach (var destination in destinations)
            {
                await _destinationService.CreateDestinationAsync(destination);
            }
            return "Turlar: 4 adet eklendi ✓";
        }

        private async Task<string> SeedCategories()
        {
            var existingCategories = await _categoryService.GetAllCategoryAsync();
            if (existingCategories.Any()) return "Kategoriler: Zaten mevcut, atlandı.";

            var categories = new List<CreateCategoryDto>
            {
                new CreateCategoryDto { CategoryName = "Kültür Turları" },
                new CreateCategoryDto { CategoryName = "Deniz Tatilleri" },
                new CreateCategoryDto { CategoryName = "Doğa Turları" },
                new CreateCategoryDto { CategoryName = "Şehir Turları" },
                new CreateCategoryDto { CategoryName = "Macera Turları" }
            };

            foreach (var category in categories)
            {
                await _categoryService.CreateCategoryAsync(category);
            }
            return "Kategoriler: 5 adet eklendi ✓";
        }

        private async Task<string> SeedBookings()
        {
            var existingBookings = await _bookingService.GetAllBookingsAsync();
            if (existingBookings.Any()) return "Rezervasyonlar: Zaten mevcut, atlandı.";

            // Get destinations to use their IDs
            var destinations = await _destinationService.GetAllDestinationAsync();
            if (!destinations.Any()) return "Rezervasyonlar: Önce turlar eklenmeli!";

            var destList = destinations.ToList();
            
            // Turkish first names and surnames for realistic data
            var firstNames = new[] { "Ahmet", "Mehmet", "Mustafa", "Ali", "Hüseyin", "Hasan", "İbrahim", "Osman", "Yusuf", "Murat",
                                     "Fatma", "Ayşe", "Emine", "Hatice", "Zeynep", "Elif", "Merve", "Büşra", "Esra", "Selin" };
            var lastNames = new[] { "Yılmaz", "Kaya", "Demir", "Çelik", "Şahin", "Yıldız", "Yıldırım", "Öztürk", "Aydın", "Özdemir",
                                    "Arslan", "Doğan", "Kılıç", "Aslan", "Çetin", "Kara", "Koç", "Kurt", "Özkan", "Şimşek" };
            var statuses = new[] { "Pending", "Confirmed", "Cancelled" };
            
            var random = new Random();

            for (int i = 1; i <= 50; i++)
            {
                var firstName = firstNames[random.Next(firstNames.Length)];
                var lastName = lastNames[random.Next(lastNames.Length)];
                var destination = destList[random.Next(destList.Count)];
                var daysAhead = random.Next(-30, 60); // Some past, some future
                var numberOfPeople = random.Next(1, 6);

                var booking = new CreateBookingDto
                {
                    FullName = $"{firstName} {lastName}",
                    Email = $"{firstName.ToLower()}.{lastName.ToLower()}{i}@email.com",
                    Phone = $"053{random.Next(0, 10)} {random.Next(100, 999)} {random.Next(10, 99)} {random.Next(10, 99)}",
                    DestinationId = destination.DestinationId,
                    BookingDate = DateTime.Now.AddDays(daysAhead),
                    NumberOfPeople = numberOfPeople
                };

                await _bookingService.CreateBookingAsync(booking);
            }
            
            return "Rezervasyonlar: 50 adet eklendi ✓";
        }
    }
}
