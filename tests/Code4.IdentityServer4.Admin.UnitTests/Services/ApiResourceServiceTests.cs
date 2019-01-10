﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Moq;
using Code4.IdentityServer4.Admin.BusinessLogic.Mappers;
using Code4.IdentityServer4.Admin.BusinessLogic.Repositories;
using Code4.IdentityServer4.Admin.BusinessLogic.Repositories.Interfaces;
using Code4.IdentityServer4.Admin.BusinessLogic.Resources;
using Code4.IdentityServer4.Admin.EntityFramework.DbContexts;
using Code4.IdentityServer4.Admin.BusinessLogic.Services;
using Code4.IdentityServer4.Admin.BusinessLogic.Services.Interfaces;
using Code4.IdentityServer4.Admin.UnitTests.Mocks;
using Xunit;

namespace Code4.IdentityServer4.Admin.UnitTests.Services
{
    public class ApiResourceServiceTests
    {
        public ApiResourceServiceTests()
        {
            var databaseName = Guid.NewGuid().ToString();

            _dbContextOptions = new DbContextOptionsBuilder<AdminDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            _storeOptions = new ConfigurationStoreOptions();
            _operationalStore = new OperationalStoreOptions();
        }

        private readonly DbContextOptions<AdminDbContext> _dbContextOptions;
        private readonly ConfigurationStoreOptions _storeOptions;
        private readonly OperationalStoreOptions _operationalStore;

        private IClientRepository<AdminDbContext> GetClientRepository(AdminDbContext context)
        {
            IClientRepository<AdminDbContext> clientRepository = new ClientRepository<AdminDbContext>(context);

            return clientRepository;
        }

        private IApiResourceRepository<AdminDbContext> GetApiResourceRepository(AdminDbContext context)
        {
            IApiResourceRepository<AdminDbContext> apiResourceRepository = new ApiResourceRepository<AdminDbContext>(context);

            return apiResourceRepository;
        }

        private IClientService<AdminDbContext> GetClientService(IClientRepository<AdminDbContext> repository, IClientServiceResources resources)
        {
            IClientService<AdminDbContext> clientService = new ClientService<AdminDbContext>(repository, resources);

            return clientService;
        }

        private IApiResourceService<AdminDbContext> GetApiResourceService(IApiResourceRepository<AdminDbContext> repository, IApiResourceServiceResources resources, IClientService<AdminDbContext> clientService)
        {
            IApiResourceService<AdminDbContext> apiResourceService = new ApiResourceService<AdminDbContext>(repository, resources, clientService);

            return apiResourceService;
        }
        
        [Fact]
        public async Task AddApiResourceAsync()
        {
            using (var context = new AdminDbContext(_dbContextOptions, _storeOptions, _operationalStore))
            {
                var apiResourceRepository = GetApiResourceRepository(context);
                var clientRepository = GetClientRepository(context);
                
                var localizerApiResourceMock = new Mock<IApiResourceServiceResources>();
                var localizerApiResource = localizerApiResourceMock.Object;

                var localizerClientResourceMock = new Mock<IClientServiceResources>();
                var localizerClientResource = localizerClientResourceMock.Object;

                var clientService = GetClientService(clientRepository, localizerClientResource);
                var apiResourceService = GetApiResourceService(apiResourceRepository, localizerApiResource, clientService);

                //Generate random new api resource
                var apiResourceDto = ApiResourceDtoMock.GenerateRandomApiResource(0);

                await apiResourceService.AddApiResourceAsync(apiResourceDto);

                //Get new api resource
                var apiResource = await context.ApiResources.Where(x => x.Name == apiResourceDto.Name).SingleOrDefaultAsync();

                var newApiResourceDto = await apiResourceService.GetApiResourceAsync(apiResource.Id);

                //Assert new api resource
                apiResourceDto.ShouldBeEquivalentTo(newApiResourceDto, options => options.Excluding(o => o.Id));
            }
        }

