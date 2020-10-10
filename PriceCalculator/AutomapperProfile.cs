using AutoMapper;
using PriceCalculator.Controllers.Models;
using PriceCalculator.Services;

namespace PriceCalculator
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<PriceDetailsRequest, PriceDetails>();
            CreateMap<PriceDetails, PriceDetailsResponse>();
        }
    }
}
