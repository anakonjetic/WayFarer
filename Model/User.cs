﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using WayFarer.Model.Enum;

namespace WayFarer.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Role Role { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Itinerary>? Itineraries { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }

        public User(string name, string surname, DateTime dateOfBirth, string email, Gender gender, Role role, string username, string password)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Role = role;
            Email = email;
            Username = username;
            Password = password;
        }
    }
}
