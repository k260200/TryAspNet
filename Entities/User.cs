using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class User
    {
        int _id;
        string _firstName;
        string _lastName;
        DateTime _birthDate;


        public User(int id, string firstName, string lastName, DateTime birthDate)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public User(string firstName, string lastName, DateTime birthDate) : this(0,
            firstName, lastName, birthDate)
        { }

        public int ID
        {
            get => _id;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("ID не может быть меньше 0!");

                _id = value;
            }
        }

        public string FirstName { 
            get => _firstName; 
            set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Имя не может быть пустым или null!");

                _firstName = value;
            } 
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Имя не может быть пустым или null!");

                _lastName = value;
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Человек ещё не родился!");
                else if (DateTime.Now.Year - value.Year > 150)
                    throw new ArgumentException("Пользователь слишком стар!");

                _birthDate = value;
            }
        }

        public int Age
        {
            get
            {
                TimeSpan timeSpan = DateTime.Now - BirthDate;
                return new DateTime(timeSpan.Ticks).Year;
            }
        }

        public override string ToString()
        {
            return $"{LastName} "
                + $"{FirstName}, "
                + $"{Age}";
        }
    }
}
