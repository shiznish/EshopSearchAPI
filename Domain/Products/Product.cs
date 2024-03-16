using Domain.Common;

namespace Domain.Products;
public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? ShortDescription { get; private set; } //200
    public string? Description { get; private set; }
    public int Popularity { get; set; }
    public Money Price { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }

    public void IncrementPupularity()
    {
        Popularity++;
    }
}
