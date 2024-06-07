using APIEscolaAuth1.DTOs;
using APIEscolaAuth1.Models;

namespace APIEscolaAuth1.Repositories.Interfaces;

public interface IAlunoRepository : IRepository<Aluno>
{
    Task<IEnumerable<Aluno>?> GetAlunosTurmaAsync(int id);
}