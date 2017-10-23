using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShopIt.Core.Models
{
	public class PersonalProfile
	{
		public PersonalProfile()
		{
			Certs = new List<Cert>();
		}

		public int UserId { get; set; }
		public ProfileCompletionStep ProfileCompletionStep { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; } // not used in the UI
		public string Mobile { get; set; }
		public string Other { get; set; }  // not used in the UI
		public string PostCode { get; set; }
		public DateTimeOffset DOB { get; set; }
		public string TaxFileNumber { get; set; }
		public string NOK { get; set; }
		public string NOKPhone { get; set; }
		public string BulidingNo { get; set; }
		public string Street { get; set; }

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
		public string State { get; set; }
		public string About { get; set; }
		public string Media { get; set; }
		public DateTimeOffset? StatementAgree { get; set; }  //Step4
		public List<Cert> Certs { get; set; }  // each cert has a cert type and inferred layout.  The cert may point to a document (photo)

		public PersonalProfile Clone()
		{
			PersonalProfile clone = new PersonalProfile();

			clone.UserId = this.UserId;
			clone.ProfileCompletionStep = this.ProfileCompletionStep;
			clone.FirstName = this.FirstName;
			clone.Email = this.Email;
			clone.Mobile = this.Mobile;
			clone.Other = this.Other;
			clone.PostCode = this.PostCode;
			clone.DOB = this.DOB;
			clone.TaxFileNumber = this.TaxFileNumber;
			clone.NOK = this.NOK;
			clone.NOKPhone = this.NOKPhone;
			clone.BulidingNo = this.BulidingNo;
			clone.Street = this.Street;
			clone.LastName = this.LastName;

			clone.State = this.State;
			clone.About = this.About;
			clone.Media = this.Media;
			clone.StatementAgree = this.StatementAgree;

			clone.Certs = new List<Cert>();
			foreach (var cert in this.Certs)
			{
				clone.Certs.Add(cert.Clone());
			}

			return clone;
		}
	}

	public enum ProfileCompletionStep
	{
		NotStart = 0,
		PPStep1 = 1,
		PPStep2 = 2,
		PPStep3 = 3,
		PPStep4 = 4,
		PPComplete = 5,
		CPStep1 = 6,
		CPStep2 = 7,
		CPComplete = 8,
	}
}
