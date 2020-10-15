namespace PriceCalculator.Services
{
    public class PriceDetails
    {
        public decimal NetAmount { get; }
        public decimal GrossAmount { get; }
        public decimal VatAmount { get; }

        public PriceDetails(decimal netAmount, decimal grossAmount, decimal vatAmount)
        {
            NetAmount = netAmount;
            GrossAmount = grossAmount;
            VatAmount = vatAmount;
        }
    }
}
