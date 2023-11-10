using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LesGo.Models
{
    public class User
    {
        public Guid id{set;get;}
        public required string Name {get;set;}

        public string Username {get;set;}
        public string Password {get;set;}
        public string Bio {get;set;}
    }
}