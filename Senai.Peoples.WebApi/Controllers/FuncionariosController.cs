using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]

    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private IFuncionariosRepository _funcionariosRepository { get; set; }

        public FuncionariosController()
        {
            _funcionariosRepository = new FuncionariosRepository();
        }

        // GET api/Funcionarios
        [HttpGet]
        public IEnumerable<FuncionarioDomain> Listar()
        {
            var funcionarios = _funcionariosRepository.Listar();
            return funcionarios;
        }

        // GET api/Funcionarios/5
        [HttpGet("{id}")]
        public FuncionarioDomain ListarPorId(int id)
        {
            return _funcionariosRepository.ListarPorId(id);
        }

        // POST api/Funcionarios
        [HttpPost]
        public IActionResult Cadastrar(Funcionarios f)
        {
            try
            {
                var listaFuncionarios = f.funcionarios;
                _funcionariosRepository.Cadastrar(listaFuncionarios);
                return Ok("Inserido com sucesso.");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/Funcionarios/5
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, FuncionarioDomain funcionario)
        {
            try
            {
                _funcionariosRepository.Atualizar(id, funcionario);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE api/Funcionarios/5
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            if (_funcionariosRepository.ListarPorId(id) == null)
            {
                return BadRequest();
            }
            try
            {
                _funcionariosRepository.Deletar(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
