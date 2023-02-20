using CarsApp.Data.Interfaces;
using CarsApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarsApp.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IRepository<Brands> _allBrands;

        public BrandsController(IRepository<Brands> allBrands)
        {
            _allBrands = allBrands;
        }

        public async Task<IActionResult> List(SortState sortOrder)
        {
            ViewData["BrandsSort"] = sortOrder == SortState.BrandsNameAsc ? SortState.BrandsNameDesc : SortState.BrandsNameAsc;
            IEnumerable<Brands> carBrands = await _allBrands.GetListAsync();
            carBrands = sortOrder switch
            {
                SortState.BrandsNameAsc => carBrands.OrderBy(x => x.Name),
                SortState.BrandsNameDesc => carBrands.OrderByDescending(x => x.Name),
                _ => carBrands.ToList()
            };
            return View(carBrands);
        }

        public IActionResult Create()
        {
            Brands brand = new Brands();
            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brands entity)
        {
            if (ModelState.IsValid)
            {
                await _allBrands.CreateAsync(entity);
                return RedirectToAction("Index", "Home");
            }
            return View(entity);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            Brands? entity = await _allBrands.GetAsync(Id);
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Brands entity)
        {
            if(entity!=null)
            {
                await _allBrands.UpdateAsync(entity);
                return RedirectToAction("Index", "Home");
            }
            return NotFound();
        }

        public async Task<IActionResult> Delete(Brands entity)
        {
            await _allBrands.DeleteAsync(entity);
            return RedirectToAction("Index", "Home");
        }
    }
}
