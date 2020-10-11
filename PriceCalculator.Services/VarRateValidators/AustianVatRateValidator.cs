using System.Linq;

namespace PriceCalculator.Services.VarRateValidators
{
    public class AustianVatRateValidator : IVatRateValidator
    {
        private readonly decimal[] validAustrianVatRates = { 10.0m, 13.0m, 20.0m };
        public bool IsValidVatRate(decimal vatRate)
        {
            return validAustrianVatRates.Contains(vatRate);
        }
    }
}
