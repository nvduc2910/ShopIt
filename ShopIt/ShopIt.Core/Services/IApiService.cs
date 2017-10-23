using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using MvvmCross.Platform;
using Newtonsoft.Json;
using ShopIt.Core.Models;
using System.Collections.Generic;
using System.Globalization;

namespace ShopIt.Core.Services
{
	public interface IApiService
	{
		//UseBeaerToken
		bool UseBeaerToken { get; set; }

		// Authorization
		Task<APIResponse<UserData>> PostLogin(string email, string password);
		Task<APIResponse<string>> PostSignup(Register register);
		Task<APIResponse<string>> PostSession(string udid, string username);
		Task<APIResponse<string>> PostForgotPassword(string email);

		//Project
		Task<APIResponse<List<ProjectHeading>>> GetProjectList();
		Task<APIResponse<Project>> CreateProject(Project project);
		Task<APIResponse<string>> EditProject(long projectId, ProjectStage stage);
		Task<APIResponse<Project>> ViewProject(long projectId);
		Task<bool> UpdateProject(Project project);
		Task<bool> DeleteProject(long projectId);

		//Alert
		Task<APIResponse<List<Alert>>> GetAlertList();
		Task<APIResponse<object>> SettingAlert(long alertId, DateTimeOffset? read, DateTimeOffset? archived);


		//Document
		Task<APIResponse<List<Document>>> GetDocumentList();


		//Invite
		Task<bool> DeleteInvitee(long projectId, string email, int? inviteeUserId);
		Task<APIResponse<Invite>> AddInvitee(long projectId, string email, int? inviteeUserId);


		//Profile
		Task<APIResponse<UserProfile>> GetProfile();
		Task<APIResponse<string>> CreatePersonalProfile(PersonalProfile personalProfile);
		Task<APIResponse<string>> UpdatePersonalProfile(PersonalProfile personalProfile);
		Task<APIResponse<string>> UpdateCompanyProfile(Company companyProfile);
		Task<APIResponse<string>> CreateCompanyProfile(Company companyProfile);
		Task<APIResponse<UserProfile>> GetInviteeProfile(int inviteeId);

		//JoinProject
		Task<APIResponse<Alert>> JoinProject(long projectId, long alertId);

		//ShareProfile
		Task<bool> ShareProfile(long userId);

		//Notify
		Task<bool> NotifyDevice(string deviceUuid, string deviceToken);

		//Contact
		Task<APIResponse<List<Invite>>> GetContactList(string search);
	}

	public class APIResponse<T> where T : class
	{
		public HttpStatusCode StatusCode;
		public T Data;
		public string ErrorData;

		public APIResponse(T data, HttpStatusCode statusCode, string errorData = null)
		{
			this.StatusCode = statusCode;
			this.Data = data;
			this.ErrorData = errorData;
		}
	}

	public class ApiService : IApiService
	{
		#region mUserData

		private UserData mUserData
		{
			get
			{
				return Mvx.Resolve<ICacheService>().CurrentUserData;
			}
			set
			{
				Mvx.Resolve<ICacheService>().CurrentUserData = value;
			}
		}

		#endregion

		#region UseBeaerToken

		public bool UseBeaerToken { get; set; }

		#endregion

		#region BeaerToken

		private string mBeaerToken
		{
			get
			{
				if (UseBeaerToken && mUserData != null)
				{
					return mUserData.BearerToken;
				}
				return null;
			}
		}

		#endregion

		#region Base Handle

		#region CheckInternet

		private static bool CheckInternet()
		{
			if (!Mvx.Resolve<IPlatformService>().DetectInternerConnection())
			{
				Mvx.Resolve<IMessageboxService>().ShowToast("Cannot connect internet.\nPlease recheck your connection.");
				return false;
			}
			else
			{
				return true;
			}
		}

		#endregion

		#region CheckBearerToken

		async Task<bool> CheckBearerToken()
		{
			if (!UseBeaerToken)
				return true;

			if (mUserData.Expires < DateTimeOffset.Now)
			{
				try
				{
					var url = ApiUrls.ApiToken;

					var data = await url.PostUrlEncodedAsync(new
					{
						refresh_token = mUserData.RefreshToken,
						grant_type = "refresh_token",
						client_id = ApiConstants.ClientId,
						client_secret = ApiConstants.ClientSecret
					}).ReceiveJson<RefreshTokenResponseData>();

					if (data != null)
					{
						mUserData.BearerToken = data.access_token;
						mUserData.RefreshToken = data.refresh_token;
						mUserData.Expires = data.expires;
						mUserData = mUserData;
					}

					return true;
				}
				catch (FlurlHttpTimeoutException)
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
					Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
					return false;
				}
				catch (FlurlHttpException ex)
				{
#if DEBUG
					if (ex.Call.Response != null)
						Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
					else
						Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
					Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
					return false;
				}
				catch (Exception ex)
				{
					Mvx.Resolve<IMessageboxService>().ShowToast(ex.Message);
					return false;
				}
			}

