using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Response;
using LesGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//Author: Bahwinder Singh
//#991418804
namespace LesGo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RideController : ControllerBase
    {
        private DB _db = new DB();

        [HttpGet]
        public async Task<IActionResult> GetAllRides()
        {

            var rides = await _db.Rides.Include(x => x.Users).ToListAsync();
            return Ok(rides);
        }

        [HttpPost("postRide/{userId}")]
        public async Task<IActionResult> PostRide(Guid userId, [FromBody] Ride ride)
        {

            if (ride == null)
                return BadRequest(
                    new Error("400",
                    "Error Posting Ride",
                    "Unexpected Error Occured While Creating Trip, Try again!"));
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return BadRequest(new Error("400", "No Driver found!", "No driver with this ID found."));

            //adding user that posted ride to ride users array & set driverId.
            ride.DriverId = userId;
            ride.Users.Add(user);
            //takes a epoch time string and converts to dateTime and save it in ride object.
            ride.RideTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(ride.RideTimeString)).UtcDateTime;

            await _db.Rides.AddAsync(ride);
            await _db.SaveChangesAsync();

            return Created("Ride Added", ride);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPassengerRides(Guid userId)
        {
            var user = await _db.Users.Include(x => x.Rides).FirstOrDefaultAsync(x => x.Id == userId);

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

            if (ride.Users == null)
                ride.Users = new List<User>();

            Console.WriteLine(ride.Users.Count);
            if (ride.Users.Count >= 4)
                return BadRequest(new Error("400", "Ride Full", "No More Seats Available."));

            ride.Users.Add(user);
            await _db.SaveChangesAsync();
            return Ok(ride);
        }

        [HttpDelete("{rideId}")]
        public async Task<IActionResult> DeleteRide(Guid rideId)
        {
            var ride = await _db.Rides.FirstOrDefaultAsync(x => x.Id == rideId);

            if (ride == null)
                return BadRequest(new Error("404", "No Ride Found", "No Ride found with this ID."));

            if (ride != null)
                _db.Rides.Remove(ride);

            return Ok(ride);
        }
    }
}