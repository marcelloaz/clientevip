using ClienteVip.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteVip.Wrapper
{
   public class EmailClienteWrapper : ModelWrapper<EmailCliente>
    {
        public EmailClienteWrapper(EmailCliente model) : base(model)
        {

        }

        public int IdOriginalValue => GetOriginalValue<int>(nameof(Id));

        public bool IdIsChanged => GetIsChanged(nameof(Id));

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        [Required(ErrorMessage = "Email requerido")]
        [EmailAddress(ErrorMessage = "Email com formato incorreto")]
        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string EmailOriginalValue => GetOriginalValue<string>(nameof(Email));

        public bool EmailIsChanged => GetIsChanged(nameof(Email));

        public string Comentario
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comentario));

        public bool CommentIsChanged => GetIsChanged(nameof(Comentario));
    }
}
