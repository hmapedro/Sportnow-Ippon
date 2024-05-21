﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using SportNow.Model;
using SportNow.Views;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http.Headers;

namespace SportNow.Services.Data.JSON
{
	public class MemberManager
	{
		//IRestService restService;

		HttpClient client;

		public List<Member> members;
		public ObservableCollection<Member> members_observable;

		public List<Payment> payments { get; private set; }


		public MemberManager()
		{
			HttpClientHandler clientHandler = new HttpClientHandler();
			clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			client = new HttpClient(clientHandler);

			/*HttpMessageHandler httpMessageHandler = GetPlatformMessageHandler();
            client = new HttpClient(httpMessageHandler);*/

        }

        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
#if NET6_0
            var handler = new CustomAndroidMessageHandler();
#elif NET7_0_OR_GREATER
            var handler = new Xamarin.Android.Net.AndroidMessageHandler();
#endif
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
#elif IOS
            var handler = new NSUrlSessionHandler
            {
                TrustOverrideForUrl = IsHttpsLocalhost
            };
            return handler;
#elif WINDOWS || MACCATALYST
            return null;
#else
                throw new PlatformNotSupportedException("Only Android, iOS, MacCatalyst, and Windows supported.");
#endif
        }

#if ANDROID && NET6_0
    internal sealed class CustomAndroidMessageHandler : Xamarin.Android.Net.AndroidMessageHandler
    {
        protected override Javax.Net.Ssl.IHostnameVerifier GetSSLHostnameVerifier(Javax.Net.Ssl.HttpsURLConnection connection)
            => new CustomHostnameVerifier();

        private sealed class CustomHostnameVerifier : Java.Lang.Object, Javax.Net.Ssl.IHostnameVerifier
        {
            public bool Verify(string hostname, Javax.Net.Ssl.ISSLSession session)
            {
                return Javax.Net.Ssl.HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session) ||
                    hostname == "10.0.2.2" && session.PeerPrincipal?.Name == "CN=localhost";
            }
        }
    }
#elif IOS
        public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
        {
            if (url.StartsWith("https://localhost"))
                return true;
            return false;
        }
