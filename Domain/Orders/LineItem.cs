using Domain.Common;
using Domain.Products;

namespace Domain.Orders;
public class LineItem : BaseEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public Money Price { get; private set; }
}
