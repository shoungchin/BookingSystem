using BookingSystem.Data;
using BookingSystem.Data.CoreModel;
using BookingSystem.Data.RequestModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly BookingDbContext _context;
        public BookingController(BookingDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> BookClass([FromBody] BookingRequest request)
        {
            var schedule = await _context.Schedules.FindAsync(request.ScheduleId);
            if (schedule == null || schedule.AvailableSlots <= 0)
            {
                return BadRequest("Class is full or does not exist.");
            }

            var userPackage = await _context.UserPackages
                .Where(up => up.UserId == request.UserId && up.RemainingCredits >= schedule.RequiredCredits)
                .FirstOrDefaultAsync();

            if (userPackage == null)
            {
                return BadRequest("Insufficient credits or no valid package.");
            }

            schedule.AvailableSlots -= 1;
            userPackage.RemainingCredits -= schedule.RequiredCredits;

            var booking = new Booking
            {
                UserId = request.UserId,
                ScheduleId = request.ScheduleId,
                Status = "Booked",
                BookedAt = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok("Class booked successfully.");
        }
    }
}
