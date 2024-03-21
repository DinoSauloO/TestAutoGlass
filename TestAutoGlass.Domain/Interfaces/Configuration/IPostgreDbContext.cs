using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using TestAutoGlass.Domain.Entities;

namespace TestAutoGlass.Domain.Interfaces.Configuration
{
    public interface IPostgreDbContext
    {
        DbSet<Products> Products { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
