using Pharmacy.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Domain.Models
{
    public class Stores: Entity
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string OpeningHours { get; set; }

        public Stores(
                      string name, string street, string city,
                      string country, string telephone, string openingHours
                      )
        {
            Name = name;
            Street = street;
            City = city;
            Country = country;
            Telephone = telephone;
            OpeningHours = openingHours;
        }

        public List<Cheques> Cheques { get; set; }

    }
}
