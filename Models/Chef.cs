using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chefs_n_dishes.Models
{
    public class Chef
    {
        [Key] // Primary Key
        public int ChefId { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        [Required]
        [FutureDate]
        [MinimumAge]
        [Display(Name = "Date of Birth:")]
        public DateTime DoB { get; set; }

        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Dish> ChefDishes {get; set;}

        public int getAge()
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int chef_bday = int.Parse(DoB.ToString("yyyyMMdd"));
            return (now - chef_bday) / 10000;
        }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime inputDate = Convert.ToDateTime(value);

            // logic for datetime =>  value.Date > CurrentTime
            if (inputDate > DateTime.UtcNow)
            {
                return new ValidationResult("Invalid birthdate.");
            }
            else 
            { 
                return ValidationResult.Success; 
            }
        }
    }
    public class MinimumAgeAttribute : ValidationAttribute
    {
        // https://stackoverflow.com/questions/29665290/validate-age-according-to-date-of-birth-using-model-in-mvc-4
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date)){
                if (date.AddYears(18) < DateTime.Now)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Must be at least 18 years old.");
        }
    }
}