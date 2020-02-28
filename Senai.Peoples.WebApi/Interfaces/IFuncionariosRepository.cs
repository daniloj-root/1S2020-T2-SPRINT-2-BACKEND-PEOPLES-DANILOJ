using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface IUsuariosRepository
    {
        IEnumerable<UsuarioDomain> Listar();
        UsuarioDomain ListarPorId(int id);
        void Cadastrar(List<UsuarioDomain> listaUsuarios);
        void Atualizar(int id, UsuarioDomain f);
        void Deletar(int id);
    }
}
