using SushiShop.Models;

namespace SushiShop.Interfaces;

public interface IOrderRepository
{
   public Order CreateOrder();
   public void GetOrderById(int id);
   void DeleteOrder(int id);
   public void SetStatusToInProgress(Order order);
   public void SetStatusToForDelivery(Order order);
   public void SetStatusToСonfirmed(Order order);
   public void SetStatusToDelivered(Order order);
}