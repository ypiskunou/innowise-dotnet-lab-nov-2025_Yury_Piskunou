namespace ProductService.Shared.RequestFeatures;

public class ProductParameters : RequestParameters
{
    public ProductParameters()
    {
        OrderBy = "name"; 
    }
    
    public decimal MinPrice { get; set; } = 0;
    public decimal MaxPrice { get; set; } = decimal.MaxValue;
    
    public bool ValidPriceRange => MaxPrice > MinPrice;
    
    public Guid? CategoryId { get; set; }
    
    public string? SearchTerm { get; set; }
}