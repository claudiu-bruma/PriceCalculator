using Moq;
using PriceCalculator.Services;
using System;
using Xunit;

namespace PriceCalculator.Services.Tests
{
    public class PriceCalculationServiceTests
    {
        public PriceCalculationServiceTests()
        {
        }

        private PriceCalculationService CreateService()
        {
            return new PriceCalculationService();
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
            PriceDetails details = new PriceDetails()
            {
                NetAmount = netAmountInput,
                GrossAmount= grossAmountInput,
                VatAmount = vatAmountInput,
                VatRate = vatRate
            };

            // Act
            service.FillInPrices(
                details);

            // Assert

            Assert.Equal(expectedNet, details.NetAmount);
            Assert.Equal(expectedGross, details.GrossAmount);
            Assert.Equal(expectedVatAmount, details.VatAmount);
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
            PriceDetails details = new PriceDetails()
            {
                NetAmount = netAmountInput,
                GrossAmount = grossAmountInput,
                VatAmount = vatAmountInput,
                VatRate = vatRate
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(()=> service.FillInPrices(
                details));
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
            PriceDetails details = new PriceDetails()
            {
                NetAmount = netAmountInput,
                GrossAmount = grossAmountInput,
                VatAmount = vatAmountInput,
                VatRate = vatRate
            };

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => service.FillInPrices(
                details));
        }         
    }
}
