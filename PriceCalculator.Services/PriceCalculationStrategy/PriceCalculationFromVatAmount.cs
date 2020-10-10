using System;

namespace PriceCalculator.Services.PriceCalculationStrategy
{
    public class PriceCalculationFromVatAmount : IPriceCalculationStrategy
    {
        public void Calculate(PriceDetails details)
        {
            if (!details.VatAmount.HasValue)
            {
                throw new ArgumentNullException(nameof(details.VatAmount));
            }
            if (details.VatRate <= 0 || details.VatRate > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(details.VatRate));
            }

            details.NetAmount = 100/details.VatRate * details.VatAmount;
            details.GrossAmount = details.NetAmount + details.VatAmount;
        }
    }

}
