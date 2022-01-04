using Pharmacy.Domain;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.UWP.ViewModels.MedicineViewModel
{
    public class MedicineViewModel
    {
        public ObservableCollection<Medicine> Medicine { get; set; }

        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string HowToUse { get; set; }

        public MedicineViewModel()
        {
            Medicine = new ObservableCollection<Medicine>();
        }

        public async void LoadAllAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                List<Medicine> list = await uow.MedicineRepository.FindAllAsync();
                Medicine.Clear();
                foreach (Medicine i in list)
                {
                    Medicine.Add(i);
                }
            }
        }

        internal async Task CreateMedicineAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Medicine medicine = new Medicine
                {
                    Category = Category,
                    Name = Name,
                    Brand = Brand,
                    Price = Price,
                    Description = Description,
                    Ingredients = Ingredients,
                    HowToUse = HowToUse
                };
                uow.MedicineRepository.Create(medicine);
                await uow.SaveAsync();
            }
        }

        internal async Task DeleteMedicineAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Medicine medicine = new Medicine
                {
                    Id = Id,
                    Category = Category,
                    Name = Name,
                    Brand = Brand,
                    Price = Price,
                    Description = Description,
                    Ingredients = Ingredients,
                    HowToUse = HowToUse
                };
                uow.MedicineRepository.Delete(medicine);
                await uow.SaveAsync();
            }
        }

        internal async Task UpdateMedicineAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Medicine medicine = new Medicine
                {
                    Id = Id,
                    Category = Category,
                    Name = Name,
                    Brand = Brand,
                    Price = Price,
                    Description = Description,
                    Ingredients = Ingredients,
                    HowToUse = HowToUse
                };
                uow.MedicineRepository.Update(medicine);
                await uow.SaveAsync();
            }
        }
    }
}
