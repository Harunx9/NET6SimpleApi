namespace SimpleApi.Model.Base;

public interface IUnitOfWork
{
    Task End();
}

public interface IUnitOfWorkFactory
{
    IUnitOfWork Begin();
}

public class InMemoryUnitOfWork : IUnitOfWork
{
    private readonly EFContext _context;

    public InMemoryUnitOfWork(EFContext context)
    {
        _context = context;
    }

    public async Task End()
    {
        await _context.SaveChangesAsync();
    }
}

public class InMemoryUnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly EFContext _context;

    public InMemoryUnitOfWorkFactory(EFContext context)
    {
        _context = context;
    }

    public IUnitOfWork Begin() => new InMemoryUnitOfWork(_context);
}