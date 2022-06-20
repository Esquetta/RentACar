using RentACar.Entities.Abstract;

namespace RentACar.Entities.Concrete
{
    public class RentDetail : IEntity
    {
        public int RentId { get; set; }
        public Rent Rent { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public decimal Price { get; set; }
    }
}
