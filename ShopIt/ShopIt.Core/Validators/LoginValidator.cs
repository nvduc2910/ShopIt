using System;
using FluentValidation;
using ShopIt.Core.Models;

namespace Validators
{
	public class LoginValidator : AbstractValidator<Login>
	{
		public LoginValidator()
		{
			RuleFor(obj => obj.Email).EmailAddress().WithMessage("Wrong Format email");
			RuleFor(obj => obj.Password).NotNull().NotEmpty().WithMessage("Please enter your password");
		}
	}
}
