namespace Application.Dto.Order;
public class OrderDto
{

    public Guid Id { get; set; }
    public string InvoiceNo { get; set; }
    public Guid CustomerId { get; set; }
    public decimal OrderAmount { get; set; }

    public string ShippingAddress { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public bool IsNew
    {
        get
        {
            if (CreatedDate != null)
            {
                TimeSpan difference = (TimeSpan)(DateTime.UtcNow - CreatedDate);
                return difference.TotalHours <= 24;
            }
            return false;
        }
    }
}