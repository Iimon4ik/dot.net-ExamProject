namespace SushiShop.Interfaces;

public interface IOrderRepository
{
   public void CreateOrder(Order order);
   public void GetOrderById(Guid id);
   void DeleteOrder(Guid id);
   void AddSushi(string sushi);
   public void SetStatusToInProgress();
   public void SetStatusToForDelivery();
   public void SetStatusToСonfirmed();
   public void SetStatusToDelivered();
}