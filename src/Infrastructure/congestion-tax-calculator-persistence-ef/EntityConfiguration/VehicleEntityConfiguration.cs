using congestion_tax_calculator_domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion;

public class VehicleEntityConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasMany(a => a.ExceptVehiclePerCities);
    }
}