        [Fact]
        public async Task GetApiResourceAsync()
        {
            using (var context = new AdminDbContext(_dbContextOptions, _storeOptions, _operationalStore))
            {
                var apiResourceRepository = GetApiResourceRepository(context);
                var clientRepository = GetClientRepository(context);

                var localizerApiResourceMock = new Mock<IApiResourceServiceResources>();
                var localizerApiResource = localizerApiResourceMock.Object;

                var localizerClientResourceMock = new Mock<IClientServiceResources>();
                var localizerClientResource = localizerClientResourceMock.Object;

                var clientService = GetClientService(clientRepository, localizerClientResource);
                var apiResourceService = GetApiResourceService(apiResourceRepository, localizerApiResource, clientService);

                //Generate random new api resource
                var apiResourceDto = ApiResourceDtoMock.GenerateRandomApiResource(0);

                await apiResourceService.AddApiResourceAsync(apiResourceDto);

                //Get new api resource
                var apiResource = await context.ApiResources.Where(x => x.Name == apiResourceDto.Name).SingleOrDefaultAsync();

                var newApiResourceDto = await apiResourceService.GetApiResourceAsync(apiResource.Id);

                //Assert new api resource
                apiResourceDto.ShouldBeEquivalentTo(newApiResourceDto, options => options.Excluding(o => o.Id));
            }
        }

        [Fact]
        public async Task RemoveApiResourceAsync()
        {
            using (var context = new AdminDbContext(_dbContextOptions, _storeOptions, _operationalStore))
            {
                var apiResourceRepository = GetApiResourceRepository(context);
                var clientRepository = GetClientRepository(context);

                var localizerApiResourceMock = new Mock<IApiResourceServiceResources>();
                var localizerApiResource = localizerApiResourceMock.Object;

                var localizerClientResourceMock = new Mock<IClientServiceResources>();
                var localizerClientResource = localizerClientResourceMock.Object;

                var clientService = GetClientService(clientRepository, localizerClientResource);
                var apiResourceService = GetApiResourceService(apiResourceRepository, localizerApiResource, clientService);

                //Generate random new api resource
                var apiResourceDto = ApiResourceDtoMock.GenerateRandomApiResource(0);

                await apiResourceService.AddApiResourceAsync(apiResourceDto);

                //Get new api resource
                var apiResource = await context.ApiResources.Where(x => x.Name == apiResourceDto.Name).SingleOrDefaultAsync();

                var newApiResourceDto = await apiResourceService.GetApiResourceAsync(apiResource.Id);

                //Assert new api resource
                apiResourceDto.ShouldBeEquivalentTo(newApiResourceDto, options => options.Excluding(o => o.Id));

                //Remove api resource
                await apiResourceService.DeleteApiResourceAsync(newApiResourceDto);

                //Try get removed api resource
                var removeApiResource = await context.ApiResources.Where(x => x.Id == apiResource.Id)
                    .SingleOrDefaultAsync();

                //Assert removed api resource
                removeApiResource.Should().BeNull();
            }
        }

