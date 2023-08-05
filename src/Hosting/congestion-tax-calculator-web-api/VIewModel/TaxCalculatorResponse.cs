namespace congestion_tax_calculator_web_api.VIewModel;

public class TaxCalculatorResponse
{
    public List<TaxCalculatorResponseItem> Items { get; set; }
}

public class TaxCalculatorResponseItem
{
    public decimal TotalTaxPrice { get; set; }
    public DateTime DateTax { get; set; }
}