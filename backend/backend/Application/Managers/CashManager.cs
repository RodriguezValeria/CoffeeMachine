using backend.Application.Interfaces;
using backend.Domain.Models;

namespace backend.Application.Managers
{
    public class CashManager
    {
        private readonly ICashRepository _cashRepository;

        public CashManager(ICashRepository cashRepository)
        {
            _cashRepository = cashRepository;
        }

        public IEnumerable<CashModel> IsCashAvailable()
        {
            var allCash = _cashRepository.GetMoney();
            if (!allCash.Any())
            {
                return Enumerable.Empty<CashModel>();
            }
            return allCash;
        }
    }
}
