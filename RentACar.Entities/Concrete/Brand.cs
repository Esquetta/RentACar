using RentACar.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Entities.Concrete
{
    public class Brand:EntityBase,IEntity
    {
        [Required]
        public string BrandName { get; set; }
        public List<Car> Cars { get; set; }
    }
}