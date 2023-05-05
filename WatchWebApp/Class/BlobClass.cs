using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Runtime;
using WatchWebApp.Models;
using WatchWebApp.Options;
using WatchWebApp.Repository;

namespace WatchWebApp.Class
{
    public class BlobClass : IBlob
    {
        private readonly AppSettings _options;
        public BlobClass(IOptions<AppSettings> options)
        {
            _options = options.Value;
        } 

        public async Task<Response<string>> DeleteImage(string imageUrl, string token)
        {
            var response = new Response<string>();
            try
            {
                if (imageUrl != null)
                {
                    var split = imageUrl.Split('/');
                    Uri url = new Uri(string.Format(_options.Url + "api/Blob/DeleteImage/{0}", split[4]));
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

        public async Task<Response<string>> UploadImage(IFormFile file, string token)
        {
            var response = new Response<string>();
            try
            {
                var url = string.Format(_options.Url + "api/Blob/UploadImage");
                using (var client = new HttpClient())
                {
                    var form = new MultipartFormDataContent();
                    //form.Add(new StringContent(name.ToString()), "name");
                    var fileContent = new StreamContent(file.OpenReadStream());
                    form.Add(fileContent, "file", file.FileName);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    var res = await client.PostAsync(url, form);
                    var contents = await res.Content.ReadAsStringAsync();

                    var json = JsonConvert.DeserializeObject<APIResponse<string>>(contents);
                    if (json.HttpCode == ResponseStatusCode.Success)
                    {
                        response.isSuccess = true;
                        response.Data = json.Data;
                        response.Message = json.DeveloperMessage;
                    }
                    else
                    {
                        response.isSuccess = false;
                        response.Data = json.Data;
                        response.Message = json.DeveloperMessage;
                    }
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
