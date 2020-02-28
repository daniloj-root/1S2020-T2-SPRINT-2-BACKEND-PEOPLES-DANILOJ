using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Domains
{
    public class TiposUsuarioDomain
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
    }

    public class TiposUsuario
    {
        public List<TiposUsuarioDomain> tiposUsuario { get; set; }
    }
}
