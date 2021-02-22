using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MySqlDevices.Core.Entities;

namespace MySqlDevices.Core.Validation
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.MailAdress)
                .EmailAddress()
                .WithMessage("Valid email address is required");
        }
    }
}
