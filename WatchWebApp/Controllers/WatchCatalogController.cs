using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using WatchWebApp.Models;
using WatchWebApp.Repository.UnitofWork;

namespace WatchWebApp.Controllers
{
    public class WatchCatalogController : Controller
    {
        private readonly IAdapter adapter;
        public WatchCatalogController(IAdapter adapter)
        {
            this.adapter = adapter;
        }
        [HttpGet]
        public async Task<IActionResult> ViewItem()
        {
            var response = new Response<IList<WatchModel2>>();
            try
            {
                var result = await this.adapter.watch.GetAllItem();
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
        }
        public IActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateItem(WatchModel2 model2)
        {
            var result = await this.adapter.watch.UpdateItem(model2);

            //return Json(result.Data, new {message = "Success" } );
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(ImageModel model)
        {
            var response = new Response<object>();
            if (model.File != null)
            {
                var imgupload = await this.adapter.watch.UploadImageBlob(model.File);
                if(imgupload.isSuccess == true)
                {
                    var link = "https://msavaaccenturestorage.blob.core.windows.net/watchcontainer/";
                    var WatchModel = new WatchModel()
                    {
                        ItemName = model.ItemName,
                        Image = String.Format(link + model.File.FileName),
                        Price = model.Price,
                        ShortDescription = model.ShortDescription,
                        FullDescription = model.FullDescription,      
                        Diameter = model.Diameter,
                        Caliber = model.Caliber,
                        Movement = model.Movement,
                        Weight = model.Weight,
                        Height = model.Height,
                        Chronograph = model.Chronograph,
                        Thickness = model.Thickness,
                        Jewel = model.Jewel,
                        CaseMaterial = model.CaseMaterial,
                        StrapMaterial = model.StrapMaterial,
                    };
                    var result = await this.adapter.watch.AddNewItem(WatchModel);
                    if (result.isSuccess == true)
                        response.Message = result.Message;
                    response.isSuccess = result.isSuccess;
                    response.isException = result.isException;
                }
            }
            //return Json(new { response });
            return RedirectToAction("ViewItem");

        }
        [HttpGet]
        public  async Task<IActionResult> GetDetails(int Id)
        {
            var response = new Response<WatchModel2>();
            var result = await this.adapter.watch.GetById(Id);
            if(result.isSuccess == true)
            {
                var data = JsonConvert.DeserializeObject<Response<WatchModel2>>(result.Data);
                response.Data = data.Data;
                ViewBag.Message = data.Data;
            }
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllItem()
        {
            var result = await this.adapter.watch.GetAllItem();
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> DelItem(int id)
        {
            var result = await this.adapter.watch.DeleteItem(id);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteItems(int id)
        {
            var result = await this.adapter.watch.DeleteItem(id);
            return Json(result);
        }
    }
}
