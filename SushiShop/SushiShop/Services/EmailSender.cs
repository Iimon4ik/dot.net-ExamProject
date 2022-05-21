namespace SushiShop;
using System.Net;
using System.Net.Mail;

public class EmailSender
{
    private string _password = "7762185!@#";
    private string _email = "ForWorkAcc1205@gmail.com";
    private string _displayName = "Sushi Shop";
    
    public string UserEmail { get; set; }
    public Guid Id { get; set; }
    public float Price { get; set; }
    public string List { get; set; }
    public Enum Status { get; set; }
    public DateTime DataNow { get; set; }
    public int Weight { get; set; }
    
    public void SendMailCreateOrder(string userEmail, Guid id, float price, string list, Enum status, DateTime dataNow, int weight, string fullName, string adress, Enum payments)
    {
        MailAddress from = new MailAddress(_email, _displayName);
        MailAddress to = new MailAddress(userEmail);
        MailMessage msg = new MailMessage(from, to);
        msg.Subject = $"Sushi Shop Order {id}";
        msg.Body = $"Hello, Dear {fullName}! \n" +
                   $"\n" +
                   "We have received your order! \n" +
                   "---------------------------------------------------------------------------------------  \n" +
                   $"Order ID: {id} \n" +
                   $"Price: {price} USD. \n" +
                   $"Sushi: {list} \n" +
                   $"Total weignt: {weight} g \n" +
                   $"Status: {status} \n" +
                   $"Order Data: {dataNow} \n" +
                   "--------------------------------------------------------------------------------------  \n" +
                   $"Delivery time: {dataNow.AddHours(1)} \n" +
                   $"Delivery to: {adress} \n" +
                   $"Payment method: {payments}";
        
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network; 
        smtp.Credentials = new NetworkCredential(from.Address, _password);
        smtp.Send(msg);
    }
    
    public void SendMailSetStatusToInProgress(string userEmail, Guid id, float price, string list, Enum status, DateTime dataNow, int weight, string fullName, string adress, Enum payments)
    {
        MailAddress from = new MailAddress(_email, _displayName);
        MailAddress to = new MailAddress(userEmail);
        MailMessage msg = new MailMessage(from, to);
        msg.Subject = $"Sushi Shop Order {id}, change status!";
        msg.Body = $"Hello, Dear {fullName}! \n" +
                   $"\n" +
                   "We are glad to inform you that your order status has changed to \"In Progress\"! \n";

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network; 
        smtp.Credentials = new NetworkCredential(from.Address, _password);
        smtp.Send(msg);
    }
    public void SendMailSetStatusToDelivery(string userEmail, Guid id, float price, string list, Enum status, DateTime dataNow, int weight, string fullName, string adress, Enum payments)
    {
        MailAddress from = new MailAddress(_email, _displayName);
        MailAddress to = new MailAddress(userEmail);
        MailMessage msg = new MailMessage(from, to);
        msg.Subject = $"Sushi Shop Order {id}, change status!";
        msg.Body = $"Hello, Dear {fullName}! \n" +
                   $"\n" +
                   "We are glad to inform you that your order status has changed to \"To Delivery\"! \n";

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network; 
        smtp.Credentials = new NetworkCredential(from.Address, _password);
        smtp.Send(msg);
    }
    public void SendMailSetStatusCompleted(string userEmail, Guid id, float price, string list, Enum status, DateTime dataNow, int weight, string fullName, string adress, Enum payments)
    {
        MailAddress from = new MailAddress(_email, _displayName);
        MailAddress to = new MailAddress(userEmail);
        MailMessage msg = new MailMessage(from, to);
        msg.Subject = $"Sushi Shop Order {id}, change status!";
        msg.Body = $"Hello, Dear {fullName}! \n" +
                   $"\n" +
                   "We are glad to inform you that your order status has changed to \"Completed\"! \n";

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network; 
        smtp.Credentials = new NetworkCredential(from.Address, _password);
        smtp.Send(msg);
    }
}