using congestion_tax_calculator_domain;
using congestion_tax_calculator_shared_domain;

namespace congestion_tax_calculator_validation;

public class ValidationVehicleService:IValidationVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public ValidationVehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task IsVehicleExist(int vehicleId)
    {
        if (!await _vehicleRepository.IfExist(vehicleId))
            throw new EntityNotFoundException("city is not valid");    
    }
}