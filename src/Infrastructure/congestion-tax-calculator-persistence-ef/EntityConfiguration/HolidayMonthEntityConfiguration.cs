using congestion_tax_calculator_domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion;

public class HolidayMonthEntityConfiguration : IEntityTypeConfiguration<HolidayMonth>
{
    public void Configure(EntityTypeBuilder<HolidayMonth> builder)
    {
        builder.HasKey(a => a.Id);
    }
}