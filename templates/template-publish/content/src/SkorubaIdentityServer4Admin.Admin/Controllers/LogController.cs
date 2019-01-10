using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Code4.IdentityServer4.Admin.BusinessLogic.Services.Interfaces;
using Code4IdentityServer4Admin.Admin.Configuration.Constants;
using Code4.IdentityServer4.Admin.EntityFramework.DbContexts;

namespace Code4IdentityServer4Admin.Admin.Controllers
{
    [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    public class LogController : BaseController
    {
        private readonly ILogService<AdminDbContext> _logService;

        public LogController(ILogService<AdminDbContext> logService,
            ILogger<ConfigurationController> logger) : base(logger)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> ErrorsLog(int? page, string search)
        {
            ViewBag.Search = search;
            var logs = await _logService.GetLogsAsync(search, page ?? 1);

            return View(logs);
        }
    }
}