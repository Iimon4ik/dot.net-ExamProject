using SushiShop.Interfaces;

namespace SushiShop.Repositorys;

public class OrderRepository: IOrderRepository 
{
    public void CreateOrder(Order order)
    {
        OrderService.orders.Add(order);
    }

    public void GetOrderById(Guid id)
    {
        foreach (Order order in OrderService.orders)
        {
            if(order.Id.Equals(id))
            {
                Console.WriteLine($"Order ID: {order.Id} - status - {order.Status}");
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
                Console.WriteLine($"Your Order [ id : {OrderService.orders[i].Id} was removed!");
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