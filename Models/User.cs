using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LesGo.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public Guid Id { set; get; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string? Bio { get; set; }
        public List<Ride> Rides { get; set; }
    }
}