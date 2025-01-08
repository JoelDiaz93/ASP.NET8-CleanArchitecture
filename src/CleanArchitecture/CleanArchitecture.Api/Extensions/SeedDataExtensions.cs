using Bogus;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Vehiculos;
using Dapper;

namespace CleanArchitecture.Api.Extensions;

public static class SeedDataExtensions
{
  public static void SeedData(this IApplicationBuilder app)
  {
    using var scope = app.ApplicationServices.CreateScope();
    var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

    using var connection = sqlConnectionFactory.CreateConnection();

    var faker = new Faker();

    List<object> vehiculos = new();
    for(var i=0; i<100; i++)
    {
      vehiculos.Add(new
      {
        Id = Guid.NewGuid(),
        Vin = faker.Vehicle.Vin(),
        Marca = faker.Vehicle.Manufacturer(),
        Modelo = faker.Vehicle.Model(),
        Color = faker.Commerce.Color(),
        Pais = faker.Address.Country(),
        Provincia = faker.Address.County(),
        Ciudad = faker.Address.City(),
        Calle = faker.Address.StreetAddress(),
        Numero = faker.Address.StreetSuffix(),
        Referencia = faker.Address.SecondaryAddress(),
        PrecioMonto = faker.Random.Decimal(50, 400),
        PrecioTipoMoneda = "USD",
        PrecioMantenimiento = faker.Random.Decimal(25, 300),
        PrecioMantenimientoTipoMoneda = "USD",
        Accesorios = new List<int>{(int)Accesorio.Wifi, (int)Accesorio.AppleCar},
        FechaUltima = DateTime.MinValue
      });
    }

    const string sql = """
      INSERT INTO public.vehiculos
        (id, marca, modelo, vin, color, direccion_pais, direccion_provincia, direccion_ciudad, direccion_calle, direccion_numero, direccion_referencia, precio_monto, precio_tipo_moneda, mantenimiento_monto, mantenimiento_tipo_moneda, accesorios, fecha_ultimo_alquiler)
        values(@Id, @Marca, @Modelo, @Vin, @Color, @Pais, @Provincia, @Ciudad, @Calle, @Numero, @Referencia, @PrecioMonto, @PrecioTipoMoneda, @PrecioMantenimiento, @PrecioMantenimientoTipoMoneda, @Accesorios, @FechaUltima)
    """;

    connection.Execute(sql, vehiculos);
  }
}