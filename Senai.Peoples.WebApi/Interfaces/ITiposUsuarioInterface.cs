using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface ITiposUsuarioRepository
    {
        IEnumerable<TiposUsuarioDomain> Listar();
        TiposUsuarioDomain ListarPorId(int id);
        void Cadastrar(List<TiposUsuarioDomain> listaTiposUsuarios);
        void Atualizar(int id, TiposUsuarioDomain f);
        void Deletar(int id);
    }
}
