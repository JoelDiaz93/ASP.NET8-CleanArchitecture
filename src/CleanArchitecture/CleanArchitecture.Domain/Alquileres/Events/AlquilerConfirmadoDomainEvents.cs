using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerConfirmadoDomainEvents(Guid AlquilerId) : IDomainEvent;