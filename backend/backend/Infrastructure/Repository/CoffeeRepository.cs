﻿using backend.Domain.Models;
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
                new CoffeeModel { CoffeeType = "Americano", Quantity = 10, Image =  "/assets/americano.png", Price = 950.0m },
                new CoffeeModel { CoffeeType = "Cappuccino", Quantity = 8, Image = "/assets/capuccino.png", Price = 1200.0m },
                new CoffeeModel { CoffeeType = "Latte", Quantity = 10, Image = "/assets/latte.png", Price = 1300.0m},
                new CoffeeModel { CoffeeType = "Mocaccino", Quantity = 15, Image = "/assets/mochaccino.png", Price = 1500.0m}
            };
        }

        public IEnumerable<CoffeeModel> GetCoffees()
        {
            return _coffees;
        }
    }
}
