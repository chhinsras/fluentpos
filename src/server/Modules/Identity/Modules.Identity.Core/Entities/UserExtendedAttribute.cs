using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Identity.Core.Entities
{
    public class UserExtendedAttribute : ExtendedAttribute<string, FluentUser>
    {
        protected override Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }
    }
}