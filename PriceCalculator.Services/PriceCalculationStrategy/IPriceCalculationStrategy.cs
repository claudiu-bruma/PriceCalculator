namespace PriceCalculator.Services.PriceCalculationStrategy
{
    public interface IPriceCalculationStrategy
    {
        PriceDetails Calculate(PriceDetailsInput details);
    }
}
