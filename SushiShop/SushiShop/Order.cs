namespace SushiShop;

public class Order
{
    public enum OrderStatus
    {
        Draft,
        Сonfirmed,
        InProgress,
        ForDelivery,
        Delivered
    }
    
    private Guid _id;
    private OrderStatus _status;
    private List<string> _sushiList;
    private double _price;
    private DateTime _orderData; 

    public Guid Id { get; private set; }
    public OrderStatus Status { get; private set; }
    public List<string> SushiList { get; private set; }
    public double Price { get; private set; }
    public DateTime OrderData { get; private set; }

    public Order(double price)
    {
        Id = Guid.NewGuid();
        Status = OrderStatus.Draft;
        SushiList = new List<string>();
        Price = price;
        OrderData = DateTime.Now;
    }

    public void AddSushi(string sushi)
    {
        SushiList.Add(sushi);
    }
}