namespace congestion_tax_calculator_domain;

public class ExtensiveRule
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public decimal MaxTax { get; set; }
    public int TaxFreeDaysBeforeHolidayNumber { get; set; }
    public int TaxFreeDaysAfterHolidayNumber { get; set; }
    public int TaxFreeAfterOnePassInMinutes { get; set; }

}