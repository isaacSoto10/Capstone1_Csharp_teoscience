using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace HobbyCenter.Models
{
        public class Tag
        {
            public int TagId { get; set; }
            public string Name { get; set; }
            public List<PostHasTags> Posts { get; set; }
        }
}