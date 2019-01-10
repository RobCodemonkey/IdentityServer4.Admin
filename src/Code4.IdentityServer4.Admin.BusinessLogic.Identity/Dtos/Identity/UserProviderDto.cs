using Code4.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity
{
    public class UserProviderDto<TUserDtoKey> : BaseUserProviderDto<TUserDtoKey>
    {
        public string UserName { get; set; }

        public string ProviderKey { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderDisplayName { get; set; }        
    }
}
