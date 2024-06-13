using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace john.cleansample.Infrastructure.Data;

public static class AppDbContextExtensions
{
  public static void AddApplicationDbContext(this IServiceCollection services, string connectionString)
  {
    services.AddDbContext<AppDbContext>(options =>
         options.UseSqlite(connectionString));
  }
}
