using Application.Core.Abstractions.Messaging;
using Application.Core.Data.UnitOfWork;
using AutoMapper;
using Domain.Shared;

namespace Application.Features.Product.Commands;
internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
    }
    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productToCreate = _mapper.Map<Domain.Products.Product>(request);
        await _unitOfWork.ProductRepository.AddAsync(productToCreate);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
