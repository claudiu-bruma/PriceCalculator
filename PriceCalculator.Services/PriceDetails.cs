namespace PriceCalculator.Services
{
    public class PriceDetails
    {
        public decimal? NetAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal VatRate { get; set; }
    }
}
