using backend.Application.Managers;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashController : ControllerBase
    {
        private readonly CashManager _cashManager;

        public CashController(CashManager cashManager)
        {
            _cashManager = cashManager;
        }

        [HttpGet]
        public IActionResult GetAvailableCash()
        {
           var availableCash = _cashManager.IsCashAvailable();
            if (!availableCash.Any())
            {
                return BadRequest("No hay más efectivo disponible");
            }
            return Ok(availableCash);
        }
    }
}
