using System;

namespace PriceCalculator.Services.PriceCalculationStrategy
{
    public class PriceCalculationFromNetAmountStrategy : IPriceCalculationStrategy
    {
        public PriceDetails Calculate(PriceDetailsInput details)
        {
            if (!details.NetAmount.HasValue)
            {
                throw new ArgumentNullException(nameof(details.NetAmount));
            }
            if (details.VatRate <= 0 || details.VatRate> 100)
            {
                throw new ArgumentOutOfRangeException(nameof(details.VatRate));
            }
            var vatAmount = details.NetAmount.Value * details.VatRate  / 100;
            var grossAmount = details.NetAmount.Value + vatAmount;
            return new PriceDetails(details.NetAmount.Value, grossAmount, vatAmount);
        }
    }

}
