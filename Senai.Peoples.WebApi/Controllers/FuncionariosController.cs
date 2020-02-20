using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Senai.Peoples.WebApi.Domains;

namespace Senai.Peoples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeoplesController : ControllerBase
    {
        // GET api/Filmes
        [HttpGet]
        public IEnumerable<FuncionarioDomain> Get()
        {
            
        }

        // GET api/Filmes/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            
        }

        // POST api/Filmes
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
        }

        // PUT api/Filmes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/Filmes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
        }
    }
}
