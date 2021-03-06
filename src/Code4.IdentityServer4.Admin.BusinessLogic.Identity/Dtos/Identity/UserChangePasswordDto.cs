﻿using System.ComponentModel.DataAnnotations;
using Code4.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity
{
    public class UserChangePasswordDto<TUserDtoKey> : BaseUserChangePasswordDto<TUserDtoKey>
    {        
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
