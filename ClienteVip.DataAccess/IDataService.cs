using ClienteVip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteVip.DataAccess
{
   public interface IDataService : IDisposable
    {
        Cliente ObterClienteById(int clienteId);

        void SalvarCliente(Cliente cliente);

        void ExcluirCliente(int clienteId);

        IEnumerable<GrupoCliente> ObterTodosGrupoCliente();

        IEnumerable<Cliente> ObtemTodosClientes();


    }
}
