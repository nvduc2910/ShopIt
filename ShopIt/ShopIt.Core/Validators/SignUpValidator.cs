
using System.Text.RegularExpressions;
using FluentValidation;
using ShopIt.Core.Models;

namespace Validators
{
	public class SignUpValidator : AbstractValidator<Register>
	{
		public SignUpValidator()
		{
			RuleFor(obj => obj.Email).NotNull().NotEmpty().WithMessage("Please enter email");
			RuleFor(obj => obj.Email).EmailAddress().WithMessage("Wrong email format");
			RuleFor(obj => obj.Password).NotNull().NotEmpty().WithMessage("Passwords must have at least one non letter");

			RuleFor(obj => obj.Password).Must(HaveOneLowCase).WithMessage("Passwords must have at least one lowercase");
			RuleFor(obj => obj.Password).Must(HaveOneUpperCase).WithMessage("Passwords must have at least one uppercase");
			RuleFor(obj => obj.Password).Must(HaveOneNumber).WithMessage("Passwords must have at least one digit");

			RuleFor(obj => obj.ConfirmPassword).NotNull().NotEmpty().WithMessage("Please enter confirm password");
			RuleFor(obj => obj.Password).Equal(obj => obj.ConfirmPassword).WithMessage("Confirm password dont match");

		}

		public bool HaveOneLowCase(string passWord)
		{
			if (passWord != null && passWord != "")
			{
				Regex regex = new Regex("[a-z]");
				return regex.IsMatch(passWord);
			}
			return false;
		}


		public bool HaveOneUpperCase(string passWord)
		{
			if (passWord != null && passWord != "")
			{
				for (int i = 0; i < passWord.Length; i++)
				{
					if (char.IsUpper(passWord[i]))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		public bool HaveOneNumber(string passWord)
		{
			if (passWord != null && passWord != "")

			{
				Regex regex = new Regex("[0-9]");
				return regex.IsMatch(passWord);
			}
			return false;
		}

	}
}
