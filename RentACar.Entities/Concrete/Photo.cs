using RentACar.Entities.Abstract;

namespace RentACar.Entities.Concrete
{
    public class Photo : EntityBase, IEntity
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}