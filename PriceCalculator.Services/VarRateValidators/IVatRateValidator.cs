using System;
using System.Collections.Generic;
using System.Text;

namespace PriceCalculator.Services.VarRateValidators
{
    public interface IVatRateValidator
    {
        bool IsValidVatRate(decimal vatRate);
    }
}
