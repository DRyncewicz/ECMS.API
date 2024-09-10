//using AutoMapper;
//using ecms.Application.Abstractions.Data;
//using ecms.Domain.Entities;
//using MediatR;
//using SharedKernel;

//namespace ecms.Application.Handlers.Commands.CreateProduct;

//public class CreateProductCommandHandler(IApplicationDbContext _applicationDbContext,
//                                         IMapper _mapper) : IRequestHandler<CreateProductCommand, Result<int>>
//{
//    public Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
//    {
//        var productEntity = new ProductEntity();
//        var product = _applicationDbContext.Products.AddAsync(productEntity, cancellationToken);
//    }
//}
