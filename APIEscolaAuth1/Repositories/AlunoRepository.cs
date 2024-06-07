using APIEscolaAuth1.Context;
using APIEscolaAuth1.DTOs;
using APIEscolaAuth1.Models;
using APIEscolaAuth1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIEscolaAuth1.Repositories;

public class AlunoRepository : Repository<Aluno>, IAlunoRepository
{
    public AlunoRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Aluno>?> GetAlunosTurmaAsync(int id)
    {
        return await _context.Alunos.Where(t => t.TurmaId == id).AsNoTracking().ToListAsync();
    }
}
