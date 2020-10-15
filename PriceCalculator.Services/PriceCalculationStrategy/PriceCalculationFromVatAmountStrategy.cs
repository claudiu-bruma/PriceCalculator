using System;

namespace PriceCalculator.Services.PriceCalculationStrategy
{
    public class PriceCalculationFromVatAmountStrategy : IPriceCalculationStrategy
    {
        public PriceDetails Calculate(PriceDetailsInput details)
        {
            if (!details.VatAmount.HasValue)
            {
                throw new ArgumentNullException(nameof(details.VatAmount));
            }
            if (details.VatRate <= 0 || details.VatRate > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(details.VatRate));
            }

            var netAmount = 100/details.VatRate * details.VatAmount.Value ;
            var grossAmount = netAmount + details.VatAmount.Value;
            return new PriceDetails(netAmount, grossAmount, details.VatAmount.Value);

        }
    }
}
