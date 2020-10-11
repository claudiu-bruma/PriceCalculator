namespace PriceCalculator.Services.PriceCalculationStrategy
{
    public interface IPriceCalculationStrategy
    {
        void Calculate(PriceDetails details);
    }
}
