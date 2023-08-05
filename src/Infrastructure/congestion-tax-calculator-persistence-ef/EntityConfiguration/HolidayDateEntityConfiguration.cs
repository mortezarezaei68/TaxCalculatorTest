using congestion_tax_calculator_domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace congestion;

public class HolidayDateEntityConfiguration : IEntityTypeConfiguration<HolidayDate>
{
    public void Configure(EntityTypeBuilder<HolidayDate> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.HolidayDateTime).HasColumnType("date");
    }
}

