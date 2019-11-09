using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteVip.DataProvider.Lookups
{
    public interface ILookupProvider<T>
    {
        IEnumerable<LookupItem> RecuperaDadosPesquisa();
    }
}
