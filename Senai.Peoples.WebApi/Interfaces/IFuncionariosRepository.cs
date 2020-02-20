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
        void Cadastrar(Funcionarios f);
        void Atualizar(int id, Funcionarios f);
        void Deletar(int id);
    }
}
