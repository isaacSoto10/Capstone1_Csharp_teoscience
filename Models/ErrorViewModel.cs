using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HobbyCenter.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}