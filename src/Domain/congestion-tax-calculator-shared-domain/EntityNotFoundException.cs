using System.Net;

namespace congestion_tax_calculator_shared_domain;

public class EntityNotFoundException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }

    
    public EntityNotFoundException(string message)
        : base(message)
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}