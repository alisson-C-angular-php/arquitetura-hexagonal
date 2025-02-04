using crudBack.Model;
using crudBack.Repository;
using Microsoft.AspNetCore.Mvc;

namespace crudBack.Controllers
{
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private AlunoRepository alunoRepository;
        public AlunoController(AlunoRepository alunoRepository)
        {
            this.alunoRepository = alunoRepository;
        }

        [HttpGet("listar-aluno")]
        public Task<List<AlunoModel>> getAlunos()
        {

            var res = alunoRepository.GetAlunos();
            return res;

        }

        [HttpPost("inserir-aluno")]
        public int inserirAlunos([FromBody] AlunoModel aluno)
        {
            var res = alunoRepository.insertAlunos(aluno.id,aluno.nome,aluno.idade,aluno.curso,aluno.ra);
            return res;

        }

        [HttpPut("atualizar-aluno")]
        public int atualizarAlunos([FromBody] AlunoModel aluno)
        {
            var res = alunoRepository.updateAluno(aluno);
            return res;
        }


        [HttpDelete("delete-aluno")]
        public int deletarAlunos([FromBody] AlunoModel aluno)
        {
            var res = alunoRepository.deleteAluno(aluno);
            return res;
        }
    }
}
