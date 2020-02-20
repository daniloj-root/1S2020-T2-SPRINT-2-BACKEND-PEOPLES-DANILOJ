using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionariosRepository : IFuncionariosRepository
    {
        private string StringConexao = "Data Source=DEV6\\SQLEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=sa@132;";
        [
                public IEnumerable<FuncionarioDomain> Listar()
        {
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();
            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT * FROM Funcionarios";

                con.Open();
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            ID = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };

                        // Adiciona o funcionario criado à tabela funcionarios
                        funcionarios.Add(funcionario);
                    }
                }
            }
            return funcionarios;
        }

        public FuncionarioDomain ListarPorId(int id)
        {
            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT * FROM Funcionarios WHERE ID = @ID";

                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    rdr = cmd.ExecuteReader();

                    FuncionarioDomain funcionario = new FuncionarioDomain
                    {
                        ID = Convert.ToInt32(rdr[0]),
                        Nome = rdr["Nome"].ToString(),
                        Sobrenome = rdr["Sobrenome"].ToString()
                    };
                    return funcionario;
                }
            }
        }

        public void Atualizar(int id, FuncionarioDomain f)
        {
            var query = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE ID = @ID";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", f.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", f.Sobrenome);
                    cmd.Parameters.AddWithValue("@ID", f.ID);
                    cmd.ExecuteReader();
                }
            }
        }

        public void Cadastrar(Funcionarios f)
        {
            var listaFuncionarios = f.listaFuncionarios;

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = $"INSERT INTO Funcionarios (Nome, Sobrenome) VALUES";
                var i = 1;
                foreach (var funcionario in listaFuncionarios)
                {
                    if (funcionario != listaFuncionarios.Last())
                    {
                        query += $"(@Nome{i},@Sobrenome{i}),";
                    }
                    else
                    {
                        query += $"(@Nome{i}, @Sobrenome{i})";
                    }
                    i++;
                }
                con.Open();
                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    i = 1;
                    foreach (var funcionario in listaFuncionarios)
                    {
                        var nomeTratado = funcionario.Nome.Replace("'", " ");
                        var sobrenomeTratado = funcionario.Nome.Replace("'", " ");
                        cmd.Parameters.AddWithValue($"@Nome{i}", nomeTratado);
                        cmd.Parameters.AddWithValue($"@Sobrenome{i}", sobrenomeTratado);
                        i++;
                    }
                    cmd.ExecuteReader();
                }
            }
        }

        public void Deletar(int id)
        {
            var query = "DELETE FROM Funcionarios WHERE ID = @ID";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteReader();
                }
            }
        }

        public void Atualizar(int id, Funcionarios f)
        {
            throw new NotImplementedException();
        }
    }
}
