using RentACar.Entities.Abstract;
using System.Collections.Generic;

namespace RentACar.Entities.Concrete
{
    public class GearBox : EntityBase, IEntity
    {
        public string GearType { get; set; }
        public int Speed { get; set; }

        public IList<Car> Cars { get; set; }
    }
}