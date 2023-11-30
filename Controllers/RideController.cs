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
    public class RideController : ControllerBase
    {
        private DB _db = new DB();

        [HttpPost("postRide/{userId}")]
        public async Task<IActionResult> PostRide(Guid userId, [FromBody] Ride ride)
        {

            if (ride == null)
                return BadRequest(
                    new Error("400",
                    "Error Posting Ride",
                    "Unexpected Error Occured While Creating Trip, Try again!"));
            ride.DriverId = userId;
            Console.WriteLine(ride.RideTimeString);
            ride.RideTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(ride.RideTimeString)).UtcDateTime;
            await _db.Rides.AddAsync(ride);
            await _db.SaveChangesAsync();

            return Created("Ride Added", ride);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPassengerRides(Guid userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return NotFound();

            return Ok(user.Rides.ToList());
        }

        [HttpPost("addRider/{rideID}/{userID}")]
        public async Task<IActionResult> AddPassenger(Guid userId, Guid rideId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return BadRequest(new Error("400", "User Not Found", "No user with provided ID found."));

            var ride = await _db.Rides.FirstOrDefaultAsync(x => x.Id == rideId);
            if (ride == null)
                return BadRequest(new Error("400", "No Ride Found!", "No ride found with this ID."));

            if (ride.Users.Count < 3)
                ride.Users.Add(user);
            await _db.SaveChangesAsync();
            return Ok(ride);
        }
    }
}