        [Fact]
        public async Task UpdateApiResourceAsync()
        {
            using (var context = new AdminDbContext(_dbContextOptions, _storeOptions, _operationalStore))
            {
                var apiResourceRepository = GetApiResourceRepository(context);
                var clientRepository = GetClientRepository(context);

                var localizerApiResourceMock = new Mock<IApiResourceServiceResources>();
                var localizerApiResource = localizerApiResourceMock.Object;

                var localizerClientResourceMock = new Mock<IClientServiceResources>();
                var localizerClientResource = localizerClientResourceMock.Object;

                var clientService = GetClientService(clientRepository, localizerClientResource);
                var apiResourceService = GetApiResourceService(apiResourceRepository, localizerApiResource, clientService);

                //Generate random new api resource
                var apiResourceDto = ApiResourceDtoMock.GenerateRandomApiResource(0);

                await apiResourceService.AddApiResourceAsync(apiResourceDto);

                //Get new api resource
                var apiResource = await context.ApiResources.Where(x => x.Name == apiResourceDto.Name).SingleOrDefaultAsync();

                var newApiResourceDto = await apiResourceService.GetApiResourceAsync(apiResource.Id);

                //Assert new api resource
                apiResourceDto.ShouldBeEquivalentTo(newApiResourceDto, options => options.Excluding(o => o.Id));

                //Detached the added item
                context.Entry(apiResource).State = EntityState.Detached;

                //Generete new api resuorce with added item id
                var updatedApiResource = ApiResourceDtoMock.GenerateRandomApiResource(apiResource.Id);

                //Update api resource
                await apiResourceService.UpdateApiResourceAsync(updatedApiResource);
                
                var updatedApiResourceDto = await apiResourceService.GetApiResourceAsync(apiResource.Id);

                //Assert updated api resuorce
                updatedApiResource.ShouldBeEquivalentTo(updatedApiResourceDto, options => options.Excluding(o => o.Id));
            }
        }

        [Fact]
        public async Task AddApiScopeAsync()
        {
            using (var context = new AdminDbContext(_dbContextOptions, _storeOptions, _operationalStore))
            {
                var apiResourceRepository = GetApiResourceRepository(context);
                var clientRepository = GetClientRepository(context);

                var localizerApiResourceMock = new Mock<IApiResourceServiceResources>();
                var localizerApiResource = localizerApiResourceMock.Object;

                var localizerClientResourceMock = new Mock<IClientServiceResources>();
                var localizerClientResource = localizerClientResourceMock.Object;

                var clientService = GetClientService(clientRepository, localizerClientResource);
                var apiResourceService = GetApiResourceService(apiResourceRepository, localizerApiResource, clientService);

                //Generate random new api resource
                var apiResourceDto = ApiResourceDtoMock.GenerateRandomApiResource(0);

                await apiResourceService.AddApiResourceAsync(apiResourceDto);

                //Get new api resource
                var apiResource = await context.ApiResources.Where(x => x.Name == apiResourceDto.Name).SingleOrDefaultAsync();

                var newApiResourceDto = await apiResourceService.GetApiResourceAsync(apiResource.Id);

                //Assert new api resource
                apiResourceDto.ShouldBeEquivalentTo(newApiResourceDto, options => options.Excluding(o => o.Id));

                //Generate random new api scope
                var apiScopeDtoMock = ApiResourceDtoMock.GenerateRandomApiScope(0, newApiResourceDto.Id);

                //Add new api scope
                await apiResourceService.AddApiScopeAsync(apiScopeDtoMock);

                //Get inserted api scope
                var apiScope = await context.ApiScopes.Where(x => x.Name == apiScopeDtoMock.Name && x.ApiResource.Id == newApiResourceDto.Id)
                    .SingleOrDefaultAsync();

                //Map entity to model
                var apiScopesDto = apiScope.ToModel();

                //Get new api scope
                var newApiScope = await apiResourceService.GetApiScopeAsync(apiScopesDto.ApiResourceId, apiScopesDto.ApiScopeId);

                //Assert
                newApiScope.ShouldBeEquivalentTo(apiScopesDto, o => o.Excluding(x => x.ResourceName));
            }
        }

