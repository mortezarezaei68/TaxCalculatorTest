using congestion_tax_calculator_domain;
using congestion_tax_calculator_shared_domain.Enums;
using congestion.calculator.Dto;
using FluentAssertions;
using NSubstitute;

namespace congestion_tax_calculator_service_test;

public class CongestionTaxCalculatorServiceTests
{
    private readonly ICongestionTaxCalculatorService _calculatorService;
    private readonly ICityRepository _cityRepository;

    public CongestionTaxCalculatorServiceTests()
    {
        _cityRepository = Substitute.For<ICityRepository>();
        _calculatorService = new CongestionTaxCalculatorService(_cityRepository);
    }

    [Fact]
    public async Task CalculateTax_ShouldReturnNullForTollFreeVehicle()
    {
        var request = new TaxCalculatorRequestDto
        {
            CityId = 1,
            VehicleId = 101,
            CheckInCheckOutTimes = new List<DateTime> { DateTime.Now }
        };
        var city = new City();
        city.AddExceptVehiclePerCity(new List<ExceptVehiclePerCity>()
        {
            new()
            {
                VehicleId = request.VehicleId,
                CityId = request.CityId
            }
        });
        _cityRepository.GetById(request.CityId).Returns(city);

        var result = await _calculatorService.CalculateTax(request);

        Assert.Null(result);
    }

    [Fact]
    public async Task CalculateTax_ShouldReturnExpectedResultsForRegularVehicle()
    {
        var request = new TaxCalculatorRequestDto
        {
            CityId = 1,
            VehicleId = 201,
            CheckInCheckOutTimes = new List<DateTime>
            {
                new(2013, 2, 8, 6, 0, 0),
                new(2013, 2, 8, 8, 0, 0),
                new(2013, 2, 8, 10, 0, 0),
                new(2013, 2, 8, 12, 0, 0),
            }
        };
        var city = new City();
        city.AddExceptVehiclePerCity(new List<ExceptVehiclePerCity>());
        city.AddTimeTaxInCities(new List<TimeTaxInCity>
        {
            new() { StartTime = new TimeSpan(6, 0, 0), FinishedTime = new TimeSpan(6, 29, 59), TaxPrice = 8 },
            new() { StartTime = new TimeSpan(6, 30, 0), FinishedTime = new TimeSpan(8, 59, 59), TaxPrice = 13 },    
            new() { StartTime = new TimeSpan(9, 40, 0), FinishedTime = new TimeSpan(10, 29, 59), TaxPrice = 8 },
            new() { StartTime = new TimeSpan(11, 30, 0), FinishedTime = new TimeSpan(12, 59, 59), TaxPrice = 13 }
        });
        city.AddHolidayDates(new List<HolidayDate>
        {
            new() { HolidayDateTime = new DateTime(2013, 12, 25) }
        });
        city.AddHolidayMonths(new List<HolidayMonth>
        {
            new() { Month = Month.July }
        });
        city.AddTaxFreeDay(new List<TaxFreeDay>
        {
            new() { Day = Day.Saturday }
        });
        city.ExtensiveRule = new ExtensiveRule
        {
            MaxTax = 60,
            TaxFreeDaysAfterHolidayNumber = 1, 
            TaxFreeDaysBeforeHolidayNumber = 1, 
            TaxFreeAfterOnePassInMinutes = 60, 
        };


        _cityRepository.GetById(request.CityId).Returns(city);

        var result = await _calculatorService.CalculateTax(request);
        result.Count.Should().BeGreaterThan(0);
    }
    
    [Fact]
    public async Task CalculateTax_ShouldReturnExpectedResultForMaxTaxExceeded()
    {
        var request = new TaxCalculatorRequestDto
        {
            CityId = 1,
            VehicleId = 201,
            CheckInCheckOutTimes = new List<DateTime>
            {
                new(2013, 2, 8, 6, 0, 0),
                new(2013, 2, 8, 8, 0, 0),
                new(2013, 2, 8, 10, 0, 0),
                new(2013, 2, 8, 12, 0, 0),
            }
        };
        var city = new City
        {
            ExtensiveRule = new ExtensiveRule
            {
                MaxTax = 40
            }
        };
        city.AddTimeTaxInCities(new List<TimeTaxInCity>
        {
            new() { StartTime = new TimeSpan(6, 0, 0), FinishedTime = new TimeSpan(6, 29, 59), TaxPrice = 8 },
            new() { StartTime = new TimeSpan(6, 30, 0), FinishedTime = new TimeSpan(8, 59, 59), TaxPrice = 13 },    
            new() { StartTime = new TimeSpan(9, 40, 0), FinishedTime = new TimeSpan(10, 29, 59), TaxPrice = 8 },
            new() { StartTime = new TimeSpan(11, 30, 0), FinishedTime = new TimeSpan(12, 59, 59), TaxPrice = 13 }
        });

        _cityRepository.GetById(request.CityId).Returns(city);

        var result = await _calculatorService.CalculateTax(request);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        var totalTax = result.Sum(r => r.TotalTaxPrice);
        totalTax.Should().BeLessOrEqualTo(40); 
    }
    
    [Fact]
    public async Task CalculateTax_ShouldReturnZeroTaxForTollFreeDates()
    {
        // Arrange
        var request = new TaxCalculatorRequestDto
        {
            CityId = 1,
            VehicleId = 201,
            CheckInCheckOutTimes = new List<DateTime>
            {
                new DateTime(2013, 7, 1, 10, 0, 0), // A date within the tax-free month
                new DateTime(2013, 7, 2, 15, 0, 0), // Another date within the tax-free month
                // Add more dates as needed
            }
        };
        var city = new City();
        city.AddHolidayMonths(new List<HolidayMonth>
        {
            new HolidayMonth { Month = Month.July }
        });
        // Mock the city repository to return a city with the tax-free month (July) in the HolidayMonths collection
        _cityRepository.GetById(request.CityId).Returns(city);

        // Act
        var result = await _calculatorService.CalculateTax(request);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result); // No tax should be charged for toll-free dates
    }


}