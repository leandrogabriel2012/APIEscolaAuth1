using APIEscolaAuth1.Context;
using APIEscolaAuth1.Models;
using APIEscolaAuth1.Repositories.Interfaces;

namespace APIEscolaAuth1.Repositories;

public class SalaRepository : Repository<Sala>, ISalaRepository
{
    public SalaRepository(AppDbContext context) : base(context) { }
}
