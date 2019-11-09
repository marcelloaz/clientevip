using ClienteVip.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteVip.Wrapper
{
   public class EnderecoWrapper : ModelWrapper<Endereco>
    {
        public EnderecoWrapper(Endereco model) : base(model)
        {

        }

        public int Id {
            get { return GetValue<int>();}
            set { SetValue(value); }
        }
        
        [Required(ErrorMessage = "CEP requerido")]
        public string CEP
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CEPOriginalValue => GetOriginalValue<string>(nameof(CEP));
        public bool CEPIsChanged => GetIsChanged(nameof(CEP));

        public string Rua
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string RuaOriginalValue => GetOriginalValue<string>(nameof(Rua));
        public bool RuaIsChanged => GetIsChanged(nameof(Rua));


        public string Cidade
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CidadeOriginalValue => GetOriginalValue<string>(nameof(Cidade));
        public bool CidadeIsChanged => GetIsChanged(nameof(Cidade));

        public string Numero
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string NumeroOriginalValue => GetOriginalValue<string>(nameof(Numero));
        public bool NumeroIsChanged => GetIsChanged(nameof(Numero));

        public string Bairro
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string BairroOriginalValue => GetOriginalValue<string>(nameof(Bairro));
        public bool BairroIsChanged => GetIsChanged(nameof(Bairro));

        public string Complemento
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string ComplementoOriginalValue => GetOriginalValue<string>(nameof(Complemento));
        public bool ComplementoIsChanged => GetIsChanged(nameof(Complemento));

    }
}
