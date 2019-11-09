using ClienteVip.DataAccess;
using ClienteVip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteVip.DataProvider.Lookups
{
    public class ClienteLookupProvider : ILookupProvider<Cliente>
    {
        public ClienteLookupProvider(Func<IDataService> criarDataService)
        {
            _criarDataService = criarDataService;
        }

        public readonly Func<IDataService> _criarDataService;
        public IEnumerable<LookupItem> RecuperaDadosPesquisa()
        {
            using (var servico = _criarDataService())
            {
                return servico.ObtemTodosClientes()
                    .Select(c => new LookupItem
                    {
                        Id = c.Id,
                        Text = $"{c.Nome} {c.Sobrenome}"
                    })
                    .OrderBy(l => l.Text)
                    .ToList();
            }
        }
    }
}
