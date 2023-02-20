using System.ComponentModel.DataAnnotations;

namespace CarsApp.Data.Models
{
    public class CarModels
    {
        public int Id { get; set; }
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Не указано название модели")]
        public string? Name { get; set; }
        public bool Active { get; set; }
        public virtual Brands? Brand { get; set; }
    }
}
