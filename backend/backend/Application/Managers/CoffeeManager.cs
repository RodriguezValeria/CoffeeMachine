using backend.Application.Interfaces;
using backend.Domain.Models;
using backend.Infrastructure.Repository;

namespace backend.Application.Managers
{
    public class CoffeeManager
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public CoffeeManager (ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        public IEnumerable<CoffeeModel> IsCoffeeAvailable()
        {
            var allCoffee = _coffeeRepository.GetCoffees();
            if (!allCoffee.Any())
            {
                return Enumerable.Empty<CoffeeModel>();
            }
            return allCoffee;
        }
    }
}
