using congestion_tax_calculator_shared_domain;
using congestion_tax_calculator_validation;
using congestion_tax_calculator_web_api.Controller;
using congestion_tax_calculator_web_api.VIewModel;
using congestion.calculator.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.Extensions;

namespace congestion_tax_calculator_service_test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
    }

    [Theory]
    [MemberData(nameof(TaxCalculatorRequestTestData.InvalidCityDate),
        MemberType = typeof(TaxCalculatorRequestTestData))]
    public void CalculatorAsync_Throws_EntityNotFoundException_For_InvalidCity(TaxCalculatorRequest request)
    {
        var congestionTaxCalculatorService = Substitute.For<ICongestionTaxCalculatorService>();
        var validationCityService = Substitute.For<IValidationCityService>();
        var validationVehicleService = Substitute.For<IValidationVehicleService>();
        validationCityService.IsCityExist(request.CityId)
            .Throws(info => throw new EntityNotFoundException("not valid entity"));
        var controller = new TaxCalculatorController(congestionTaxCalculatorService, validationCityService,
            validationVehicleService);
        Action act = () => controller.CalculatorAsync(request).Wait();

        act.Should().Throw<EntityNotFoundException>();
    }

    [Theory]
    [MemberData(nameof(TaxCalculatorRequestTestData.InvalidVehicleDate),
        MemberType = typeof(TaxCalculatorRequestTestData))]
    public void CalculatorAsync_Throws_EntityNotFoundException_For_InvalidVehicle(TaxCalculatorRequest request)
    {
        var congestionTaxCalculatorService = Substitute.For<ICongestionTaxCalculatorService>();
        var validationCityService = Substitute.For<IValidationCityService>();
        var validationVehicleService = Substitute.For<IValidationVehicleService>();
        validationVehicleService.IsVehicleExist(request.VehicleId)
            .Throws(info => throw new EntityNotFoundException("not valid entity"));
        var controller = new TaxCalculatorController(congestionTaxCalculatorService, validationCityService,
            validationVehicleService);
        Action act = () => controller.CalculatorAsync(request).Wait();

        act.Should().Throw<EntityNotFoundException>();
    }
}

public static class TaxCalculatorRequestTestData
{
    public static IEnumerable<object[]> InvalidCityDate()
    {
        // Provide different test scenarios with input data and expected responses
        yield return new object[]
        {
            new TaxCalculatorRequest
            {
                CityId = 0,
                VehicleId = 1,
                CheckInCheckOutTimes = new List<DateTime>
                {
                    new DateTime(2023, 08, 01, 9, 0, 0),
                    new DateTime(2023, 08, 01, 17, 0, 0)
                }
            }
        };
    }

    public static IEnumerable<object[]> InvalidVehicleDate()
    {
        // Provide different test scenarios with input data and expected responses
        yield return new object[]
        {
            new TaxCalculatorRequest
            {
                CityId = 1,
                VehicleId = 0,
                CheckInCheckOutTimes = new List<DateTime>
                {
                    new DateTime(2023, 08, 01, 9, 0, 0),
                    new DateTime(2023, 08, 01, 17, 0, 0)
                }
            }
        };
    }
}