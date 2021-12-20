using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Domain.Models
{
   public class Baskets
    {
        public int Quantity { get; set; }

        public int MedicineID { get; set; }

        public int UserID { get; set; }




        public Users Users { get; set; }  // 1-many relationship

        public Medicine Medicine { get; set; } //1-many relationship

        

        public Baskets(int quantity, int medicineID, int userID)
        {
            Quantity = quantity;
            MedicineID = medicineID;
            UserID = userID;
        }

        public List<Cheques> Cheques { get; set; }
    }
}
