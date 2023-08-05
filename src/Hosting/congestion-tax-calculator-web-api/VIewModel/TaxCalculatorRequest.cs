namespace congestion_tax_calculator_web_api.VIewModel;

public class TaxCalculatorRequest
{
    public int CityId { get; set; }
    public int VehicleId { get; set; }
    public List<DateTime> CheckInCheckOutTimes { get; set; }
}