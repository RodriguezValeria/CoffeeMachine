using backend.Application.Interfaces;
using backend.Application.Managers;
using backend.Domain.Models;
using Moq;

namespace UnitTesting
{
    public class CashManagerTest
    {
        private CashManager _cashManager;
        private Mock<ICashRepository> _cashRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _cashRepositoryMock = new Mock<ICashRepository>();
            _cashManager = new CashManager(_cashRepositoryMock.Object);
        }

        [Test]
        public void IsCashAvailable_ReturnsCash_WhenCashExists()
        {
            // Arrange
            var cashList = new List<CashModel>
            {
                new CashModel { Denomination = 500, Quantity = 10 },
                new CashModel { Denomination = 100, Quantity = 5 }
            };
            _cashRepositoryMock.Setup(x => x.GetMoney()).Returns(cashList);

            // Act
            var result = _cashManager.IsCashAvailable();

            // Assert
            Assert.AreEqual(cashList, result);
        }

        [Test]
        public void IsCashAvailable_ReturnsEmpty_WhenNoCashTypesExist()
        {
            // Arrange
            var emptyCashList = new List<CashModel>();
            _cashRepositoryMock.Setup(x => x.GetMoney()).Returns(emptyCashList);

            // Act
            var result = _cashManager.IsCashAvailable();

            // Assert
            Assert.IsEmpty(result);
        }
    }
}