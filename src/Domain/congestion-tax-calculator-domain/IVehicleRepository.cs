namespace congestion_tax_calculator_domain;

public interface IVehicleRepository
{
    Task<Vehicle> GetById(int id);
    Task<bool> IfExist(int id);
}