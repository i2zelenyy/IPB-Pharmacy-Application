using Pharmacy.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Domain.Models
{
    public class Users : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; }
        public string Telephone { get; set; }
        public string Street { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public List<Baskets> Baskets { get; set; } // 1-many relationship

        public Users()
        {

        }

        public Users(
                     string firstName, string lastName, DateTime dateOfBirth,
                     string username, string telephone, string street,
                     string password, string email, bool isAdmin
                     )
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Username = username;
            Telephone = telephone;
            Street = street;
            Password = password;
            Email = email;
            IsAdmin = isAdmin;
        }
    }
}
