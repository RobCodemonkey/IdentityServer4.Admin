using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Code4.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Code4.IdentityServer4.Admin.BusinessLogic.Identity.Services.Interfaces;
using Code4IdentityServer4Admin.Admin.ExceptionHandling;
using Code4IdentityServer4Admin.Admin.Configuration.Constants;
using Code4.IdentityServer4.Admin.EntityFramework.DbContexts;
using Code4.IdentityServer4.Admin.EntityFramework.Identity.Entities.Identity;

namespace Code4IdentityServer4Admin.Admin.Controllers
{
    [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    [TypeFilter(typeof(ControllerExceptionFilterAttribute))]
    public class IdentityController : BaseIdentityController<AdminDbContext, UserDto<int>, int, RoleDto<int>, int, int, int, UserIdentity, UserIdentityRole, int, UserIdentityUserClaim, UserIdentityUserRole, UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken>
    {
        public IdentityController(IIdentityService<AdminDbContext, UserDto<int>, int, RoleDto<int>, int, int, int, UserIdentity, UserIdentityRole, int, UserIdentityUserClaim, UserIdentityUserRole, UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken> identityService, ILogger<ConfigurationController> logger, IStringLocalizer<IdentityController> localizer)
            : base(identityService, logger, localizer)
        {
        }
    }
}