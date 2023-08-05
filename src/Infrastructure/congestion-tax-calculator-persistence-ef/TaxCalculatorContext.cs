using congestion_tax_calculator_domain;
using Microsoft.EntityFrameworkCore;

namespace congestion;

public class TaxCalculatorContext:DbContext
{
    public TaxCalculatorContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CityEntityConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<City> Cities{ get; set; }
    public DbSet<ExceptVehiclePerCity> ExceptVehiclePerCities { get; set; }
    public DbSet<ExtensiveRule> ExtensiveRules{ get; set; }
    public DbSet<HolidayDate> HolidayDates { get; set; }
    public DbSet<HolidayMonth> HolidayMonths { get; set; }
    public DbSet<TimeTaxInCity> TimeTaxInCities { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<TaxFreeDay> TaxFreeDays { get; set; }
    
}