using MediatR;
using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Application.
Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}