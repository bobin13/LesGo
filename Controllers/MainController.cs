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

        [HttpGet("driver/{userId}")]
        public async Task<IActionResult> GetDriverTrips(Guid userId)
        {

            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return Ok(user);
        }


        [HttpGet("user/{userID}")]
        public async Task<IActionResult> GetUserById(Guid userID)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userID);
            return Ok(user);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _db.Users.ToListAsync();
            return Ok(users);
        }
    }
}