namespace PriceCalculator.Services.PriceCalculationServices
{
    public interface IPriceCalculationService
    {
        PriceDetails GetCalculatedPrice(PriceDetailsInput details);
    }
}
