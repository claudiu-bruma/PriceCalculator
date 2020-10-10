using PriceCalculator.Services.PriceCalculationStrategy;
using System;

namespace PriceCalculator.Services
{
    public class PriceCalculationService : IPriceCalculationService
    {
        public void FillInPrices(PriceDetails details)
        {
            IPriceCalculationStrategy priceCalculationStrategy = ChoosePriceCalculationStrategy(details);
            priceCalculationStrategy.Calculate(details);
        }

        private static IPriceCalculationStrategy ChoosePriceCalculationStrategy(PriceDetails details)
        {
            if (details.NetAmount.HasValue && details.NetAmount.Value > 0)
            {
                return new PriceCalculationFromNetAmount();
            }
            if (details.GrossAmount.HasValue && details.GrossAmount.Value > 0)
            {
                return new PriceCalculationFromGrossAmount();
            }
            if (details.VatAmount.HasValue && details.VatAmount.Value > 0)
            {
                return new PriceCalculationFromVatAmount();
            }
            throw new ArgumentException("Unexpected State: No data to perform calculation. Vat ammount, net ammount and gross amount were all null");
        }
    }
}
