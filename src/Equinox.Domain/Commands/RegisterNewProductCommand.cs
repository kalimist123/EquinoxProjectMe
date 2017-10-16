using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class RegisterNewProductCommand : ProductCommand
    {
        public RegisterNewProductCommand(string name, string code, string category)
        {
            Name = name;
            Code=code;
            Category=category;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}