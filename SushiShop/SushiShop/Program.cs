using Newtonsoft.Json;
using SushiShop;
using SushiShop.Interfaces;
using SushiShop.Repositorys;

const string PATH = @"/Users/alexey/CSharp.DiplomProject/SushiShop/SushiShop/Data/sushi_list.json";
// string PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"SushiShop\sushi_list.json");

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


while (mainFlag.Equals(true))
{
    Console.WriteLine("Welcome to our sushi shop!");
    Console.WriteLine("To get started, type [Start].");
    string firstChoice = Console.ReadLine();
    if ((firstChoice.ToLower()).Equals("start"))
    {
        Order order = orderRepository.CreateOrder();
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
                MainMenu(order);
                goto Back;
            case "2":
                // bool thirdFlag = true;
                while (true)
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
                }
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
                }
                else if (idCancel.Equals("0"))
                {
                    Console.Clear();
                    goto Back;
                }
                else
                {
                    Validator.WrongDataMessage();
                }
                break;
            case "0":
                orderRepository.DeleteOrder(order.Id);
                ByeBye();
                break;
            default:
                Validator.WrongDataMessage();
                goto Back;
        }
    }
    else
    {
        Validator.WrongDataMessage();
    }
}

void MainMenu(Order order)
{
    order = orderRepository.CreateOrder();
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
    List<string> draftList = new List<string>();

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
                draftList.Add(sushi[userNum - 1].Name);
                sumOrder += sushi[userNum - 1].Price;
                totalWeight += sushi[userNum - 1].Weight;
                Console.WriteLine($"Sushi: \"{sushi[userNum - 1].Name}\" - Weight: {sushi[userNum - 1].Weight}g - Price: {sushi[userNum - 1].Price}$ -> Added to cart!");
            }
            else if (userNum == 0 && sumOrder != 0)
            {
                order.SushiList = draftList;
                Console.Clear();
                secondaryFlag = false;
                order.Price = sumOrder;
                var cartList = string.Empty;
                order.OrderDataTime = DateTime.Now;
                string trueData;

                order.SetStatusToInProgress();
                
                foreach (var name in order.SushiList) cartList = cartList + name + ", ";
                
                OrderInfo(order, totalWeight);

                Console.WriteLine("For To proceed with your order, enter your details: ");
                
                Console.WriteLine("Enter your [full name]: ");
                string fullName = Validator.Validation();
                Console.WriteLine();
                
                Console.WriteLine("Enter your [address]: ");
                string address = Validator.Validation();
                Console.WriteLine();
                
                Console.WriteLine("Enter your [E-mail]: ");
                string emailUser = Validator.EmailAddressValidation();
                Console.WriteLine();
                
                Console.WriteLine("Enter your [phone number]: ");
                string phoneNumber = Validator.PhoneValidation();
                Console.WriteLine();
                
                Console.Write("Confirm that the entered data is correct.");
                Console.Write("Press [Y] - yes, [N] - no.");
                Console.WriteLine();
                
                trueData = Validator.ConfirmationValidation();
                if (trueData.Equals("y"))
                {
                    var customer = new Customer(fullName, address, emailUser, order.Id, phoneNumber);
                    order.SetStatusToСonfirmed();

                    // Console.WriteLine("Press any key to continue...");
                    // Console.ReadKey();
                    email.SendMailCreateOrder(emailUser, order.Id, order.Price, cartList, order.Status, order.OrderDataTime, totalWeight, customer.FullName, customer.Address, customer.PaymentsMethod);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine("Thanks for your order!");
                }
                orderRepository.DeleteOrder(order.Id);
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
                Validator.WrongDataMessage();
            }
        }
        else
        {
            Validator.WrongDataMessage();
        }
    }
}

void ByeBye()
{
    Console.Clear();
    Console.WriteLine("Bye-bye!");
}

void OrderInfo(Order order, int totalWeight)
{
    order.Status = Order.OrderStatus.Draft;
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


