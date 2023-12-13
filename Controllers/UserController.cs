using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Response;
using LesGo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//Author: Akshay Soni

namespace LesGo.Controllers
{
    // Allow CORS for all origins. (Caution!)
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private DB _db = new DB();

        //Get All the Users
        [HttpGet()]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _db.Users.Include(x => x.Rides).ToListAsync();
            return Ok(users);
        }

        //getting a user and all its rides with Id.
        [HttpGet("{userID}")]
        public async Task<IActionResult> GetUserById(Guid userID)
        {
            var user = await _db.Users.Include(x => x.Rides).FirstOrDefaultAsync(x => x.Id == userID);
            return Ok(user);
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            //returning Badrequest error if data incomplete.
            if (user == null)
                BadRequest(new Error("400", "User Can't be Added!", "Unexpected Error Occured! try Again."));

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return Created("user Created", user);

        }

        [HttpGet("driver/{userId}")]
        public async Task<IActionResult> GetDriverTrips(Guid userId)
        {

            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return Ok(user);
        }
    }
}
