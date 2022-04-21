using SushiShop;

Order o1 = new Order(87.4);
Console.WriteLine(o1.Id);
Console.WriteLine(o1.Status);
Console.WriteLine(o1.SushiList);
Console.WriteLine(o1.Price);
Console.WriteLine(o1.OrderData);
o1.Status = Order.EnumStatus.Сonfirmed;

Console.WriteLine(o1.Id);
Console.WriteLine(o1.Status);
Console.WriteLine(o1.SushiList);
Console.WriteLine(o1.Price);
Console.WriteLine(o1.OrderData);