using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.UI.Services.Abstract
{
    public interface IRentCookieeService: ICookieeService<Rent>
    {
        string GetUserName();
    }
}
