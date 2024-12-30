using ErrorOr;
using MediatR;
using SavePlan.API.Common;
using System.Transactions;

namespace SavePlan.API.Behaviors;

public sealed class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : SavePlan.API.Common.IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            transaction.Complete();

            return response;
        }
    }
}
