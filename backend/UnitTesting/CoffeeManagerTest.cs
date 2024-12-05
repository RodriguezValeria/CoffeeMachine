using backend.Application.Interfaces;
using backend.Application.Managers;
using backend.Domain.Models;
using Moq;

namespace UnitTesting
{
    public class CoffeeManagerTest
    {
        private CoffeeManager _coffeeManager;
        private Mock<ICoffeeRepository> _coffeeRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _coffeeRepositoryMock = new Mock<ICoffeeRepository>();
            _coffeeManager = new CoffeeManager(_coffeeRepositoryMock.Object);
        }

        [Test]
        public void IsCoffeeAvailable_ReturnsAllCoffee_WhenCoffeeExists()
        {
            // Arrange
            var coffeeList = new List<CoffeeModel>
            {
                new CoffeeModel { CoffeeType = "Americano", Quantity = 10, Price = 950.0m },
                new CoffeeModel { CoffeeType = "Latte", Quantity = 5, Price = 1300.0m }
            };
            _coffeeRepositoryMock.Setup(x => x.GetCoffees()).Returns(coffeeList);

            // Act
            var result = _coffeeManager.IsCoffeeAvailable();

            // Assert
            Assert.AreEqual(coffeeList, result); // Ensure the method returns all coffee
        }

        [Test]
        public void IsCoffeeAvailable_ReturnsEmpty_WhenNoCoffeeExists()
        {
            // Arrange
            var emptyCoffeeList = new List<CoffeeModel>();
            _coffeeRepositoryMock.Setup(x => x.GetCoffees()).Returns(emptyCoffeeList);

            // Act
            var result = _coffeeManager.IsCoffeeAvailable();

            // Assert
            Assert.IsEmpty(result); // Ensure the method returns an empty collection
        }
    }
}
