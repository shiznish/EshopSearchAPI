using Application.Core.Abstractions.Messaging;
using Application.Core.Data.UnitOfWork;
using AutoMapper;
using Domain.Shared;

namespace Application.Features.Category.Commands;
internal sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var categoryToCreate = _mapper.Map<Domain.Products.Category>(request);
            await _unitOfWork.GetRepository<Domain.Products.Category>().AddAsync(categoryToCreate);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception)
        {

            throw;
        }

    }
}
