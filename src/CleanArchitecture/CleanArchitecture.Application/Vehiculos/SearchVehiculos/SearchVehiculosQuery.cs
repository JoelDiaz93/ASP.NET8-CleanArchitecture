using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos;

public record SearchVechiulosQuery(
  DateOnly fechaInicio, 
  DateOnly fechaFin
) : IQuery<IReadOnlyList<VehiculoResponse>>;