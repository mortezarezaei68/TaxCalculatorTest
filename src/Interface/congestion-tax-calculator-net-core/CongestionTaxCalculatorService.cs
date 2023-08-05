using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using congestion_tax_calculator_domain;
using congestion_tax_calculator_shared_domain.Enums;
using congestion.calculator.Dto;

public class CongestionTaxCalculatorService : ICongestionTaxCalculatorService
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */
    private readonly ICityRepository _cityRepository;


    public CongestionTaxCalculatorService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }


    public async Task<List<TaxCalculatorResponseDto>> CalculateTax(TaxCalculatorRequestDto request)
    {
        var tax = await GetTax(request.CheckInCheckOutTimes, request.CityId, request.VehicleId);
        return tax;
    }

    public async Task<List<TaxCalculatorResponseDto>> GetTax(List<DateTime> dates, int cityId, int vehicleId)
    {
        var city = await _cityRepository.GetById(cityId);
        
        if (IsTollFreeVehicle(city.ExceptVehiclePerCities, vehicleId))
            return default;

        var correctDates = dates.Except(dates.Where(a =>
                IsTollFreeDate(a, city.HolidayDates, city.HolidayMonths, city.TaxFreeDays, city.ExtensiveRule)))
            .OrderByDescending(a => a.Date).ToList();
        
        var dictionaryValues = new Dictionary<DateTime, decimal>();

        foreach (var t in correctDates)
        {
            var fee = city.TimeTaxInCities.FirstOrDefault(a => a.FinishedTime >= t.TimeOfDay &&
                                                               a.StartTime <= t.TimeOfDay)?.TaxPrice;

            if (fee != null && IsMaxTaxInDay(t,dictionaryValues,city.ExtensiveRule.MaxTax,fee.Value))
                continue;

            if (fee is not null && dictionaryValues.Count == 0)
            {
                dictionaryValues.Add(t,fee.Value);
            }
            else if(fee is not null && IsTaxFreeAfterOnePassInMinutes(t,dictionaryValues,city.ExtensiveRule.TaxFreeAfterOnePassInMinutes, out var keyValuePair))
            {
                if (fee>keyValuePair.Value)
                {
                    dictionaryValues.Remove(keyValuePair.Key);
                    dictionaryValues.Add(t,fee.Value);
                }
            }
            else if(fee is not null )
            {
                dictionaryValues.Add(t,fee.Value);
            }
        }

        return dictionaryValues.GroupBy(a=>a.Key.Date).Select(b=>new TaxCalculatorResponseDto()
        {
            DateTax = b.Key,
            TotalTaxPrice = b.Sum(a=>a.Value)
        }).ToList();
    }

    private bool IsTollFreeVehicle(IEnumerable<ExceptVehiclePerCity> exceptVehiclePerCities, int vehicleId)
    {
        return exceptVehiclePerCities.Any(a => a.VehicleId == vehicleId);
    }

    private bool IsTaxFreeAfterOnePassInMinutes(DateTime current, 
        Dictionary<DateTime, decimal> dictionary, int taxFreeAfterOnePassInMinutes,
        out KeyValuePair<DateTime, decimal> valuePair)
    {
        valuePair = dictionary.FirstOrDefault(a =>current.Date==a.Key.Date &&
            current.Subtract(a.Key).TotalMinutes <= taxFreeAfterOnePassInMinutes);
        return valuePair.Value!=0;
    }
    private bool IsMaxTaxInDay(DateTime current, 
        Dictionary<DateTime, decimal> dictionary, decimal maxTax, decimal fee)
    {
        var sum = dictionary.Where(a =>current.Date==a.Key.Date).Sum(a=>a.Value);
        return sum > maxTax || sum+fee>maxTax;
    }

    private bool IsTollFreeDate(DateTime date, IEnumerable<HolidayDate> cityHolidayDates,
        IEnumerable<HolidayMonth> cityHolidayMonths, IEnumerable<TaxFreeDay> cityTaxFreeDays,
        ExtensiveRule extensiveRule)
    {
        return cityTaxFreeDays.Any(a => a.Day == (Day)date.DayOfWeek) ||
               cityHolidayDates.Any(a =>
                   a.HolidayDateTime == date ||
                   a.HolidayDateTime.AddDays(extensiveRule.TaxFreeDaysAfterHolidayNumber) <= date ||
                   date >= a.HolidayDateTime.AddDays(-extensiveRule.TaxFreeDaysBeforeHolidayNumber)) ||
               cityHolidayMonths.Any(a => a.Month == (Month)date.Month);
    }
}

public interface ICongestionTaxCalculatorService
{
    Task<List<TaxCalculatorResponseDto>> CalculateTax(TaxCalculatorRequestDto request);
}