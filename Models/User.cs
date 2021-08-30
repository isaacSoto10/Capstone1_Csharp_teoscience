using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HobbyCenter.Models
    {
        public class User
        {
            
            [Key]
            public int UserId { get; set; }

            [Required]
            [MinLength(2, ErrorMessage="Please enter a First Name with a least 2 characters")]
            public string FirstName { get; set; }

            [Required]
            [MinLength(2, ErrorMessage="Please enter a Last Name with a least 2 characters")]
            public string LastName { get; set; }

            [Required]
            [MinLength (3, ErrorMessage="User name should be between 3 and 15 characters")]
            [MaxLength (15, ErrorMessage="User name should be between 3 and 15 characters")]
            public string UserName { get; set; }

            [Required]
            [MinLength(8, ErrorMessage="Password sould be at least 8 characters")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [NotMapped] 
            [DataType(DataType.Password)]
            public string ConfPass { get; set; }

            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;

            public List<Hobby> UserHobby {get; set; } //Connects Users to hobby they created
            
        }

        public class Login
        {
            public string UserLogin { get; set; }

            [DataType(DataType.Password)]
            public string PasswordLogin { get; set; }

        }
    }