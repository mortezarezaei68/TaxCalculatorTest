namespace congestion_tax_calculator_domain;

public interface ICityRepository
{
    Task<City> GetById(int id);
    Task<bool> IfExist(int id);
}