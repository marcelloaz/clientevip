using ClienteVip.Commnad;
using ClienteVip.Eventos;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ClienteVip.ViewModel
{
    public class MainViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private IClienteEditarViewModel _selectedClienteEditarViewModel;

        public DelegateCommand FecharAbaClienteTabCommand { get; }
        public IMenuPrincipalViewModel MenuPrincipalViewModel { get; private set; }

        private Func<IClienteEditarViewModel> _clienteEditarViewModelCriacao;
       

        public ObservableCollection<IClienteEditarViewModel> ClienteEditarViewModels { get; private set; }

        public MainViewModel(IEventAggregator eventAggregator, 
            IMenuPrincipalViewModel menuPrincipalViewModel,
             Func<IClienteEditarViewModel> clienteEditarViewModelCriacao)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AbrirEditarViewEvento>().Subscribe(OnOpenClienteTab);
            FecharAbaClienteTabCommand = new DelegateCommand(OnFecharAbaClienteTabExecute);
            MenuPrincipalViewModel = menuPrincipalViewModel;
            _clienteEditarViewModelCriacao = clienteEditarViewModelCriacao;
            ClienteEditarViewModels = new ObservableCollection<IClienteEditarViewModel>();
            AdicionarClienteCommand = new DelegateCommand(AdicionarExecute);
        }

        public void OnClosing(CancelEventArgs e)
        {
            if (ClienteEditarViewModels.Any(f => f.Cliente.IsChanged))
            {
               
            }
        }

        private void AdicionarExecute(object obj)
        {
            IClienteEditarViewModel clienteEditarViewModel = _clienteEditarViewModelCriacao();
            ClienteEditarViewModels.Add(clienteEditarViewModel);
            clienteEditarViewModel.Iniciar();
            SelecaoClienteEditarViewModel = clienteEditarViewModel;
        }

        public IClienteEditarViewModel SelecaoClienteEditarViewModel
        {
            get { return _selectedClienteEditarViewModel; }
            set
            {
                _selectedClienteEditarViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand AdicionarClienteCommand { get; set; }

        private void OnFecharAbaClienteTabExecute(object obj)
        {
            var clienteEditVmFechar = obj as IClienteEditarViewModel;

            if(clienteEditVmFechar !=null)
            {
                ClienteEditarViewModels.Remove(clienteEditVmFechar);
            }
        }

        private void OnOpenClienteTab(int clienteId)
        {
            IClienteEditarViewModel clienteEditarVm =
              ClienteEditarViewModels.SingleOrDefault(vm => vm.Cliente.Id == clienteId);
            if (clienteEditarVm == null)
            {
                clienteEditarVm = _clienteEditarViewModelCriacao();
                ClienteEditarViewModels.Add(clienteEditarVm);
                clienteEditarVm.Iniciar(clienteId);
            }
            SelectedClienteEditarViewModel = clienteEditarVm;
        }


        public IClienteEditarViewModel SelectedClienteEditarViewModel
        {
            get { return _selectedClienteEditarViewModel; }
            set
            {
                _selectedClienteEditarViewModel = value;
                OnPropertyChanged();
            }
        }

        public void Iniciar()
        {
            MenuPrincipalViewModel.CarregarMenuPrincipal();
        }
    }
}
