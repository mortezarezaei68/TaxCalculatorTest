using congestion_tax_calculator_domain;
using Microsoft.EntityFrameworkCore;

namespace congestion;

public class CityRepository:ICityRepository
{
    private readonly TaxCalculatorContext _context;

    public CityRepository(TaxCalculatorContext context)
    {
        _context = context;
    }

    public async Task<bool> IfExist(int id)
    {
        return await _context.Cities.AnyAsync(a => a.Id == id);
    }
    public async Task<City> GetById(int id)
        =>await _context.Cities
            .Include(a=>a.TimeTaxInCities)
            .Include(a=>a.ExtensiveRule)
            .Include(a=>a.HolidayDates)
            .Include(a=>a.HolidayMonths)
            .Include(a=>a.TaxFreeDays)
            .Include(a=>a.ExceptVehiclePerCities)
            .FirstOrDefaultAsync(a => a.Id == id);
    
}