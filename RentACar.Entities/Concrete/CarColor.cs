using RentACar.Entities.Abstract;

namespace RentACar.Entities.Concrete
{
    public class CarColor:EntityBase,IEntity
    {
        public string Color { get; set; }
    }
}