using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LesGo.Models
{
    public class Ride
    {
        public Guid id{get;set;}
        public DateTime RideTime{get;set;}
        public List<User>? Riders{get;set;}
        public Guid DriverId{get;set;}
        public int Price{get;set;}
    }
}