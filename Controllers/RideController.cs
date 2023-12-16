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

        [HttpGet("byid/{rideId}")]
        public async Task<IActionResult> GetRideById(Guid rideId)
        {

            var ride = await _db.Rides.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == rideId);
            if (ride == null)
                return NotFound("Ride not found!");

            return Ok(ride);
        }

        [HttpPost("postRide/{userId}")]
        public async Task<IActionResult> PostRide(Guid userId, [FromBody] Ride ride)
        {
            Console.WriteLine($"origin {ride.Origin} / Dest: {ride.Destination}");
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

        [HttpGet("find")]
        public async Task<IActionResult> FindRide([FromQuery] string origin, [FromQuery] string destination)
        {
            var date = "";

            if (origin != null && destination != null)
            {
                //converting to lower case
                origin = origin.ToLower();
                destination = destination.ToLower();

                var rides1 = _db.Rides
                .Include(x => x.Users)
                .Where(x => x.Origin.ToLower() == origin && x.Destination.ToLower() == destination);

                return Ok(rides1);

            }


            if (origin == null || destination == null || date == null)
                return BadRequest(new Error("400", "Invalid Request!", "origin, destination and date required!"));

            var rides = _db.Rides.Where(x => x.Users.Count() <= 4);
            var availableRides = new List<Ride>();
            var enteredTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(date)).UtcDateTime;
            foreach (var ride in rides)
            {
                var rideTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(ride.RideTimeString)).UtcDateTime;
                Console.WriteLine(enteredTime.DayOfYear + "//" + rideTime.DayOfYear);
                if (origin == ride.Origin && destination == ride.Destination)
                    return Ok("Date found");
            }
            return Ok();
        }

        [HttpDelete("{rideId}")]
        public async Task<IActionResult> DeleteRide(Guid rideId)
        {
            var ride = await _db.Rides.FirstOrDefaultAsync(x => x.Id == rideId);

            if (ride == null)
                return BadRequest(new Error("404", "No Ride Found", "No Ride found with this ID."));

            if (ride != null)
                _db.Rides.Remove(ride);
            await _db.SaveChangesAsync();
            return Ok(ride);
        }
    }
}
