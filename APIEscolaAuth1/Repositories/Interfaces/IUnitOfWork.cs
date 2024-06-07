namespace APIEscolaAuth1.Repositories.Interfaces;

public interface IUnitOfWork
{
    IAlunoRepository AlunoRepository { get; }
    ISalaRepository SalaRepository { get; }
    ITurmaRepository TurmaRepository { get; }
    Task CommitAsync();
}
