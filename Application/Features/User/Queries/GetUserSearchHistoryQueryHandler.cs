using Application.Core.Abstractions.Messaging;
using Application.Core.Data.UnitOfWork;
using Application.Features.Product.Queries;
using AutoMapper;
using Domain.Customers;
using Domain.Shared;

namespace Application.Features.User.Queries;
public sealed class GetUserSearchHistoryQueryHandler : IQueryHandler<GetUserSearchHistoryQuery, PagedList<SearchHistoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetUserSearchHistoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<PagedList<SearchHistoryDto>>> Handle(GetUserSearchHistoryQuery request, CancellationToken cancellationToken)
    {

        var searchHistoryQuery = _unitOfWork
                          .GetRepository<SearchHistory>()
                          .GetQueryable()
                          .Where(s => s.UserId == request.userId)
                          .OrderByDescending(o => o.Timestamp)
                          .Select(p => new SearchHistoryDto(
                              p.SearchQuery,
                              p.Timestamp,
                              p.SortColumn,
                              p.SortOrder
                           ));
        var searchHistory = await PagedList<SearchHistoryDto>.CreateAsync(
            searchHistoryQuery,
            request.Page,
        request.PageSize);

        return searchHistory;
    }
}
