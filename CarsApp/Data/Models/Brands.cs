using System.ComponentModel.DataAnnotations;

namespace CarsApp.Data.Models
{
    public class Brands
    {
        public int Id { get; set; }

        [Display(Name = "Введите название ")]
        [Required(ErrorMessage = "Не указано название марки")]
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