        [Fact]
        public async Task GetApiScopeAsync()
        {
            using (var context = new AdminDbContext(_dbContextOptions, _storeOptions, _operationalStore))
            {
                var apiResourceRepository = GetApiResourceRepository(context);
                var clientRepository = GetClientRepository(context);

                var localizerApiResourceMock = new Mock<IApiResourceServiceResources>();
                var localizerApiResource = localizerApiResourceMock.Object;

                var localizerClientResourceMock = new Mock<IClientServiceResources>();
                var localizerClientResource = localizerClientResourceMock.Object;

                var clientService = GetClientService(clientRepository, localizerClientResource);
                var apiResourceService = GetApiResourceService(apiResourceRepository, localizerApiResource, clientService);

                //Generate random new api resource
                var apiResourceDto = ApiResourceDtoMock.GenerateRandomApiResource(0);

                await apiResourceService.AddApiResourceAsync(apiResourceDto);

                //Get new api resource
                var apiResource = await context.ApiResources.Where(x => x.Name == apiResourceDto.Name).SingleOrDefaultAsync();

                var newApiResourceDto = await apiResourceService.GetApiResourceAsync(apiResource.Id);

                //Assert new api resource
                apiResourceDto.ShouldBeEquivalentTo(newApiResourceDto, options => options.Excluding(o => o.Id));

                //Generate random new api scope
                var apiScopeDtoMock = ApiResourceDtoMock.GenerateRandomApiScope(0, newApiResourceDto.Id);

                //Add new api scope
                await apiResourceService.AddApiScopeAsync(apiScopeDtoMock);

                //Get inserted api scope
                var apiScope = await context.ApiScopes.Where(x => x.Name == apiScopeDtoMock.Name && x.ApiResource.Id == newApiResourceDto.Id)
                    .SingleOrDefaultAsync();

                //Map entity to model
                var apiScopesDto = apiScope.ToModel();

                //Get new api scope
                var newApiScope = await apiResourceService.GetApiScopeAsync(apiScopesDto.ApiResourceId, apiScopesDto.ApiScopeId);

                //Assert
                newApiScope.ShouldBeEquivalentTo(apiScopesDto, o => o.Excluding(x => x.ResourceName));
            }
        }

        [Fact]
        public async Task UpdateApiScopeAsync()
        {
            using (var context = new AdminDbContext(_dbContextOptions, _storeOptions, _operationalStore))
            {
                var apiResourceRepository = GetApiResourceRepository(context);
                var clientRepository = GetClientRepository(context);

                var localizerApiResourceMock = new Mock<IApiResourceServiceResources>();
                var localizerApiResource = localizerApiResourceMock.Object;

                var localizerClientResourceMock = new Mock<IClientServiceResources>();
                var localizerClientResource = localizerClientResourceMock.Object;

                var clientService = GetClientService(clientRepository, localizerClientResource);
                var apiResourceService = GetApiResourceService(apiResourceRepository, localizerApiResource, clientService);

                //Generate random new api resource
                var apiResourceDto = ApiResourceDtoMock.GenerateRandomApiResource(0);

                await apiResourceService.AddApiResourceAsync(apiResourceDto);

                //Get new api resource
                var apiResource = await context.ApiResources.Where(x => x.Name == apiResourceDto.Name).SingleOrDefaultAsync();

                var newApiResourceDto = await apiResourceService.GetApiResourceAsync(apiResource.Id);

                //Assert new api resource
                apiResourceDto.ShouldBeEquivalentTo(newApiResourceDto, options => options.Excluding(o => o.Id));

                //Generate random new api scope
                var apiScopeDtoMock = ApiResourceDtoMock.GenerateRandomApiScope(0, newApiResourceDto.Id);

                //Add new api scope
                await apiResourceService.AddApiScopeAsync(apiScopeDtoMock);

                //Get inserted api scope
                var apiScope = await context.ApiScopes.Where(x => x.Name == apiScopeDtoMock.Name && x.ApiResource.Id == newApiResourceDto.Id)
                    .SingleOrDefaultAsync();
                
                //Map entity to model
                var apiScopesDto = apiScope.ToModel();

                //Get new api scope
                var newApiScope = await apiResourceService.GetApiScopeAsync(apiScopesDto.ApiResourceId, apiScopesDto.ApiScopeId);

                //Assert
                newApiScope.ShouldBeEquivalentTo(apiScopesDto, o => o.Excluding(x => x.ResourceName));

                //Detached the added item
                context.Entry(apiScope).State = EntityState.Detached;

                //Update api scope
                var updatedApiScope = ApiResourceDtoMock.GenerateRandomApiScope(apiScopesDto.ApiScopeId, apiScopesDto.ApiResourceId);

                await apiResourceService.UpdateApiScopeAsync(updatedApiScope);
                
                var updatedApiScopeDto = await apiResourceService.GetApiScopeAsync(apiScopesDto.ApiResourceId, apiScopesDto.ApiScopeId);

                //Assert updated api scope
                updatedApiScope.ShouldBeEquivalentTo(updatedApiScopeDto, o => o.Excluding(x => x.ResourceName));
            }
        }

