﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Domains
{
    public class UsuarioDomain
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int IdTipoUsuario { get; set; }
    }

    public class Usuarios
    {
        public List<UsuarioDomain> usuarios { get; set; }
    }
}
