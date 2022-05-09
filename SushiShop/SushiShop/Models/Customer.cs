namespace SushiShop;

public class Customer
{
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
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public Payments PaymentsMethod { get; set; }
    public string PhoneNumber { get; set; }
    public Guid OrderId { get; set; }

    public Customer(string fullName,string address,string email, Guid orderId, string phoneNumber)
    {
        FullName = fullName;
        Address = address;
        Email = email;
        PaymentsMethod = Payments.Cash;
        PhoneNumber = phoneNumber;
        OrderId = orderId;
    }
}