        [Fact]
        public async Task DeleteApiScopeAsync()
        {
            using (var context = new AdminDbContext(_dbContextOptions, _storeOptions, _operationalStore))
            {
                var apiResourceRepository = GetApiResourceRepository(context);
                var clientRepository = GetClientRepository(context);

                var localizerApiResourceMock = new Mock<IApiResourceServiceResources>();
                var localizerApiResource = localizerApiResourceMock.Object;

                var localizerClientResourceMock = new Mock<IClientServiceResources>();
                var localizerClientResource = localizerClientResourceMock.Object;

                var clientService = GetClientService(clientRepository, localizerClientResource);
                var apiResourceService = GetApiResourceService(apiResourceRepository, localizerApiResource, clientService);

                //Generate random new api resource
                var apiResourceDto = ApiResourceDtoMock.GenerateRandomApiResource(0);

                await apiResourceService.AddApiResourceAsync(apiResourceDto);

                //Get new api resource
                var apiResource = await context.ApiResources.Where(x => x.Name == apiResourceDto.Name).SingleOrDefaultAsync();

                var newApiResourceDto = await apiResourceService.GetApiResourceAsync(apiResource.Id);

                //Assert new api resource
                apiResourceDto.ShouldBeEquivalentTo(newApiResourceDto, options => options.Excluding(o => o.Id));

                //Generate random new api scope
                var apiScopeDtoMock = ApiResourceDtoMock.GenerateRandomApiScope(0, newApiResourceDto.Id);

                //Add new api scope
                await apiResourceService.AddApiScopeAsync(apiScopeDtoMock);

                //Get inserted api scope
                var apiScope = await context.ApiScopes.Where(x => x.Name == apiScopeDtoMock.Name && x.ApiResource.Id == newApiResourceDto.Id)
                    .SingleOrDefaultAsync();

                //Map entity to model
                var apiScopesDto = apiScope.ToModel();

                //Get new api scope
                var newApiScope = await apiResourceService.GetApiScopeAsync(apiScopesDto.ApiResourceId, apiScopesDto.ApiScopeId);

                //Assert
                newApiScope.ShouldBeEquivalentTo(apiScopesDto, o => o.Excluding(x => x.ResourceName));

                //Delete it
                await apiResourceService.DeleteApiScopeAsync(newApiScope);

                var deletedApiScope = await context.ApiScopes.Where(x => x.Name == apiScopeDtoMock.Name && x.ApiResource.Id == newApiResourceDto.Id)
                    .SingleOrDefaultAsync();

                //Assert after deleting
                deletedApiScope.Should().BeNull();
            }
        }

