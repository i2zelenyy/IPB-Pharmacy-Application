using Pharmacy.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Domain.Models
{
    public class Cheques: Entity
    {
        public DateTime Date;
        public DateTime Time;
        public float TotalPrice;
        public int StoreID;
        public int BasketID;

        public Cheques(DateTime date, DateTime time, 
            float totalPrice, int storeID, int basketID)
        {
            Date = date;
            Time = time;
            TotalPrice = totalPrice;
            StoreID = storeID;
            BasketID = basketID;
        }

        public Baskets Baskets { get; set; }  // 1-many relationship
        public Stores Stores { get; set; }  // 1-many relationship

    }
}
