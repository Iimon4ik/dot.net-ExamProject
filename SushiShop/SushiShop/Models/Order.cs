using Microsoft.Extensions.Logging;
using SushiShop.Attribute;
using SushiShop.Services;

namespace SushiShop.Models;

[PriceValidation(0)]
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
    
    private int _id;
    private OrderStatus _status;
    private List<string> _sushiList;
    private double _price;
    private DateTime _orderData; 

    private readonly IFileService _fileService;
    private readonly ILoggerService<Order> _loggerService;
    
    public int Id { get; private set; }
    public OrderStatus Status { get; set; }
    public List<string> SushiList { get; set; }
    public float Price { get; set; }
    public DateTime OrderDataTime { get; set; }



    public Order()
    {
        Id = 1;
        Status = OrderStatus.Draft;
        SushiList = new List<string>();
        Price = 0;
        OrderDataTime = default;
        _fileService = new FileService();
        _loggerService = new LoggerService<Order>(_fileService);
        _loggerService.Log(LogLevel.Information, $"Заказ {Id} создан.");
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