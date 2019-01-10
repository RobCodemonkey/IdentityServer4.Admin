using System;
using System.Collections.Generic;
using System.Text;
using Code4.IdentityServer4.Admin.BusinessLogic.Helpers;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Resources
{
    public interface IIdentityResourceServiceResources
    {
        ResourceMessage IdentityResourceDoesNotExist();

        ResourceMessage IdentityResourceExistsKey();

        ResourceMessage IdentityResourceExistsValue();
    }
}
