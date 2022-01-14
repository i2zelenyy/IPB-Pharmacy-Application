using Pharmacy.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Domain.Models
{
    public class Cheques: Entity
    {
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public float TotalPrice { get; set; }
        public int StoresID { get; set; }
        public int BasketsID { get; set; }

        public Baskets Baskets { get; set; }  // 1-many relationship
        public Stores Stores { get; set; }  // 1-many relationship

        public Cheques()
        {

        }

        public Cheques(
                       DateTime date, string time,
                       float totalPrice, int storesID, int basketsID
                       )
        {
            Date = date;
            Time = time;
            TotalPrice = totalPrice;
            StoresID = storesID;
            BasketsID = basketsID;
        }
    }
}
