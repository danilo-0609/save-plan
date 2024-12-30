using ErrorOr;

namespace SavePlan.API.Common;

public interface IRequest<out TResponse> : MediatR.IRequest<TResponse>
    where TResponse : IErrorOr
{
}
