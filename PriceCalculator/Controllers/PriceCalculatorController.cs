using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PriceCalculator.Controllers.Models;
using PriceCalculator.Services;

namespace PriceCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceCalculatorController : ControllerBase
    {
        private IPriceCalculationService _priceCalculationService;
        private readonly IMapper _mapper;
        public PriceCalculatorController(IPriceCalculationService priceCalculationService, IMapper mapper)
        {
            _priceCalculationService = priceCalculationService;
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
            var priceDetailsDto = _mapper.Map<PriceDetails>(priceDetails);
            _priceCalculationService.FillInPrices(priceDetailsDto);
            var response = _mapper.Map<PriceDetailsResponse>(priceDetailsDto);
            return new OkObjectResult(response);
        }
    }
}
