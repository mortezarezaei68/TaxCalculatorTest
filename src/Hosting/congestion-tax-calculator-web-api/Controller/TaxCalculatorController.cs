using congestion_tax_calculator_validation;
using congestion_tax_calculator_web_api.VIewModel;
using congestion.calculator.Dto;
using Microsoft.AspNetCore.Mvc;

namespace congestion_tax_calculator_web_api.Controller;

[ApiController]
[Route("api/[controller]")]
public class TaxCalculatorController:ControllerBase
{
    private readonly ICongestionTaxCalculatorService _congestionTaxCalculatorService;
    private readonly IValidationCityService _validationCityService;
    private readonly IValidationVehicleService _validationVehicleService;

    public TaxCalculatorController(ICongestionTaxCalculatorService congestionTaxCalculatorService, IValidationCityService validationCityService, IValidationVehicleService validationVehicleService)
    {
        _congestionTaxCalculatorService = congestionTaxCalculatorService;
        _validationCityService = validationCityService;
        _validationVehicleService = validationVehicleService;
    }
    [HttpPost]
    public async Task<IActionResult> CalculatorAsync([FromBody] TaxCalculatorRequest request)
    {
        await _validationCityService.IsCityExist(request.CityId);
        await _validationVehicleService.IsVehicleExist(request.VehicleId);
        
       var data= await _congestionTaxCalculatorService.CalculateTax(new TaxCalculatorRequestDto()
        {
            CityId = request.CityId,
            VehicleId = request.VehicleId,
            CheckInCheckOutTimes = request.CheckInCheckOutTimes
        });
       var result = new TaxCalculatorResponse()
       {
           Items = data.Select(a => new TaxCalculatorResponseItem
           {
               DateTax = a.DateTax,
               TotalTaxPrice = a.TotalTaxPrice
           }).ToList()
       };
       return Ok(result);
    }
}