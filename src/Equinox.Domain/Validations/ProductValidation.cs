using System;
using Equinox.Domain.Commands;
using FluentValidation;

namespace Equinox.Domain.Validations
{
    public abstract class ProductValidation<T> : AbstractValidator<T> where T : ProductCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }

        protected void ValidateCode()
        {
            RuleFor(c => c.Code)
                .NotEmpty().WithMessage("Please ensure you have entered the Code")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }


        protected void ValidateCategory()
        {
            RuleFor(c => c.Category)
                .NotEmpty().WithMessage("Please ensure you have entered the Category")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }


        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    
    }
}