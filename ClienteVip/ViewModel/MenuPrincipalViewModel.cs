
using ClienteVip.Commnad;
using ClienteVip.DataProvider.Lookups;
using ClienteVip.Eventos;
using ClienteVip.Model;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ClienteVip.ViewModel
{
    public interface IMenuPrincipalViewModel {

        void CarregarMenuPrincipal();
    }

    public class MenuPrincipalViewModel : IMenuPrincipalViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private ILookupProvider<Cliente> _clienteLookupProvider;

        public ObservableCollection<MenuPrincipalItemViewModel> MenuPrincipalItem { get; set; }

        public MenuPrincipalViewModel(IEventAggregator eventAggregator, 
            ILookupProvider<Cliente> clienteLookupProvider)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SalvarClienteEvento>().Subscribe(OnFriendSaved);
            _eventAggregator.GetEvent<ExcluirClienteEvento>().Subscribe(OnFriendDeleted);
            _clienteLookupProvider = clienteLookupProvider;
            MenuPrincipalItem = new ObservableCollection<MenuPrincipalItemViewModel>();
        }


        private void OnFriendDeleted(int clienteId)
        {
            var navigationItem =
              MenuPrincipalItem.SingleOrDefault(item => item.ClienteId == clienteId);
            if (navigationItem != null)
            {
                MenuPrincipalItem.Remove(navigationItem);
            }
        }


        private void OnFriendSaved(Cliente savedCliente)
        {
            var navigationItem =
              MenuPrincipalItem.SingleOrDefault(item => item.ClienteId == savedCliente.Id);
            if (navigationItem != null)
            {
                navigationItem.Texto = string.Format("{0} {1}", savedCliente.Nome, savedCliente.Sobrenome);
            }
            else
            {
                CarregarMenuPrincipal();
            }
        }
    

    public void CarregarMenuPrincipal()
        {
            MenuPrincipalItem.Clear();

            foreach (var menuPrincipalItem in _clienteLookupProvider.RecuperaDadosPesquisa())
            {
                MenuPrincipalItem.Add(
                  new MenuPrincipalItemViewModel(
                    menuPrincipalItem.Id,
                    menuPrincipalItem.Text,
                    _eventAggregator));
            }
        }
        
    }

    public class MenuPrincipalItemViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private string _texto;

        public MenuPrincipalItemViewModel(int clienteId,
          string texto,
          IEventAggregator eventAggregator)
        {
            ClienteId = clienteId;
            Texto = texto;
            _eventAggregator = eventAggregator; 
            ComandoEditarClienteView = new DelegateCommand(ExecutaAbrirTelaEditarCliente);
        }

        private void ExecutaAbrirTelaEditarCliente(object obj)
        {
            _eventAggregator.GetEvent<AbrirEditarViewEvento>().Publish(ClienteId);
        }

        public int ClienteId { get; private set; }
        public string Texto {
            get { return _texto; }
            set {
                _texto = value;
                OnPropertyChanged();
            }
        }

        public ICommand ComandoEditarClienteView { get; set; }
    }
}
