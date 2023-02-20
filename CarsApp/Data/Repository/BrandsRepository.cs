using CarsApp.Data.Interfaces;
using CarsApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsApp.Data.Repository
{
    public class BrandsRepository : IRepository<Brands>
    {
        private readonly ApplicationContext _applicationContext;
        
        public BrandsRepository(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }
        public async Task CreateAsync(Brands entity)
        {
            await _applicationContext.Brands.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Brands entity)
        {
            _applicationContext.Brands.Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Brands?> GetAsync(int id) => await _applicationContext.Brands.FindAsync(id);

        public async Task<IEnumerable<Brands>> GetListAsync() => await _applicationContext.Brands.ToListAsync();

        public async Task UpdateAsync(Brands entity)
        {
            _applicationContext.Brands.Update(entity);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
