using System;
using FluentValidation;

namespace Validators
{
	public class EmailValidator : AbstractValidator<string>
	{
		public EmailValidator()
		{
			RuleFor(email => email).NotNull().WithMessage("Email can not empty");
			RuleFor(email => email).EmailAddress().WithMessage("Wrong email format");
		}
	}
}
