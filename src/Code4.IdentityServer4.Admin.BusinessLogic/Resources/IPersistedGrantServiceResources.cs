using Code4.IdentityServer4.Admin.BusinessLogic.Helpers;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Resources
{
    public interface IPersistedGrantServiceResources
    {
        ResourceMessage PersistedGrantDoesNotExist();

        ResourceMessage PersistedGrantWithSubjectIdDoesNotExist();
    }
}
