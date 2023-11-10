using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LesGo.Models;
using Microsoft.AspNetCore.Mvc;

namespace LesGo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost("login/")]
        public async Task<IActionResult> Login(User user){

            return Ok();
        }
    }
}