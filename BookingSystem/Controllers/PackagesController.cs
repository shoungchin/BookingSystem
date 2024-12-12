using BookingSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [ApiController]
    [Route("api/packages")]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackagesController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailablePackages([FromQuery] string country)
        {
            var packages = await _packageService.GetAvailablePackagesAsync(country);
            return Ok(packages);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserPackages([FromQuery] int userId)
        {
            var userPackages = await _packageService.GetUserPackagesAsync(userId);
            return Ok(userPackages);
        }
    }
}
