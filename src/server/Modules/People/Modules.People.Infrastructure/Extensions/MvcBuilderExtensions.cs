using System.Reflection;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.People.Infrastructure.Extensions
{
    internal static class MvcBuilderExtensions
    {
        internal static IMvcBuilder AddPeopleValidation(this IMvcBuilder builder)
        {
            return builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(IPeopleDbContext))));
        }
    }
}