using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using WatchWebApp.Class.Core;
using WatchWebApp.Models;
using WatchWebApp.Options;
using WatchWebApp.Repository.UnitofWork;

namespace WatchWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdapter _adapter;
        public readonly AppSettings _options;
        public HomeController(ILogger<HomeController> logger, IAdapter adapter, IOptions<AppSettings> options)
        {
            _logger = logger;
            _adapter = adapter;
            _options = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ErMessage = null;
            var response = new Response<IList<WatchModel2>>();

            //Get Token
            var token = await _adapter.token.GetToken();
            if (token.Data != string.Empty && token.isSuccess)
            {
                var result = await this._adapter.watch.GetAllItem(token.Data);
                if (result.isSuccess)
                {
                    var data1 = JsonConvert.DeserializeObject<ResData>(result.Data);
                    response.Data = data1?.data;
                    response.isSuccess = result.isSuccess;
                }
                else
                {
                    response.Message = result.Message;
                }
            }
            else
            {
                response.Message = "Error on requesting token, please try again!";
            }
            ViewBag.ErMessage = response.Message;
            return View(response.Data);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}