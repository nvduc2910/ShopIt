using System;
using FluentValidation;
using ShopIt.Core.Models;
namespace ShopIt.Core.Validators
{
	public class ProjectValidator : AbstractValidator<Project>
	{
		public ProjectValidator()
		{
			RuleFor(ob => ob.Title).NotNull().WithMessage("Please enter title");
			RuleFor(ob => ob.Title).NotNull().WithMessage("Please enter title");

			RuleFor(ob => ob.Description).NotNull().WithMessage("Please enter description");
			RuleFor(ob => ob.Description).NotNull().WithMessage("Please enter description");
		}
	}
}
