using backend.Domain.Models;

namespace backend.Application.Interfaces
{
    public interface ICashRepository
    {
        public IEnumerable<CashModel> GetMoney();
    }
}
