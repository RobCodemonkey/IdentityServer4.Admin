using System.Collections.Generic;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity
{
    public class UserProvidersDto<TUserDtoKey> : UserProviderDto<TUserDtoKey>
    {
        public UserProvidersDto()
        {
            Providers = new List<UserProviderDto<TUserDtoKey>>();
        }

        public string UserName { get; set; }

        public List<UserProviderDto<TUserDtoKey>> Providers { get; set; }
    }
}
