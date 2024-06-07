using APIEscolaAuth1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIEscolaAuth1.Context;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Turma> Turmas { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
