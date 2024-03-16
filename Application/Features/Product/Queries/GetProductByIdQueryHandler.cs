using Application.Core.Abstractions.Messaging;
using Application.Core.Data.UnitOfWork;
using AutoMapper;
using Domain.Shared;

namespace Application.Features.Product.Queries;
public record GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
    }
    public async Task<Result<ProductDetailsDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _unitOfWork
           .ProductRepository
           .GetAsync(request.Id);

            if (product is null)
            {
                return Result.Failure<ProductDetailsDto>(new Error(
                    "Product.NotFound",
                    $"The product with Id {request.Id} was not found"));
            }

            product.IncrementPupularity();
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<ProductDetailsDto>(product);

            return response;
        }
        catch (Exception ex)
        {

            throw;
        }



    }
}
