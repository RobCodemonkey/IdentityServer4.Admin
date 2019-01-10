using Code4.IdentityServer4.Admin.BusinessLogic.Identity.Helpers;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Identity.Resources
{
    public interface IPersistedGrantAspNetIdentityServiceResources
    {
        ResourceMessage PersistedGrantDoesNotExist();

        ResourceMessage PersistedGrantWithSubjectIdDoesNotExist();
    }
}
