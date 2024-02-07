using ErrorOr;
using MediatR;

namespace CarCareAlliance.Application.Common.CQRS
{
    public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
    {
    }
}
