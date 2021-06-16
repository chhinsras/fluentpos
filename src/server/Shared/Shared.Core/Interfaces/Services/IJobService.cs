using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IJobService
    {
        string Enqueue(Expression<Func<Task>> methodCall);
    }
}