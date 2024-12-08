using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

public record ReservarAlquilerCommand(
  Guid vehiculoId,
  Guid userId,
  DateOnly FechaInicio,
  DateOnly FechaFin
) : ICommand<Guid>;