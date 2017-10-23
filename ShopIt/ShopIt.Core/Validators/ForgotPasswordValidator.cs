using System;
using FluentValidation;

namespace Validators
{
	public class ForgotPasswordValidator : AbstractValidator<string>
	{
		public ForgotPasswordValidator()
		{
			RuleFor(email => email).NotNull().WithMessage("Email can not empty");
			RuleFor(email => email).EmailAddress().WithMessage("Wrong email format");
		}
	}
}
