using congestion_tax_calculator_shared_domain.Enums;

namespace congestion_tax_calculator_domain;

public class HolidayMonth
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public Month Month { get; set; }

}