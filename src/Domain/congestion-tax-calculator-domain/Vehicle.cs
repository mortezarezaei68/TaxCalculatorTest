namespace congestion_tax_calculator_domain;

public class Vehicle
{
    public int Id { get; set; }
    public string Name { get; set; }
    private readonly List<ExceptVehiclePerCity> _exceptVehiclePerCities = new();
    public IReadOnlyCollection<ExceptVehiclePerCity> ExceptVehiclePerCities =>
        _exceptVehiclePerCities;
}