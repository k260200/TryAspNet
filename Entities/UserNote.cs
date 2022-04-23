using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    // Пользователь-награды
    public class UserNote
    {
        private User _user;

        public User User { set => _user = value; }

        public int Id { get => _user.ID; }

        public string FirstName { get => _user.FirstName; }

        public string LastName { get => _user.LastName; }

        public DateTime BirthDate { get => _user.BirthDate; }

        public int Age { get => _user.Age; }

        public string Rewards { get; set; }
    }
}
