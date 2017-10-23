using System;
namespace ShopIt.Core
{
	public static class ApiConstants
	{
		public const int MaxRetryTime = 2;
		public static string ClientId = "ShopIt-api-client_1607859";
		public static string ClientSecret = "ShopIt-api-plsecrect_80d3da4d89a6311";

#if DEBUG
		//public const string AzureBlobUrl = "https://parkfitstore.blob.core.windows.net/ShopIt";
		//public const string AzureContainer = "ShopIt";

		//public const string AzureAccountName = "parkfitstore";
		//public const string AzureKey = "DZqx/7Fd/zBEaugRZTwjGnRtgdvAZqA9LzEVdNtV/bAYpzjg1Lx+Kx7oACb/LjhFswqHB9qWWI2SaNMTl1Ivqg==";
		//public const string AzureSAS = "?sv=2015-12-11&ss=bfqt&srt=sco&sp=rwdlacup&se=2057-02-11T02:55:24Z&st=2017-02-08T18:55:24Z&spr=https&sig=BeRD9tusNDKJqHJXhw2GVQDLa6nLNxxDz184uV3IymM%3D";

		public const string AzureBlobUrl = "https://ShopIt.blob.core.windows.net/ShopItstore";
		public const string AzureContainer = "ShopItstore";

		public const string AzureAccountName = "ShopIt";
		public const string AzureKey = "4CtSOtEZOYsBaeNAtLc1SCWPUFxne52RwJLsePUPdfxKqc4dsG4or2I3ND46RZUHdadC3xlc1oEt4sD+QeoXPA==";
		public const string AzureSAS = "?sv=2015-12-11&ss=b&srt=sco&sp=rwdlac&se=2050-01-18T17:56:31Z&st=2017-01-16T09:56:31Z&spr=https&sig=%2BK26UjKhOHAMYZFAyaV93eAr1%2FzjcM8wuRhMRQW51%2FM%3D";

#else
		//public const string AzureBlobUrl = "https://parkfitstore.blob.core.windows.net/ShopIt";
		//public const string AzureContainer = "ShopIt";

		//public const string AzureAccountName = "parkfitstore";
		//public const string AzureKey = "DZqx/7Fd/zBEaugRZTwjGnRtgdvAZqA9LzEVdNtV/bAYpzjg1Lx+Kx7oACb/LjhFswqHB9qWWI2SaNMTl1Ivqg==";
		//public const string AzureSAS = "?sv=2015-12-11&ss=bfqt&srt=sco&sp=rwdlacup&se=2057-02-11T02:55:24Z&st=2017-02-08T18:55:24Z&spr=https&sig=BeRD9tusNDKJqHJXhw2GVQDLa6nLNxxDz184uV3IymM%3D";

		public const string AzureBlobUrl = "https://ShopIt.blob.core.windows.net/ShopItstore";
		public const string AzureContainer = "ShopItstore";

		public const string AzureAccountName = "ShopIt";
		public const string AzureKey = "4CtSOtEZOYsBaeNAtLc1SCWPUFxne52RwJLsePUPdfxKqc4dsG4or2I3ND46RZUHdadC3xlc1oEt4sD+QeoXPA==";
		public const string AzureSAS = "?sv=2015-12-11&ss=b&srt=sco&sp=rwdlac&se=2050-01-18T17:56:31Z&st=2017-01-16T09:56:31Z&spr=https&sig=%2BK26UjKhOHAMYZFAyaV93eAr1%2FzjcM8wuRhMRQW51%2FM%3D";
#endif

	}

	public static class ApiUrls
	{
		//public const string Root = "http://ShopIt-api-dev.azurewebsites.net";
	    public const string Root = "http://ShopIt.azurewebsites.net";

		public const string ApiV1 = Root + "/api/v1";
		public const string Api = Root + "/api";

		//Authorization
		public const string Register = Api + "/Account/Register";
		//public const string ApiToken = "http://ShopIt-api-dev.azurewebsites.net/token";

		public const string ApiToken = Root + "/token";
		public const string Session = ApiV1 + "/Session";
		public const string ForgotPassword = Api + "/Account/ForgotPassword";


		//Project 
		public const string ProjectList = ApiV1 + "/ProjectList";
		public const string Project = ApiV1 + "/Project";

		//JoinProject
		public const string JoinProject = ApiV1 + "/JoinProject";

		//Alert
		public const string Alert = ApiV1 + "/Alert";

		//Document
		public const string DocumentList = ApiV1 + "Document";

		//Invites
		public const string DeleteInvitee = ApiV1 + "/Invite";
		public const string AddInvitee = ApiV1 + "/Invite";
		public const string GetInvitee = ApiV1 + "/Invite";

		//Profile
		public const string GetProfile = ApiV1 + "/UserProfile";
		public const string PersonalProfile = ApiV1 + "/PersonalProfile";
		public const string CompanyProfile = ApiV1 + "/Company";

		//ShareProfile
		public const string ShareProfile = ApiV1 + "/Report";

		//Notify Device
		public const string NotifyDevice = ApiV1 + "/NotifyDevice";

		//Contacts
		public const string Contacts = ApiV1 + "/Contacts";
	}


	public static class Messages
	{
		public const string ForgotPasswordSuccess = "Please check your email for the reset link.";
		public const string FotgotPasswordFail = "Your email not exist";

		// Create Project
		public const string EmptyProjectName = "Please enter project name";
		public const string EmptyDescriptionProjectName = "Please enter project description";
		public const string SaveDescriptionSuccess = "Save successful";
	}
}
