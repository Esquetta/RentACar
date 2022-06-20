using RentACar.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Entities.Concrete
{
    public class Car : EntityBase, IEntity
    {
        [Required]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string HorsePower { get; set; }
        [Required]

        public int CarColorId { get; set; }
        public CarColor CarColor { get; set; }

        [Required]
        public int GearBoxId { get; set; }
        public GearBox GearBox { get; set; }
        [Required]

        public int FuelId { get; set; }
        public Fuel Fuel { get; set; }
        [Required]
        public int Miles { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool For_Rent { get; set; }
        public List<Photo> Photos { get; set; }

        public IList<RentDetail> RendDetails { get; set; }

    }
}
