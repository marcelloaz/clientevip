using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteVip.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public int GrupoClienteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public bool Vip { get; set; }
        public Endereco Endereco { get; set; }
        public List<EmailCliente> Emails { get; set; }
    }
}
