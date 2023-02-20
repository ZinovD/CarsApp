using CarsApp.Data.Interfaces;
using CarsApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarsApp.Controllers
{
    public class CarModelsController : Controller
    {
        private readonly IRepository<CarModels> _allCarModels;
        private readonly IRepository<Brands> _allBrands;

        public CarModelsController(IRepository<CarModels> allCarModels,IRepository<Brands> allBrands)
        {
            _allCarModels = allCarModels;
            _allBrands = allBrands;
        }

        public async Task<IActionResult> List(SortState sortOrder)
        {
            ViewData["ModelsSort"] = sortOrder == SortState.ModelsNameAsc ? SortState.ModelsNameDesc : SortState.ModelsNameAsc;
            ViewData["BrandsSort"] = sortOrder == SortState.BrandsNameAsc ? SortState.BrandsNameDesc : SortState.BrandsNameAsc;

            IEnumerable<CarModels> carModels = await _allCarModels.GetListAsync();
            carModels = sortOrder switch
            {
                SortState.ModelsNameAsc => carModels.OrderBy(x=>x.Name),
                SortState.ModelsNameDesc => carModels.OrderByDescending(x => x.Name),
                SortState.BrandsNameAsc => carModels.OrderBy(x => x.Brand.Name),
                SortState.BrandsNameDesc => carModels.OrderByDescending(x => x.Brand.Name),
                _ => carModels.ToList()
            };
            return View(carModels);
        }

        public async  Task<IActionResult> Create()
        {
            ViewBag.Brands= new SelectList(await _allBrands.GetListAsync(), "Id", "Name");
            CarModels carModel = new CarModels();
            return View(carModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarModels entity)
        {
            if (ModelState.IsValid)
            {
                await _allCarModels.CreateAsync(entity);
                entity.Brand = await _allBrands.GetAsync(entity.BrandId);
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Brands = new SelectList(await _allBrands.GetListAsync(), "Id", "Name");
                return View(entity);
            }  
        }

        public async Task<IActionResult> Edit(int Id)
        {
            CarModels? entity = await _allCarModels.GetAsync(Id);
            ViewBag.Brands = new SelectList(await _allBrands.GetListAsync(), "Id", "Name");
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarModels entity)
        {
            if (entity != null)
            {
                await _allCarModels.UpdateAsync(entity);
                return RedirectToAction("Index", "Home");
            }
            return NotFound();
        }

        public async Task<IActionResult> Delete(CarModels entity)
        {
            await _allCarModels.DeleteAsync(entity);
            return RedirectToAction("Index","Home");
        }
    }
}
