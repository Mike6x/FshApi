using System.Linq.Expressions;

namespace FSH.WebApi.Application.Common.Interfaces;

public interface IJobService : ITransientService
{
    string Enqueue(IRequest request);
    string AddOrUpdate(string jobId, IRequest request, string recurring, DateTime schedule);

    bool Delete(string jobId);
    bool Delete(string jobId, string fromState);

    bool Requeue(string jobId);
    bool Requeue(string jobId, string fromState);

    string Enqueue(Expression<Action> methodCall);
    string Enqueue(Expression<Func<Task>> methodCall);
    string Enqueue<T>(Expression<Action<T>> methodCall);
    string Enqueue<T>(Expression<Func<T, Task>> methodCall);

    string Schedule(Expression<Action> methodCall, TimeSpan delay);
    string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay);
    string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay);
    string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay);

    string Schedule(Expression<Action> methodCall, DateTimeOffset enqueueAt);
    string Schedule(Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt);
    string Schedule<T>(Expression<Action<T>> methodCall, DateTimeOffset enqueueAt);
    string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt);

    string Recurring(string jobId, Expression<Action> methodCall, string recurring, DateTime schedule);
    string Recurring(string jobId, Expression<Func<Task>> methodCall, string recurring, DateTime schedule);
    string Recurring<T>(string jobId, Expression<Action<T>> methodCall, string recurring, DateTime schedule);
    string Recurring<T>(string jobId, Expression<Func<T, Task>> methodCall, string recurring, DateTime schedule);

    bool DeleteRecurring(string jobId);
}