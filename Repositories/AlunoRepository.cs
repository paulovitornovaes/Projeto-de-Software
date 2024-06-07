using System.Linq.Expressions;
using Iduff.Contracts;
using Iduff.Models;
using Microsoft.EntityFrameworkCore;

namespace Iduff.Repositories;

public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
{
    private readonly DbSet<Aluno> _dbSet;
    private readonly DbSet<CargaHoraria> _dbSetCh;
    public AlunoRepository(IduffContext context) : base(context)
    {
        _dbSet = context.Set<Aluno>();
        _dbSetCh = context.Set<CargaHoraria>();
    }

    public async Task<CargaHoraria> CreateAlunoCargaHoraria(Aluno aluno)
    {
        var cargaHoraria = new CargaHoraria()
        {
            total = 0,
            estagio = 0,
            ministrarPalestras = 0,
            organizarPalestras = 0,
            presencaPalestras = 0
        };
        await _dbSetCh.AddAsync(cargaHoraria);
        await _context.SaveChangesAsync();
        return cargaHoraria;

    }
    public async Task<string> GetMatriculaByEmail(string email)
    {
        var aluno = await _dbSet.SingleOrDefaultAsync(m => m.Email == email);
        return aluno?.matricula.ToString()!;
    }
    public async Task<Aluno> GetAlunoByMatricula(string matricula)
    {
        return (await _dbSet.SingleOrDefaultAsync(m => m.matricula == long.Parse(matricula)))!;
    }
}
