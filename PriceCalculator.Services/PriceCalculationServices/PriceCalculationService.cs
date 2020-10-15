using Microsoft.Extensions.Logging;
using PriceCalculator.Services.PriceCalculationStrategy;
using System;

namespace PriceCalculator.Services.PriceCalculationServices
{
    public class PriceCalculationService : IPriceCalculationService
    {
        private readonly ILogger<PriceCalculationService> _logger;
        public PriceCalculationService(ILogger<PriceCalculationService> logger)
        {
            _logger = logger;
        }
        public PriceDetails GetCalculatedPrice(PriceDetailsInput details )
        {
            IPriceCalculationStrategy priceCalculationStrategy = ChoosePriceCalculationStrategy(details);
            return priceCalculationStrategy.Calculate(details);
        }

        private IPriceCalculationStrategy ChoosePriceCalculationStrategy(PriceDetailsInput details)
        {
            if (details.NetAmount.HasValue && details.NetAmount.Value > 0)
            {
                return new PriceCalculationFromNetAmountStrategy();
            }
            if (details.GrossAmount.HasValue && details.GrossAmount.Value > 0)
            {
                return new PriceCalculationFromGrossAmountStrategy();
            }
            if (details.VatAmount.HasValue && details.VatAmount.Value > 0)
            {
                return new PriceCalculationFromVatAmountStrategy();
            }
            _logger.LogWarning("Unexpected State: No data to perform calculation. Vat ammount, net ammount and gross amount were all null");
            throw new ArgumentException("Unexpected State: No data to perform calculation. Vat ammount, net ammount and gross amount were all null");
        }
    }
}
