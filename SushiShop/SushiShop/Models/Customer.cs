using Microsoft.Extensions.Logging;
using SushiShop.Services;

namespace SushiShop.Models;
public class Customer
{
    private readonly IFileService _fileService;
    private readonly ILoggerService<Customer> _loggerService;
    
    private string _fullName;
    private string _address;
    private string _eMail;
    private string _phoneNumber;
    private int _orderId;
    
    public string FullName { get; }
    public string Address { get; }
    private string PhoneNumber { get; }
    private int OrderId { get; }

    public Customer(string fullName, string address, string phoneNumber, int orderId)
    {
        _fileService = new FileService();
        _loggerService = new LoggerService<Customer>(_fileService);
        FullName = fullName;
        Address = address;
        PhoneNumber = phoneNumber;
        OrderId = orderId;
        _loggerService.Log(LogLevel.Information, $"New customer {FullName} created.");
    }
}