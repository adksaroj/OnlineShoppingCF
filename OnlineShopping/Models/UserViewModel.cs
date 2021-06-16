﻿using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        public string Role { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
    }
}