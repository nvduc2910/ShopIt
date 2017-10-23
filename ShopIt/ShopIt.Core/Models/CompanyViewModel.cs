using System;
using System.Collections.Generic;

namespace Swapcerts.Core.Models
{
	public class Company
	{
		public Company()
		{
			Certs = new List<Cert>();
		}

		public int UserId { get; set; 
		public ProfileCompletionStep ProfileCompletionStep { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string Other { get; set; }
		public bool UseBusinessName { get; set; }  // Display as BusinessName in public profil
		public string BusinessName { get; set; }
		public string ABN { get; set; }
		public bool? TaxRegistered { get; set; }  //GST registere
		public string TaxFileNumber { get; set; }  //TF
		public bool AddressSameAs { get; set; }  // Same as Personal Profil
		public string BulidingNo { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string CompanyType { get; set; }  // not use
		public string Website { get; set; }  // not use
		public string Description { get; set; }  // not use
		public string About { get; set; }  // not use
		public string Media { get; set; }  // not use
		public DateTimeOffset? StatementAgree { get; set; }

		public List<Cert> Certs { get; set; }  // each cert has a cert type and inferred layout.  The cert may point to a document (photo
	}
}
