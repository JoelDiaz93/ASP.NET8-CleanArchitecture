using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using Dapper;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos;

internal sealed class SearchVechiulosQueryHandler : IQueryHandler<SearchVechiulosQuery, IReadOnlyList<VehiculoResponse>>
{
  private static readonly int[] ActiveAlquilerStatuses = 
  {
    (int)AlquilerStatus.Reservado,
    (int)AlquilerStatus.Confirmado,
    (int)AlquilerStatus.Completado
  };
  private readonly ISqlConnectionFactory _sqlConnectionFactory;

  public SearchVechiulosQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
  {
    _sqlConnectionFactory = sqlConnectionFactory;
  }

  public async Task<Result<IReadOnlyList<VehiculoResponse>>> Handle(
    SearchVechiulosQuery request,
    CancellationToken cancellationToken
  )
  {
    if(request.fechaInicio > request.fechaFin)
    {
      return new List<VehiculoResponse>();
    }

    using var connection = _sqlConnectionFactory.CreateConnection();
    const string sql = """
      SELECT
        a.id as Id,
        a.modelo as Modelo,
        a.vin as Vin,
        a.precio_monto as PrecioMonto,
        a.precio_tipo_moneda as TipoMoneda,
        a.direccion_pais as Pais,
        a.direccion_provincia as Provincia,
        a.direccion_ciudad as Ciudad,
        a.direccion_calle as Calle,
        a.direccion_numero as Numero,
        a.direccion_referencia as Referencia
      FROM vehiculos AS a
      WHERE NOT EXISTS
      (
        SELECT 1
        FROM alquileres AS b
        WHERE 
          b.vehiculo_id = a.id
          b.duracion_inicio <= @EndDate AND
          b.duracion_final  >= @StartDate AND
          b.status = ANY(@ActiveAlquilerStatuses)
      )
    """;

    var vehiculos = await connection
    .QueryAsync<VehiculoResponse, DireccionResponse, VehiculoResponse>
    (
      sql,
      (vehiculo, direccion) => {
        vehiculo.Direccion = direccion;
        return vehiculo;
      },
      new
      {
        StartDate = request.fechaInicio,
        EndDate = request.fechaFin,
        ActiveAlquilerStatuses
      },
      splitOn: "Pais"
    );

    return vehiculos.ToList();
  }
}