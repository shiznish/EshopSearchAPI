using Domain.Common;

namespace Domain.Products;
public class Category : BaseEntity
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}
