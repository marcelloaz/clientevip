using ClienteVip.DataAccess;
using ClienteVip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteVip.DataProvider.Lookups
{
    public class GrupoClienteLookupProvider : ILookupProvider<GrupoCliente>
    {
        public GrupoClienteLookupProvider(Func<IDataService> criarDataService)
        {
            _criarDataService = criarDataService;
        }

        public readonly Func<IDataService> _criarDataService;
        public IEnumerable<LookupItem> RecuperaDadosPesquisa()
        {
            using (var servico = _criarDataService())
            {
                return servico.ObterTodosGrupoCliente()
                    .Select(c => new LookupItem
                    {
                        Id = c.Id,
                        Text = c.Nome
                    })
                    .OrderBy(l => l.Text)
                    .ToList();
            }
        }
    }
}
