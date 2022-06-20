using RentACar.UI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACar.UI.Models
{
    public class RentedCarsWithCustomerListDto
    {
        public CarForListDto CarForList { get; set; }
        public string CustomerUserName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurName { get; set; }
        [DataType(DataType.Date)]
        public DateTime GivenDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
        public bool IsFinished { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }

    }
}
