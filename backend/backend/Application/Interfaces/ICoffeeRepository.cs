using backend.Domain.Models;

namespace backend.Application.Interfaces
{
    public interface ICoffeeRepository
    {
        public IEnumerable<CoffeeModel> GetCoffees();
    }
}
