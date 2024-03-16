using Domain.Common;

namespace Domain.Customers;
public class Customer : BaseEntity
{

    public string IdentityGuid { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    public Address Address { get; private set; }
}
