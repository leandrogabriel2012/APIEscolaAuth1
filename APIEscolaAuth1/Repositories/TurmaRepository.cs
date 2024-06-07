using APIEscolaAuth1.Context;
using APIEscolaAuth1.Models;
using APIEscolaAuth1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIEscolaAuth1.Repositories;

public class TurmaRepository : Repository<Turma>, ITurmaRepository
{
    public TurmaRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Turma>> GetTurmasSalaAsync(int id)
    {
        return await _context.Turmas.Where(s => s.SalaId == id).AsNoTracking().ToListAsync();
    }
}
