namespace Demo.Modules.Common.Application.Contracts;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
}