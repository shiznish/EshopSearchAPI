namespace Application.Core.Abstractions.Sort.Relevance.Product;
public class ProductSearchService : IProductSearchService
{
    public Task<IEnumerable<Domain.Products.Product>> SearchAndSortProductsAsync(
        IEnumerable<Domain.Products.Product> products,
        string searchTerm)
    {
        // Calculate IDF (Inverse Document Frequency) for the entire collection of products
        double idf = CalculateInverseDocumentFrequency(products, searchTerm);

        var result = products.Select(product =>
                                            {
                                                //// Calculate TF (Term Frequency) and field length score for each product
                                                double tf = CalculateTermFrequency(product, searchTerm);
                                                //double fieldLengthScore = CalculateFieldLengthScore(product);

                                                //// Calculate the relevance score for the product using the pre-calculated IDF
                                                //double relevanceScore = tf * idf * fieldLengthScore;
                                                double relevanceScore = tf;

                                                return new ProductRelevanceScore(product, relevanceScore);
                                            })
                                            //.OrderByDescending(item => item.RelevanceScore)
                                            .Select(item => item.Product)
                                            .ToList();
        return Task.FromResult<IEnumerable<Domain.Products.Product>>(result);
    }
    private double CalculateTermFrequency(Domain.Products.Product product, string searchTerm)
    {
        // Define points for each field
        int namePoints = 3;
        int shortDescriptionPoints = 2;
        int descriptionPoints = 1;

        int termFrequency = CountOccurrences(product.Name, searchTerm) * namePoints +
                            CountOccurrences(product.Description ?? "", searchTerm) * descriptionPoints +
                            CountOccurrences(product.ShortDescription ?? "", searchTerm) * shortDescriptionPoints;

        Console.WriteLine($"Product: {product.Name}, Term Frequency Score: {termFrequency}");

        return termFrequency;
    }

    private double CalculateInverseDocumentFrequency(IEnumerable<Domain.Products.Product> products, string searchTerm)
    {
        // Calculate the inverse document frequency (IDF) for the search term
        int documentFrequency = products.Count(p => (p.Name + p.ShortDescription + p.Description).Contains(searchTerm));
        Console.WriteLine($"documentFrequency: {documentFrequency}");
        return Math.Log((double)products.Count() / (1 + documentFrequency));
    }

    private double CalculateFieldLengthScore(Domain.Products.Product product)
    {
        // Calculate the field length score based on the length of the description field
        double maxDescriptionLength = 2000; // Example maximum description length
        double normalizedDescriptionLength = (product.Description?.Length ?? 0) / maxDescriptionLength;
        return 1 - normalizedDescriptionLength; // Inverse relationship: shorter length -> higher score
    }

    private int CountOccurrences(string text, string searchTerm)
    {
        // Count the number of occurrences of the search term in the text
        int count = 0;
        int index = text.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase);
        while (index != -1)
        {
            count++;
            index = text.IndexOf(searchTerm, index + 1, StringComparison.OrdinalIgnoreCase);
        }
        return count;
    }

    public record ProductRelevanceScore(Domain.Products.Product Product, double RelevanceScore);
}

