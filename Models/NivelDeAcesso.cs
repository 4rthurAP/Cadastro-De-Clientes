using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeClientes.Models
{
    public class NivelDeAcesso
    {

        public bool Inclusao { get; set; }

        public bool Alteracao { get; set; }

        public bool Exclusao { get; set; }
    }
}
