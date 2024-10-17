using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews.Events;

public static class ReviewErrors
{
  public static readonly Error NotEligible = new(
    "Review.NotEligible",
    "Este review y calificacion para el auto no es eligible porque aun no se completa el alquiler"
  );
}