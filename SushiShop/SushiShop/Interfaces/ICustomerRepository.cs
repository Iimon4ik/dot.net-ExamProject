using SushiShop.Models;

namespace SushiShop.Interfaces;

public interface ICustomerRepository
{
    Customer CreateCustomer(string fullName, string address, string phoneNumber, Guid guid);
}