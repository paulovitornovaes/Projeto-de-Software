using System.Linq.Expressions;
using Iduff.Contracts;
using Iduff.Models;
using Microsoft.EntityFrameworkCore;

namespace Iduff.Repositories;

public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
{
    private readonly DbSet<Aluno> _dbSet;

    public AlunoRepository(IduffContext context) : base(context)
    {
        _dbSet = context.Set<Aluno>();
    }
    
    public async Task<Aluno> GetAlunoByMatricula(string matricula)
    {
        return (await _dbSet.SingleOrDefaultAsync(m => m.matricula == long.Parse(matricula)))!;
    }
}
