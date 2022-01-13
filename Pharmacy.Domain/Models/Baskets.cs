using System;
using System.Collections.Generic;
using System.Text;
using Pharmacy.Domain.SeedWork;

namespace Pharmacy.Domain.Models
{
   public class Baskets: Entity
    {
        public int Quantity { get; set; }

        public int MedicineID { get; set; }

        public int UserID { get; set; }

        public Users Users { get; set; }  // 1-many relationship

        public Medicine Medicine { get; set; } // 1-many relationship

        public List<Cheques> Cheques { get; set; } // 1-many relationship

        public Baskets()
        {

        }

        public Baskets(int quantity, int medicineID, int userID)
        {
            Quantity = quantity;
            MedicineID = medicineID;
            UserID = userID;
        }   
    }
}
