﻿using Warden.Services.Nancy;

namespace Warden.Services.Organizations.Modules
{
    public abstract class ModuleBase : ApiModuleBase
    {
        protected ModuleBase(string modulePath = "") : base(modulePath)
        {
        }
    }
}