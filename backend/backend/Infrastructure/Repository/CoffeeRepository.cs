using backend.Domain.Models;
using backend.Application.Interfaces;

namespace backend.Infrastructure.Repository
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly List<CoffeeModel> _coffees;

        public CoffeeRepository ()
        {
            _coffees = new List<CoffeeModel>
            {
                new CoffeeModel { CoffeeType = "Americano", Quantity = 10, Image =  "", Price = 950.0m },
                new CoffeeModel { CoffeeType = "Capuccino", Quantity = 8, Image = "", Price = 1200.0m },
                new CoffeeModel { CoffeeType = "Latte", Quantity = 10, Image = "", Price = 1300.0m},
                new CoffeeModel { CoffeeType = "Mochaccino", Quantity = 15, Image = "", Price = 1500.0m}
            };
        }

        public IEnumerable<CoffeeModel> GetCoffees()
        {
            return _coffees;
        }

        public bool UpdateQuantities(string coffeeType)
        {
            return true;
        }
    }
}
