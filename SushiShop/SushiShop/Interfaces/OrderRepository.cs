using SushiShop.Interfaces;

namespace SushiShop.Repositorys;

public class OrderRepository: IOrderRepository 
{
    public Order CreateOrder()
    {
        var order = new Order();
        if (OrderService.orders.Count == 0)
        {
            OrderService.orders.Add(order);
            return order;
        }
        else
        {
            var order1 = new Order();
            order = order1;
            OrderService.orders.Add(order);
            return order;
        }
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

    public void SetStatusToInProgress()
    {
        throw new NotImplementedException();
    }

    public void SetStatusToForDelivery()
    {
        throw new NotImplementedException();
    }

    public void SetStatusToСonfirmed()
    {
        throw new NotImplementedException();
    }

    public void SetStatusToDelivered()
    {
        throw new NotImplementedException();
    }
}