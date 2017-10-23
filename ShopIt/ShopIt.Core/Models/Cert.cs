using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ShopIt.Core.Models
{
	public class Cert
	{
		public long? Id { get; set; } // for GE
		public int UserId { get; set; }
		public int CertTypeId { get; set; }
		public CertType CertType { get; set; }

		/// Below fields may be optional or not display in the UI based on the CertType
		[JsonIgnoreAttribute]
		private string mDescriptor;

		public string Descriptor
		{
			get
			{
				if (mDescriptor == null)
				{
					mDescriptor = String.Empty;
				}
				return mDescriptor;
			}
			set
			{
				mDescriptor = value;
			}
		}

		[JsonIgnoreAttribute]
		private string mIdentifier;

		public string Identifier
		{
			get
			{
				if (mIdentifier == null)
				{
					mIdentifier = string.Empty;
				}
				return mIdentifier;
			}
			set
			{
				mIdentifier = value;
			}
		}
		public DateTimeOffset? Expiry { get; set; }

		[JsonIgnoreAttribute]
		public string ExpiryText
		{
			get
			{
				return Expiry == null ? "" : Expiry.Value.ToLocalTime().ToString("dd/MM/yyyy");
			}
		}

		public decimal? Amount { get; set; }


		[JsonIgnoreAttribute]
		public string AmoutText
		{
			get
			{
				return ConvertDecimalToString(Amount.ToString());
			}
		}

		public DateTimeOffset? Created { get; set; }
		public DateTimeOffset? Updated { get; set; }
		public bool? Ticked { get; set; }
		public Document Document { get; set; }

		#region Method

		public string ConvertDecimalToString(string value)
		{
			int count = 0;
			var valueStr = value.Replace(",", string.Empty).Replace("$", string.Empty);
			string temp = valueStr;
			for (int i = valueStr.Length; i > 0; i--)
			{
				count++;
				if (count == 3 && i != 1)
				{
					temp = temp.Insert(i - 1, ",");
					count = 0;
				}
			}
			return "$" + temp;
		}

		public Cert Clone()
		{
			Cert clone = new Cert();
			clone.Id = this.Id;
			clone.UserId = this.UserId;
			clone.CertTypeId = this.CertTypeId;
			clone.CertType = this.CertType;

			clone.Descriptor = this.Descriptor;
			clone.Identifier = this.Identifier;
			clone.Expiry = this.Expiry;

			clone.Expiry = this.Expiry;

			clone.Amount = this.Amount;
			clone.Created = this.Created;
			clone.Updated = this.Updated;
			clone.Ticked = this.Ticked;
			clone.Document = this.Document;

			return clone;
		}
		#endregion

	}

	public class CertType
	{
		public string Title { get; set; } //eg: Construction Induction Card,  Trade Contractor License, Operational Certificates et
		public bool UseTitleAsBannerGroup { get; set; } // eg: True on: Licenses, Operational Certificates et
		public bool EnforceExpiry { get; set; }  // Cert.Expiry cannot be Null
		public bool EnforceIdentifier { get; set; }
		public bool EnforceDescriptor { get; set; }
		public bool DisplayAsCheckbox { get; set; }
		public bool TickOnly { get; set; }  // Don't show the other fields - display as a checkbox
		public bool AmountPrompt { get; set; }  // Show the Amount fiel
		public bool RepeatForMultiples { get; set; }  // Show the ( + ) action to repeat the enabled fields.
		public bool DocumentOptional { get; set; } // Enforce validation on the supply of a document
		public ProfileCompletionStep DisplayStep { get; set; }  // During the create process - where in the form to displa
		public short DisplayOrder { get; set; } // Display order in the for
	}

}