			return true;
		}

		#endregion

		#region HandleForGET

		async Task<APIResponse<T>> HandleForGET<T>(Url url, bool needShowError = true) where T : class
		{
			if (!CheckInternet())
			{
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable);
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable);

			for (int countRetry = 1; countRetry <= ApiConstants.MaxRetryTime; countRetry++)
			{
				try
				{
					string data;
					HttpResponseMessage response;

					if (string.IsNullOrEmpty(mBeaerToken))
					{
						response = await url.GetAsync();
					}
					else
					{
						response = await url.WithOAuthBearerToken(mBeaerToken).GetAsync();
					}

					data = await response.Content.ReadAsStringAsync();
					if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(data))
					{
						T objecReturn = JsonConvert.DeserializeObject<T>(data);
						return new APIResponse<T>(objecReturn, HttpStatusCode.OK);
					}

					return new APIResponse<T>(null, response.StatusCode);
				}
				catch (FlurlHttpTimeoutException)
				{
					if (countRetry < ApiConstants.MaxRetryTime)
					{
						continue;
					}
					if (needShowError)
					{
#if DEBUG
						Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
						Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
					}

					return new APIResponse<T>(null, HttpStatusCode.RequestTimeout);
				}
				catch (FlurlHttpException ex)
				{
					if (countRetry < ApiConstants.MaxRetryTime)
					{
						continue;
					}

					if (ex.Call.Response != null)
					{
						if (needShowError)
						{
#if DEBUG
							Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
#else
							Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
						}
						return new APIResponse<T>(null, ex.Call.Response.StatusCode, ex.Call.ErrorResponseBody);
					}
					else
					{
						if (needShowError)
						{
#if DEBUG
							Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
							Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
						}
					}

					return new APIResponse<T>(null, HttpStatusCode.MethodNotAllowed);
				}
				catch (Exception exc)
				{
					if (countRetry < ApiConstants.MaxRetryTime)
					{
						continue;
					}
					if (needShowError)
					{
#if DEBUG
						Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
						Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
					}
				}
			}
			return new APIResponse<T>(null, (HttpStatusCode)1000);
		}

		#endregion

		#region HandleForPOST

		async Task<APIResponse<T>> HandleForPOST<T>(Url url, object jsonObject = null, bool needShowError = true) where T : class
		{
			if (!CheckInternet())
			{
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable);
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable);

			try
			{
				string data;
				HttpResponseMessage response;

				if (string.IsNullOrEmpty(mBeaerToken))
				{
					response = await url.PostJsonAsync(jsonObject);
				}
				else
				{
					response = await url.WithOAuthBearerToken(mBeaerToken).PostJsonAsync(jsonObject);
				}

				data = await response.Content.ReadAsStringAsync();
				if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(data))
				{
					T objecReturn = JsonConvert.DeserializeObject<T>(data);
					return new APIResponse<T>(objecReturn, HttpStatusCode.OK);
				}

				return new APIResponse<T>(null, response.StatusCode);
			}
			catch (FlurlHttpTimeoutException)
			{
				if (needShowError)
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
					Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				}

				return new APIResponse<T>(null, HttpStatusCode.RequestTimeout);
			}
			catch (FlurlHttpException ex)
			{
				if (ex.Call.Response != null)
				{
					if (needShowError)
					{
#if DEBUG
						Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
#else
						Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
					}
					return new APIResponse<T>(null, ex.Call.Response.StatusCode, ex.Call.ErrorResponseBody);
				}
				else
				{
					if (needShowError)
					{
#if DEBUG
						Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
						Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
					}
				}

				return new APIResponse<T>(null, HttpStatusCode.MethodNotAllowed);
			}
			catch (Exception exc)
			{
				if (needShowError)
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
					Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				}
			}
			return null;
		}

		async Task<bool> HandleForPOST(Url url, object jsonObject = null)
		{
			if (!CheckInternet())
			{
				return false;
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return false;

			try
			{

				if (string.IsNullOrEmpty(mBeaerToken))
				{
					await url.PostJsonAsync(jsonObject).ReceiveString();
				}
				else
				{
					await url.WithOAuthBearerToken(mBeaerToken).PostJsonAsync(jsonObject).ReceiveString();
				}

				return true;
			}
			catch (FlurlHttpTimeoutException)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			catch (FlurlHttpException ex)
			{
#if DEBUG
				if (ex.Call.Response != null)
					Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
				else
					Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			catch (Exception exc)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			return false;
		}

		#endregion

		#region HandleForPUT

		async Task<APIResponse<T>> HandleForPUT<T>(Url url, object jsonObject = null, bool needShowError = true) where T : class
		{
			if (!CheckInternet())
			{
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable); ;
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable);

			try
			{
				string data;
				HttpResponseMessage response;

				if (string.IsNullOrEmpty(mBeaerToken))
				{
					response = await url.PutJsonAsync(jsonObject);
				}
				else
				{
					response = await url.WithOAuthBearerToken(mBeaerToken).PutJsonAsync(jsonObject);
				}

				data = await response.Content.ReadAsStringAsync();
				if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(data))
				{
					T objecReturn = JsonConvert.DeserializeObject<T>(data);
					return new APIResponse<T>(objecReturn, HttpStatusCode.OK);
				}

				return new APIResponse<T>(null, response.StatusCode);
			}
			catch (FlurlHttpTimeoutException)
			{
				if (needShowError)
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
					Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				}

				return new APIResponse<T>(null, HttpStatusCode.RequestTimeout);
			}
			catch (FlurlHttpException ex)
			{
				if (ex.Call.Response != null)
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
#else
					Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
					return new APIResponse<T>(null, ex.Call.Response.StatusCode, ex.Call.ErrorResponseBody);
				}
				else
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
					Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				}

				return new APIResponse<T>(null, HttpStatusCode.MethodNotAllowed);
			}
			catch (Exception exc)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			return null;
		}

		async Task<bool> HandleForPUT(Url url, object jsonObject = null)
		{
			if (!CheckInternet())
			{
				return false;
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return false;

			try
			{
				if (string.IsNullOrEmpty(mBeaerToken))
				{
					await url.PutJsonAsync(jsonObject).ReceiveString();
				}
				else
				{
					await url.WithOAuthBearerToken(mBeaerToken).PutJsonAsync(jsonObject).ReceiveString();
				}

				return true;
			}
			catch (FlurlHttpTimeoutException)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			catch (FlurlHttpException ex)
			{
#if DEBUG
				if (ex.Call.Response != null)
					Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
				else
					Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			catch (Exception exc)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			return false;
		}

		#endregion

		#region HandleForDELETE

		async Task<bool> HandleForDELETE(Url url)
		{
			if (!CheckInternet())
			{
				return false;
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return false;

			try
			{
				if (string.IsNullOrEmpty(mBeaerToken))
				{
					await url.DeleteAsync();
				}
				else
				{
					await url.WithOAuthBearerToken(mBeaerToken).DeleteAsync();
				}

				return true;
			}
			catch (FlurlHttpTimeoutException)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			catch (FlurlHttpException ex)
			{
#if DEBUG
				if (ex.Call.Response != null)
					Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
				else
					Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			catch (Exception exc)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}

			return false;
		}

		async Task<HttpStatusCode> HandleStatusCodeForDELETE(Url url)
		{
			if (!CheckInternet())
			{
				return HttpStatusCode.ServiceUnavailable;
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return HttpStatusCode.ServiceUnavailable;

			try
			{
				HttpResponseMessage message = await url.DeleteAsync();

				return message.StatusCode;
			}
			catch (FlurlHttpTimeoutException)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				return HttpStatusCode.RequestTimeout;
			}
			catch (FlurlHttpException ex)
			{
				if (ex.Call.Response != null)
				{
					//Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
					return ex.Call.Response.StatusCode;
				}
				else
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else

					Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				}
			}
			catch (Exception exc)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}

			return (HttpStatusCode)1000;
		}

		#endregion

		#endregion

		#region Authorization

		#region Login

		public async Task<APIResponse<UserData>> PostLogin(string email, string password)
		{
			try
			{
				var url = ApiUrls.ApiToken;

				string data;
				HttpResponseMessage response;

				response = await url.PostUrlEncodedAsync(new
				{
					username = email,
					password = password,
					grant_type = "password",
					client_id = ApiConstants.ClientId,
					client_secret = ApiConstants.ClientSecret
				});

				data = await response.Content.ReadAsStringAsync();
				if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(data))
				{
					UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(data);
					UserData userData = new UserData
					{
						UserKey = userResponse.sessionUserKey,
						BearerToken = userResponse.access_token,
						UserId = userResponse.user_id,
						Email = userResponse.email_address,
						Expires = userResponse.expires,
						RefreshToken = userResponse.refresh_token,
						UserName = userResponse.username
					};

					return new APIResponse<UserData>(userData, response.StatusCode);
				}

				return new APIResponse<UserData>(null, response.StatusCode);
			}
			catch (FlurlHttpTimeoutException)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				return new APIResponse<UserData>(null, HttpStatusCode.RequestTimeout);
			}
			catch (FlurlHttpException ex)
			{

				if (ex.Call.Response != null)
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
#else
				
#endif
					return new APIResponse<UserData>(null, ex.Call.Response.StatusCode, ex.Call.ErrorResponseBody);
				}
				else
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
					Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
					return new APIResponse<UserData>(null, HttpStatusCode.MethodNotAllowed);
				}
			}
			catch (Exception ex)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast(ex.Message);
