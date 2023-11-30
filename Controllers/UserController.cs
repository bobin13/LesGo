using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Response;
using LesGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LesGo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private DB _db = new DB();

        [HttpGet()]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _db.Users.ToListAsync();
            return Ok(users);
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
    }
}