using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Response;
using LesGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

//Author: Sasidurka Venkatesan 
//# 991542294
namespace LesGo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SignupController : ControllerBase
    {
        private DB _db = new DB();

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return Created("New User Created", user);
        }


    }
}
