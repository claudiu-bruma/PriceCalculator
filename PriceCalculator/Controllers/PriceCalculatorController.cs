using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceCalculator.Controllers.Models;
using PriceCalculator.Services;
using PriceCalculator.Services.PriceCalculationServices;
using PriceCalculator.Services.VarRateValidators;
using System.Net;

namespace PriceCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceCalculatorController : ControllerBase
    {
        private IPriceCalculationService _priceCalculationService;
        private IVatRateValidator _vatRateValidator;
        private readonly IMapper _mapper;
        public PriceCalculatorController(
            IPriceCalculationService priceCalculationService, IVatRateValidator vatRateValidator, IMapper mapper)
        {
            _priceCalculationService = priceCalculationService;
            _vatRateValidator = vatRateValidator;
            _mapper = mapper;
        }
        /// <summary>
        /// Fills in missing price elements based on net, gross or vat ammount.
        /// </summary>
        /// <param name="priceDetails">incomplete input data</param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<PriceDetailsResponse> Put(PriceDetailsRequest priceDetails)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            if (!_vatRateValidator.IsValidVatRate(priceDetails.VatRate))
            {
                return StatusCode(StatusCodes.Status451UnavailableForLegalReasons, $"Vat rate {priceDetails.VatRate} is not a legally valid vat rate ");
            }
            var priceDetailsDto = _mapper.Map<PriceDetails>(priceDetails);
            _priceCalculationService.FillInPrices(priceDetailsDto);
            var response = _mapper.Map<PriceDetailsResponse>(priceDetailsDto);
            return new OkObjectResult(response);
        }
    }
}
