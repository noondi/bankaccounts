using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bankAccounts.Models 
{  
    public class RegisterUserView: BaseEntity
    {
        [Required]
        [MinLength(2)]
        public string FirstName {get; set; }
        
        [Required]
        [MinLength(2)]
        public string LastName {get; set; }

        [Required]
        [EmailAddress]
        public string Email {get; set; } 

        [Required]
        [MinLength(2)]
        [DataType(DataType.Password)]
        public string Password {get; set; } 

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
         public string ConfirmPassword {get; set; } 
        
    }
}