namespace congestion_tax_calculator_validation;

public interface IValidationVehicleService
{
    Task IsVehicleExist(int vehicleId);
}