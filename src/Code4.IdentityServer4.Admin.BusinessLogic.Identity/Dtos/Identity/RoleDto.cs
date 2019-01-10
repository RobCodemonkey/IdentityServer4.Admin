using System.ComponentModel.DataAnnotations;
using Code4.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity
{
    public class RoleDto<TKey> : BaseRoleDto<TKey>
    {      
        [Required]
        public string Name { get; set; }
    }
}