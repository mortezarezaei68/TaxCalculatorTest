using congestion_tax_calculator_domain;
using congestion_tax_calculator_shared_domain;

namespace congestion_tax_calculator_validation;

public class ValidationCityService:IValidationCityService
{
    private readonly ICityRepository _cityRepository;

    public ValidationCityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task IsCityExist(int cityId)
    {
        if (!await _cityRepository.IfExist(cityId))
            throw new EntityNotFoundException("city is not valid");

    }

}