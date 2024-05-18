using Iduff.Contracts;
using Iduff.Models;
using Microsoft.EntityFrameworkCore;

namespace Iduff.Repositories;

public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
{
    
    public AlunoRepository(IduffContext context) : base(context) { }
}
