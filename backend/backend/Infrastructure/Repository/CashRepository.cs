using backend.Application.Interfaces;
using backend.Domain.Models;
using System.Security.Cryptography.X509Certificates;

namespace backend.Infrastructure.Repository
{
    public class CashRepository : ICashRepository
    {
        private readonly List<CashModel> _cash;

        public CashRepository()
        {
            _cash = new List<CashModel>
            {
                new CashModel { Denomination = 1000, Quantity = 0 },
                new CashModel { Denomination = 500, Quantity = 20 },
                new CashModel { Denomination = 100, Quantity = 30 },
                new CashModel { Denomination = 50, Quantity = 50 },
                new CashModel { Denomination = 25, Quantity = 25 },
            };
        }
        public IEnumerable<CashModel> GetMoney()
        {
            return _cash;
        }
    }
}
