﻿using System.ComponentModel;
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
bool secondaryFlag;
bool mainFlag = true;
OrderRepository orderRepository = new OrderRepository();
Order order = orderRepository.CreateOrder();


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
                secondaryFlag = true;
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
                        orderRepository.GetOrderById(order.Id);
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
                if (idCancel.Equals(order.Id) && !idCancel.Equals("0"))
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
                ByeBye();
                break;
            default:
                WrongDataMessage();
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
        Console.WriteLine($"Size: {sushi[i].Size}, Weight: {sushi[i].Weight} g");
        Console.WriteLine($"Сompound: {sushi[i].Сompound}");
        Console.WriteLine($"Сalories: {sushi[i].Сalories}");
        Console.WriteLine($"Price: {sushi[i].Price} $");
        Console.WriteLine("---------------------------");
    }
    Console.WriteLine($"Enter the sushi number [1-{sushi.Count}] to add to the order and [0] for end: ");
    float sumOrder = 0;
    int totalWeight = 0;

    while (secondaryFlag.Equals(true))
    {
        bool res;
        int userNum;
        var enter = Console.ReadLine();
        res = int.TryParse(enter, out userNum);
        if (res == true)
        {
            if (userNum > 0 && userNum <= sushi.Count)
            {
                order.SushiList.Add(sushi[userNum - 1].Name);
                sumOrder += sushi[userNum - 1].Price;
                totalWeight += sushi[userNum - 1].Weight;
                Console.WriteLine($"Sushi: \"{sushi[userNum - 1].Name}\" - Weight: {sushi[userNum - 1].Weight}g - Price: {sushi[userNum - 1].Price}$ -> Added to cart!");
            }
            else if (userNum == 0 && sumOrder != 0)
            {
                Console.Clear();
                
                secondaryFlag = false;
                order.Price = sumOrder;
                var cartList = string.Empty;
                order.OrderDataTime = DateTime.Now;

                order.SetStatusToInProgress();
                
                foreach (var name in order.SushiList) cartList = cartList + name + ", ";
                
                OrderInfo(totalWeight);

                Console.WriteLine("For To proceed with your order, enter your details: ");
                
                Console.WriteLine("Enter your [full name]: ");
                string fullName = Validation();
                Console.WriteLine();
                
                Console.WriteLine("Enter your [address]: ");
                string address = Validation();
                Console.WriteLine();
                
                Console.WriteLine("Enter your [E-mail]: ");
                string emailUser = EmailAddressValidation();
                Console.WriteLine();
                
                Console.WriteLine("Enter your [phone number]: ");
                string phoneNumber = Validation();
                Console.WriteLine();

                var customer = new Customer(fullName, address, emailUser, order.Id, phoneNumber);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                email.SendMail(emailUser, order.Id, order.Price, cartList, order.Status, order.OrderDataTime, totalWeight,
                    customer.FullName, customer.Address, customer.PaymentsMethod);
            }
            else if (userNum == 0 && sumOrder == 0)
            {
                Console.WriteLine("Your cart is empty!");
                Console.WriteLine("Press any key to continue...");
                orderRepository.DeleteOrder(order.Id);
                Console.ReadKey();
                
                secondaryFlag = false;
            }
            else
            {
                WrongDataMessage();
            }
        }
        else
        {
            WrongDataMessage();
        }
    }

    Console.WriteLine("Thanks for your order!");
}

void ByeBye()
{
    Console.Clear();
    Console.WriteLine("Bye-bye!");
}

void WrongDataMessage()
{
    Console.WriteLine("Wrong data!");
}

string Validation()
{
    while (true)
    {
        string data = Console.ReadLine();
        if (data.Equals(String.Empty))
        {
            Console.Write("You didn't enter anything! Enter a valid value!");
            Console.WriteLine();
        }
        else
        {
            return data;
        }
    }
}

string EmailAddressValidation()
{
    while (true)
    {
        string data = Console.ReadLine();
        if (data.Equals(String.Empty))
        {
            Console.Write("You didn't enter anything! Enter a valid value!");
            Console.WriteLine();
        }
        else
        {
            if (data.Contains('@') && (data.Contains(".ru")||data.Contains(".com")||data.Contains(".by")||data.Contains(".org")||data.Contains(".ua")||data.Contains(".net")))
            {
                return data;
            }
            Console.Write("Your email should contain '@' and domain (.com/.ru/.by/.ua/.org/.net)! Enter a valid value!");
            Console.WriteLine();
        }
    }
}

void OrderInfo(int totalWeight)
{
    var cartList = string.Empty;
    foreach (var name in order.SushiList) cartList = cartList + name + ", ";
    Console.WriteLine("Your Order is: ");
    Console.WriteLine("******************************");
    Console.WriteLine($"Order ID: {order.Id}");
    Console.WriteLine($"Price: {order.Price} USD.");
    Console.WriteLine($"Status: {order.Status}");
    Console.Write($"Sushi: {cartList}");

    Console.WriteLine();
                
    Console.WriteLine($"Total weignt: {totalWeight} g");
    Console.WriteLine($"Data: {order.OrderDataTime}");
    Console.WriteLine("******************************");

    Console.WriteLine();
}


