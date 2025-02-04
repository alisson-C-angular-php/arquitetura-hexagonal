using crudBack.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace crudBack.Repository
{
    public class AlunoRepository
    {
        private readonly Configuration configuration;

        public AlunoRepository(Configuration configuration)
        {
            this.configuration = configuration;
        }


        public int deleteAluno(AlunoModel aluno)
        {
            using (MySqlConnection connection = new MySqlConnection(configuration.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_delete_aluno", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    cmd.Parameters.AddWithValue("@p_codigo", aluno.id);

                    cmd.ExecuteNonQuery();

                }
            }
            return 1;

        }

        public int updateAluno(AlunoModel aluno)
        {
            using (MySqlConnection connection = new MySqlConnection(configuration.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_aluno", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    cmd.Parameters.AddWithValue("@p_codigo", aluno.id);

                    cmd.Parameters.AddWithValue("@p_nome", aluno.nome);
                    cmd.Parameters.AddWithValue("@p_curso", aluno.curso);
                    cmd.Parameters.AddWithValue("@p_idade", aluno.idade);

                    cmd.Parameters.AddWithValue("@p_ra", aluno.ra);

                    cmd.ExecuteNonQuery();

                }
            }
            return 1;

        }

        public  int insertAlunos(int id,string nome,int idade,string curso,string ra)
        {
            using (MySqlConnection connection = new MySqlConnection(configuration.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_aluno", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    cmd.Parameters.AddWithValue("@p_codigo", id);

                    cmd.Parameters.AddWithValue("@p_nome", nome);
                    cmd.Parameters.AddWithValue("@p_curso", curso);
                    cmd.Parameters.AddWithValue("@p_idade", idade);

                    cmd.Parameters.AddWithValue("@p_ra", ra);

                    cmd.ExecuteNonQuery();

                }
            }
            return 1;
        }

        public async Task<List<AlunoModel>> GetAlunos()
        {
            List<AlunoModel> alunos = new List<AlunoModel>();

            using (MySqlConnection connection = new MySqlConnection(configuration.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_select_aluno", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    await connection.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var aluno = new AlunoModel
                            {
                                id = reader.GetInt32("id"),
                                nome = reader.GetString("nome"),
                                idade = reader.GetInt32("idade"),
                                curso = reader.GetString("curso"),
                                ra = reader.GetString("ra")
                            };

                            alunos.Add(aluno);
                        }
                    }
                }
            }

            return alunos;
        }
    }
}
