﻿using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Enums;
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

        public IEnumerable<UsuarioDomain> Listar()
        {
            List<UsuarioDomain> Usuarios = new List<UsuarioDomain>();
            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT ID, Nome, Sobrenome FROM Usuarios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UsuarioDomain Usuario = new UsuarioDomain
                        {
                            ID = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };
                        Usuarios.Add(Usuario);
                    }
                }
            }
            return Usuarios;
        }

        public UsuarioDomain ListarPorId(int id)
        {
            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = $"SELECT ID, Nome, Sobrenome FROM Usuarios WHERE ID = @ID AND IdTipoUsuario = {TipoUsuario.FUNCIONARIO}";

                con.Open();

                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        UsuarioDomain Usuario = new UsuarioDomain
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };
                        return Usuario;
                    }
                    return null;
                }
            }
        }

        public void Atualizar(int id, UsuarioDomain Usuario)
        {
            var query = $"UPDATE Usuarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE ID = @ID AND IdTipoUsuario = {TipoUsuario.FUNCIONARIO}";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", Usuario.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", Usuario.Sobrenome);
                    cmd.Parameters.AddWithValue("@ID", Usuario.ID);
                    cmd.ExecuteReader();
                }
            }
        }

        public void Cadastrar(List<UsuarioDomain> listaUsuarios)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = $"INSERT INTO Usuarios (Nome, Sobrenome, @IdTipoUsuario) VALUES";
                var i = 1;
                foreach (var Usuario in listaUsuarios)
                {
                    if (Usuario != listaUsuarios.Last())
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

                    listaUsuarios = listaUsuarios.Where(u => u.IdTipoUsuario == (int) TipoUsuario.USUARIO || u.IdTipoUsuario == (int) TipoUsuario.FUNCIONARIO).ToList();

                    foreach (var usuario in listaUsuarios)
                    {
                        usuario.Nome = usuario.Nome.Replace("'", " ");
                        usuario.Sobrenome = usuario.Sobrenome.Replace("'", " ");
                        cmd.Parameters.AddWithValue($"@Nome{i}", usuario.Nome);
                        cmd.Parameters.AddWithValue($"@Sobrenome{i}", usuario.Sobrenome);
                        cmd.Parameters.AddWithValue($"@TipoUsuario{i}", usuario.IdTipoUsuario);
                        i++;
                    }
                    cmd.ExecuteReader();
                }
            }
        }

        public void Deletar(int id)
        {
            var query = "DELETE FROM Usuarios WHERE ID = @ID";

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
