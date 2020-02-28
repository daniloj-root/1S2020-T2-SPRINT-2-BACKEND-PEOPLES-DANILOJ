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
    public class TiposUsuarioController : ControllerBase
    {
        private ITiposUsuarioRepository _tiposUsuarioRepository { get; set; }

        public TiposUsuarioController()
        {
            _tiposUsuarioRepository = new TiposUsuarioRepository();
        }

        // GET api/TiposUsuario
        [HttpGet]
        public IEnumerable<TiposUsuarioDomain> Listar()
        {
            var tiposUsuarios = _tiposUsuarioRepository.Listar();
            return tiposUsuarios;
        }

        // GET api/TiposUsuarios/5
        [HttpGet("{id}")]
        public TiposUsuarioDomain ListarPorId(int id)
        {
            return _tiposUsuarioRepository.ListarPorId(id);
        }

        // POST api/TiposUsuarios
        [HttpPost]
        public IActionResult Cadastrar (TiposUsuario t)
        {
            try
            {
                var listaTiposUsuarios = t.tiposUsuario;
                _tiposUsuarioRepository.Cadastrar(listaTiposUsuarios);
                return Ok("Inserido com sucesso.");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/TiposUsuarios/5
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, TiposUsuarioDomain tiposUsuario)
        {
            try
            {
                _tiposUsuarioRepository.Atualizar(id, tiposUsuario);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE api/TiposUsuarios/5
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            if (_tiposUsuarioRepository.ListarPorId(id) == null)
            {
                return BadRequest();
            }
            try
            {
                _tiposUsuarioRepository.Deletar(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
