using CarsApp.Data.Interfaces;
using CarsApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsApp.Data.Repository
{
    public class CarModelsRepository : IRepository<CarModels>
    { 
        private readonly ApplicationContext _applicationContext;
        public CarModelsRepository(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }
        public async Task CreateAsync(CarModels entity)
        {
            await _applicationContext.CarModels.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CarModels entity)
        {
            _applicationContext.CarModels.Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<CarModels?> GetAsync(int id)=>await _applicationContext.CarModels.FindAsync(id);

        public async Task<IEnumerable<CarModels>> GetListAsync() => await _applicationContext.CarModels.Include(i=>i.Brand).ToListAsync();

        public async Task UpdateAsync(CarModels entity)
        {
            _applicationContext.CarModels.Update(entity);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
