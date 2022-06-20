using RentACar.Entities.Abstract;
using System.Collections.Generic;

namespace RentACar.Entities.Concrete
{
    public class Fuel : EntityBase, IEntity
    {
        public string FuelType { get; set; }

        public IList<Car> Cars { get; set; }
    }
}