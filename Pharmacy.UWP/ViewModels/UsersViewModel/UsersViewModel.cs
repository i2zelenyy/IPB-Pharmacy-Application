using Pharmacy.Domain;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.UWP.ViewModels.UsersViewModel
{
    public class UsersViewModel
    {
        public ObservableCollection<Users> Users { get; set; }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; }
        public string Telephone { get; set; }
        public string Street { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public UsersViewModel()
        {
            Users = new ObservableCollection<Users>();
        }

        public async Task LoadAllAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                List<Users> list = await uow.UsersRepository.FindAllAsync();
                Users.Clear();
                foreach (Users i in list)
                {
                    Users.Add(i);
                }
            }
        }

        internal async Task CreateUserAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Users user = new Users
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    DateOfBirth = DateOfBirth,
                    Username = Username,
                    Telephone = Telephone,
                    Street = Street,
                    Password = Password,
                    Email = Email,
                    IsAdmin = IsAdmin
                };
                uow.UsersRepository.Create(user);
                await uow.SaveAsync();
            }
        }

        internal async Task DeleteUserAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Users user = new Users
                {
                    Id = Id,
                    FirstName = FirstName,
                    LastName = LastName,
                    DateOfBirth = DateOfBirth,
                    Username = Username,
                    Telephone = Telephone,
                    Street = Street,
                    Password = Password,
                    Email = Email,
                    IsAdmin = IsAdmin
                };
                uow.UsersRepository.Delete(user);
                await uow.SaveAsync();
            }
        }

        internal async Task UpdateUserAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Users user = new Users
                {
                    Id = Id,
                    FirstName = FirstName,
                    LastName = LastName,
                    DateOfBirth = DateOfBirth,
                    Username = Username,
                    Telephone = Telephone,
                    Street = Street,
                    Password = Password,
                    Email = Email,
                    IsAdmin = IsAdmin
                };
                uow.UsersRepository.Update(user);
                await uow.SaveAsync();
            }
        }

        internal async Task GetAuthorisedUserAsync(string username, string password)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                List<Users> list = await uow.UsersRepository.FindAllAsync();
                Users.Clear();
                foreach (Users i in list)
                {
                    if (i.Username == username)
                    {
                        if (i.Password == password)
                            Users.Add(i);
                    }                      
                }
            }
        }

        internal async Task GetDuplicatedUsernameAsync(string username, string password)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                List<Users> list = await uow.UsersRepository.FindAllAsync();
                Users.Clear();
                foreach (Users i in list)
                {
                    if (i.Username == username)
                    {
                        Users.Add(i);
                    }
                }
            }
        }

    }
}
