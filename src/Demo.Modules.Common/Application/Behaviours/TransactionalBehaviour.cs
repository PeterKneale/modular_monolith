using Demo.Modules.Common.Application.Contracts;

namespace Demo.Modules.Common.Application.Behaviours;

public class TransactionalBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionalBehaviour(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        await _unitOfWork.BeginTransactionAsync();
        var response = await next();
        await _unitOfWork.CommitTransactionAsync();
        return response;
    }
}