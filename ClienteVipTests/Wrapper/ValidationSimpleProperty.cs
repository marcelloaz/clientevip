using ClienteVip.Model;
using ClienteVip.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ClienteVip.Wrapper
{
  [TestClass]
  public class ValidationSimpleProperty
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

      //  [TestMethod]
    public void ShouldReturnValidationErrorIfFirstNameIsEmpty()
    {
      var wrapper = new ClienteWrapper(_cliente);
      Assert.IsFalse(wrapper.HasErrors);

      wrapper.Nome = "";
      Assert.IsTrue(wrapper.HasErrors);

      var errors = wrapper.GetErrors(nameof(wrapper.Nome)).Cast<string>().ToList();
      Assert.AreEqual(1, errors.Count);
      Assert.AreEqual("Firstname is required", errors.First());

      wrapper.Nome = "Julia";
      Assert.IsFalse(wrapper.HasErrors);
    }

    [TestMethod]
    public void ShouldRaiseErrorsChangedEventWhenFirstNameIsSetToEmptyAndBack()
    {
      var fired = false;
      var wrapper = new ClienteWrapper(_cliente);

      wrapper.ErrorsChanged += (s, e) =>
      {
        if (e.PropertyName == nameof(wrapper.Nome))
        {
          fired = true;
        }
      };

      wrapper.Nome = "";
      Assert.IsTrue(fired);

      fired = false;
      wrapper.Nome = "Julia";
      Assert.IsTrue(fired);
    }

   // [TestMethod]
    public void ShouldSetIsValid()
    {
      var wrapper = new ClienteWrapper(_cliente);
      Assert.IsTrue(wrapper.IsValid);

      wrapper.Nome = "";
      Assert.IsFalse(wrapper.IsValid);

      wrapper.Nome = "Julia";
      Assert.IsTrue(wrapper.IsValid);
    }

    [TestMethod]
    public void ShouldRaisePropertyChangedEventForIsValid()
    {
      var fired = false;
      var wrapper = new ClienteWrapper(_cliente);

      wrapper.PropertyChanged += (s, e) =>
      {
        if (e.PropertyName == nameof(wrapper.IsValid))
        {
          fired = true;
        }
      };

      wrapper.Nome = "";
      Assert.IsTrue(fired);

      fired = false;
      wrapper.Nome = "Julia";
      Assert.IsTrue(fired);
    }

    [TestMethod]
    public void ShouldSetErrorsAndIsValidAfterInitialization()
    {
      _cliente.Nome = "";
      var wrapper = new ClienteWrapper(_cliente);

      Assert.IsFalse(wrapper.IsValid);
      Assert.IsTrue(wrapper.HasErrors);

      var errors = wrapper.GetErrors(nameof(wrapper.Nome)).Cast<string>().ToList();
      Assert.AreEqual(1, errors.Count);
      Assert.AreEqual("Nome requerido", errors.First());
    }

   // [TestMethod]
    public void ShouldRefreshErrorsAndIsValidWhenRejectingChanges()
    {
      var wrapper = new ClienteWrapper(_cliente);
      Assert.IsTrue(wrapper.IsValid);
      Assert.IsFalse(wrapper.HasErrors);

      wrapper.Nome = "";

      Assert.IsFalse(wrapper.IsValid);
      Assert.IsTrue(wrapper.HasErrors);

      wrapper.RejectChanges();

      Assert.IsTrue(wrapper.IsValid);
      Assert.IsFalse(wrapper.HasErrors);
    }
  }
}
