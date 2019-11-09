using Autofac;
using ClienteVip.DataAccess;
using ClienteVip.DataProvider;
using ClienteVip.DataProvider.Lookups;
using ClienteVip.Model;
using ClienteVip.ViewModel;
using Microsoft.Practices.Prism.PubSubEvents;

namespace ClienteVip.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<ClienteLookupProvider>().As<ILookupProvider<Cliente>>();
            builder.RegisterType<ArquivoDataService>().As<IDataService>();
            builder.RegisterType<GrupoClienteLookupProvider>().As<ILookupProvider<GrupoCliente>>();
            builder.RegisterType<ClienteDataProvider>().As<IClienteDataProvider>();

            builder.RegisterType<MenuPrincipalViewModel>().As<IMenuPrincipalViewModel>();
            builder.RegisterType<ClienteEditarViewModel>().As<IClienteEditarViewModel>();
            builder.RegisterType<MainViewModel>().AsSelf();
            return builder.Build();
        }
    }
}
