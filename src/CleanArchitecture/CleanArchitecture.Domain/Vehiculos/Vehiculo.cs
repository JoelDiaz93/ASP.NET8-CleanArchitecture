using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos;

public sealed class Vehiculo : Entity
{
  public Vehiculo(
    Guid id,
    Marca marca,
    Modelo modelo,
    Vin vin,
    Color color,
    Direccion? direccion,
    Moneda precio,
    Moneda mantenimiento,
    DateTime? fecha,
    List<Accesorio> accesorios
  ) : base(id) {
    Marca = marca;
    Modelo = modelo;
    Vin = vin;
    Color = color;
    Direccion = direccion;
    Precio = precio;
    Mantenimiento = mantenimiento;
    FechaUltimoAlquiler = fecha;
    Accesorios = accesorios;
   }
  public Marca? Marca { get; private set; }
  public Modelo? Modelo { get; private set; }
  public Vin? Vin { get; private set; }
  public Color? Color { get; private set; }
  public Direccion? Direccion { get; private set; }
  public Moneda? Precio { get; private set; }
  public Moneda? Mantenimiento { get; private set; }
  public DateTime? FechaUltimoAlquiler { get; internal set; }
  public List<Accesorio> Accesorios { get; private set; } = new();
}