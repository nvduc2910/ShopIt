using System;
using FluentValidation;
using ShopIt.Core.Models;
using ViewModels.Items;

namespace Validators
{
	public class CertValidator : AbstractValidator<CertTypeItemViewModel>
	{

		public CertValidator()
		{
			//For personal
			//for step 2
			RuleFor(obj => obj.CertItem.Identifier).NotNull().NotEmpty().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 1).WithMessage("Please enter Card Number");
			RuleFor(obj => obj.CertItem.Expiry).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 1).WithMessage("Please enter Card Expiry");
			RuleFor(obj => obj.CertItem.Document).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 1).WithMessage("Please Take Photo Of Card");

			//for step 3
			RuleFor(obj => obj.CertItem.Descriptor).NotNull().NotEmpty().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 2).WithMessage("Please enter Type Of Trade");
			RuleFor(obj => obj.CertItem.Identifier).NotNull().NotEmpty().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 2).WithMessage("Please enter Trade Licence Number");
			RuleFor(obj => obj.CertItem.Document).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 2).WithMessage("Please Take Photo Of Licence");
			RuleFor(obj => obj.CertItem.Expiry).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 2).WithMessage("Please enter Expiry date for Trade Licence Expiry");

			RuleFor(obj => obj.CertItem.Expiry).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 3).WithMessage("Please enter Expiry date for Income Insured");
			RuleFor(obj => obj.CertItem.Document).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 3).WithMessage("Please Take Photo Of Income Insured");

			RuleFor(obj => obj.CertItem.Expiry).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 4).WithMessage("Please enter Expiry date for First Aid Certified");
			RuleFor(obj => obj.CertItem.Document).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 4).WithMessage("Please Take Photo Of First Aid Certified");

			//for company
			RuleFor(obj => obj.CertItem.Descriptor).NotNull().NotEmpty().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 6).WithMessage("Please enter Type Of Other Certificate");
			RuleFor(obj => obj.CertItem.Expiry).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 6).WithMessage("Please enter Expiry date for Other Certificate");
			RuleFor(obj => obj.CertItem.Document).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 6).WithMessage("Please Take Photo Of Other Certificate");

			RuleFor(obj => obj.CertItem.Expiry).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 7).WithMessage("Please enter Expiry date for workers compensation");
			RuleFor(obj => obj.CertItem.Document).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 7).WithMessage("Please Take Photo Of workers compensation");

			RuleFor(obj => obj.CertItem.Expiry).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 8).WithMessage("Please enter Expiry date for Public Liability Insurance");
			RuleFor(obj => obj.CertItem.Document).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 8).WithMessage("Please Take Photo Of Public Liability Insurance");
			RuleFor(obj => obj.CertItem.Amount).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 8).WithMessage("Please enter Dollar value of cover for Public Liability Insurance");

			RuleFor(obj => obj.CertItem.Expiry).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 9).WithMessage("Please enter Expiry date for Product Liability Insurance");
			RuleFor(obj => obj.CertItem.Document).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 9).WithMessage("Please Take Photo Of Product Liability Insurance");
			RuleFor(obj => obj.CertItem.Amount).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 9).WithMessage("Please enter Dollar value of cover for Product Liability Insurance");

			RuleFor(obj => obj.CertItem.Descriptor).NotNull().NotEmpty().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 10).WithMessage("Please enter Type Of Licences");
			RuleFor(obj => obj.CertItem.Expiry).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 10).WithMessage("Please enter Expiry date for Licences");
			RuleFor(obj => obj.CertItem.Document).NotNull().When(CheckIfOneFilledByType).When(obj => obj.CertItem.CertTypeId == 10).WithMessage("Please Take Photo Of Licences");
		}

		private bool CheckIfOneFilledByType(CertTypeItemViewModel certTypeItem)
		{
			if (certTypeItem.CertItem.CertTypeId == 1)
			{
				if (certTypeItem.CertItem.Identifier == null || certTypeItem.CertItem.Identifier == String.Empty)
				{
					return true;
				}
				//else if (certTypeItem.CertItem.Expiry == null || certTypeItem.CertItem.Expiry == DateTimeOffset.MinValue)
				//{
				//	return true;
				//}
				else if (certTypeItem.CertItem.Document == null)
				{
					return true;
				}
				return false;
			}

			else if (certTypeItem.CertItem.CertTypeId == 2)
			{
				if ((certTypeItem.CertItem.Descriptor == null || certTypeItem.CertItem.Descriptor == "") &&
					(certTypeItem.CertItem.Identifier == null || certTypeItem.CertItem.Identifier == "") &&
					(certTypeItem.LocalPathImage == null))
				{
					return false;
				}
				if (certTypeItem.CertItem.Descriptor == null || certTypeItem.CertItem.Descriptor == string.Empty)
				{
					return true;
				}
				else if (certTypeItem.CertItem.Identifier == null || certTypeItem.CertItem.Identifier == string.Empty)
				{
					return true;
				}
				else if (certTypeItem.CertItem.Document == null)
				{
					return true;
				}
				else if (certTypeItem.CertItem.Expiry == null)
				{
					return true;
				}
				return false;
			}

			else if (certTypeItem.CertItem.CertTypeId == 6 || certTypeItem.CertItem.CertTypeId == 10)
			{
				if ((certTypeItem.CertItem.Descriptor == null || certTypeItem.CertItem.Descriptor == string.Empty)
					&& (certTypeItem.CertItem.Expiry == null)
					&& (certTypeItem.CertItem.Document == null))
				{
					return false;
				}

				if (certTypeItem.CertItem.Descriptor == null || certTypeItem.CertItem.Descriptor == string.Empty)
				{
					return true;
				}
				else if (certTypeItem.CertItem.Expiry == null)
				{
					return true;
				}
				else if (certTypeItem.CertItem.Document == null)
				{
					return true;
				}
			}

			else if (certTypeItem.CertItem.CertTypeId == 7 || certTypeItem.CertItem.CertTypeId == 3 || certTypeItem.CertItem.CertTypeId == 4)
			{
				if (certTypeItem.CertItem.Ticked == true)
				{
					if ((certTypeItem.CertItem.Expiry == null)
						&& (certTypeItem.CertItem.Document == null))
					{
						return false;
					}
					else if (certTypeItem.CertItem.Expiry == null)
					{
						return true;
					}
					else if (certTypeItem.CertItem.Document == null)
					{
						return true;
					}
				}
			}

			else if (certTypeItem.CertItem.CertTypeId == 8 || certTypeItem.CertItem.CertTypeId == 9)
			{
				if (certTypeItem.CertItem.Ticked == true)
				{
					if (certTypeItem.CertItem.Amount == null)
					{
						return true;
					}
					else if (certTypeItem.CertItem.Expiry == null)
					{
						return true;
					}
					else if (certTypeItem.CertItem.Document == null)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	public class PersonalProfileValidator : AbstractValidator<PersonalProfile>
	{
		public PersonalProfileValidator()
		{
			RuleFor(obj => obj.FirstName).NotEmpty().When(CheckInfoPersonalProfile).WithMessage("Please enter First Name");
			RuleFor(obj => obj.LastName).NotEmpty().When(CheckInfoPersonalProfile).WithMessage("Please enter Last Name");
			RuleFor(obj => obj.Email).NotEmpty().When(CheckInfoPersonalProfile).WithMessage("Please enter Email Address");
			RuleFor(obj => obj.Email).EmailAddress().WithMessage("Wrong Email format");
			RuleFor(obj => obj.DOB).NotEmpty().When(CheckInfoPersonalProfile).WithMessage("Please enter Last Name");
			RuleFor(obj => obj.Mobile).NotEmpty().When(CheckInfoPersonalProfile).WithMessage("Please enter Mobile Number");
			RuleFor(obj => obj.BulidingNo).NotEmpty().When(CheckInfoPersonalProfile).WithMessage("Please enter Unit/Dwelling Number");
			RuleFor(obj => obj.Street).NotEmpty().When(CheckInfoPersonalProfile).WithMessage("Please enter Street Name");
			RuleFor(obj => obj.PostCode).NotEmpty().When(CheckInfoPersonalProfile).WithMessage("Please enter Post Code");
			RuleFor(obj => obj.State).NotEmpty().When(CheckInfoPersonalProfile).WithMessage("Please enter State");
			RuleFor(obj => obj.NOK).NotEmpty().When(CheckInfoPersonalProfile).WithMessage("Please enter Full Name");
			RuleFor(obj => obj.NOKPhone).NotEmpty().When(CheckInfoPersonalProfile).WithMessage("Please enter Contact Number");
		}

		private bool CheckInfoPersonalProfile(PersonalProfile personalProfile)
		{
			if (personalProfile.FirstName == null || personalProfile.FirstName == string.Empty)
			{
				return true;
			}
			else if (personalProfile.LastName == null || personalProfile.LastName == string.Empty)
			{
				return true;
			}
			else if (personalProfile.Email == null || personalProfile.Email == string.Empty)
			{
				return true;
			}

			else if (personalProfile.DOB == null || personalProfile.DOB == DateTimeOffset.MinValue)
			{
				return true;
			}

			else if (personalProfile.Mobile == null || personalProfile.Mobile == string.Empty)
			{
				return true;
			}

			else if (personalProfile.BulidingNo == null || personalProfile.BulidingNo == string.Empty)
			{
				return true;
			}
			else if (personalProfile.Street == null || personalProfile.Street == string.Empty)
			{
				return true;
			}

			else if (personalProfile.PostCode == null || personalProfile.PostCode == string.Empty)
			{
				return true;
			}
			else if (personalProfile.State == null || personalProfile.PostCode == string.Empty)
			{
				return true;
			}

			else if (personalProfile.NOK == null || personalProfile.NOK == string.Empty)
			{
				return true;
			}
			else if (personalProfile.NOKPhone == null || personalProfile.NOKPhone == string.Empty)
			{
				return true;
			}
			return false;
		}
	}

	public class CompanyProfileValidator : AbstractValidator<Company>
	{
		public CompanyProfileValidator()
		{
			RuleFor(obj => obj.BusinessName).NotEmpty().When(CheckCompanyProfileInfo).WithMessage("Please enter Business Name");
			RuleFor(obj => obj.ABN).NotEmpty().When(CheckCompanyProfileInfo).WithMessage("Please enter ABN");
			RuleFor(obj => obj.Description).NotEmpty().When(CheckCompanyProfileInfo).WithMessage("Please enter Position/Job Title");
			RuleFor(obj => obj.BulidingNo).NotEmpty().When(CheckCompanyProfileInfo).WithMessage("Please enter Unit/Dwelling Number");
			RuleFor(obj => obj.Street).NotEmpty().When(CheckCompanyProfileInfo).WithMessage("Please enter Street Name");
			RuleFor(obj => obj.PostCode).NotEmpty().When(CheckCompanyProfileInfo).WithMessage("Please enter Post Code");
			RuleFor(obj => obj.State).NotEmpty().When(CheckCompanyProfileInfo).WithMessage("Please enter State");
		}

		public bool CheckCompanyProfileInfo(Company companyProfile)
		{
			if (companyProfile.BusinessName == null || companyProfile.BusinessName == string.Empty)
			{
				return true;
			}
			else if (companyProfile.ABN == null || companyProfile.ABN == string.Empty)
			{
				return true;
			}
			else if (companyProfile.JobTitle == null || companyProfile.JobTitle == string.Empty)
			{
				return true;
			}
			else if (companyProfile.AddressSameAs == false)
			{
				if (companyProfile.BulidingNo == null || companyProfile.BulidingNo == string.Empty)
				{
					return true;
				}
				else if (companyProfile.Street == null || companyProfile.Street == string.Empty)
				{
					return true;
				}
				else if (companyProfile.PostCode == null || companyProfile.PostCode == string.Empty)
				{
					return true;
				}
				else if (companyProfile.State == null || companyProfile.State == string.Empty)
				{
					return true;
				}
			}
			return false;
		}
	}
}
