namespace SushiShop;
using System.Net;
using System.Net.Mail;

public class Email
{
    private string _email = "test1909test@mail.com";
    private string _password = "0RnHUgxwHuswDpiCgwBU";
    private string _displayName = "Sushi Shop";

    public string UserEmail { get; set; }
    public int Id { get; set; }
    public float Price { get; set; }
    public string List { get; set; }
    public Enum Status { get; set; }
    public DateTime DataNow { get; set; }

    public void SendMailCreateOrder(string userEmail, int id, float price, string list, DateTime dataNow, 
        string fullName, string adress)
    {
        MailAddress from = new MailAddress(_email, _displayName);
        MailAddress to = new MailAddress(userEmail);
        MailMessage msg = new MailMessage(from, to);
        msg.Subject = $"Заказ суши {id}";
        msg.Body = $"Добрый день {fullName}! Ваш заказ сформироване! \n" +
                   $"Номер заказа: {id} \n" +
                   $"Цена: {price} руб. \n" +
                   $"Ваши суши: {list} \n" +
                   $"Дата заказа: {dataNow} \n" +
                   $"Ожидаемое время доставки: {dataNow.AddHours(1)} \n" +
                   $"Адрес доставки: {adress}";

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.mail.ru";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Credentials = new NetworkCredential(from.Address, _password);
        smtp.Send(msg);
    }
}