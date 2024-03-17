using Application.Core.Abstractions.Messaging;
using Application.Core.Abstractions.Sort.Relevance.Product;
using Application.Core.Data.UnitOfWork;
using Application.Identity;
using AutoMapper;
using Domain.Customers;
using Domain.Shared;
using System.Linq.Expressions;

namespace Application.Features.Product.Queries;
public record GetAllProductsQueryHandler
    : IQueryHandler<GetAllProductsQuery, PagedList<ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IProductSearchService _productSearchService;
    private readonly ICurrentUser _currentUser;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IProductSearchService productSearchService, ICurrentUser currentUser)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
        this._productSearchService = productSearchService;
        this._currentUser = currentUser;
    }
    public async Task<Result<PagedList<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Products.Product> productsQuery = _unitOfWork.ProductRepository.GetQueryable();

        DateTime timeStamp = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            productsQuery = productsQuery.Where(p =>
                p.Name.Contains(request.SearchTerm) ||
                p.Description.Contains(request.SearchTerm) ||
                p.ShortDescription.Contains(request.SearchTerm));
        }


        if (request.SortOrder?.ToLower() == "popularity")
        {
            productsQuery = productsQuery.OrderByDescending(product => product.Popularity);
        }
        //else if (request.SortOrder?.ToLower() == "relevance")
        //{
        //    // Sort by relevance based on the count of occurrences of the search term

        //    productsQuery = productsQuery.OrderByDescending(p => CountOccurrences(p.Name, request.SearchTerm) +
        //                                                       CountOccurrences(p.Description ?? "", request.SearchTerm) +
        //                                                       CountOccurrences(p.ShortDescription ?? "", request.SearchTerm));

        //}
        else if (request.SortOrder?.ToLower() == "desc")
        {
            productsQuery = productsQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            productsQuery = productsQuery.OrderBy(GetSortProperty(request));
        }


        var productResponsesQuery = productsQuery
            .Select(p => new ProductResponse(
                p.Id,
                p.Name,
                p.ShortDescription,
                p.Price.Currency,
                p.Price.Amount));

        var products = await PagedList<ProductResponse>.CreateAsync(
            productResponsesQuery,
            request.Page,
            request.PageSize);

        var searchhistory = SearchHistory.Create(request.SearchTerm, timeStamp, _currentUser.UserId, request.SortColumn, request.SortOrder);
        await _unitOfWork.GetRepository<Domain.Customers.SearchHistory>().AddAsync(searchhistory);
        await _unitOfWork.SaveChangesAsync();

        return products;
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
    private static Expression<Func<Domain.Products.Product, object>> GetSortProperty(GetAllProductsQuery request) =>
        request.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            "date" => product => product.CreatedDate,
            "amount" => product => product.Price.Amount,
            "popularity" => product => product.Popularity,
            _ => product => product.Id
        };
}

