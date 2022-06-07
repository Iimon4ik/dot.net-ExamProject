using Newtonsoft.Json;
using SushiShop;
using SushiShop.Interfaces;
using SushiShop.Models;

const string PATH = @"/Users/etcio/source/repos/Iimon4ik/DiplomProject/SushiShop/SushiShop/Data/sushi_list.json";
// string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data/sushi_list.json");

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
var sushi = GetSushiMenu();
Email email = new Email();
bool secondaryFlag;
bool mainFlag = true;
OrderRepository orderRepository = new OrderRepository();
CustomerRepository customerRepository = new CustomerRepository();

while (mainFlag.Equals(true))
{
    Console.WriteLine("Добро пожаловать в Суши-бот! Чтобы начать, введите \"Начать\".");
    string answer = Console.ReadLine();
    if ((answer.ToLower()).Equals("начать"))
    {
        Order order = orderRepository.CreateOrder();
        mainFlag = false;
        Console.Clear();
        Back:
        Console.Clear();
        Console.WriteLine("Что вы хотите сделать?\n");
        Console.WriteLine("Введите \"Меню\" чтобы посмотреть меню.\n");
        Console.WriteLine("Введите \"Заказ\" чтобы посмотреть статус заказа.\n");
        Console.WriteLine("Введите \"Отмена\" чтобы отменить заказ.\n\n\n");
        Console.WriteLine("Введите \"Выход\" чтобы выйти из бота, но я все таки советую хотя бы посмотреть наше меню =)");
        string secondChoice = Console.ReadLine().ToLower();
        Console.Clear();
        switch (secondChoice)
        {
            case "меню":
                secondaryFlag = true;
                MainMenu(order);
                goto Back;
            case "заказ":
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Пожалуйста, введите номер вашего заказ!");
                    Console.WriteLine("или \"0\" чтобы вернуться в прыдущее меню.");
                    string? idGet = Validator.Validation();
                    
                    if (idGet != null && !idGet.Equals(null) && !idGet.Equals("0"))
                    {
                        orderRepository.GetOrderById(int.Parse(idGet));
                    }
                    else if(idGet.ToLower().Equals("0"))
                    {
                        goto Back;
                    }
                }
            case "отмена":
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Пожалуйста, введите номер заказа");
                    Console.WriteLine("Или введите \"0\" чтобы вернуться в прошлое меню.");
                    string idGet = Console.ReadLine().ToLower() ;
                    if (!idGet.Equals(null) && !idGet.Equals("0"))
                    {
                        orderRepository.DeleteOrder(int.Parse(idGet));
                        Console.WriteLine("Нажмите что-нибудь чтобы продолжить");
                        Console.ReadKey();
                        goto Back;
                    }
                    Validator.WrongDataMessage();
                }
            case "выход":
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
    Console.WriteLine("Меню суши");
    Console.WriteLine();
    for (int i = 0; i < sushi.Count; i++)
    {
        Console.WriteLine($"№: {sushi[i].Id}");
        Console.WriteLine($"Название: {sushi[i].Name}");
        Console.WriteLine($"Шт.: {sushi[i].Size}");
        Console.WriteLine($"Описание: {sushi[i].Сompound}");
        Console.WriteLine($"Цена: {sushi[i].Price} руб.");
        Console.WriteLine();
    }
    Console.WriteLine($"Введите номер суши от \"1\" до \"{sushi.Count}\" для добавления в заказ и \"0\" для завершения выбора: ");
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
                Console.WriteLine($"Суши: \"{sushi[userNum - 1].Name}\", цена {sushi[userNum - 1].Price}руб. добавлено в заказ!");
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
                
                foreach (var food in order.SushiList) cartList = cartList + food + ", ";
                
                OrderInfo(order, totalWeight);

                Console.WriteLine("Чтобы оформить ваш заказ, введите Ваши реквизиты: ");
                
                Console.WriteLine("Введите своё имя: ");
                string name = Validator.Validation();
                Console.WriteLine();
                
                Console.WriteLine("Введите ваш адрес: ");
                string address = Validator.Validation();
                Console.WriteLine();
                
                Console.WriteLine("Введите ваш email: ");
                string emailUser = Validator.EmailAddressValidation();
                Console.WriteLine();
                
                Console.WriteLine("Введите ваш номер телефона: ");
                string phoneNumber = Validator.PhoneValidation();
                Console.WriteLine();
                
                Console.WriteLine("Введеные вами данные корректны?");
                Console.WriteLine("  Имя: {0} \n  Адрес: {1} \n  Email: {2} \n  Номер телефона: {3}", name, address, 
                    emailUser, phoneNumber);
                Console.Write("Введите Да или Нет");
                Console.WriteLine();
                
                trueData = Validator.ConfirmationValidation();
                
                if (trueData.Equals("да"))
                {
                    Customer customer = customerRepository.CreateCustomer(name, address, phoneNumber, order.Id);
                    email.SendMailCreateOrder(emailUser, order.Id, order.Price, cartList, order.OrderDataTime,
                        customer.FullName, customer.Address);
                    OrderService.orders.Add(order);
                    Console.WriteLine("Нажмите любую кнопку чтобы продолжить.");
                    Console.WriteLine("Спасибо за заказ!");
                }
                else if ((trueData.Equals("нет")))
                {
                    orderRepository.DeleteOrder(order.Id);
                }
            }
            else if (userNum == 0 && sumOrder == 0)
            {
                Console.WriteLine("Ваша корзина заказов пуста!");
                Console.WriteLine("Нажмите любую кнопку чтобы продолжить.");
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
    Console.WriteLine("До свидания!");
}

void OrderInfo(Order order, int totalWeight)
{
    order.Status = Order.OrderStatus.Draft;
    var cartList = string.Empty;
    foreach (var name in order.SushiList) cartList = cartList + name + ", ";
    Console.WriteLine("Ваш заказ: ");
    Console.WriteLine();
    Console.WriteLine($"Номер заказа: {order.Id}");
    Console.WriteLine($"Стоимость: {order.Price} руб.");
    Console.WriteLine($"Статус: {order.Status}");
    Console.Write($"Список заказа: {cartList}");
    Console.WriteLine();       
    Console.WriteLine($"Дата: {order.OrderDataTime}");
    Console.WriteLine();
}