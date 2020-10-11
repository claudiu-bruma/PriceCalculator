using System;

namespace PriceCalculator.Services.PriceCalculationStrategy
{
    public class PriceCalculationFromNetAmount : IPriceCalculationStrategy
    {
        public void Calculate(PriceDetails details)
        {
            if (!details.NetAmount.HasValue)
            {
                throw new ArgumentNullException(nameof(details.NetAmount));
            }
            if (details.VatRate <= 0 || details.VatRate> 100)
            {
                throw new ArgumentOutOfRangeException(nameof(details.VatRate));
            }
            details.VatAmount = details.NetAmount * details.VatRate / 100;
            details.GrossAmount = details.NetAmount + details.VatAmount;            
        }
    }

}
