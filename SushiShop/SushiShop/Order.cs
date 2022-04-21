namespace SushiShop;

public class Order
{
    public enum EnumStatus
    {
        Draft,
        Сonfirmed,
        InProgress,
        ForDelivery,
        Delivered
    }
    
    private Guid _id;
    private EnumStatus _status;
    private List<string> _sushiList;
    private double _price;
    private DateTime _orderData; 

    public Guid Id { get; set; }
    public EnumStatus Status { get; set; }
    public List<string> SushiList { get; set; }
    public double Price { get; set; }
    public DateTime OrderData { get; set; }

    public Order(double price)
    {
        Id = Guid.NewGuid();
        Status = EnumStatus.Draft;
        SushiList = new List<string>(10);
        Price = price;
        OrderData = DateTime.Now;
    }
}