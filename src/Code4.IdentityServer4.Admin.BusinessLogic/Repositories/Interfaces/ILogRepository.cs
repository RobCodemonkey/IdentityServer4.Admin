using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Code4.IdentityServer4.Admin.BusinessLogic.Shared.Dtos.Common;
using Code4.IdentityServer4.Admin.EntityFramework.Entities;
using Code4.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Repositories.Interfaces
{
    public interface ILogRepository<TDbContext> where TDbContext : DbContext, IAdminLogDbContext
    {
        Task<PagedList<Log>> GetLogsAsync(string search, int page = 1, int pageSize = 10);
    }
}