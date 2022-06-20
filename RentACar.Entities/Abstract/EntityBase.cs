using System;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Entities.Abstract
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
