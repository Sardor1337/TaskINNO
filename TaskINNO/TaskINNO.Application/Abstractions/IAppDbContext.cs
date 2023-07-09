using Microsoft.EntityFrameworkCore;

using TaskINNO.Domain.Entities;

namespace TaskINNO.Application.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
