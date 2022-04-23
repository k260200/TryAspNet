using System;
using System.Collections.Generic;

using Entities;

using System.ComponentModel.DataAnnotations;

namespace UsersAndRewardsWeb.Models.ViewEntities
{
    public class SingleUserViewModel : IValidatableObject
    {
        public SingleUserViewModel()
        {

        }
        public SingleUserViewModel(User user)
        {
            ID = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            BirthDate = user.BirthDate;
        }

        public SingleUserViewModel(int id, string firstName, string lastName, DateTime birthDate)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public SingleUserViewModel(string firstName, string lastName, DateTime birthDate) : this(0,
            firstName, lastName, birthDate)
        { }

        public int ID { get; set; }

        [Required(ErrorMessage = "First name can not be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name can not be empty")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Birth date can not be empty")]
        
        public DateTime BirthDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            TimeSpan timeSpan = new TimeSpan();

            if (BirthDate > DateTime.Now)
                errors.Add(new ValidationResult("User is too young", new[] { nameof(BirthDate) }));
            else if (new DateTime((timeSpan = DateTime.Now - BirthDate).Ticks).Year > 150)
                errors.Add(new ValidationResult("User is too old", new[] { nameof(BirthDate) }));

            return errors;
        }
    }
}
