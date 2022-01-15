using Pharmacy.Domain;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.UWP.ViewModels.ChequesViewModel
{
    public class BasketsViewModel
    {
        public ObservableCollection<Cheques> Cheques { get; set; }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public float TotalPrice { get; set; }
        public int StoresID { get; set; }
        public int BasketsID { get; set; }

        public Baskets Baskets { get; set; }
        public Stores Stores { get; set; }

        public BasketsViewModel()
        {
            Cheques = new ObservableCollection<Cheques>();
        }

        public async Task LoadAllAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                List<Cheques> list = await uow.ChequesRepository.FindAllAsync();
                Cheques.Clear();
                foreach (Cheques i in list)
                {
                    Cheques.Add(i);
                }
            }
        }

        public async Task LoadChequeAsync(int BasketsID)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                List<Cheques> list = await uow.ChequesRepository.FindAllAsync();
                foreach (Cheques i in list)
                {
                    if (i.BasketsID == BasketsID)
                    {
                        Cheques.Add(i);
                    }
                }
            }
        }

        internal async Task CreateChequeAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Cheques cheque = new Cheques
                {
                    Date = Date,
                    Time = Time,
                    TotalPrice = TotalPrice,
                    StoresID = StoresID,
                    BasketsID = BasketsID,
                    Baskets = Baskets,
                    Stores = Stores
                };
                uow.ChequesRepository.Create(cheque);
                await uow.SaveAsync();
            }
        }

        internal async Task DeleteChequeAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Cheques cheque = new Cheques
                {
                    Id = Id,
                    Date = Date,
                    Time = Time,
                    TotalPrice = TotalPrice,
                    StoresID = StoresID,
                    BasketsID = BasketsID,
                    Baskets = Baskets,
                    Stores = Stores
                };
                uow.ChequesRepository.Delete(cheque);
                await uow.SaveAsync();
            }
        }

        internal async Task UpdateChequeAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Cheques cheque = new Cheques
                {
                    Id = Id,
                    Date = Date,
                    Time = Time,
                    TotalPrice = TotalPrice,
                    StoresID = StoresID,
                    BasketsID = BasketsID,
                    Baskets = Baskets,
                    Stores = Stores
                };
                uow.ChequesRepository.Update(cheque);
                await uow.SaveAsync();
            }
        }

    }
}
