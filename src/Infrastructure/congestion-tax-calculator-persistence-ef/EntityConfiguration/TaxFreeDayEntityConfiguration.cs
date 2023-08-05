using congestion_tax_calculator_domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion;

public class TaxFreeDayEntityConfiguration : IEntityTypeConfiguration<TaxFreeDay>
{
    public void Configure(EntityTypeBuilder<TaxFreeDay> builder)
    {
        builder.HasKey(a => a.Id);
    }
}