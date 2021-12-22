using Pharmacy.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Domain.Models
{
    public class Medicine: Entity
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string HowToUse { get; set; }

        public List<Baskets> Baskets { get; set; }

        public Medicine()
        {

        }

        public Medicine(
                        string category, string name, string brand,
                        float price, string description, string ingredients, string howToUse
                        )
        {
            Category = category;
            Name = name;
            Brand = brand;
            Price = price;
            Description = description;
            Ingredients = ingredients;
            HowToUse = howToUse;
        }
    }
}
