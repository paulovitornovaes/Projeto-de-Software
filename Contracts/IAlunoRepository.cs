using System.Linq.Expressions;
using Iduff.Models;

namespace Iduff.Contracts;

public interface IAlunoRepository  : IBaseRepository<Aluno>
{
    Task<Aluno> GetAlunoByMatricula(string matricula);
    Task<string> GetMatriculaByEmail(string email);
}