namespace congestion_tax_calculator_domain;

public class TimeTaxInCity
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public decimal TaxPrice { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan FinishedTime { get; set; }
 
}