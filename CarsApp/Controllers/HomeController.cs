using CarsApp.Data.Interfaces;
using CarsApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CarsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Brands> _allBrands;
        private readonly IRepository<CarModels> _allCarModels;


        public HomeController(ApplicationContext applicationContext, IRepository<Brands> allBrands, IRepository<CarModels> allCarModels, ILogger<HomeController> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
            _allCarModels = allCarModels;
            _allBrands = allBrands;
            if (!_applicationContext.Brands.Any())
            {
                Brands Audi = new Brands { Name = "Audi", Active = true };
                Brands BMW = new Brands { Name = "BMW", Active = false };
                Brands Ford = new Brands { Name = "Ford", Active = true };
                Brands Mazda = new Brands { Name = "Mazda", Active = true };
                Brands Kia = new Brands { Name = "Kia", Active = false };

                var list = new CarModels[]
                    {
                        new CarModels { Name = "A3", BrandId = 1, Brand = Audi, Active = true },
                        new CarModels { Name = "A8", BrandId = 1, Brand = Audi, Active = true },
                        new CarModels { Name = "Q3", BrandId = 1, Brand = Audi, Active = true },
                        new CarModels { Name = "Q4", BrandId = 1, Brand = Audi, Active = true },
                        new CarModels { Name = "R8", BrandId = 1, Brand = Audi, Active = true },
                        new CarModels { Name = "X4", BrandId = 2, Brand = BMW, Active = false },
                        new CarModels { Name = "M2", BrandId = 2, Brand = BMW, Active = false },
                        new CarModels { Name = "X5", BrandId = 2, Brand = BMW, Active = false },
                        new CarModels { Name = "X6", BrandId = 2, Brand = BMW, Active = false },
                        new CarModels { Name = "X3", BrandId = 2, Brand = BMW, Active = false },
                        new CarModels { Name = "Explorer", BrandId = Ford.Id, Brand = Ford, Active = true },
                        new CarModels { Name = "Fiesta", BrandId = Ford.Id, Brand = Ford, Active = true },
                        new CarModels { Name = "Focus", BrandId = Ford.Id, Brand = Ford, Active = true },
                        new CarModels { Name = "Mondeo", BrandId = Ford.Id, Brand = Ford, Active = true },
                        new CarModels { Name = "Edge", BrandId = Ford.Id, Brand = Ford, Active = true },
                        new CarModels { Name = "3", BrandId = Mazda.Id, Brand = Mazda, Active = true },
                        new CarModels { Name = "CX5", BrandId = Mazda.Id, Brand = Mazda, Active = true },
                        new CarModels { Name = "RX8", BrandId = Mazda.Id, Brand = Mazda, Active = true },
                        new CarModels { Name = "MX5", BrandId = Mazda.Id, Brand = Mazda, Active = true },
                        new CarModels { Name = "Tribute", BrandId = Mazda.Id, Brand = Mazda, Active = true },
                        new CarModels { Name = "Rio", BrandId = Kia.Id, Brand = Kia, Active = false },
                        new CarModels { Name = "Ceed", BrandId = Kia.Id, Brand = Kia, Active = false },
                        new CarModels { Name = "Cerato", BrandId = Kia.Id, Brand = Kia, Active = false },
                        new CarModels { Name = "K9", BrandId = Kia.Id, Brand = Kia, Active = false },
                        new CarModels { Name = "K5", BrandId = Kia.Id, Brand = Kia, Active = false }
                    };
                Dictionary<string, CarModels> models = new Dictionary<string, CarModels>();
                foreach (CarModels el in list) models.Add(el.Name, el);

                applicationContext.Brands.AddRange(Audi, BMW, Ford, Mazda, Kia);
                applicationContext.CarModels.AddRange(models.Select(i => i.Value));
                applicationContext.SaveChanges();
            }
        }

        public async Task<IActionResult> Index(SortState sortOrder)
        {
            ViewData["ModelsSort"] = sortOrder == SortState.ModelsNameAsc ? SortState.ModelsNameDesc : SortState.ModelsNameAsc;
            ViewData["BrandsSort"] = sortOrder == SortState.BrandsNameAsc ? SortState.BrandsNameDesc : SortState.BrandsNameAsc;
            IEnumerable<Brands> carBrands = await _allBrands.GetListAsync();
            IEnumerable<CarModels> carModels = await _allCarModels.GetListAsync();
            carBrands = sortOrder switch
            {
                SortState.BrandsNameAsc => carBrands.OrderBy(x => x.Name),
                SortState.BrandsNameDesc => carBrands.OrderByDescending(x => x.Name),
                _ => carBrands.ToList()
            };
            carModels = sortOrder switch
            {
                SortState.ModelsNameAsc => carModels.OrderBy(x => x.Name),
                SortState.ModelsNameDesc => carModels.OrderByDescending(x => x.Name),
                SortState.BrandsNameAsc => carModels.OrderBy(x => x.Brand.Name),
                SortState.BrandsNameDesc => carModels.OrderByDescending(x => x.Brand.Name),
                _ => carModels.ToList()
            };
            ViewBag.Brands = carBrands;
            ViewBag.CarModels=carModels;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}