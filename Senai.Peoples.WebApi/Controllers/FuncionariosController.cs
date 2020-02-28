using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]

    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private const string secretKey = "VGhyb3cgZG93biBhbGwgdGhlIHN0dWZmIGluIHRoZSBraXRjaGVuIGZvb2xlZCBhZ2FpbiB0aGlua2luZyB0aGUgZG9nIGxpa2VzIG1lIHBsYXk";

        private IUsuariosRepository _usuariosRepository { get; set; }

        public UsuariosController()
        {
            _usuariosRepository = new UsuariosRepository();
        }

        // GET api/Usuarios
        [HttpGet]
        public IEnumerable<UsuarioDomain> Listar()
        {
            var usuarios = _usuariosRepository.Listar();
            return usuarios;
        }

        // GET api/Usuarios/5
        [HttpGet("{id}")]
        public UsuarioDomain ListarPorId(int id)
        {
            return _usuariosRepository.ListarPorId(id);
        }

        // POST api/Usuarios
        [HttpPost]
        public IActionResult Cadastrar(Usuarios f)
        {
            try
            {
                var listaUsuarios = f.usuarios;
                _usuariosRepository.Cadastrar(listaUsuarios);
                return Ok("Inserido com sucesso.");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        public IActionResult Login (UsuarioDomain usuario)
        {
            var usuarioBuscado = _usuariosRepository.ListarPorId(usuario.ID);

            if (usuarioBuscado == null)
            {
                return NotFound("Não foi possível validar email e/ou senha");
            }

          
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.ID.ToString()),
                new Claim(ClaimTypes.Surname, usuarioBuscado.Sobrenome),
                new Claim(ClaimTypes.Name, usuarioBuscado.Nome),
                new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Gera o token
            var token = new JwtSecurityToken(
                issuer: "Senai.Peoples.WebApi",
                audience: "Senai.Peoples.WebApi",
                claims: claims,                   
                expires: DateTime.Now.AddMinutes(15), 
                signingCredentials: creds
            );

            // Retorna Ok com o token
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        // PUT api/Usuarios/5
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, UsuarioDomain usuario)
        {
            try
            {
                _usuariosRepository.Atualizar(id, usuario);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE api/Usuarios/5
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            if (_usuariosRepository.ListarPorId(id) == null)
            {
                return BadRequest();
            }
            try
            {
                _usuariosRepository.Deletar(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
