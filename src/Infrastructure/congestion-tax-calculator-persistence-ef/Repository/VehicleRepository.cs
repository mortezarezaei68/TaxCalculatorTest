using congestion_tax_calculator_domain;
using Microsoft.EntityFrameworkCore;

namespace congestion;

public class VehicleRepository:IVehicleRepository
{
    private readonly TaxCalculatorContext _context;

    public VehicleRepository(TaxCalculatorContext context)
    {
        _context = context;
    }

    public async Task<Vehicle> GetById(int id)
        =>await _context.Vehicles
            .Include(a=>a.ExceptVehiclePerCities).FirstOrDefaultAsync(a => a.Id == id);

    public async Task<bool> IfExist(int id)
    => await _context.Vehicles.AnyAsync(a => a.Id == id);
}