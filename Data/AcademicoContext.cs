using Microsoft.EntityFrameworkCore;
using ProjetoAcademico.Models;

namespace ProjetoAcademico.Data;

public class AcademicoContext : DbContext
{
    public AcademicoContext(DbContextOptions<AcademicoContext> options) : base(options)
    {
    }

    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Instituicao> Instituicoes => Set<Instituicao>();
}
