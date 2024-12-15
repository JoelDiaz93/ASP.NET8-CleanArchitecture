using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

internal abstract class Repository<T>
where T : Entity
{
  protected readonly ApplicationDbContext DbContext;

  protected Repository(ApplicationDbContext dbcontext)
  {
    DbContext = dbcontext;
  }

  public async Task<T?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken = default 
  )
  {
    return await DbContext.Set<T>()
    .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
  }

  public void Add(T entity)
  {
    DbContext.Add<T>(entity); 
  }
}