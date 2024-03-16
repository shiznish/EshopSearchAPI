using Domain.Common;
using Domain.Customers;
using Domain.Products;

namespace Domain.Orders;
public class Order : BaseEntity
{
    private readonly List<LineItem> _lineItems = new();

    public string InvoiceNo { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Money OrderAmount { get; set; }
    public Address ShippingAddress { get; set; }
    public IReadOnlyList<LineItem> LineItems => _lineItems.ToList();


    //public static Order Create(Guid customerId)
    //{
    //    var order = new Order
    //    {
    //        Id = new Guid.NewGuid(),
    //        CustomerId = customerId
    //    };

    //    return order;
    //}

    //public void Add(Guid productId, Money price)
    //{
    //    var lineItem = new LineItem(
    //        new LineItemId(Guid.NewGuid()),
    //        Id,
    //        productId,
    //        price);

    //    _lineItems.Add(lineItem);
    //}

    //public void RemoveLineItem(Guid lineItemId, IOrderRepository orderRepository)
    //{
    //    if (orderRepository.HasOneLineItem(this))
    //    {
    //        return;
    //    }

    //    var lineItem = _lineItems.FirstOrDefault(li => li.Id == lineItemId);

    //    if (lineItem is null)
    //    {
    //        return;
    //    }

    //    _lineItems.Remove(lineItem);

    //    // Raise(new LineItemRemovedDomainEvent(Guid.NewGuid(), Id, lineItem.Id));
    //}

}
