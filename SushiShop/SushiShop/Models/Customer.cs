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
    private Payments _paymentsMethod;
    private string _phoneNumber;
    private Guid _orderId;
    
    public enum Payments
    {
        Cash,
        DebitCard,
        CreditCard,
        LongTermsPayments
    }
    public string FullName { get; }
    public string Address { get; }
    public Payments PaymentsMethod { get; }
    private string PhoneNumber { get; }
    private Guid OrderId { get; }

    public Customer(string fullName, string address, string phoneNumber, Guid orderId)
    {
        _fileService = new FileService();
        _loggerService = new LoggerService<Customer>(_fileService);
        FullName = fullName;
        Address = address;
        PaymentsMethod = Payments.Cash;
        PhoneNumber = phoneNumber;
        OrderId = orderId;
        _loggerService.Log(LogLevel.Information, $"New customer {FullName} created.");
    }
}