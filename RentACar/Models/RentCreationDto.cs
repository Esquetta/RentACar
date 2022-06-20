using RentACar.Entities.Concrete;
using RentACar.UI.Models.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RentACar.UI.Models
{
    public class RentCreationDto
    {
        public Rent Rent { get; set; }
        public RentDetail RentDetail { get; set; }
        public CarForListDto Car { get; set; }
        [DataType(DataType.Date)]

        public DateTime TakenDate { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; } = DateTime.Now;

    }
}
