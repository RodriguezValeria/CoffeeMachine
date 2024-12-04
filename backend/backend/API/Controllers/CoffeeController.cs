using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Application.Interfaces;
using backend.Domain.Models;
using backend.Infrastructure.Repository;

namespace backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public CoffeeController(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var coffees = _coffeeRepository.GetCoffees();
            return Ok(coffees);
        }
    }
}
