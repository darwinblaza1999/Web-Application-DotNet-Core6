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

namespace WatchWebApp.Class
{
    public class WatchClass : IWatch
    {
        public async Task<Response<object>> AddNewItem(WatchModel model)
        {
			var response = new Response<object>();
			try
			{
				Uri url = new Uri(string.Format(APIUrl.WatchApi() + "api/Watch/AddItem"));
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				using (var client = new WebClient()) {
					client.Headers.Add("content-type", "application/json");
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

        public async Task<Response<object>> DeleteItem(int id)
        {
            var response = new Response<object>();
            try
            {
                Uri url = new Uri(string.Format(APIUrl.WatchApi() + "api/Watch/DeleteItem/{0}", id));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                request.Method = "DELETE";
                request.ContentType = "application/json";
                StreamReader read = new StreamReader(request.GetResponse().GetResponseStream());
                var res = read.ReadToEnd();
                response.Data = JsonConvert.DeserializeObject<DataTable>(res);
                response.isSuccess = true;
                response.Message = "";
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

        public async Task<Response<string>> GetAllItem()
        {
            var response = new Response<string>();
            try
            {
                Uri url = new Uri(string.Format(APIUrl.WatchApi() + "api/Watch/GetAllItem"));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                request.Method = "GET";
                request.ContentType = "application/json";
                StreamReader read = new StreamReader(request.GetResponse().GetResponseStream());
                var res = read.ReadToEnd();
                response.Data = res;
                response.isSuccess = true;
                response.Message = "";
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
        public async Task<Response<string>> GetById(int Id)
        {
            var response = new Response<string>();
            try
            {
                Uri url = new Uri(string.Format(APIUrl.WatchApi() + "api/Watch/GetItemById/{0}", Id));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                request.Method = "GET";
                request.ContentType = "application/json";
                StreamReader read = new StreamReader(request.GetResponse().GetResponseStream());
                var res = read.ReadToEnd();
                response.Data = res;
                response.isSuccess = true;
                response.Message = "";
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

        public async Task<Response<object>> UpdateItem(WatchModel2 model)
        {
            var response = new Response<object>();
            try
            {
                Uri url = new Uri(string.Format(APIUrl.WatchApi() + "api/Watch/UpdateItem"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var client = new WebClient())
                {
                    client.Headers.Add("content-type", "application/json");
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

        public async Task<Response<object>> UploadImageBlob(IFormFile file)
        {
            var response = new Response<object>();
            try
            {
                var conString = "DefaultEndpointsProtocol=https;AccountName=msavaaccenturestorage;AccountKey=1lNqO+3lkLKGDaOBwF30motJNwBhGXLS0X8QDWFwRlcPAZV993B6Nt+2b9xcXhE2gCW1ZfXt7kzp+AStAlaelA==;EndpointSuffix=core.windows.net";
                BlobServiceClient blob = new(conString);
                BlobContainerClient conclient = blob.GetBlobContainerClient("watchcontainer");
                await conclient.CreateIfNotExistsAsync();

                if (file != null)
                {
                    BlobClient blobClient = conclient.GetBlobClient(file.FileName);
                    BlobHttpHeaders httpHeaders = new()
                    {
                        ContentType = file.ContentType
                    };
                    await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);

                    response.isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.isException = true;
                response.Message = ex.Message;
            }
            return response;
            
        }
    }
}
