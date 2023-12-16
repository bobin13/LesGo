using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//Author: Sasi Venkatesan
namespace LesGo.Models
{
    public class Ride
    {
        [Key]
        public Guid Id { get; set; }

        public string RideTimeString { get; set; }        

        public DateTime RideTime { get; set; }

        [Required]
        public string Origin { get; set; }

        public string Destination { get; set; }
        
        public List<User> Users { get; set; }

        public Guid DriverId { get; set; }

        [Required]
        public int Price { get; set; }

    }
}
