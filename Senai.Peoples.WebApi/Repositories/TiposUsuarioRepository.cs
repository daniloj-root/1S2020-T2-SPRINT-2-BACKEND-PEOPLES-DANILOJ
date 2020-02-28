using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class TiposUsuarioRepository : ITiposUsuarioRepository
    {
        private string StringConexao = "Data Source=DEV6\\SQLEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=sa@132;";

        public IEnumerable<TiposUsuarioDomain> Listar()
        {
            List<TiposUsuarioDomain> TiposUsuarios = new List<TiposUsuarioDomain>();
            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT ID, Descricao FROM TiposUsuario";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        TiposUsuarioDomain TiposUsuario = new TiposUsuarioDomain
                        {
                            ID = Convert.ToInt32(rdr[0]),
                            Descricao = rdr["Descricao"].ToString()
                        };
             
                        TiposUsuarios.Add(TiposUsuario);
                    }
                }
            }
            return TiposUsuarios;
        }

        public TiposUsuarioDomain ListarPorId(int id)
        {
            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT ID, Descricao FROM TiposUsuario WHERE ID = @ID";

                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        TiposUsuarioDomain TiposUsuario = new TiposUsuarioDomain
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            Descricao = rdr["Descricao"].ToString()
                        };
                        return TiposUsuario;
                    }
                    return null;
                }
            }
        }

        public void Atualizar(int id, TiposUsuarioDomain TiposUsuario)
        {
            var query = "UPDATE TiposUsuario SET Descricao = @Descricao WHERE ID = @ID";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", TiposUsuario.ID);
                    cmd.Parameters.AddWithValue("@Descricao", TiposUsuario.Descricao);
                    cmd.ExecuteReader();
                }
            }
        }

        public void Cadastrar(List<TiposUsuarioDomain> listaTiposUsuarios)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = $"INSERT INTO TiposUsuario (Descricao) VALUES";
                var i = 1;
                foreach (var TiposUsuario in listaTiposUsuarios)
                {
                    if (TiposUsuario != listaTiposUsuarios.Last())
                    {
                        query += $"(@Descricao{i}),";
                    }
                    else
                    {
                        query += $"(@Descricao{i})";
                    }
                    i++;
                }
                con.Open();
                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    i = 1;
                    foreach (var tipoUsuario in listaTiposUsuarios)
                    {
                        var descricaoTratada = tipoUsuario.Descricao.Replace("'", " ");
                        cmd.Parameters.AddWithValue($"@Descricao{i}", tipoUsuario.Descricao);
                        i++;
                    }
                    cmd.ExecuteReader();
                }
            }
        }

        public void Deletar(int id)
        {
            var query = "DELETE FROM TiposUsuario WHERE ID = @ID";

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
    }
}
