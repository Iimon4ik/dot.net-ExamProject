namespace SushiShop.Interfaces;

public interface IOrderRepository
{
   public Order CreateOrder();
   public void GetOrderById(Guid id);
   void DeleteOrder(Guid id);
   void AddSushi(string sushi);
   public void SetStatusToInProgress(Order order);
   public void SetStatusToForDelivery(Order order);
   public void SetStatusToСonfirmed(Order order);
   public void SetStatusToDelivered(Order order);
}