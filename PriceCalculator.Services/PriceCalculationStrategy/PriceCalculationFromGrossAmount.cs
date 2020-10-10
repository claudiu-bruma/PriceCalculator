using System;

namespace PriceCalculator.Services.PriceCalculationStrategy
{
    public class PriceCalculationFromGrossAmount : IPriceCalculationStrategy
    {
        public void Calculate(PriceDetails details)
        {
            if (!details.GrossAmount.HasValue)
            {
                throw new ArgumentNullException(nameof(details.GrossAmount));
            }
            if (details.VatRate <= 0 || details.VatRate > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(details.VatRate));
            }
            
            details.NetAmount = details.GrossAmount * 100 / (100 + details.VatRate);
            details.VatAmount = details.NetAmount * details.VatRate / 100;
        }
    }

}
