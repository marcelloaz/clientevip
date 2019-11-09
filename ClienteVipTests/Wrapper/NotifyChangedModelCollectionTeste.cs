using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteVip.Model;
using ClienteVip.Wrapper;
using System.Linq;

namespace ClienteVipTests.Wrapper
{
    /// <summary>
    /// Descrição resumida para NotifyChangedModelCollectionTeste
    /// </summary>
    [TestClass]
    public class NotifyChangedModelCollectionTeste
    {
        private EmailCliente _emailCliente;
        private Cliente _cliente;

        [TestInitialize]
        public void Inicializador()
        {
             _emailCliente = new EmailCliente { Email = "imcello@hotmail.com" };
            _cliente = new Cliente()
            {
                Id = 1,
                Nome = "José",
                Endereco = new Endereco(),
                Emails = new List<EmailCliente>
                {
                    new EmailCliente {Email="marcelloaze@gmail.com" },
                    _emailCliente
                }
            };
        }

        [TestMethod]
        public void VerificarModelColecaoPopulado()
        {
            var wrapper = new ClienteWrapper(_cliente);
            Assert.IsNotNull(wrapper.Emails);
            VerificarSeModelColecaoEmailEstaSincronizado(wrapper);
        }

        [TestMethod]
        public void VerificarSincroniaModelRemoverEmailAposAtulizacaoEmail()
        {
           
            var wrapper = new ClienteWrapper(_cliente);
            var emailParaExclusao = wrapper.Emails.Single(ew => ew.Model == _emailCliente);
            wrapper.Emails.Remove(emailParaExclusao);
            VerificarSeModelColecaoEmailEstaSincronizado(wrapper);
        }

        [TestMethod]
        public void VerificarSincroniaModelAdicionarEmailAposAtulizacaoEmail()
        {
            _cliente.Emails.Remove(_emailCliente);
            var wrapper = new ClienteWrapper(_cliente);
            wrapper.Emails.Add(new EmailClienteWrapper(_emailCliente));
            VerificarSeModelColecaoEmailEstaSincronizado(wrapper);
        }

        private void VerificarSeModelColecaoEmailEstaSincronizado(ClienteWrapper wrapper)
        {
            Assert.AreEqual(_cliente.Emails.Count, wrapper.Emails.Count);
            Assert.IsTrue(_cliente.Emails.All(e => wrapper.Emails.Any(we => we.Model == e)));
        }
    }
}
