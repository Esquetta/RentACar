using System;
using System.ComponentModel.DataAnnotations;

namespace RentACar.UI.Models
{

    public class CarForCreationModel
    {
        [Required]
        public int BrandId { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Product Date")]
        [Range(typeof(DateTime), "01-01-1950", "01-01-2023",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime ProductDate { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string HorsePower { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int GearBoxId { get; set; }

        [Required]
        public int FuelId { get; set; }
        [Required]
        public int Miles { get; set; }
        [Required]
        public string Description { get; set; }





    }
}
