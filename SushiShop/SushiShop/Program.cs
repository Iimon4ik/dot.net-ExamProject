using System.ComponentModel;
using Newtonsoft.Json;
using SushiShop;
using System.Net;
using System.Net.Mail;
using SushiShop.Repositorys;

// Тут берем суши из JSON и закидываем в список

const string PATH = @"/Users/alexey/CSharp.DiplomProject/SushiShop/SushiShop/sushi_list.json";
var sushi = GetSushiMenu();

static List<Sushi> GetSushiMenu()
{
    string fileName = PATH;
    if (File.Exists(fileName))
    {
        var sushi = JsonConvert.DeserializeObject<List<Sushi>>(File.ReadAllText(fileName));
        return sushi;
    }
    return null; 
};

EmailSender email = new EmailSender();

bool mainFlag = true;
bool secondaryFlag = true;
OrderRepository orders = new OrderRepository();
Order order = new Order(0);
orders.CreateOrder(order);

while (mainFlag.Equals(true))
{
    Console.WriteLine("Welcome to our sushi shop!");
    Console.WriteLine("To get started, type [Start].");
    string firstChoice = Console.ReadLine();
    if ((firstChoice.ToLower()).Equals("start"))
    {
        mainFlag = false;
        Console.Clear();
        Back:
        Console.Clear();
        Console.WriteLine("Select the required item:");
        Console.WriteLine();
        Console.WriteLine("Press [1] to show menu.");
        Console.WriteLine("Press [2] to show order status.");
        Console.WriteLine("Press [3] to cancel your order.");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Press [0] to exit.");
        string secondChoice = Console.ReadLine();
        Console.Clear();
        switch (secondChoice)
        {
            case "1":
                Point1();
                goto Back;
            case "2":
                bool thirdFlag = true;
                while (thirdFlag)
                {
                    Console.Clear();
                    Console.WriteLine("Please, enter Order ID!");
                    Console.WriteLine("Or [0] for return to prev. menu.");
                    // orders.GetOrderById(order.Id);
                    string idGet = Console.ReadLine();
                    if (!idGet.Equals(null) && !idGet.Equals("0"))
                    {
                        Guid.Parse(idGet);
                        orders.GetOrderById(order.Id);
                        Console.WriteLine("Press any key to continue...");
                        goto Back;
                    }
                    else if (idGet.Equals("0"))
                    {
                        Console.Clear();
                        goto Back;
                    }
                    else
                    {
                        Console.WriteLine("Error!");
                    }
                }
                break;
            case "3":
                Console.Clear();
                Console.WriteLine("Please, enter Order ID!");
                Console.WriteLine("Or [0] for return to prev. menu.");
                string idCancel = Console.ReadLine();
                if (idCancel.Equals("aaaaa") && !idCancel.Equals("0"))
                {
                    Console.Clear();
                    Console.WriteLine($"Your order ID [{idCancel}] has been successfully canceled!");
                    Console.WriteLine("---------------------------");
                    Console.WriteLine("Press [0] for return to prev. menu.");
                    string idCancelBack = Console.ReadLine();
                    if (idCancelBack.Equals("0"))
                    {
                        goto Back;
                    }
                    else
                    {
                        Console.WriteLine("Error!");
                    }
                }
                else if (idCancel.Equals("0"))
                {
                    Console.Clear();
                    goto Back;
                }
                else
                {
                    Console.WriteLine("Error!");
                }
                break;
            case "0":
                Point0();
                break;
            default:
                PointDefault();
                goto Back;
        }
    }
    else
    {
        Console.WriteLine("Wrong data!");
    }
}

void Point1()
{
    Console.Clear();
    Console.WriteLine("--- Sushi-Shop Menu ---");
    Console.WriteLine();
    for (int i = 0; i < sushi.Count; i++)
    {
        Console.WriteLine($"Number: {sushi[i].Id}");
        Console.WriteLine($"Name: {sushi[i].Name}");
        Console.WriteLine($"Size: {sushi[i].Size}, Weignt: {sushi[i].Weignt} g");
        Console.WriteLine($"Сompound: {sushi[i].Сompound}");
        Console.WriteLine($"Сalories: {sushi[i].Сalories}");
        Console.WriteLine($"Price: {sushi[i].Price} $");
        Console.WriteLine("---------------------------");
    }
    Console.WriteLine("Enter the sushi number [1-15] to add to the order and [0] for end: ");
    float sumOrder = 0;
    int totalWeignt = 0;
    while (secondaryFlag.Equals(true))
    {
        int enter = Int16.Parse(Console.ReadLine());
                    
        if (enter > 0 && enter < 16)
        {
            order.SushiList.Add(sushi[enter].Name);
            sumOrder += sushi[enter].Price;
            totalWeignt += sushi[enter].Weignt;
            Console.WriteLine($"\" Sushi: {sushi[enter].Name} - Weight: {sushi[enter].Weignt} g - Price: {sushi[enter].Price} $\" -> Added to cart!");
        }
        else if(enter == 0 && sumOrder != 0)
        {
            Console.Clear();
            secondaryFlag = false;
            order.Price = sumOrder;
            order.OrderDataTime = DateTime.Now;
            order.SetStatusToInProgress();
            string list = String.Empty;
            foreach (var name in order.SushiList)
            {
                list = list + name + ", ";
            }
            Console.WriteLine("Your Order is: ");
            Console.WriteLine("******************************");
            Console.WriteLine($"Order ID: {order.Id}" );
            Console.WriteLine($"Price: {order.Price} USD.");
            Console.WriteLine($"Status: {order.Status}");
            Console.Write($"Sushi: {list}");
            
            // foreach (var name in order.SushiList)
            // {
            //     Console.Write(name + ", ");
            // }
            
            Console.WriteLine();
            Console.WriteLine($"Total weignt: {totalWeignt} g");
            Console.WriteLine($"Data: {order.OrderDataTime}");
            Console.WriteLine("******************************");
            
            Console.WriteLine();
            
            Console.WriteLine("For To proceed with your order, enter your details: ");
            Console.WriteLine("Enter your [full name]: ");
            string fullName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter your [address]: ");
            string address = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter your [E-mail]: ");
            string emailUser = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter your [phone number]: ");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine();

            Customer customer = new Customer(fullName, address, emailUser, order.Id, phoneNumber);
            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            email.SendMail(emailUser, order.Id, order.Price, list, order.Status, order.OrderDataTime, totalWeignt, customer.FullName, customer.Address, customer.PaymentsMethod);
        }
        else if (enter == 0 && sumOrder == 0)
        {
            Console.WriteLine("Your cart is empty!");
        }
        else
        {
            Console.WriteLine("Wrong data!");
        }
    }
    Console.WriteLine("Thanks for order!");
}

void Point0()
{
    Console.Clear();
    Console.WriteLine("Bye-bye!");
}

void PointDefault()
{
    Console.Clear();
    Console.WriteLine("Wrong data!");
}


