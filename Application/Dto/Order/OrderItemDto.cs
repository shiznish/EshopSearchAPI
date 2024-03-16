﻿namespace Application.Dto.Order;
internal class OrderItemDto
{
    public int OrderId { get; set; }
    public string Item { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Total => UnitPrice * Quantity;
}
