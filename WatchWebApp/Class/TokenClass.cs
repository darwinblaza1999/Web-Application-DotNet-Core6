using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.Runtime;
using System.Text;
using WatchWebApp.Models;
using WatchWebApp.Options;
using WatchWebApp.Repository;

namespace WatchWebApp.Class
{
    public class TokenClass : IToken
    {
        public readonly AppSettings _option;
        public TokenClass(IOptions<AppSettings> option)
        {
            _option = option.Value;
        }
        public async Task<Response<string>> GetToken()
        {
            var response = new Response<string>();
            try
            {
                TokenModel tokenModel = new TokenModel
                {
                    apiUsername = _option.Username,
                    apiPassword = _option.Password,
                    apiKey = _option.ApiKey
                };

                Uri url = new Uri(string.Format(_option.Url + "api/Auth/GetToken"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var client = new WebClient())
                {
                    client.Headers.Add("content-type", "application/json");
                    var data = Encoding.ASCII.GetString(client.UploadData(url, "POST", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tokenModel))));
                    var json = JsonConvert.DeserializeObject<TokenResponse<object>>(data);
                    response.Data = json.token;
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
