using APIEscolaAuth1.Context;
using APIEscolaAuth1.Repositories.Interfaces;

namespace APIEscolaAuth1.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IAlunoRepository? _alunoRepository;
    private ISalaRepository? _salaRepository;
    private ITurmaRepository? _turmaRepository;
    private AppDbContext _context;
    private ILogger<UnitOfWork> _logger;

    public UnitOfWork(AppDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IAlunoRepository AlunoRepository
    { 
        get
        {
            return _alunoRepository ?? new AlunoRepository(_context);
        }
    }

    public ISalaRepository SalaRepository
    {
        get
        {
            return _salaRepository ?? new SalaRepository(_context);
        }
    }

    public ITurmaRepository TurmaRepository
    {
        get 
        {
            return _turmaRepository ?? new TurmaRepository(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
