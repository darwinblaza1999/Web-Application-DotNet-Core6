using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using WatchWebApp.Class.Core;
using WatchWebApp.Models;
using WatchWebApp.Options;
using WatchWebApp.Repository.UnitofWork;

namespace WatchWebApp.Controllers
{
    public class WatchCatalogController : Controller
    {
        private readonly IAdapter _adapter;
        private readonly AppSettings _options;
        public WatchCatalogController(IAdapter adapter, IOptions<AppSettings> options)
        {
            _adapter = adapter;
            _options = options.Value;
        }

        public IActionResult AddItem()
        {
            return View();
        }

        public IActionResult ViewShop()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItem([FromBody]WatchModel2 model2)
        {
            var response = new Response<object>();
            ViewBag.msgerror = null;
            if (ModelState.IsValid)
            {
                #region Get Token Bearer
                var token = await _adapter.token.GetToken();
                if (token.Data != string.Empty && token.isSuccess)
                {
                    if (model2.File == null)
                    {
                        #region Update item details
                        var result = await _adapter.watch.UpdateItem(model2, token.Data);
                        if (result.isSuccess == true)
                        {
                            var data = JsonConvert.DeserializeObject<APIResponse<object>>(result.Data);
                            if (data.Code != 10)
                            {
                                response.isSuccess = false;
                                response.Message = data.DeveloperMessage;
                            }
                            else
                            {
                                response.isSuccess = true;
                                response.Message = data.DeveloperMessage;
                            }
                        }
                        else
                        {
                            response.isSuccess = false;
                            response.Message = result.Message;
                        }
                        #endregion
                    }
                }
                else
                {
                    response.isSuccess = false;
                    response.Message = token.Message;
                }
                #endregion
            }
            else
            {
                response.isSuccess = false;
                response.Message = "Error while validating data, please try again!.";
            }
            return Json(new { response });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(ImageModel model)
        {
            var response = new Response<object>();
            ViewBag.ResMessage = null;
            if (ModelState.IsValid)
            {
                #region Get Token Bearer
                var token = await _adapter.token.GetToken();
                if (token.Data != string.Empty && token.isSuccess)
                {
                    #region Image upload
                    var resupload = await _adapter.blob.UploadImage(model.File, token.Data);
                    if (resupload.isSuccess)
                    {
                        #region Adding of Items
                        var WatchModel = new WatchModel()
                        {
                            ItemName = model.ItemName,
                            Image = resupload.Data,
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

                        var result = await _adapter.watch.AddNewItem(WatchModel, token.Data);
                        if (result.isSuccess)
                        {
                            var data = JsonConvert.DeserializeObject<APIResponse<object>>(result.Data);
                            if (data.Code == 10)
                            {
                                response.isSuccess = true;
                                response.Message = data.DeveloperMessage;
                                response.Title = "Add new Item";
                            }
                            else
                            {
                                response.Message = data.DeveloperMessage;
                            }
                        }
                        else
                        {
                            response.Message = result.Message;
                        }
                        #endregion
                    }
                    else
                    {
                        response.Message = resupload.Message;
                    }
                    #endregion
                }
                else
                {
                    response.Message = token.Message;
                }
                #endregion
            }

            if(response.isSuccess)
            {
                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                ViewBag.ResMessage = response.Message;
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails(int Id)
        {
            var response = new Response<WatchModel2>();
            ViewBag.msgerror = null;

            #region Get Token Bearer
            var token = await _adapter.token.GetToken();
            if (token.Data != string.Empty && token.isSuccess)
            {
                #region Get record
                var result = await _adapter.watch.GetById(Id, token.Data);
                if (result.isSuccess)
                {
                    var data = JsonConvert.DeserializeObject<Response<WatchModel2>>(result.Data);
                    response.Data = data.Data;
                    response.isSuccess = true;
                }
                else
                {
                    response.Message = result.Message;
                }
                #endregion
            }
            else
            {
                response.Message = token.Message;
            }
            #endregion

            ViewBag.msgerror = response.Message;
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteItems(int id)
        {
            Response<object> response = new Response<object>();

            #region Get Token Bearer
        var token = await _adapter.token.GetToken();
            if (token.Data != string.Empty && token.isSuccess)
            {
                #region Get Image url
                var getimage = await _adapter.watch.GetById(id, token.Data);
                if (getimage.isSuccess)
                {
                    var data = JsonConvert.DeserializeObject<Response<WatchModel2>>(getimage.Data);
                    var imglink = data.Data.Image;
                    #region Delete image in BlobStorage
                    var imagedel = await _adapter.blob.DeleteImage(imglink, token.Data);
                    if (imagedel.isSuccess)
                    {
                        #region Delete of Item
                        var result = await _adapter.watch.DeleteItem(id, token.Data);
                        if (result.isSuccess)
                        {
                            response.Message = result.Message;
                            response.isSuccess = true;
                        }
                        else
                        {
                            response.Message = result.Message;
                            response.isSuccess = false;
                        }
                        #endregion
                    }
                    else
                    {
                        response.Message = imagedel.Message;
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                response.Message = token.Message;
            }
            #endregion
            if(response.isSuccess)
            {
                return Json(new { response });
            }
            else
            {
                return View("GetDetails");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateImage(WatchImage model2)
        {
            var response = new Response<object>();
            ViewBag.msgerror = null;
            if(model2.File != null)
            {
                #region Get token Bearer
                var token = await _adapter.token.GetToken();
                if (token.Data != string.Empty && token.isSuccess)
                {
                    #region Upload image on blob
                    var upload = await _adapter.blob.UploadImage(model2.File, token.Data);
                    if (upload.isSuccess == true && model2.Image != null)
                    {
                        #region delete image on blob
                        var Imagedel = await _adapter.blob.DeleteImage(model2.Image, token.Data);
                        if (Imagedel.isSuccess)
                        {
                            #region Update image
                            var model = new WatchImage
                            {
                                Image = upload.Data,
                                ItemNo = model2.ItemNo,
                            };
                            var img = await _adapter.watch.UpdateImage(model, token.Data);
                            if (img.isSuccess)
                            {
                                var data = JsonConvert.DeserializeObject<Response<WatchModel2>>(img.Data);
                                response.Data = data.Data;
                                response.isSuccess = img.isSuccess;
                            }
                            else
                            {
                                response.Message = img.Message;
                            }
                            #endregion
                        }
                        else
                        {
                            response.Message = response.Message;
                        }
                        #endregion
                    }
                    else
                    {
                        response.Message = upload.Message;
                    }
                    #endregion
                }
                else
                {
                    response.Message = token.Message;
                }
                #endregion
            }
            else
            {
                response.Message = "Please select image";
                response.Data = model2;
            }

            ViewBag.msgerror = response.Message;
            return View("GetDetails", response.Data);
        }
    }
}
