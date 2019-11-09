using ClienteVip.Model;
using ClienteVip.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClienteVip.Wrapper
{
    public class ClienteWrapper : ModelWrapper<Cliente>
    {
        public ClienteWrapper(Cliente model) : base(model)
        {
           
        }

        protected override void InicializarColecoes(Cliente model)
        {
            if (model.Emails == null)
                throw new ArgumentException("Informe o Email.");
            Emails = new ChangeTrackingCollection<EmailClienteWrapper>(
                    model.Emails.Select(e => new EmailClienteWrapper(e)));

            RegisterCollection(Emails, model.Emails);
        }


        protected override void InicializarPropriedadeComplexas(Cliente model)
        {
            if (model.Endereco == null)
                throw new ArgumentException("Informe o endereço.");

            Endereco = new EnderecoWrapper(model.Endereco);

            RegisterComplex(Endereco);

        }

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string Nome
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string NomeOriginalValue => GetOriginalValue<string>(nameof(Nome));
        public bool NomeIsChanged => GetIsChanged(nameof(Nome));

        public string Sobrenome
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string SobrenomeOriginalValue => GetOriginalValue<string>(nameof(Sobrenome));
        public bool SobrenomeIsChanged => GetIsChanged(nameof(Sobrenome));

        public int GrupoClienteId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int GrupoClienteIdOriginalValue => GetOriginalValue<int>(nameof(GrupoClienteId));
        public bool GrupoClienteIdIsChanged => GetIsChanged(nameof(GrupoClienteId));

        public DateTime? DataNascimento
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }

        public DateTime? DataNascimentoOriginalValue => GetOriginalValue<DateTime?>(nameof(DataNascimento));
        public bool DataNascimentoIsChanged => GetIsChanged(nameof(DataNascimento));

        public bool Vip
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool VipOriginalValue => GetOriginalValue<bool>(nameof(Vip));
        public bool VipIsChanged => GetIsChanged(nameof(Vip));

        public EnderecoWrapper Endereco { get; private set; }

        public ChangeTrackingCollection<EmailClienteWrapper> Emails { get; private set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                yield return new ValidationResult("Nome requerido",
                  new[] { nameof(Nome) });
            }
            if (Vip && Emails.Count == 0)
            {
                yield return new ValidationResult("Cliente vip deve possuir email",
                  new[] { nameof(Vip), nameof(Emails) });
            }
        }

    }
}
