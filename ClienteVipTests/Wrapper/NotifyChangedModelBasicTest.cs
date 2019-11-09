using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteVip.Model;
using ClienteVip.Wrapper;

namespace ClienteVipTests.Wrapper
{
    /// <summary>
    /// Descrição resumida para NotifyChangedModelBasicTest
    /// </summary>
    [TestClass]
    public class NotifyChangedModelBasicTest
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
        public void DefineNovoValorParaPropriedaPropertyChangedEventModel()
        {
            var flag = false;
            var wrapper = new ClienteWrapper(_cliente);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Nome")
                {
                    flag = true;
                }
            };
            wrapper.Nome = "Maria";
            Assert.IsTrue(flag);
        }
    }
}
