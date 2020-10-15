using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PriceCalculator.Controllers.Models;
using PriceCalculator.Services;
using PriceCalculator.Services.PriceCalculationServices;
using PriceCalculator.Services.VarRateValidators;
using System;
using System.Net;

namespace PriceCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceCalculatorController : ControllerBase
    {
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IVatRateValidator _vatRateValidator;
        private readonly IMapper _mapper;
        private readonly ILogger<PriceCalculatorController> _logger;
        public PriceCalculatorController(
            IPriceCalculationService priceCalculationService, IVatRateValidator vatRateValidator, IMapper mapper, ILogger<PriceCalculatorController> logger)
        {
            _priceCalculationService = priceCalculationService;
            _vatRateValidator = vatRateValidator;
            _mapper = mapper;
            _logger = logger;
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
                _logger.LogDebug("Received invalid message : ", priceDetails);
                return new BadRequestObjectResult(ModelState);
            }
            if (!_vatRateValidator.IsValidVatRate(priceDetails.VatRate))
            {
                _logger.LogInformation("Received unallowed vat rate : ", priceDetails);
                return StatusCode(StatusCodes.Status451UnavailableForLegalReasons, $"Vat rate {priceDetails.VatRate} is not a legally valid vat rate ");
            }
            var priceDetailsDto = _mapper.Map<PriceDetailsInput>(priceDetails);
            try
            {
                _priceCalculationService.GetCalculatedPrice(priceDetailsDto);
                var response = _mapper.Map<PriceDetailsResponse>(priceDetailsDto);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while processing message", priceDetails, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something went wrong processing your request. If you keep seeing this... you're doing something wrong. Get in touch and we'll tell you what.");
            }

        }
    }
}
