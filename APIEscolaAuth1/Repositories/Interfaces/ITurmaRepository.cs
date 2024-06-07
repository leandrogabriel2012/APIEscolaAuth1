using APIEscolaAuth1.Models;

namespace APIEscolaAuth1.Repositories.Interfaces;

public interface ITurmaRepository : IRepository<Turma>
{
    Task<IEnumerable<Turma>> GetTurmasSalaAsync(int id);
}
