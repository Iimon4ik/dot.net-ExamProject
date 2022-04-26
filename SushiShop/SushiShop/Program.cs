using Newtonsoft.Json;
using SushiShop;

const string PATH = @"/Users/alexey/CSharp.DiplomProject/SushiShop/SushiShop/sushi_list.json";
var sushi = GetSushiMenu();
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
    Console.WriteLine();
}

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

Order o1 = new Order(sushi[0].Price);
o1.AddSushi(sushi[0].Name);

// Order o2 = new Order(134.94);
Console.WriteLine(o1.Id);
Console.WriteLine(o1.Status);
Console.WriteLine(o1.SushiList);
Console.WriteLine(o1.Price);
Console.WriteLine(o1.OrderData);
//
// Console.WriteLine();
//
// Console.WriteLine(o2.Id);
// Console.WriteLine(o2.Status);
// Console.WriteLine(o2.SushiList);
// Console.WriteLine(o2.Price);
// Console.WriteLine(o2.OrderData);
