using SushiShop.Models;

namespace SushiShop.Interfaces;

public class OrderRepository: IOrderRepository 
{
    public Order CreateOrder()
    {
        var order = new Order();
        OrderService.orders.Add(order);
        return order;

    }
    
    public void GetOrderById(int id)
    {
        try
        {
            foreach (Order order in OrderService.orders)
            {
                if (order.Id.Equals(id))
                {
                    Console.WriteLine($"Заказ номер: {order.Id} - состояние > {order.Status}");
                }
                else
                {
                    throw new Exception("Заказ с таким номером не существует");
                }
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine($"Exception: {exception.Message}");
            Console.WriteLine($"Method: {exception.TargetSite}");
            Console.WriteLine($"Exception StackTrace: {exception.StackTrace}");
            Console.WriteLine();
        }
    }

    public void DeleteOrder(int id)
    {
        try
        {
            for (int i = OrderService.orders.Count - 1; i >= 0; i--)
            {
                if (OrderService.orders[i].Id.Equals(id))
                {
                    OrderService.orders.Remove(OrderService.orders[i]);
                }
            }
            throw new Exception("Заказ с таким номером не существует!");
        }
        catch(Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine($"Exception: {exception.Message}");
            Console.WriteLine($"Method: {exception.TargetSite}");
            Console.WriteLine($"Exception StackTrace: {exception.StackTrace}");
            Console.WriteLine();
        }
    }

    public void SetStatusToInProgress(Order order)
    {
        order.Status = Order.OrderStatus.InProgress;
    }

    public void SetStatusToForDelivery(Order order)
    {
        order.Status = Order.OrderStatus.ForDelivery;
    }

    public void SetStatusToСonfirmed(Order order)
    {
        order.Status = Order.OrderStatus.Сonfirmed;
    }

    public void SetStatusToDelivered(Order order)
    {
        order.Status = Order.OrderStatus.Delivered;
    }
}