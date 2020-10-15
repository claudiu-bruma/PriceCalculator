using Microsoft.Extensions.Logging;
using Moq;
using PriceCalculator.Services.PriceCalculationServices;
using System;
using Xunit;

namespace PriceCalculator.Services.Tests
{
    public class PriceCalculationServiceTests
    {
        private MockRepository _mockRepository;
        
        public PriceCalculationServiceTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private PriceCalculationService CreateService()
        {
            var logger = new Mock<ILogger<PriceCalculationService>>();
            return new PriceCalculationService(logger.Object);
        }

        [Theory]
        [InlineData(100,0,0,10,100,110,10)]
        [InlineData(100, -1, -1, 10, 100, 110, 10)]
        [InlineData(100, -1, 0, 10, 100, 110, 10)]
        [InlineData(100, 0, -1, 10, 100, 110, 10)]
        [InlineData(0, 110, 0, 10, 100, 110, 10)]
        [InlineData(-1, 110, -1, 10, 100, 110, 10)]
        [InlineData(-1, 110, 0, 10, 100, 110, 10)]
        [InlineData(0, 110, -1, 10, 100, 110, 10)]
        [InlineData(0, 0, 10, 10, 100, 110, 10)]
        [InlineData(-1, 0, 10, 10, 100, 110, 10)]
        [InlineData(-1, -1, 10, 10, 100, 110, 10)]
        [InlineData(0, -1, 10, 10, 100, 110, 10)]
        public void FillInPrices_ValidInputProvided_ReturnsCorrectAmounts(
            decimal netAmountInput, decimal grossAmountInput, decimal vatAmountInput, 
            decimal vatRate, 
            decimal expectedNet, decimal expectedGross, decimal expectedVatAmount )
        {
            // Arrange
            var service = CreateService();
            PriceDetailsInput details = new PriceDetailsInput()
            {
                NetAmount = netAmountInput,
                GrossAmount= grossAmountInput,
                VatAmount = vatAmountInput,
                VatRate = vatRate
            };

            // Act
            var calculatedPrice= service.GetCalculatedPrice(
                details);

            // Assert

            Assert.Equal(expectedNet, calculatedPrice.NetAmount);
            Assert.Equal(expectedGross, calculatedPrice.GrossAmount);
            Assert.Equal(expectedVatAmount, calculatedPrice.VatAmount);
            _mockRepository.VerifyAll();
        }
        [Theory]
        [InlineData(0,0,0,10)]
        [InlineData(-1, 0, 0, 10)]
        [InlineData(-1,-1, 0, 10)]
        [InlineData(-1, -1,-1, 10)]
        [InlineData(0, -1, 0, 10)]
        [InlineData(0, -1, -1, 10)]
        [InlineData(0, 0, -1, 10)]
        [InlineData(0, 0, 0, 0)]
        [InlineData(-1, 0, 0, 0)]
        [InlineData(-1, -1, 0, 0)]
        [InlineData(-1, -1, -1, 0)]
        [InlineData(0, -1, 0, 0)]
        [InlineData(0, -1, -1, 0)]
        [InlineData(0, 0, -1, 0)]
        [InlineData(0, 0, 0, -1)]
        [InlineData(-1, 0, 0, -1)]
        [InlineData(-1, -1, 0, -1)]
        [InlineData(-1, -1, -1, -1)]
        [InlineData(0, -1, 0, -1)]
        [InlineData(0, -1, -1, -1)]
        [InlineData(0, 0, -1, -1)]
        public void FillInPrices_NoValidDataProided_ArgumentExceptionRaised(
            decimal netAmountInput, decimal grossAmountInput, decimal vatAmountInput,
            decimal vatRate )
        {
            // Arrange
            var service = CreateService();
            PriceDetailsInput details = new PriceDetailsInput()
            {
                NetAmount = netAmountInput,
                GrossAmount = grossAmountInput,
                VatAmount = vatAmountInput,
                VatRate = vatRate
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(()=> service.GetCalculatedPrice(
                details));
            _mockRepository.VerifyAll();
        }

        [Theory]
        [InlineData(10, 0, 0, 0)]
        [InlineData(10, 0, 0, -1)]
        public void FillInPrices_NoVatRateProided_ArgumentOutOfRangeExceptionRaised(
            decimal netAmountInput, decimal grossAmountInput, decimal vatAmountInput,
            decimal vatRate)
        {
            // Arrange
            var service = CreateService();
            PriceDetailsInput details = new PriceDetailsInput()
            {
                NetAmount = netAmountInput,
                GrossAmount = grossAmountInput,
                VatAmount = vatAmountInput,
                VatRate = vatRate
            };

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => service.GetCalculatedPrice(
                details));
            _mockRepository.VerifyAll();
        }         
    }
}
