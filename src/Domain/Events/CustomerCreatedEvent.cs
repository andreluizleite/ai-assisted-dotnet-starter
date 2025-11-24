using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Events;

public class CustomerCreatedEvent
{
    public Customer Customer { get; }
    public CustomerCreatedEvent(Customer customer) => Customer = customer;
}
