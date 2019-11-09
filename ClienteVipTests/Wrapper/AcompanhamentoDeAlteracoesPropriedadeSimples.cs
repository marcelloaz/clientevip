using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteVip.Model;
using ClienteVip.Wrapper;

namespace ClienteVipTests.Wrapper
{
    [TestClass]
    public class AcompanhamentoDeAlteracoesPropriedadeSimples
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
        public void AlteraValorOriginalArmazenado()
        {
            var wrapper = new ClienteWrapper(_cliente);
            Assert.AreEqual("José", wrapper.NomeOriginalValue);

            wrapper.Nome = "Maria";
            Assert.AreEqual("José", wrapper.NomeOriginalValue);
        }


        [TestMethod]
        public void ExibirFoiAlterado()
        {
            var wrapper = new ClienteWrapper(_cliente);
            Assert.IsFalse(wrapper.NomeIsChanged);
           // Assert.IsFalse(wrapper.IsChanged);

            wrapper.Nome = "Maria";
            Assert.IsTrue(wrapper.NomeIsChanged);
            //Assert.IsTrue(wrapper.IsChanged);

            wrapper.Nome = "José";
            Assert.IsFalse(wrapper.NomeIsChanged);
           // Assert.IsTrue(wrapper.IsChanged);
        }

        [TestMethod]
        public void DefineNovoValorParaPropriedaPropertyChangedEventModelNomeAlterado()
        {
            var fired = false;
            var wrapper = new ClienteWrapper(_cliente);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.NomeIsChanged))
                {
                    fired = true;
                }
            };
            wrapper.Nome = "Pedro";
            Assert.IsTrue(fired);
        }

    }
}
