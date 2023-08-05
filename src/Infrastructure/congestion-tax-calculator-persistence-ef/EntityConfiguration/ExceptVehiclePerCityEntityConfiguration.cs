using congestion_tax_calculator_domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion;

public class ExceptVehiclePerCityEntityConfiguration : IEntityTypeConfiguration<ExceptVehiclePerCity>
{
    public void Configure(EntityTypeBuilder<ExceptVehiclePerCity> builder)
    {
        builder.HasKey(a => a.Id);
    }
}