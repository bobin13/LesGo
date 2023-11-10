using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LesGo.Models
{
    public class DB : DbContext
    {
        public DbSet<User> Users {get;set;}
        public DbSet<Ride> Rides {get;set;}
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=LesGoDatabase.db");
        }
    }
}