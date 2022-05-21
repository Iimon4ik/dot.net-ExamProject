namespace SushiShop.Attribute;

public class PriceValidationAttribute:System.Attribute
{
    public float Price { get; }
    public PriceValidationAttribute(){ }
    public PriceValidationAttribute(float price) => Price = price;
}