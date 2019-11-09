using Autofac;
using ClienteVip.Startup;
using ClienteVip.View;
using ClienteVip.ViewModel;
using System.Windows;

namespace ClienteVip
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new Bootstrapper();
            IContainer container = bootstrapper.Bootstrap();

            var mainViewModel = container.Resolve<MainViewModel>();
            MainWindow = new MainWindow(mainViewModel);
            MainWindow.Show();
            mainViewModel.Iniciar();
        }
    }
}
