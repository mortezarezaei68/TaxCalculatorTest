namespace congestion_tax_calculator_validation;

public interface IValidationCityService
{
    Task IsCityExist(int cityId);
}