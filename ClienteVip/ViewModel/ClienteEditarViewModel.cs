using ClienteVip.Commnad;
using ClienteVip.DataProvider;
using ClienteVip.DataProvider.Lookups;
using ClienteVip.Eventos;
using ClienteVip.Model;
using ClienteVip.Wrapper;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClienteVip.ViewModel
{
    public interface IClienteEditarViewModel
    {
        void Iniciar(int? clienteId = null);
        ClienteWrapper Cliente { get; }
    }

    public class ClienteEditarViewModel : Observable, IClienteEditarViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IClienteDataProvider _clienteDataProvider;
        private readonly ILookupProvider<GrupoCliente> _grupoClienteLookupProvider;
        private ClienteWrapper _cliente;
        private IEnumerable<LookupItem> _grupoClientes;
        private EmailClienteWrapper _emailSelecionado;

        public ClienteEditarViewModel(IEventAggregator eventAggregator,
            IClienteDataProvider clienteDataProvider,
            ILookupProvider<GrupoCliente> grupoClienteLookupProvider)
        {
            _eventAggregator = eventAggregator;
            _clienteDataProvider = clienteDataProvider;
            _grupoClienteLookupProvider = grupoClienteLookupProvider;

            SalvarCommand = new DelegateCommand(SalvarExecute,SalvarPodeExecute);
            ExcluirCommand = new DelegateCommand(ExcluirExecute, ExcluirPodeExecute);

            NovoCommand = new DelegateCommand(NovoExecute, NovoPodeExecute);

            AdicionarEmailCommand = new DelegateCommand(AdicionarEmailExecute);
            RemoverEmailCommand = new DelegateCommand(RemoveEmailExecute, RemoverEmailPodeExecute);

        }

        private bool RemoverEmailPodeExecute(object arg)
        {
            return EmailSelecionado != null;
        }

        private void RemoveEmailExecute(object obj)
        {
            Cliente.Emails.Remove(EmailSelecionado);
            ((DelegateCommand)RemoverEmailCommand).RaiseCanExecuteChanged();
        }

        private void AdicionarEmailExecute(object obj)
        {
            Cliente.Emails.Add(new EmailClienteWrapper(new EmailCliente()));
        }

        public ICommand AdicionarEmailCommand { get; private set; }
        public ICommand RemoverEmailCommand { get; private set; }
        public ICommand SalvarCommand { get; private set; }
        public ICommand ExcluirCommand { get; private set; }
        public ICommand NovoCommand { get; private set; }

        private void SalvarExecute(object obj)
        {
            _clienteDataProvider.SalvarCliente(Cliente.Model);
            Cliente.AcceptChanges();
            _eventAggregator.GetEvent<SalvarClienteEvento>().Publish(Cliente.Model);
            InvalidarComandos();
        }

        private bool SalvarPodeExecute(object arg)
        {
            return Cliente.IsChanged && Cliente.IsValid;
        }
        
        private bool NovoPodeExecute(object arg)
        {
            return Cliente.IsChanged;
        }

        private void NovoExecute(object obj)
        {
            Cliente.RejectChanges();
        }

        private void ExcluirExecute(object obj)
        {
            _clienteDataProvider.ExcluirCliente(Cliente.Id);
            _eventAggregator.GetEvent<ExcluirClienteEvento>().Publish(Cliente.Id);
        }

        private bool ExcluirPodeExecute(object arg)
        {
            return Cliente != null && Cliente.Id > 0;
        }

        public void Iniciar(int? clienteId = null)
        {
            GrupoClienteLookup = _grupoClienteLookupProvider.RecuperaDadosPesquisa();

            var cliente = clienteId.HasValue
                ? _clienteDataProvider.ObterClienteById(clienteId.Value)
                : new Cliente { Endereco = new Endereco(), Emails = new List<EmailCliente>() };
          
            Cliente = new ClienteWrapper(cliente);
            Cliente.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Cliente.IsChanged)
                || e.PropertyName == nameof(Cliente.IsValid))
                {
                    InvalidarComandos();
                }
            };

            InvalidarComandos();

        }

        public ClienteWrapper Cliente
        {
            get { return _cliente; }
            private set
            {
                _cliente = value;
                OnPropertyChanged();
            }
        }

        public EmailClienteWrapper EmailSelecionado
        {
            get { return _emailSelecionado; }
            set
            {
                _emailSelecionado = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoverEmailCommand).RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<LookupItem> GrupoClienteLookup
        {
            get { return _grupoClientes; }
            set
            {
                _grupoClientes = value;
                OnPropertyChanged();
            }
        }

        private void InvalidarComandos()
        {
            ((DelegateCommand)SalvarCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)ExcluirCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NovoCommand).RaiseCanExecuteChanged();
        }
    }
}
