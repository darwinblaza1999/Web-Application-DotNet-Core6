using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WatchWebApp.Models;
using WatchWebApp.Repository.UnitofWork;

namespace WatchWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdapter _adapter;
        public HomeController(ILogger<HomeController> logger, IAdapter adapter)
        {
            _logger = logger;
            _adapter = adapter;
        }

        public async Task<IActionResult> Index()
        {
            var response = new Response<IList<WatchModel2>>();
            try
            {
                var result = await this._adapter.watch.GetAllItem();
                if (result.isSuccess)
                {
                    var resdata = JsonConvert.DeserializeObject<ResData>(result.Data);
                    response.Data = resdata.data;
                    response.isSuccess = result.isSuccess;
                    response.Message = result.Message;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            //return Json(new { response.Data, response.isSuccess});
            return View(response.Data);
            //return View();
        }
        public IActionResult PartialView()
        {
            var model = new ImageModel();
            return PartialView("PartialView", model);
        }
        public IActionResult PartialItemView()
        {   
            return PartialView("PartialItemView");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}