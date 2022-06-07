using SushiShop.Models;

namespace SushiShop.Interfaces;

public class CustomerRepository: ICustomerRepository
{
    public Customer CreateCustomer(string fullName, string address, string phoneNumber, int guid)
    {
        var customer = new Customer(fullName, address, phoneNumber, guid);
        CustomerService.customers.Add(customer);
        return customer;
    }
}