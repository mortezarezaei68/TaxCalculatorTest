using congestion_tax_calculator_shared_domain.Enums;

namespace congestion_tax_calculator_domain;

public class TaxFreeDay
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public Day Day { get; set; }
}