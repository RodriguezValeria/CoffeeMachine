using Microsoft.AspNetCore.Mvc;
using backend.Application.Managers;

namespace backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly CoffeeManager _coffeeManager;

        public CoffeeController(CoffeeManager coffeeManager)
        {
            _coffeeManager = coffeeManager;
        }

        [HttpGet]
        public IActionResult GetCoffee()
        {
            var coffees = _coffeeManager.IsCoffeeAvailable();
            if (!coffees.Any())
            {
                return BadRequest("No hay más efectivo disponible");
            }
            return Ok(coffees);
        }
    }
}
