using congestion_tax_calculator_domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion;

public class CityEntityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasMany(a => a.TimeTaxInCities);
        builder.HasMany(a => a.TaxFreeDays);
        builder.HasOne(a => a.ExtensiveRule);
        builder.HasMany(a => a.HolidayDates);
        builder.HasMany(a => a.HolidayMonths);
        builder.HasMany(a => a.ExceptVehiclePerCities);
    }
}