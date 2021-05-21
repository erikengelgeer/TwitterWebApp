using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterWebApp.Models
{
    public class User 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please add a username.")]
        [StringLength(20, ErrorMessage = "Username must be less than {1} characters.")]
        [Display(Name = "Username: ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please add a first name.")]
        [Display(Name = "First name: ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please add a last name.")]
        [Display(Name = "Last name: ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please add a password.")]
        [StringLength(20, ErrorMessage = "Password must be less than {1} characters.")]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please add a date of birth.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a correct date format dd/mm/yyyy")]
        [Display(Name = "Date of Birth: ")]
        public DateTime DateOfBirth { get; set; }

    }
}
