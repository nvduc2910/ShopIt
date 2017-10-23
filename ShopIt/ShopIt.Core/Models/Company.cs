using System.Collections.Generic;

namespace ShopIt.Core.Models
{
	public class Company
	{
		public Company()
		{
			Certs = new List<Cert>();
		}
		public int UserId { get; set; }
		public ProfileCompletionStep ProfileCompletionStep { get; set; }
		public string Email { get; set; }
		private string mMobile;
		public string Mobile
		{
			get
			{
				if (mMobile == null)
				{
					mMobile = string.Empty;
				}
				return mMobile;
			}
			set
			{
				mMobile = value;
			}
		}

		public string Other { get; set; }
		public bool UseBusinessName { get; set; }  // Display as BusinessName in public profile
		public string BusinessName { get; set; }
		public string ABN { get; set; }
		public bool? TaxRegistered { get; set; }  //GST registered
		public string TaxFileNumber { get; set; }  //TFN
		public bool AddressSameAs { get; set; }  // Same as Personal Profile
		public string BulidingNo { get; set; }
		public string Street { get; set; }
		public string PostCode { get; set; }
		private string mCity;
		public string City
		{
			get
			{
				if (mCity == null)
				{
					mCity = string.Empty;
				}
				return mCity;
			}
			set
			{
				mCity = value;
			}
		}

		private string mState;
		public string State
		{
			get
			{
				if (mState == null)
				{
					mState = string.Empty;
				}
				return mState;
			}
			set
			{
				mState = value;
			}
		}

		public string CompanyType { get; set; }  // not used
		public string Website { get; set; }  // not used
		public string Description { get; set; }  // not used
		public string About { get; set; }  // not used
		public string Media { get; set; }  // not used
		public System.DateTimeOffset? StatementAgree { get; set; }

		public List<Cert> Certs { get; set; }  // each cert has a cert type and inferred layout.  The cert may point to a document (photo)
		public string JobTitle { get; set; }

		public Company Clone()
		{
			Company clone = new Company();

			clone.UserId = this.UserId;

			clone.ProfileCompletionStep = this.ProfileCompletionStep;

			clone.Email = this.Email;

			clone.Mobile = this.Mobile;

			clone.Other = this.Other;

			clone.UseBusinessName = this.UseBusinessName;
			clone.BusinessName = this.BusinessName;
			clone.ABN = this.ABN;
			clone.TaxRegistered = this.TaxRegistered;
			clone.TaxFileNumber = this.TaxFileNumber;
			clone.AddressSameAs = this.AddressSameAs;
			clone.BulidingNo = this.BulidingNo;

			clone.Street = this.Street;
			clone.PostCode = this.PostCode;
			clone.City = this.City;

			clone.State = this.State;

			clone.CompanyType = this.CompanyType;
			clone.Website = this.Website;
			clone.Description = this.Description;

			clone.About = this.About;

			clone.Media = this.Media;

			clone.StatementAgree = this.StatementAgree;


			clone.Certs = new List<Cert>();
			foreach (var cert in this.Certs)
			{
				clone.Certs.Add(cert.Clone());
			}

			clone.JobTitle = this.JobTitle;

			return clone;
		}
	}
}
