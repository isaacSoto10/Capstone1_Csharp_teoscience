using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
    namespace HobbyCenter.Models
    {
        public class PostHasTags
        {
            public int PostHasTagsId { get; set; }
            public int HobbyId { get; set; }
            public int TagId { get; set; }

            public Hobby Hobby { get; set; }
            public Tag Tag { get; set; }
        }
}