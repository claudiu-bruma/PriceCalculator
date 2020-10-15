using System;

namespace PriceCalculator.Services.PriceCalculationStrategy
{
    public class PriceCalculationFromGrossAmountStrategy : IPriceCalculationStrategy
    {
        public PriceDetails Calculate(PriceDetailsInput details)
        {
            if (!details.GrossAmount.HasValue)
            {
                throw new ArgumentNullException(nameof(details.GrossAmount));
            }
            if (details.VatRate <= 0 || details.VatRate > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(details.VatRate));
            }
            
            var netAmount = details.GrossAmount.Value * 100 / (100 + details.VatRate);
            var vatAmount = netAmount * details.VatRate / 100;

            return new PriceDetails(netAmount, details.GrossAmount.Value, vatAmount);
        }
    }

}
