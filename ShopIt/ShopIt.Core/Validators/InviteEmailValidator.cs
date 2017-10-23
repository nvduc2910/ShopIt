using System;
using System.Collections.Generic;
using FluentValidation;
using ShopIt.Core.Models;

namespace ShopIt.Core.Validators
{
	public class InviteEmailValidator : AbstractValidator<InviteEmail>
	{
		public InviteEmailValidator()
		{
			RuleFor(obj => obj.Email).NotNull().WithMessage("Email can not empty");
			RuleFor(obj => obj.Email).EmailAddress().WithMessage("Wrong email format");

			RuleFor(obj =>obj).Must(CheckIfEmailNotExist).When(ob=>ob.Invites != null).WithMessage("Email has already invited");

			//RuleFor(obj => obj.Email).NotNull().NotEmpty().When(CheckIfOneFilled).WithMessage("sda");
		}

		//private bool CheckIfOneFilled(Cert certItem)
		//{
		//	if (certItem.Descriptor == null || certItem.Descriptor == string.Empty)
		//	{
		//		return true;
		//	}

		//}

		private bool CheckIfEmailNotExist(InviteEmail inviteEmail)
		{
			var exist = false;
			foreach (var invite in inviteEmail.Invites)
			{
				if (invite.InviteeEmail == inviteEmail.Email)
				{
					exist = true;
					break;
				}
			}

			return !exist;
		}
	}

	public class InviteEmail
	{
		public string Email;
		public List<Invite> Invites;

		public InviteEmail(string email, List<Invite> invites)
		{
			this.Email = email;
			this.Invites = invites;
		}
	}
}
