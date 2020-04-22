using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chefs_n_dishes.Models
{
    public class Dish
    {
        [Key] // Primary Key
        public int DishId { get; set; }
        
        [Required]
        [Display(Name = "Dish Name:")]
        public string DishName { get; set; }

        [Required]
        [Range(0,Int32.MaxValue)]
        [Display(Name = "Calories:")]
        public int Calories { get; set; }
        
        [Required]
        [Range(1,5)]
        [Display(Name = "Tastiness:")]
        public int Tastiness { get; set; }

        [Required]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Chef Name:")]
        public int ChefId { get; set; } // Foreign Key
        
        public Chef OwnerChef {get; set;} // Reference to include
    }
}