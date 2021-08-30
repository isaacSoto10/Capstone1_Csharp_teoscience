using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HobbyCenter.Models
    {
        public class Hobby
        {
            
            [Key]
            public int HobbyId { get; set; }
            
            [Required]
            public string Name { get; set; }
            
            public int UserId { get; set; }
            


            [Required]
            public string Description { get; set; }
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;
            
            public List<PostHasTags> Tags {get; set;}
       
        }
    }