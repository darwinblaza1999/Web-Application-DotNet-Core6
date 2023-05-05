using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using WatchWebApp.Models;
using WatchWebApp.Repository;
using Microsoft.Extensions.Options;
using WatchWebApp.Options;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Net.Http;

namespace WatchWebApp.Class
{
    public class WatchClass : IWatch
    {
        private readonly AppSettings _settings;
        public WatchClass(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task<Response<string>> AddNewItem(WatchModel model, string token)
        {
			var response = new Response<string>();
			try
			{
				Uri url = new Uri(string.Format(_settings.Url + "api/Watch/AddItem"));
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				using (var client = new WebClient()) {
					client.Headers.Add("content-type", "application/json");
                    client.Headers.Add("Authorization", "Bearer "+ token);
					response.Data = Encoding.ASCII.GetString(client.UploadData(url, "POST", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))));
					response.isSuccess = true;
				}
			}
			catch(WebException webex)
			{
				WebResponse webResponse = webex.Response;
				using (Stream stream = webResponse.GetResponseStream())
				{
					StreamReader reader = new StreamReader(stream);
					response.isException = true;
					response.Message = reader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
                response.isException = true;
                response.Message = ex.Message;
            }
			return response;
        }

        public async Task<Response<object>> DeleteItem(int id, string token)
        {
            var response = new Response<object>();
            try
            {
                Uri url = new Uri(string.Format(_settings.Url + "api/Watch/DeleteItem/{0}", id));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                request.Method = "DELETE";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + token);
                StreamReader read = new StreamReader(request.GetResponse().GetResponseStream());
                var res = read.ReadToEnd();
                response.isSuccess = true;
                response.Message = "Ok";
            }
            catch (WebException webex)
            {
                WebResponse webResponse = webex.Response;
                using (Stream stream = webResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    response.isException = true;
                    response.isSuccess = false;
                    response.Message = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

                response.isException = true;
                response.isSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<string>> GetAllItem(string token)
        {
            var response = new Response<string>();
            try
            {
                Uri url = new Uri(string.Format(_settings.Url + "api/Watch/GetAllItem"));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + token);
                StreamReader read = new StreamReader(request.GetResponse().GetResponseStream());
                var res = read.ReadToEnd();
                response.Data = res;
                response.isSuccess = true;
            }
            catch(WebException webex)
            {
                WebResponse webResponse = webex.Response;
                using (Stream stream = webResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    response.isException = true;
                    response.isSuccess = false;
                    response.Message = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

                response.isException = true;
                response.isSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response<string>> GetById(int Id, string token)
        {
            var response = new Response<string>();
            try
            {
                Uri url = new Uri(string.Format(_settings.Url + "api/Watch/GetItemById/{0}", Id));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + token);
                StreamReader read = new StreamReader(request.GetResponse().GetResponseStream());
                var res = read.ReadToEnd();
                response.Data = res;
                response.isSuccess = true;
            }
            catch (WebException webex)
            {
                WebResponse webResponse = webex.Response;
                using (Stream stream = webResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    response.isException = true;
                    response.isSuccess = false;
                    response.Message = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

                response.isException = true;
                response.isSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<string>> UpdateImage(WatchImage model, string token)
        {
            var response = new Response<string>();
            try
            {
                Uri url = new Uri(string.Format(_settings.Url + "api/Watch/UpdateImage"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var client = new WebClient())
                {
                    client.Headers.Add("content-type", "application/json");
                    client.Headers.Add("Authorization", "Bearer " + token);
                    response.Data = Encoding.ASCII.GetString(client.UploadData(url, "PUT", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))));
                    response.isSuccess = true;
                }
            }
            catch (WebException webex)
            {
                WebResponse webResponse = webex.Response;
                using (Stream stream = webResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    response.isException = true;
                    response.Message = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                response.isException = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<string>> UpdateItem(WatchModel2 model, string token)
        {
            var response = new Response<string>();
            try
            {
                Uri url = new Uri(string.Format(_settings.Url + "api/Watch/UpdateItem"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var client = new WebClient())
                {
                    client.Headers.Add("content-type", "application/json");
                    client.Headers.Add("Authorization", "Bearer " + token);
                    response.Data = Encoding.ASCII.GetString(client.UploadData(url, "PUT", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))));
                    response.isSuccess = true;
                }
            }
            catch (WebException webex)
            {
                WebResponse webResponse = webex.Response;
                using (Stream stream = webResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    response.isException = true;
                    response.isSuccess = false;
                    response.Message = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                response.isException = true;
                response.isSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
