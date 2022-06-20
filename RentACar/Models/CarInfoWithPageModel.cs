using RentACar.Entities.Concrete;
using System.Collections.Generic;

namespace RentACar.UI.Models
{
    public class CarInfoWithPageModel
    {
        public List<Brand> Brands { get; set; }
        public string CurrentModel { get; set; }
        public string Page { get; set; }
    }
}
