using System;
using System.Collections.Generic;

namespace congestion.calculator.Dto;

public class TaxCalculatorResponseDto
{
    public decimal TotalTaxPrice { get; set; }
    public DateTime DateTax { get; set; }
}
public class TaxCalculatorRequestDto
{
    public int CityId { get; set; }
    public int VehicleId { get; set; }
    public List<DateTime> CheckInCheckOutTimes { get; set; }
}