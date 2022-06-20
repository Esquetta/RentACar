using Microsoft.AspNetCore.Identity;
using RentACar.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace RentACar.Entities.Concrete
{
    public class Customer:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Rent> Rents { get; set; }
    }
}