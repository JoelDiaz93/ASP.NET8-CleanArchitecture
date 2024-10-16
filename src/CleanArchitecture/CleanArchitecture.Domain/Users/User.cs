using System.Security.Cryptography.X509Certificates;
using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users;

public sealed class User : Entity
{
  private User(
    Guid id,
    Nombre nombre, 
    Apellido apellido, 
    Email email
  ) : base(id)
  {
    Nombre = nombre;
    Apellido = apellido;
    Email = email;
  }
  public Nombre? Nombre { get; private set; }
  public Apellido? Apellido { get; private set; }
  public Email? Email { get; private set; }

  //Encapsulamos el metodo de creacion de ojetos de mi clase con el constructor como privado y con un metodo estatico publico que se haga cargo en la creacion de los objetos de esta clase
  public static User Create(
    Nombre nombre, 
    Apellido apellido, 
    Email email
  )
  {
    var user = new User(Guid.NewGuid(), nombre, apellido,email);
    return user;
  }
}