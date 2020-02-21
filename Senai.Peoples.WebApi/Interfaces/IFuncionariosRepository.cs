using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface IFuncionariosRepository
    {
        IEnumerable<FuncionarioDomain> Listar();
        FuncionarioDomain ListarPorId(int id);
        void Cadastrar(List<FuncionarioDomain> listaFuncionarios);
        void Atualizar(int id, FuncionarioDomain f);
        void Deletar(int id);
    }
}
