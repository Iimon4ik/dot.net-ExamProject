using SushiShop.Interfaces;

namespace SushiShop.Repositorys;

public class OrderRepository: IOrderRepository 
{
    public Order CreateOrder()
    {
        var order = new Order();
        OrderService.orders.Add(order);
        return order;

    }

    public void GetOrderById(Guid id)
    {
        foreach (Order order in OrderService.orders)
        {
            if(order.Id.Equals(id))
            {
                Console.WriteLine($"Order ID: {order.Id} - STATUS -> {order.Status}");
            }
            else
            {
                Console.WriteLine("Order with this ID doesn't exist!");
            }
        }
    }

    public void DeleteOrder(Guid id)
    {
        for (int i = OrderService.orders.Count - 1; i >= 0; i--)
        {
            if(OrderService.orders[i].Id.Equals(id))
            {
                OrderService.orders.Remove(OrderService.orders[i]);
            }
        }
    }

    public void AddSushi(string sushi)
    {
        throw new NotImplementedException();
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