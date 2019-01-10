using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Code4.IdentityServer4.Admin.BusinessLogic.Dtos.Log;
using Code4.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Services.Interfaces
{
    public interface ILogService<TDbContext> where TDbContext : DbContext, IAdminLogDbContext
    {
        Task<LogsDto> GetLogsAsync(string search, int page = 1, int pageSize = 10);
    }
}