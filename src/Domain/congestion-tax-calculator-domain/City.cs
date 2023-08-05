using congestion_tax_calculator_shared_domain;

namespace congestion_tax_calculator_domain;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    private readonly List<ExceptVehiclePerCity> _exceptVehiclePerCities = new();
    public IReadOnlyCollection<ExceptVehiclePerCity> ExceptVehiclePerCities =>
        _exceptVehiclePerCities;
    
    public ExtensiveRule ExtensiveRule { get; set; }
    
    private readonly List<HolidayDate> _holidayDates = new();
    public IReadOnlyCollection<HolidayDate> HolidayDates =>
        _holidayDates;
    
    private readonly List<HolidayMonth> _holidayMonths = new();
    public IReadOnlyCollection<HolidayMonth> HolidayMonths =>
        _holidayMonths;  
    
    private readonly List<TaxFreeDay> _taxFreeDays = new();
    public IReadOnlyCollection<TaxFreeDay> TaxFreeDays =>
        _taxFreeDays;
    
    private readonly List<TimeTaxInCity> _timeTaxInCities = new();
    public IReadOnlyCollection<TimeTaxInCity> TimeTaxInCities =>
        _timeTaxInCities;

    public void AddExceptVehiclePerCity(List<ExceptVehiclePerCity> exceptVehiclePerCities)
    {
        _exceptVehiclePerCities.AddRange(exceptVehiclePerCities);
    }

    public void AddTimeTaxInCities(List<TimeTaxInCity> timeTaxInCities)
    {
        _timeTaxInCities.AddRange(timeTaxInCities);
    }
    public void AddHolidayDates(List<HolidayDate> holidayDates)
    {
        _holidayDates.AddRange(holidayDates);
    }
    public void AddHolidayMonths(List<HolidayMonth> holidayMonths)
    {
        _holidayMonths.AddRange(holidayMonths);
    }  
    public void AddTaxFreeDay(List<TaxFreeDay> taxFreeDays)
    {
        _taxFreeDays.AddRange(taxFreeDays);
    }
}