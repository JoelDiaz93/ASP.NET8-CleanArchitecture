namespace CleanArchitecture.Domain.Vehiculos;

public record Direccion 
(
  string Pais,
  string Provincia,
  string Ciudad,
  string Calle,
  string Numero,
  string Referencia
);