using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Reward
    {
        int _id;
        string _title;
        string _description;


        public Reward(int id, string title, string description)
        {
            ID = id;

            Title = title;
            Description = description;
        }

        public Reward(string title, string description) : this(0, title, description) { }

        public Reward(int id, string title) : this(id, title, "") { }

        public Reward(string title) : this(0, title, "") { }

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

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Название награды не может быть пустым или null!");

                if (value.Length > 50)
                    throw new ArgumentException("Длина названия награды не должна превышать 50 символов!");

                _title = value;
            }
        }

        public string Description 
        { 
            get => _description;
            set
            {
                if (value.Length > 250)
                    throw new ArgumentException("Длина описания награды не должна превышать 250 символов!");

                _description = value;
            }
        }

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
