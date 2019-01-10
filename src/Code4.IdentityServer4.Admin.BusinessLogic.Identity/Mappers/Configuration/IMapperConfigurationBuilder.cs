using System;
using Microsoft.AspNetCore.Identity;
using Code4.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Identity.Mappers.Configuration
{
    public interface IMapperConfigurationBuilder
    {
        IMapperConfigurationBuilder UseIdentityMappingProfile<TUserDto, TUserDtoKey, TRoleDto, TRoleDtoKey, TUser, TRole,
            TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>()
            where TUserDto : UserDto<TUserDtoKey>
            where TRoleDto : RoleDto<TRoleDtoKey>
            where TUser : IdentityUser<TKey>
            where TRole : IdentityRole<TKey>
            where TKey : IEquatable<TKey>
            where TUserClaim : IdentityUserClaim<TKey>
            where TUserRole : IdentityUserRole<TKey>
            where TUserLogin : IdentityUserLogin<TKey>
            where TRoleClaim : IdentityRoleClaim<TKey>
            where TUserToken : IdentityUserToken<TKey>;
    }
}
