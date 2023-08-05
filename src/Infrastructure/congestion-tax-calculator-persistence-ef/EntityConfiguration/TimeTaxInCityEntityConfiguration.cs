using congestion_tax_calculator_domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion;

public class TimeTaxInCityEntityConfiguration : IEntityTypeConfiguration<TimeTaxInCity>
{
    public void Configure(EntityTypeBuilder<TimeTaxInCity> builder)
    {
        builder.HasKey(a => a.Id);
    }
}