using congestion_tax_calculator_domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion;

public class ExtensiveRuleEntityConfiguration : IEntityTypeConfiguration<ExtensiveRule>
{
    public void Configure(EntityTypeBuilder<ExtensiveRule> builder)
    {
        builder.HasKey(a => a.Id);
    }
}