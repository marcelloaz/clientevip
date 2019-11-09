using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteVip.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClienteVip.Model;

namespace ClienteVip.Wrapper.Tests
{
    [TestClass()]
    public class BasicoTests
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

        [TestMethod()]
        public void VerificarModelPopulado()
        {
            var wrapper = new ClienteWrapper(_cliente);
            Assert.AreEqual(_cliente, wrapper.Model); 
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LancarExecaoCasoNullNoModel()
        {
            try
            {
                var wrapper = new ClienteWrapper(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("model", ex.ParamName);
                throw;
            }
        }

        [TestMethod()]
        public void RecuperarEAlteraValorParaPropriedadeModel()
        {
            var wrapper = new ClienteWrapper(_cliente);
            Assert.AreEqual(_cliente.Nome, wrapper.Nome);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void LancarExeptionCasoOEndereoDoClienteSejaNulo()
        {
            try
            {
                _cliente.Endereco = null;
                var wrapper = new ClienteWrapper(_cliente);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Informe o endereço.", ex.Message);
                throw;
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void LancarExeptionCasoOEmailDoClienteSejaNulo()
        {
            try
            {
                _cliente.Emails = null;
                var wrapper = new ClienteWrapper(_cliente);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Informe o Email.", ex.Message);
                throw;
            }
        }


        [TestMethod()]
        public void DefineNovoValorParaPropriedadeModel()
        {
            var wrapper = new ClienteWrapper(_cliente);
            wrapper.Nome = "Maria";
            Assert.AreEqual(wrapper.Nome, _cliente.Nome);
        }
    }
}