using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Code4.IdentityServer4.Admin.BusinessLogic.Dtos.Configuration;
using Code4.IdentityServer4.Admin.BusinessLogic.Helpers;
using Code4.IdentityServer4.Admin.BusinessLogic.Mappers;
using Code4.IdentityServer4.Admin.BusinessLogic.Repositories.Interfaces;
using Code4.IdentityServer4.Admin.BusinessLogic.Resources;
using Code4.IdentityServer4.Admin.BusinessLogic.Services.Interfaces;
using Code4.IdentityServer4.Admin.BusinessLogic.Shared.ExceptionHandling;
using Code4.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Services
{
    public class IdentityResourceService<TDbContext> : IIdentityResourceService<TDbContext>
        where TDbContext : DbContext, IAdminConfigurationDbContext
    {
        private readonly IIdentityResourceRepository<TDbContext> _identityResourceRepository;
        private readonly IIdentityResourceServiceResources _identityResourceServiceResources;

        public IdentityResourceService(IIdentityResourceRepository<TDbContext> identityResourceRepository,
            IIdentityResourceServiceResources identityResourceServiceResources)
        {
            _identityResourceRepository = identityResourceRepository;
            _identityResourceServiceResources = identityResourceServiceResources;
        }

        public async Task<IdentityResourcesDto> GetIdentityResourcesAsync(string search, int page = 1, int pageSize = 10)
        {
            var pagedList = await _identityResourceRepository.GetIdentityResourcesAsync(search, page, pageSize);
            var identityResourcesDto = pagedList.ToModel();

            return identityResourcesDto;
        }

        public async Task<IdentityResourceDto> GetIdentityResourceAsync(int identityResourceId)
        {
            var identityResource = await _identityResourceRepository.GetIdentityResourceAsync(identityResourceId);
            if (identityResource == null) throw new UserFriendlyErrorPageException(string.Format(_identityResourceServiceResources.IdentityResourceDoesNotExist().Description, identityResourceId));

            var identityResourceDto = identityResource.ToModel();

            return identityResourceDto;
        }

        public async Task<bool> CanInsertIdentityResourceAsync(IdentityResourceDto identityResource)
        {
            var resource = identityResource.ToEntity();

            return await _identityResourceRepository.CanInsertIdentityResourceAsync(resource);
        }

        public async Task<int> AddIdentityResourceAsync(IdentityResourceDto identityResource)
        {
            var canInsert = await CanInsertIdentityResourceAsync(identityResource);
            if (!canInsert)
            {
                throw new UserFriendlyViewException(string.Format(_identityResourceServiceResources.IdentityResourceExistsValue().Description, identityResource.Name), _identityResourceServiceResources.IdentityResourceExistsKey().Description, identityResource);
            }

            var resource = identityResource.ToEntity();

            return await _identityResourceRepository.AddIdentityResourceAsync(resource);
        }

        public async Task<int> UpdateIdentityResourceAsync(IdentityResourceDto identityResource)
        {
            var canInsert = await CanInsertIdentityResourceAsync(identityResource);
            if (!canInsert)
            {
                throw new UserFriendlyViewException(string.Format(_identityResourceServiceResources.IdentityResourceExistsValue().Description, identityResource.Name), _identityResourceServiceResources.IdentityResourceExistsKey().Description, identityResource);
            }

            var resource = identityResource.ToEntity();

            return await _identityResourceRepository.UpdateIdentityResourceAsync(resource);
        }

        public async Task<int> DeleteIdentityResourceAsync(IdentityResourceDto identityResource)
        {
            var resource = identityResource.ToEntity();

            return await _identityResourceRepository.DeleteIdentityResourceAsync(resource);
        }

        public IdentityResourceDto BuildIdentityResourceViewModel(IdentityResourceDto identityResource)
        {
            ComboBoxHelpers.PopulateValuesToList(identityResource.UserClaimsItems, identityResource.UserClaims);

            return identityResource;
        }
    }
}