        [Fact]
        public async Task AddApiSecretAsync()
        {
            using (var context = new AdminDbContext(_dbContextOptions, _storeOptions, _operationalStore))
            {
                var apiResourceRepository = GetApiResourceRepository(context);
                var clientRepository = GetClientRepository(context);

                var localizerApiResourceMock = new Mock<IApiResourceServiceResources>();
                var localizerApiResource = localizerApiResourceMock.Object;

                var localizerClientResourceMock = new Mock<IClientServiceResources>();
                var localizerClientResource = localizerClientResourceMock.Object;

                var clientService = GetClientService(clientRepository, localizerClientResource);
                var apiResourceService = GetApiResourceService(apiResourceRepository, localizerApiResource, clientService);

                //Generate random new api resource
                var apiResourceDto = ApiResourceDtoMock.GenerateRandomApiResource(0);

                await apiResourceService.AddApiResourceAsync(apiResourceDto);

                //Get new api resource
                var apiResource = await context.ApiResources.Where(x => x.Name == apiResourceDto.Name).SingleOrDefaultAsync();

                var newApiResourceDto = await apiResourceService.GetApiResourceAsync(apiResource.Id);

                //Assert new api resource
                apiResourceDto.ShouldBeEquivalentTo(newApiResourceDto, options => options.Excluding(o => o.Id));

                //Generate random new api secret
                var apiSecretsDto = ApiResourceDtoMock.GenerateRandomApiSecret(0, newApiResourceDto.Id);

                //Add new api secret
                await apiResourceService.AddApiSecretAsync(apiSecretsDto);

                //Get inserted api secret
                var apiSecret = await context.ApiSecrets.Where(x => x.Value == apiSecretsDto.Value && x.ApiResource.Id == newApiResourceDto.Id)
                    .SingleOrDefaultAsync();

                //Map entity to model
                var secretsDto = apiSecret.ToModel();

                //Get new api secret    
                var newApiSecret = await apiResourceService.GetApiSecretAsync(secretsDto.ApiSecretId);

                //Assert
                newApiSecret.ShouldBeEquivalentTo(secretsDto, o => o.Excluding(x => x.ApiResourceName));
            }
        }

        [Fact]
        public async Task DeleteApiSecretAsync()
        {
            using (var context = new AdminDbContext(_dbContextOptions, _storeOptions, _operationalStore))
            {
                var apiResourceRepository = GetApiResourceRepository(context);
                var clientRepository = GetClientRepository(context);

                var localizerApiResourceMock = new Mock<IApiResourceServiceResources>();
                var localizerApiResource = localizerApiResourceMock.Object;

                var localizerClientResourceMock = new Mock<IClientServiceResources>();
                var localizerClientResource = localizerClientResourceMock.Object;

                var clientService = GetClientService(clientRepository, localizerClientResource);
                var apiResourceService = GetApiResourceService(apiResourceRepository, localizerApiResource, clientService);

                //Generate random new api resource
                var apiResourceDto = ApiResourceDtoMock.GenerateRandomApiResource(0);

                await apiResourceService.AddApiResourceAsync(apiResourceDto);

                //Get new api resource
                var apiResource = await context.ApiResources.Where(x => x.Name == apiResourceDto.Name).SingleOrDefaultAsync();

                var newApiResourceDto = await apiResourceService.GetApiResourceAsync(apiResource.Id);

                //Assert new api resource
                apiResourceDto.ShouldBeEquivalentTo(newApiResourceDto, options => options.Excluding(o => o.Id));

                //Generate random new api secret
                var apiSecretsDtoMock = ApiResourceDtoMock.GenerateRandomApiSecret(0, newApiResourceDto.Id);

                //Add new api secret
                await apiResourceService.AddApiSecretAsync(apiSecretsDtoMock);

                //Get inserted api secret
                var apiSecret = await context.ApiSecrets.Where(x => x.Value == apiSecretsDtoMock.Value && x.ApiResource.Id == newApiResourceDto.Id)
                    .SingleOrDefaultAsync();

                //Map entity to model
                var apiSecretsDto = apiSecret.ToModel();

                //Get new api secret    
                var newApiSecret = await apiResourceService.GetApiSecretAsync(apiSecretsDto.ApiSecretId);

                //Assert
                newApiSecret.ShouldBeEquivalentTo(apiSecretsDto, o => o.Excluding(x => x.ApiResourceName));

                //Delete it
                await apiResourceService.DeleteApiSecretAsync(newApiSecret);

                var deletedApiSecret = await context.ApiSecrets.Where(x => x.Value == apiSecretsDtoMock.Value && x.ApiResource.Id == newApiResourceDto.Id)
                    .SingleOrDefaultAsync();

                //Assert after deleting
                deletedApiSecret.Should().BeNull();
            }
        }
    }
}