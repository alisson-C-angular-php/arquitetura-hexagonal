using crudBack.Model;
using crudBack.outbound.service;
using crudBack.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace crudBack.Controllers
{
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private AlunoRepository alunoRepository;
        private readonly RedisService redisService;
        private readonly RabbitMQService rabbitMQService;

        public AlunoController(AlunoRepository alunoRepository, RabbitMQService rabbitMQService, RedisService redisService)
        {
            this.alunoRepository = alunoRepository;
            this.rabbitMQService = rabbitMQService;
            this.redisService = redisService;
        }

        [HttpGet("listar-aluno")]
        public async Task<ActionResult<List<AlunoModel>>> GetAlunos()
        {
            var cacheKey = "lista_alunos";
            var cachedAlunos = await redisService.GetFromCache(cacheKey);

            if (cachedAlunos != null)
            {
                return JsonConvert.DeserializeObject<List<AlunoModel>>(cachedAlunos);
            }

            var alunos = await alunoRepository.GetAlunos();
            await redisService.SetToCache(cacheKey, JsonConvert.SerializeObject(alunos), TimeSpan.FromMinutes(10));
            rabbitMQService.SendMessage("alunos_queue", "Lista de alunos consultada");

            return alunos;
        }

        [HttpPost("inserir-aluno")]
        public async Task<int> InserirAlunos([FromBody] AlunoModel aluno)
        {
            var res = alunoRepository.insertAlunos(aluno.id, aluno.nome, aluno.idade, aluno.curso, aluno.ra);

            rabbitMQService.SendMessage("alunos_queue", $"Novo aluno inserido: {aluno.nome}");

            var cacheKey = "lista_alunos";
            await redisService.RemoveFromCache(cacheKey);  
            var alunosAtualizados = await alunoRepository.GetAlunos();
            await redisService.SetToCache(cacheKey, JsonConvert.SerializeObject(alunosAtualizados), TimeSpan.FromMinutes(10));

            return res;
        }

        [HttpPut("atualizar-aluno")]
        public async Task<int> AtualizarAlunos([FromBody] AlunoModel aluno)
        {
            var res = alunoRepository.updateAluno(aluno);

            rabbitMQService.SendMessage("alunos_queue", $"Aluno atualizado: {aluno.nome}");

            var cacheKey = "lista_alunos";
            await redisService.RemoveFromCache(cacheKey);  
            var alunosAtualizados = await alunoRepository.GetAlunos();
            await redisService.SetToCache(cacheKey, JsonConvert.SerializeObject(alunosAtualizados), TimeSpan.FromMinutes(10));

            return res;
        }

        [HttpDelete("delete-aluno")]
        public async Task<int> DeletarAlunos([FromBody] AlunoModel aluno)
        {
            var res = alunoRepository.deleteAluno(aluno);

            rabbitMQService.SendMessage("alunos_queue", $"Aluno excluído: {aluno.nome}");

            var cacheKey = "lista_alunos";
            await redisService.RemoveFromCache(cacheKey);  
            var alunosAtualizados = await alunoRepository.GetAlunos();
            await redisService.SetToCache(cacheKey, JsonConvert.SerializeObject(alunosAtualizados), TimeSpan.FromMinutes(10));

            return res;
        }
    }
}