#endif

        public async Task<int> updateToken(string memberid, string token)
		{
			Debug.WriteLine("MemberManager.updateToken "+ memberid+" "+ token);
			if ((memberid == null) | (token == null) | (memberid == "") | (token == ""))
			{
				Debug.WriteLine("MemberManager.updateToken memberid or token empty");
				return -4;
			}
			Uri uri = new Uri(string.Format(Constants.RestUrl_Update_Token + "?userid=" + memberid + "&token=" + token, string.Empty));
			try
			{
				HttpResponseMessage response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					//return true;
					string content = await response.Content.ReadAsStringAsync();
					Debug.WriteLine("content=" + content);
					List<Login> loginResultList = JsonConvert.DeserializeObject<List<Login>>(content);
					return loginResultList[0].login;
				}
				else
				{
					Debug.WriteLine("login not ok");
					return -2;
				}
			}
			catch
			{
				Debug.WriteLine("http request error");
				return -3;
			}
		}


		/*public async Task<string> Login(User user)
		{
			var return_value = "0";
			Debug.WriteLine("Login");
			Uri uri = new Uri(string.Format(Constants.RestUrl_Login + "?username=" + user.Username + "&password=" + user.Password, string.Empty));
			try
			{

				return_value = "-3";
				HttpResponseMessage response = await client.GetAsync(uri);
				return_value = "-4";
				if (response.IsSuccessStatusCode)
				{
					//return true;
					return_value = "-5";
					string content = await response.Content.ReadAsStringAsync();
					return_value = content;
					Debug.WriteLine("content=" + content);
					List<Login> loginResultList= JsonConvert.DeserializeObject<List<Login>>(content);
					Debug.WriteLine("loginResultList=" + loginResultList[0].login);
					return_value = "-7";
					return "1";
				}
				else
				{
					Debug.WriteLine("login not ok");
					return "-2";
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine("http request error "+ e.ToString());
				return return_value+ e.ToString();
			}
		}*/

		public async Task<string> Login(User user)
		{
			Debug.WriteLine("Login "+ Constants.RestUrl_Login + "?username=" + user.Username + "&password=" + user.Password);
			Uri uri = new Uri(string.Format(Constants.RestUrl_Login + "?username=" + user.Username + "&password=" + user.Password, string.Empty));
			try
			{
				HttpResponseMessage response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					//return true;
					string content = await response.Content.ReadAsStringAsync();
					Debug.WriteLine("content=" + content);
					List<Login> loginResultList = JsonConvert.DeserializeObject<List<Login>>(content);
					return Convert.ToString(loginResultList[0].login);
				}
				else
				{
					Debug.WriteLine("login not ok");
					return "-2";
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine("http request error");
				return "-3 "+e.Message +" "+e.InnerException.Message; ;
			}
		}

		public async Task<List<Member>> GetMembers(User user)
		{
			Debug.WriteLine("GetMembers "+ user.Username);
			Uri uri = new Uri(string.Format(Constants.RestUrl_Get_Member_Info + "?username=" + user.Username ));
			try
			{
				HttpResponseMessage response = await client.GetAsync(uri);
			

				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine("login ok AQUI");
					string content = await response.Content.ReadAsStringAsync();
					Debug.WriteLine("content = "+ content);
					members = JsonConvert.DeserializeObject<List<Member>>(content);

				}
				else
				{
					Debug.WriteLine("login not ok");
				}
				return members;
			}
			catch (Exception e)
			{
				Debug.WriteLine("http request error");
				Debug.Print(e.StackTrace);
				return null;
			}

		}

		public async Task<List<Member>> GetMemberStudents(string memberid)
		{
			Debug.WriteLine("GetMemberStudents "+ Constants.RestUrl_Get_Member_Students_Info + "?userid=" + memberid);
			Uri uri = new Uri(string.Format(Constants.RestUrl_Get_Member_Students_Info + "?userid=" + memberid));
			try
			{
				HttpResponseMessage response = await client.GetAsync(uri);


				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					Debug.WriteLine("content = " + content);
					members = JsonConvert.DeserializeObject<List<Member>>(content);

				}
				else
				{
					Debug.WriteLine("login not ok");
				}
				return members;
			}
			catch (Exception e)
			{
				Debug.WriteLine("http request error");
				Debug.Print(e.StackTrace);
				return null;
			}

		}

		public async Task<List<Member>> GetStudentsDojo(string memberid, string dojo)
		{
			Debug.WriteLine("GetStudentsDojo dojo="+dojo);
			Debug.Print(Constants.RestUrl_Get_Students_Dojo_Info + "?userid=" + memberid + "&dojo=" + dojo);
			Uri uri = new Uri(string.Format(Constants.RestUrl_Get_Students_Dojo_Info + "?userid=" + memberid+ "&dojo="+dojo));
			try
			{
				HttpResponseMessage response = await client.GetAsync(uri);


				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					Debug.WriteLine("content = " + content);
					members= JsonConvert.DeserializeObject<List<Member>>(content);

				}
				else
				{
					Debug.WriteLine("login not ok");
				}
				return members;
			}
			catch (Exception e)
			{
				Debug.WriteLine("http request error");
				Debug.Print(e.StackTrace);
				return null;
			}

		}


		public async Task<int> GetMemberStudents_Count(string memberid)
		{
			Debug.WriteLine("MemberManager.GetMemberStudents_Count - "+ Constants.RestUrl_Get_Member_Students_Count + "?userid=" + memberid);
			Uri uri = new Uri(string.Format(Constants.RestUrl_Get_Member_Students_Count + "?userid=" + memberid));
			try
			{
				HttpResponseMessage response = await client.GetAsync(uri);

				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					Debug.WriteLine("content = " + content);
					List<Result> createResultList = JsonConvert.DeserializeObject<List<Result>>(content);

					return Convert.ToInt32(createResultList[0].result);
				}
				else
				{
					Debug.WriteLine("login not ok");
					return -1;
				}	
			}
			catch (Exception e)
			{
				Debug.WriteLine("http request error");
				Debug.Print(e.StackTrace);
				return -2;
			}
		}


		public async Task<int> GetExaminations(Member member)
		{
			Debug.WriteLine("GetExaminations");
			Uri uri = new Uri(string.Format(Constants.RestUrl_Get_Examinations + "?userid=" + member.id, string.Empty));
			try
			{
				HttpResponseMessage response = await client.GetAsync(uri);
				var result = 0;
				if (response.IsSuccessStatusCode)
				{
					//return true;
					string content = await response.Content.ReadAsStringAsync();
					Debug.WriteLine("content=" + content);
					member.examinations = JsonConvert.DeserializeObject<List<Examination>>(content);
					result = 1;
				}
				else
				{
					Debug.WriteLine("login not ok");
					result = -1;
				}

				return result;
			}
			catch
			{
				Debug.WriteLine("http request error");
				return -1;
			}
		}


		/*public async Task<int> GetFees(Member member)
		{
			Debug.WriteLine("GetFees begin");
			Uri uri = new Uri(string.Format(Constants.RestUrl_Get_Fees + "?userid=" + member.id, string.Empty));
			HttpResponseMessage response = await client.GetAsync(uri);
			var result = 0;
			if (response.IsSuccessStatusCode)
			{
				//return true;
				string content = await response.Content.ReadAsStringAsync();
				Debug.WriteLine("content=" + content);
				List<Fee> feesTemp = JsonConvert.DeserializeObject<List<Fee>>(content);
				member.currentFee = feesTemp[0];
				result = 1;
			}
			else
			{
				Debug.WriteLine("error getting fees");
				result = -1;
			}

			return result;
		}*/

		public async Task<int> GetPastFees(Member member)
		{
			Debug.WriteLine("GetPastFees");
			Uri uri = new Uri(string.Format(Constants.RestUrl_Get_Past_Fees + "?userid=" + member.id, string.Empty));
			try {
				HttpResponseMessage response = await client.GetAsync(uri);
				var result = 0;
				if (response.IsSuccessStatusCode)
				{
					//return true;
					string content = await response.Content.ReadAsStringAsync();
					member.pastFees = JsonConvert.DeserializeObject<List<Fee>>(content);
					result = 1;
				}
				else
				{
					Debug.WriteLine("error getting fees");
					result = -1;
				}

				return result;
			}
			catch
			{
				Debug.WriteLine("http request error");
				return -1;
			}
		}


		public async Task<int> GetCurrentFees(Member member)
		{
			Debug.WriteLine("GetCurrentFees begin "+ Constants.RestUrl_Get_Current_Fees + "?userid=" + member.id);
			Uri uri = new Uri(string.Format(Constants.RestUrl_Get_Current_Fees + "?userid=" + member.id, string.Empty));
			try
            {

            
				HttpResponseMessage response = await client.GetAsync(uri);
				var result = 0;
				if (response.IsSuccessStatusCode)
				{
					//return true;
					string content = await response.Content.ReadAsStringAsync();
					List<Fee> feesTemp = JsonConvert.DeserializeObject<List<Fee>>(content);
					if ((feesTemp != null) & (feesTemp.Count > 0))

					{
						member.currentFee = feesTemp[0];
					}
					result = 1;
				}
				else
				{
					Debug.WriteLine("error getting fees");
					result = -1;
				}

				return result;
			}
			catch (Exception e)
			{
				Debug.WriteLine("MemberManager.GetCurrentFees http request error "+e.ToString());
				return -1;
			}
		}

		public async Task<int> CreateFee(Member member, string period)
		{
			Debug.WriteLine("CreateFee begin "+ Constants.RestUrl_Create_Fee + "?userid=" + member.id + "&period=" + period);
			Uri uri = new Uri(string.Format(Constants.RestUrl_Create_Fee+ "?userid=" + member.id + "&period=" + period, string.Empty));
			try {
				HttpResponseMessage response = await client.GetAsync(uri);
				var result = 0;
				if (response.IsSuccessStatusCode)
				{
					//return true;
					string content = await response.Content.ReadAsStringAsync();
					Debug.WriteLine("content=" + content);
					//member.fees = JsonConvert.DeserializeObject<List<Fee>>(content);
					result = 1;
				}
				else
				{
					Debug.WriteLine("error creating fee");
					result = -1;
				}

				return result;
			}
			catch
			{
				Debug.WriteLine("http request error");
				return -1;
			}
		}

		public async Task<int> ChangePassword(string email, string newpassword)
		{
			Uri uri = new Uri(string.Format(Constants.RestUrl_Update_Password + "?username=" + email + "&password=" + newpassword, string.Empty));
			try {
				HttpResponseMessage response = await client.GetAsync(uri);

				if (response.IsSuccessStatusCode)
				{
					//return true;
					string content = await response.Content.ReadAsStringAsync();
					Debug.WriteLine("content=" + content);
					List<Login> loginResultList = JsonConvert.DeserializeObject<List<Login>>(content);

					return loginResultList[0].login;
				}
				else
				{
					Debug.WriteLine("login not ok");
					return -2;
				}
			}
			catch
			{
				Debug.WriteLine("http request error");
				return -3;
			}
		}


		public async Task<int> RecoverPassword(string email)
		{
			Uri uri = new Uri(string.Format(Constants.RestUrl_Recover_Password + "?username=" + email, string.Empty));
			Debug.Print(Constants.RestUrl_Recover_Password + "?username=" + email);
			try { 
				HttpResponseMessage response = await client.GetAsync(uri);

				if (response.IsSuccessStatusCode)
				{
					//return true;
					string content = await response.Content.ReadAsStringAsync();
					List<Login> loginResultList = JsonConvert.DeserializeObject<List<Login>>(content);

					return loginResultList[0].login;
				}
				else
				{
					Debug.WriteLine("login not ok");
					return -2;
				}
			}
			catch 
			{
				Debug.WriteLine("http request error");
				return -3;
			}
		}

		public async Task<string> UpdateMemberInfo(Member member)
		{

			string member_first_name = member.name.Substring(0, member.name.IndexOf(" "));
			string member_last_name = member.name.Substring(member.name.IndexOf(" ")+1);

			Debug.Print("UpdateMemberInfo");
			Uri uri = new Uri(string.Format(Constants.RestUrl_Update_Member_Info + "?memberid=" + member.id
				+ "&memberfirstname=" + member_first_name + "&memberlastname=" + member_last_name + "&membernif=" + member.nif + "&membercc=" + member.cc_number
				+ "&memberphone=" + member.phone + "&memberemail=" + member.email + "&memberaddress=" + member.address
				+ "&membercity=" + member.city + "&memberpostalcode=" + member.postalcode
				+ "&membernameenc1=" + member.name_enc1 + "&memberemailenc1=" + member.mail_enc1 + "&memberphoneenc1=" + member.phone_enc1
				+ "&membernameenc2=" + member.name_enc2 + "&memberemailenc2=" + member.mail_enc2 + "&memberphoneenc2=" + member.phone_enc2
				, string.Empty));
			try
			{
				HttpResponseMessage response = await client.GetAsync(uri);
				var result = "0";

				if (response.IsSuccessStatusCode)
				{
					//return true;
					string content = await response.Content.ReadAsStringAsync();
					Debug.WriteLine("content=" + content);
					List<Result> createResultList = JsonConvert.DeserializeObject<List<Result>>(content);

					return createResultList[0].result;
				}
				else
				{
					Debug.WriteLine("error updating member info");
					result = "-1";
				}

				return result;
			}
			catch
			{
				Debug.WriteLine("http request error");
				return "-1";
			}
		}


        public async Task<string> createNewMember(Member member)
        {

            string member_first_name = member.name.Substring(0, member.name.IndexOf(" "));
            string member_last_name = member.name.Substring(member.name.IndexOf(" ") + 1);
			string query = Constants.RestUrl_Create_New_Member + "?memberfirstname=" + member_first_name + "&memberlastname=" + member_last_name
				+ "&member_type=" + member.member_type + "&dojo=" + member.dojoid
				+ "&gender=" + member.gender + "&birthdate=" + member.birthdate + "&cc_number=" + member.cc_number
				+ "&nif=" + member.nif + "&number_fnkp=" + member.number_fnkp + "&job=" + member.job
				+ "&email=" + member.email + "&phone=" + member.phone
				+ "&address=" + member.address + "&city=" + member.city + "&postalcode=" + member.postalcode
				+ "&name_enc1=" + member.name_enc1 + "&mail_enc1=" + member.mail_enc1 + "&phone_enc1=" + member.phone_enc1
				+ "&name_enc2=" + member.name_enc2 + "&mail_enc2=" + member.mail_enc2 + "&phone_enc2=" + member.phone_enc2
				+ "&emergencycontact=" + member.emergencyContact + "&emergencyphone=" + member.emergencyPhone
                + "&consentimento_regulamento=" + member.consentimento_regulamento;
            Debug.Print("createNewMember - " + query);
            Uri uri = new Uri(string.Format(query, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                var result = "0";

                if (response.IsSuccessStatusCode)
                {
                    //return true;
                    string content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("content=" + content);
                    List<Result> createResultList = JsonConvert.DeserializeObject<List<Result>>(content);

                    return createResultList[0].result;
                }
                else
                {
                    Debug.WriteLine("error updating member info");
                    result = "-3";
                }

                return result;
            }
            catch
            {
                Debug.WriteLine("http request error");
                return "-4";
            }
        }

        public async Task<List<Payment>> GetFeePayment(string feeid)
		{
			Debug.Print("GetFeePayment feeid = "+ Constants.RestUrl_Get_FeePayment + "?feeid=" + feeid);
			Uri uri = new Uri(string.Format(Constants.RestUrl_Get_FeePayment + "?feeid=" + feeid, string.Empty));
			try
			{
				HttpResponseMessage response = await client.GetAsync(uri);

				if (response.IsSuccessStatusCode)
				{
					//return true;
					string content = await response.Content.ReadAsStringAsync();
					payments = JsonConvert.DeserializeObject<List<Payment>>(content);
				}

				return payments;
			}
			catch
			{
				Debug.WriteLine("http request error");
				return null;
			}
		}

        public async Task<string> Upload_Member_Photo(Stream stream)//Stream mfile, string fileName)
        {
            var url = Constants.RestUrl_Upload_Member_Photo;
            url += "?userid=" + App.member.id; //any parameters you want to send to the php page.
            Debug.Print("Upload_Member_Photo " + url);
            try
            {
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri("https://" + Constants.server);
                //client.BaseAddress = new Uri(Constants.server);
                MultipartFormDataContent form = new MultipartFormDataContent();
                StreamContent content = new StreamContent(stream);

                string fName = App.member.id + "_photo";

                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "fileToUpload",
                    FileName = fName
                };
                form.Add(content);
                var response = await client.PostAsync(url, form);
                var result = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception Caught: " + e.ToString());
                return "-1";
            }
            return "0";
        }

        public async Task<string> Delete_Member_Photo(string memberid)
        {
            Debug.WriteLine("Delete_Member_Photo " + Constants.RestUrl_Delete_Member_Photo + "?userid=" + memberid);
            Uri uri = new Uri(string.Format(Constants.RestUrl_Delete_Member_Photo + "?userid=" + memberid, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                var result = "0";
                if (response.IsSuccessStatusCode)
                {
                    //return true;
                    string content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("content=" + content);
                    List<Result> createResultList = JsonConvert.DeserializeObject<List<Result>>(content);

                    return createResultList[0].result;
                }
                else
                {
                    Debug.WriteLine("error deleting member photo");
                    result = "-1";
                }

                return result;
            }
            catch
            {
                Debug.WriteLine("http request error");
                return "-2";
            }
        }

        public async Task<List<Member>> GetMembers_To_Approve()
        {
            Debug.WriteLine("GetMembers_To_Approve - " + Constants.RestUrl_Get_Members_To_Approve+"?userid="+App.member.id);
            Uri uri = new Uri(string.Format(Constants.RestUrl_Get_Members_To_Approve + "?userid=" + App.member.id));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("GetMembers_To_Approve - content = " + content);
                    members = JsonConvert.DeserializeObject<List<Member>>(content);

                }
                else
                {
                    Debug.WriteLine("GetMembers_To_Approve - not ok");
                }
                return members;
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetMembers_To_Approve - http request error");
                Debug.Print(e.StackTrace);
                return null;
            }

        }

        public async Task<string> Update_Member_Approved_Status(string userid, string name, string email, string status, string classid)
        {


            Debug.Print("Update_Member_Approved_Status " + Constants.RestUrl_Update_Member_Approved_Status + "?userid=" + userid + "&name=" + name + "&email=" + email + "&status=" + status + "&classid=" + classid);
            Uri uri = new Uri(string.Format(Constants.RestUrl_Update_Member_Approved_Status + "?userid=" + userid + "&name=" + name + "&email=" + email + "&status=" + status + "&classid=" + classid, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                var result = "0";

                if (response.IsSuccessStatusCode)
                {
                    //return true;
                    string content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("content=" + content);
                    List<Result> createResultList = JsonConvert.DeserializeObject<List<Result>>(content);

                    return createResultList[0].result;
                }
                else
                {
                    Debug.WriteLine("error updating member info");
                    result = "-1";
                }

                return result;
            }
            catch
            {
                Debug.WriteLine("http request error");
                return "-1";
            }
        }

        public async Task<List<Member>> GetPersonalCoaches()
        {
            Debug.WriteLine("GetPersonalCoaches - " + Constants.RestUrl_Get_PersonalCoaches);
            Uri uri = new Uri(string.Format(Constants.RestUrl_Get_PersonalCoaches));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("GetPersonalCoaches - content = " + content);
                    members = JsonConvert.DeserializeObject<List<Member>>(content);

                }
                else
                {
                    Debug.WriteLine("GetPersonalCoaches - not ok");
                }
                return members;
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetPersonalCoaches - http request error");
                Debug.Print(e.StackTrace);
                return null;
            }

        }

        public async Task<string> Request_Personal(string coachName, string coachEmail, string memberName, string memberPhone, string classtype, string numeroAulasSemanais, string objetivos, string disponibilidade)
        {


            Debug.Print("Update_Member_Approved_Status " + Constants.RestUrl_Request_Personal + "?coachname=" + coachName + "&coachemail=" + coachEmail + "&membername=" + memberName + "&memberphone=" + memberPhone + "&classtype=" + classtype + "& numeroaulassemanais=" + numeroAulasSemanais + "&disponibilidade=" + disponibilidade + "&objetivos=" + objetivos);
            Uri uri = new Uri(string.Format(Constants.RestUrl_Request_Personal + "?coachname=" + coachName + "&coachemail=" + coachEmail + "&membername=" + memberName + "&memberphone=" + memberPhone + "&classtype=" + classtype + "&numeroaulassemanais=" + numeroAulasSemanais + "&disponibilidade=" + disponibilidade + "&objetivos=" + objetivos, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                var result = "0";

                if (response.IsSuccessStatusCode)
                {
                    //return true;
                    string content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("content=" + content);
                    List<Result> createResultList = JsonConvert.DeserializeObject<List<Result>>(content);

                    return createResultList[0].result;
                }
                else
                {
                    Debug.WriteLine("error updating member info");
                    result = "-1";
                }

                return result;
            }
            catch
            {
                Debug.WriteLine("http request error");
                return "-1";
            }
        }

        public async Task<int> CreateObjective(string memberid, string name, string epoca, string objective)
        {
            Debug.WriteLine("CreateObjective " + Constants.RestUrl_Create_Objective + "?userid=" + memberid + "&name=" + name + "&epoca=" + epoca + "&objective="+ objective);
            Uri uri = new Uri(string.Format(Constants.RestUrl_Create_Objective + "?userid=" + memberid + "&name=" + name + "&epoca=" + epoca + "&objective=" + objective, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                var result = 0;
                if (response.IsSuccessStatusCode)
                {
                    //return true;
                    string content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("content=" + content);
                    //member.fees = JsonConvert.DeserializeObject<List<Fee>>(content);
                    result = 1;
                }
                else
                {
                    Debug.WriteLine("error creating fee");
                    result = -1;
                }

                return result;
            }
            catch
            {
                Debug.WriteLine("http request error");
                return -1;
            }
        }

        public async Task<List<Objective>> GetObjectives_bySeason(string memberid, string epoca)
        {
            Debug.WriteLine("GetObjectives_bySeason - " + Constants.RestUrl_Get_Objectives_bySeason + "?userid=" + memberid + "&epoca=" + epoca);
            Uri uri = new Uri(string.Format(Constants.RestUrl_Get_Objectives_bySeason + "?userid=" + memberid + "&epoca=" + epoca));

			List<Objective> objectives = new List<Objective>();
			try
            {
                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("GetObjectives_bySeason - content = " + content);
                    objectives = JsonConvert.DeserializeObject<List<Objective>>(content);

                }
                else
                {
                    Debug.WriteLine("GetObjectives_bySeason - not ok");
                }
                return objectives;
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetObjectives_bySeason - http request error");
                Debug.Print(e.StackTrace);
                return null;
            }

        }

    }
}