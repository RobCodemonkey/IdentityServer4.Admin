using Microsoft.EntityFrameworkCore;
using Code4.IdentityServer4.Admin.EntityFramework.Entities;

namespace Code4.IdentityServer4.Admin.EntityFramework.Interfaces
{
    public interface IAdminLogDbContext
    {
        DbSet<Log> Logs { get; set; }
    }
}
