using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerRechazadoDomainEvents(Guid Id) : IDomainEvent;