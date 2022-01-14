using Pharmacy.Domain;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.UWP.ViewModels.BasketViewModel
{
    public class BasketViewModel
    {
        
        public ObservableCollection<Baskets> Baskets { get; set; }

        public int Id { get; set; }
        public int UserID { get; set; }
        public int MedicineID { get; set; }

        private int quantity;
        public int Quantity
        {
            get { return this.quantity; }
            set
            {
                this.quantity = value;
                NotifyPropertyChanged();
            }
        }
        public Users Users { get; set; }
        public Medicine Medicine { get; set; }

        public BasketViewModel()
        {
            Baskets = new ObservableCollection<Baskets>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async Task LoadAllAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                List<Baskets> list = await uow.BasketsRepository.FindAllAsync();
                Baskets.Clear();
                foreach (Baskets i in list)
                {
                    Baskets.Add(i);
                }
            }
        }

        public async Task LoadBasketAsync(int UserID)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                List<Baskets> list = await uow.BasketsRepository.FindAllAsync();
                Baskets.Clear();
                foreach (Baskets i in list)
                {
                    if (i.UserID == UserID)
                    {
                        Baskets.Add(i);
                    }
                }
            }
        }

        internal async Task CreateBasketAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Baskets basket = new Baskets
                {
                    UserID = UserID,
                    MedicineID = MedicineID,
                    Quantity = Quantity,
                    Users = Users,
                    Medicine = Medicine
                };
                uow.BasketsRepository.Create(basket);
                await uow.SaveAsync();
            }
        }

        internal async Task UpdateBasketAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Baskets basket = new Baskets
                {
                    Id = Id,
                    UserID = UserID,
                    MedicineID = MedicineID,
                    Quantity = Quantity,
                    Users = Users,
                    Medicine = Medicine
                };
                uow.BasketsRepository.Update(basket);
                await uow.SaveAsync();
            }
        }

        internal async Task DeleteBasketAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Baskets basket = new Baskets
                {
                    Id = Id,
                    UserID = UserID,
                    MedicineID = MedicineID,
                    Quantity = Quantity,
                    Users = Users,
                    Medicine = Medicine
                };
                uow.BasketsRepository.Delete(basket);
                await uow.SaveAsync();
            }
        }


    }
}