#else
				Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif

				return new APIResponse<UserData>(null, HttpStatusCode.MethodNotAllowed);
			}

			return new APIResponse<UserData>(null, (HttpStatusCode)1000);
		}

		#endregion

		#region Signup

		public async Task<APIResponse<string>> PostSignup(Register register)
		{
			var url = ApiUrls.Register;

			var data = await HandleForPOST<string>(url, register, false);

			return data;
		}

		#endregion

		#region Session

		public async Task<APIResponse<string>> PostSession(string udid, string username)
		{
			var url = ApiUrls.Session.SetQueryParams(new
			{
				udid = udid,
				username = username
			});

			var data = await HandleForPOST<string>(url);

			return data;
		}

		#endregion

		#region ForgotPassword

		public async Task<APIResponse<string>> PostForgotPassword(string emailForPassword)
		{
			var url = ApiUrls.ForgotPassword.SetQueryParams(new
			{
				email = emailForPassword,
			});

			var data = await HandleForPOST<string>(WebUtility.UrlDecode(url));
			return data;
		}

		#endregion


		#endregion

		#region GetProjectList

		public async Task<APIResponse<List<ProjectHeading>>> GetProjectList()
		{
			var url = ApiUrls.ProjectList.SetQueryParams(new
			{
				userKey = mUserData.UserKey
			});

			var data = await HandleForGET<List<ProjectHeading>>(url);

			return data;
		}

		#endregion

		#region CreateProject
		public async Task<APIResponse<Project>> CreateProject(Project project)
		{
			var url = ApiUrls.Project.SetQueryParams(new
			{
				userKey = mUserData.UserKey
			});

			var data = await HandleForPOST<Project>(url, project);
			return data;
		}

		#endregion

		#region EditProject
		public async Task<APIResponse<string>> EditProject(long projectId, ProjectStage stage)
		{
			var url = ApiUrls.Project.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
				projectId = projectId,
				stage = stage,
			});

			var data = await HandleForPUT<string>(url);
			return data;
		}


		#endregion

		#region ViewProject
		public async Task<APIResponse<Project>> ViewProject(long projectId)
		{
			var url = ApiUrls.Project.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
				projectId = projectId
			});

			var data = await HandleForGET<Project>(url);
			return data;
		}
		#endregion

		#region GetAlertList


		public async Task<APIResponse<List<Alert>>> GetAlertList()
		{
			var url = ApiUrls.Alert.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
			});

			var data = await HandleForGET<List<Alert>>(url);
			return data;
		}


		#endregion

		#region  SettingAlert

		public async Task<APIResponse<object>> SettingAlert(long alertId, DateTimeOffset? read, DateTimeOffset? archived)
		{
			var url = ApiUrls.Alert.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
				alertId = alertId,
				read = read == null ? "" : read.Value.ToString(new CultureInfo("en-US")),
				archived = archived == null ? "" : archived.Value.ToString(new CultureInfo("en-US")),
			});

			var data = await HandleForPUT<object>(url);
			return data;
		}

		#endregion

		#region GetDocumentList 

		public async Task<APIResponse<List<Document>>> GetDocumentList()
		{
			var url = ApiUrls.DocumentList.SetQueryParams(new
			{
				userKey = mUserData.UserKey,

			});

			var data = await HandleForGET<List<Document>>(url);
			return data;
		}

		#endregion

		#region DeleteInvitee

		public async Task<bool> DeleteInvitee(long projectId, string email, int? inviteeUserId)
		{
			var url = ApiUrls.DeleteInvitee.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
				projectId = projectId,
				email = email,
				inviteeUserId = inviteeUserId == null ? "" : inviteeUserId.Value.ToString()
			});

			var data = await HandleForDELETE(url);
			return data;
		}
		#endregion

		#region AddInvitee

		public async Task<APIResponse<Invite>> AddInvitee(long projectId, string email, int? inviteeUserId)
		{
			var url = ApiUrls.AddInvitee.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
				projectId = projectId,
				email = (email == null) ? "" : email,
				inviteeUserId = (inviteeUserId == null) ? "" : inviteeUserId.Value.ToString()
			});

			var data = await HandleForPOST<Invite>(url, null, false);

			return data;
		}
		#endregion

		#region GetPersonalProfile

		public async Task<APIResponse<UserProfile>> GetProfile()
		{
			var url = ApiUrls.GetProfile.SetQueryParams(new
			{
				userKey = mUserData.UserKey,

			});

			var data = await HandleForGET<UserProfile>(url);
			return data;

		}
		#endregion

		#region UpdatePersonalProfile

		public async Task<APIResponse<string>> UpdatePersonalProfile(PersonalProfile personalProfile)
		{
			var url = ApiUrls.PersonalProfile.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
			});

			var data = await HandleForPUT<string>(url, personalProfile);
			return data;
		}

		#endregion

		#region UpdateCompanyProfile

		public async Task<APIResponse<string>> UpdateCompanyProfile(Company companyProfile)
		{
			var url = ApiUrls.CompanyProfile.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
			});

			var data = await HandleForPUT<string>(url, companyProfile);
			return data;
		}

		#endregion

		#region GetInviteProfile

		public async Task<APIResponse<UserProfile>> GetInviteeProfile(int inviteeId)
		{
			var url = ApiUrls.GetInvitee.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
				inviteeUserId = inviteeId,

			});

			var data = await HandleForGET<UserProfile>(url);
			return data;

		}
		#endregion

		#region CreatePersonalProfile

		public async Task<APIResponse<string>> CreatePersonalProfile(PersonalProfile personalProfile)
		{
			var url = ApiUrls.PersonalProfile.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
			});
			var data = await HandleForPOST<string>(url, personalProfile);
			return data;
		}

		#endregion

		#region CreateCompanyProfile


		public async Task<APIResponse<string>> CreateCompanyProfile(Company companyProfile)
		{

			var url = ApiUrls.CompanyProfile.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
			});

			var data = await HandleForPOST<string>(url, companyProfile);
			return data;
		}

		#endregion

		#region JoinProject

		public async Task<APIResponse<Alert>> JoinProject(long projectId, long alertId)
		{
			var url = ApiUrls.JoinProject.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
				projectId = projectId,
				alertId = alertId,
			});
			var data = await HandleForPOST<Alert>(url);
			return data;
		}
		#endregion

		#region UpdateProject

		public async Task<bool> UpdateProject(Project project)
		{
			var url = ApiUrls.Project.SetQueryParams(new
			{
				userKey = mUserData.UserKey
			});

			var data = await HandleForPUT(url, project);
			return data;
		}

		#endregion

		#region DeleteProject

		public async Task<bool> DeleteProject(long projectId)
		{
			var url = ApiUrls.Project.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
				projectId = projectId
			});

			var data = await HandleForDELETE(url);
			return data;
		}


		#endregion

		#region ShareProfile

		public async Task<bool> ShareProfile(long userId)
		{
			var url = ApiUrls.ShareProfile.SetQueryParams(new
			{
				userId = userId
			});

			var data = await HandleForPOST(url);
			return data;
		}

		#endregion

		#region NotifyDevice
		public async Task<bool> NotifyDevice(string deviceUuid, string deviceToken)
		{
			var url = ApiUrls.NotifyDevice.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
				deviceUuid = deviceUuid,
				deviceToken = deviceToken,
			});

			var data = await HandleForPOST(url);
			return data;
		}
		#endregion

		#region GetContactList

		public async Task<APIResponse<List<Invite>>> GetContactList(string search)
		{
			var url = ApiUrls.Contacts.SetQueryParams(new
			{
				userKey = mUserData.UserKey,
				search = search
			});

			var data = await HandleForGET<List<Invite>>(url, false);

			return data;
		}

		#endregion
	}
}
