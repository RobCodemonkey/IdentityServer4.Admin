using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Code4.IdentityServer4.Admin.BusinessLogic.Dtos.Grant;
using Code4.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Services.Interfaces
{
    public interface IPersistedGrantService<TDbContext>
        where TDbContext : DbContext, IAdminPersistedGrantDbContext
    {
        Task<PersistedGrantsDto> GetPersitedGrantsByUsers(string search, int page = 1, int pageSize = 10);
        Task<PersistedGrantsDto> GetPersitedGrantsByUser(string subjectId, int page = 1, int pageSize = 10);
        Task<PersistedGrantDto> GetPersitedGrantAsync(string key);
        Task<int> DeletePersistedGrantAsync(string key);
        Task<int> DeletePersistedGrantsAsync(string userId);
    }
}