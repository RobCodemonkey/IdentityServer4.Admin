using System.ComponentModel.DataAnnotations;
using Code4.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity
{
    public class UserClaimDto<TUserDtoKey> : BaseUserClaimDto<TUserDtoKey>
    {
        [Required]
        public string ClaimType { get; set; }

        [Required]
        public string ClaimValue { get; set; }
    }
}