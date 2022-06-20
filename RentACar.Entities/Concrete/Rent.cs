using RentACar.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Entities.Concrete
{
    public class Rent : EntityBase, IEntity
    {
        [DataType(DataType.Date)]
        public DateTime DateOfIssue { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsFinished { get; set; }
    }
}
