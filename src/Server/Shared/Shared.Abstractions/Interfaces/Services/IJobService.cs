using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Abstractions.Interfaces.Services
{
    public interface IJobService
    {
        string Enqueue(Expression<Func<Task>> methodCall);
    }
}
