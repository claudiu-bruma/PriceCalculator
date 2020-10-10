using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace PriceCalculator.Controllers.Models
{
    public class PriceDetailsRequest : IValidatableObject
    {
        public decimal? NetAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? VatAmount { get; set; }
        [Required]
        public decimal VatRate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (VatRate <= 0 || VatRate>=100)
            {
                yield return new ValidationResult(
                    $"VatRate missing or invalid",
                    new[] { nameof(VatRate) });
            }
            var validInputsProvided = 0;
            if (NetAmount.HasValue && NetAmount.Value > 0)
            {
                validInputsProvided++;
            }
            if (GrossAmount.HasValue && GrossAmount.Value > 0)
            {
                validInputsProvided++;
            }
            if (VatAmount.HasValue && VatAmount.Value > 0)
            {
                validInputsProvided++;
            }
            if (validInputsProvided == 0)
            {
                yield return new ValidationResult(
                    $"No input provided for calculation. Please specify NetPrice, GrossPrice Or VatAmount for calculation. Only one should be provided."
                  );
            }
            if (validInputsProvided > 1)
            {
                yield return new ValidationResult(
                    $"Too many input provided for calculation. Please specify only NetPrice, GrossPrice Or VatAmount for calculation. Only one should be provided.");
            }

        }
    }
}
