using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Code4.IdentityServer4.Admin.BusinessLogic.Dtos.Log;
using Code4.IdentityServer4.Admin.BusinessLogic.Mappers;
using Code4.IdentityServer4.Admin.BusinessLogic.Repositories.Interfaces;
using Code4.IdentityServer4.Admin.BusinessLogic.Services.Interfaces;
using Code4.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Services
{
    public class LogService<TDbContext> : ILogService<TDbContext>
        where TDbContext : DbContext, IAdminLogDbContext
    {
        private readonly ILogRepository<TDbContext> _repository;

        public LogService(ILogRepository<TDbContext> repository)
        {
            _repository = repository;
        }

        public async Task<LogsDto> GetLogsAsync(string search, int page = 1, int pageSize = 10)
        {
            var pagedList = await _repository.GetLogsAsync(search, page, pageSize);
            var logs = pagedList.ToModel();

            return logs;
        }
    }
}
