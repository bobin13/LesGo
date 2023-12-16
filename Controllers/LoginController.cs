using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Response;
using LesGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

//Author: Sasidurka Venkatesan 
//#991542294
namespace LesGo.Controllers
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private DB _db = new DB();

        [HttpPost()]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            Console.WriteLine($"username: {loginRequest.username} / password: {loginRequest.password}");
            return Ok(new
            {
                username = loginRequest.username,
                password = loginRequest.password
            });
        }
    }
}
