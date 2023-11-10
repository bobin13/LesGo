using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LesGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LesGo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private DB _db = new DB();

        [HttpGet("driver/{driverID}")]
        public async Task<IActionResult> GetDriverTrips(Guid driverID){

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user){
            await _db.AddAsync(user);
            await _db.SaveChangesAsync();
            Console.WriteLine("addedd");
            return Created("User Created",user);;
        }

        [HttpGet("user/{userID}")]
        public async Task<IActionResult> GetUserById(Guid userID){
            var user = await _db.Users.FirstOrDefaultAsync(x => x.id == userID);
            return Ok(user);
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetAllUsers(){
            var users = await _db.Users.ToListAsync();
            return Ok(users);
        }
    }
}