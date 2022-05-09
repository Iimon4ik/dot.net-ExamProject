namespace SushiShop;
using System.Net;
using System.Net.Mail;

public class EmailSender
{
    public string UserEmail { get; set; }
    public Guid Id { get; set; }
    public float Price { get; set; }
    public string List { get; set; }
    public Enum Status { get; set; }
    public DateTime DataNow { get; set; }
    public int Weignt { get; set; }
    
    public void SendMail(string userEmail, Guid id, float price, string list, Enum status, DateTime dataNow, int weignt)
    {
        MailAddress from = new MailAddress("ForWorkAcc1205@gmail.com", "Sushi Shop Console Project");
        MailAddress to = new MailAddress(userEmail);
        MailMessage msg = new MailMessage(from, to);
        msg.Subject = $"New Test Order {id}";
        msg.Body = "Hello, Dear Customer! \n" +
                   "We have a new order! \n" +
                   "---------------------------------------------------------------------------------------   \n" +
                   $"Order ID: {id} \n" +
                   $"Price: {price} USD. \n" +
                   $"Sushi: {list} \n" +
                   $"Total weignt: {weignt} g \n" +
                   $"Status: {status} \n" +
                   $"Order Data: {dataNow} \n" +
                   $"Delivery time: {dataNow.AddHours(1)} \n" +
                   "--------------------------------------------------------------------------------------  \n";
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network; 
        smtp.Credentials = new NetworkCredential(from.Address, "7762185!@#");
        smtp.Send(msg);
    }
}