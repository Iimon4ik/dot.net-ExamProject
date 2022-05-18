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
    public float Price { get; set; }
    public DateTime OrderDataTime { get; set; }

    public Order()
    {
        Id = Guid.NewGuid();
        Status = OrderStatus.Draft;
        SushiList = new List<string>();
        Price = 0;
        OrderDataTime = default;
    }
    public void AddSushi(string sushi)
    {
        SushiList.Add(sushi);
    }

    public void SetStatusToInProgress()
    {
         Status = OrderStatus.InProgress;
    }

    public void SetStatusToForDelivery()
    {
        Status = OrderStatus.ForDelivery;
    }

    public void SetStatusToСonfirmed()
    {
        Status = OrderStatus.Сonfirmed;
    }

    public void SetStatusToDelivered()
    {
        Status = OrderStatus.Delivered;
    }
}