namespace EmployeeManagement_Backend.Repositories;

public interface IPersistence
{
    public Task BeginTransactionAsync();
    public Task CommitAsync();
    public Task RollbackAsync();
    public void SaveChanges();
    public Task SaveChangesAsync();
}