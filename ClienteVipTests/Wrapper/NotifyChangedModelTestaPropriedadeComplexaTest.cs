using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteVip.Model;
using ClienteVip.Wrapper;

namespace ClienteVipTests.Wrapper
{

    [TestClass]
    public class NotifyChangedModelTestaPropriedadeComplexaTest
    {
        private Cliente _cliente;

        [TestInitialize]
        public void Inicializador()
        {
            _cliente = new Cliente()
            {
                Id = 1,
                Nome = "José",
                Endereco = new Endereco(),
                Emails = new List<EmailCliente>()
            };
        }
        [TestMethod]
       public void DefineEnderecoInicializado()
        {
            var wrapper = new ClienteWrapper(_cliente);
            Assert.AreEqual(_cliente.Endereco, wrapper.Endereco.Model);

        }
    }
}
