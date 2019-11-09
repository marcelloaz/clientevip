using ClienteVip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteVip.DataProvider
{
    public interface IClienteDataProvider
    {
        Cliente ObterClienteById(int id);

        void SalvarCliente(Cliente cliente);

        void ExcluirCliente(int id);
    }
}
