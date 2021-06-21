﻿using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidatior : AbstractValidator<Message>
    {
        public MessageValidatior()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı adresini boş geçemezsiniz.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu adını boş geçemezsiniz.");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Mesaj kısmını boş geçemezsiniz.");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapınız.");
            RuleFor(x => x.Subject).MaximumLength(100).WithMessage("Lütfen en az 100 karakterden fazla değer girişi yapmayınız.");
        }
    }
}
