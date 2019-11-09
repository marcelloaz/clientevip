using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClienteVip.DataAccess;
using ClienteVip.Model;

namespace ClienteVip.DataProvider
{
    public class ClienteDataProvider : IClienteDataProvider
    {
        public ClienteDataProvider(Func<IDataService> criarDataService)
        {
            _criarDataService = criarDataService;
        }

        public readonly Func<IDataService> _criarDataService;

        public void ExcluirCliente(int id)
        {
            using (var servico = _criarDataService())
            {
                servico.ExcluirCliente(id);
            }
        }

        public Cliente ObterClienteById(int id)
        {
            using (var servico = _criarDataService())
            {
                return servico.ObterClienteById(id);
                   
            }
        }

        public void SalvarCliente(Cliente cliente)
        {
            using (var servico = _criarDataService())
            {
                servico.SalvarCliente(cliente);
            }
        }
    }
}
