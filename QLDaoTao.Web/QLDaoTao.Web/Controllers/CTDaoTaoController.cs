using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QLDaoTao.Web.Models;
using QLDaoTao.Web.Models.ChuongTrinhDaoTao;
using QLDaoTao.Web.Models.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace QLDaoTao.Web.Controllers
{
    public class CTDaoTaoController : Controller
    {
        private readonly IWebHostEnvironment hostingEnviroment;

        public CTDaoTaoController(IWebHostEnvironment hostingEnviroment)
        {
            this.hostingEnviroment = hostingEnviroment;
        }

            public IActionResult Index()
            {
                var DanhSachChuongTrinh = new List<CTDaoTaoView>();
                var url = $"{Common.Common.ApiUrl}/CTDaoTao/DanhSachCTDaoTao";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "GET";

                var response = httpWebRequest.GetResponse();
                {
                    string responseData;
                    Stream responseStream = response.GetResponseStream();
                    try
                    {
                        StreamReader streamReader = new StreamReader(responseStream);
                        try
                        {
                            responseData = streamReader.ReadToEnd();
                        }
                        finally
                        {
                            ((IDisposable)streamReader).Dispose();
                        }
                    }
                    finally
                    {
                        ((IDisposable)responseStream).Dispose();
                    }
                    DanhSachChuongTrinh = JsonConvert.DeserializeObject<List<CTDaoTaoView>>(responseData);
                }
                return View(DanhSachChuongTrinh);  
            }


        public IActionResult XemCTDaoTao(int id)
        {
            var CTDaoTao = new CTDaoTao();
            var url = $"{Common.Common.ApiUrl}/CTDaoTao/LayCTDaoTaoBangId/{id}";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";

            var response = httpWebRequest.GetResponse();
            {
                string responseData;
                Stream responseStream = response.GetResponseStream();
                try
                {
                    StreamReader streamReader = new StreamReader(responseStream);
                    try
                    {
                        responseData = streamReader.ReadToEnd();
                    }
                    finally
                    {
                        ((IDisposable)streamReader).Dispose();
                    }
                }
                finally
                {
                    ((IDisposable)responseStream).Dispose();
                }
                CTDaoTao = JsonConvert.DeserializeObject<CTDaoTao>(responseData);
            }

            return View(CTDaoTao);
        }


        public IActionResult DanhSachCTDaoTaoVoHieuLuc()
        {
            var DanhSachChuongTrinh = new List<CTDaoTaoView>();
            var url = $"{Common.Common.ApiUrl}/CTDaoTao/DanhSachCTDaoTaoVoHieuLuc";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";

            var response = httpWebRequest.GetResponse();
            {
                string responseData;
                Stream responseStream = response.GetResponseStream();
                try
                {
                    StreamReader streamReader = new StreamReader(responseStream);
                    try
                    {
                        responseData = streamReader.ReadToEnd();
                    }
                    finally
                    {
                        ((IDisposable)streamReader).Dispose();
                    }
                }
                finally
                {
                    ((IDisposable)responseStream).Dispose();
                }
                DanhSachChuongTrinh = JsonConvert.DeserializeObject<List<CTDaoTaoView>>(responseData);
            }
            return View(DanhSachChuongTrinh);
        }


        public IActionResult TaoCTDaoTao()
        {
            TempData["ThanhCong"] = null;
            TempData["Loi"] = null;
            return View();
        }

        [HttpPost]
        public IActionResult TaoCTDaoTao(TaoCTDaoTaoView model)
        {

            string uniqueFilename = LuuFileNoiDungCTVaoTrongRoot(model);
            string uniqueFilename2 = LuuFileQuydinhVaoTrongRoot(model);

            CTDaoTao newCTDaoTao = new CTDaoTao()
            {
                MaCT = model.MaCT,
                TenCT = model.TenCT,
                ThoiGianDaotao = model.ThoiGianDaotao,
                HocPhi = model.HocPhi,
                NgayBanHanh = model.NgayBanHanh,
                NoiDungCT = uniqueFilename,
                QDBanHanh = uniqueFilename2,
                GhiChu = model.GhiChu
               
            };

            ResponseCTDaoTao ketQua = new ResponseCTDaoTao();
            
            var url = $"{Common.Common.ApiUrl}/CTDaoTao/TaoCTDaoTao";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(newCTDaoTao);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var resKetQua = streamReader.ReadToEnd();
                ketQua = JsonConvert.DeserializeObject<ResponseCTDaoTao>(resKetQua);
            }
          

            if (ketQua.Id != "0")
             {
                   TempData["ThanhCong"] = ketQua.Message;
                    ModelState.Clear();
            }
             else 
             {
                   TempData["Loi"] = "Tên chương trình không được trùng";
             }
            
       
            return View(new TaoCTDaoTaoView() { });
        }

        
        public IActionResult SuaCTDaoTao(int id)
        {
            var CTDaoTao = new CTDaoTao();
            var url = $"{Common.Common.ApiUrl}/CTDaoTao/LayCTDaoTaoBangId/{id}";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";

            var response = httpWebRequest.GetResponse();
            {
                string responseData;
                Stream responseStream = response.GetResponseStream();
                try
                {
                    StreamReader streamReader = new StreamReader(responseStream);
                    try
                    {
                        responseData = streamReader.ReadToEnd();
                    }
                    finally
                    {
                        ((IDisposable)streamReader).Dispose();
                    }
                }
                finally
                {
                    ((IDisposable)responseStream).Dispose();
                }
                CTDaoTao = JsonConvert.DeserializeObject<CTDaoTao>(responseData);
            }
            TempData["ThanhCong"] = null;

            SuaCTDaoTaoView suaCTDaoTao = new SuaCTDaoTaoView
            {
                CTDaoTao_Id = CTDaoTao.CTDaoTao_Id,
                MaCT = CTDaoTao.MaCT,
                TenCT = CTDaoTao.TenCT,
                HocPhi = CTDaoTao.HocPhi,
                NgayBanHanh = CTDaoTao.NgayBanHanh,
                ThoiGianDaotao = CTDaoTao.ThoiGianDaotao,
                NoiDungCTDaTonTai = CTDaoTao.NoiDungCT,
                QuyDinhBanHanhDaTonTai = CTDaoTao.QDBanHanh
            };

            return View(suaCTDaoTao);
        }

        [HttpPost]
        public IActionResult SuaCTDaoTao(SuaCTDaoTaoView model)
        {
            string uniqueFileName = string.Empty;
            string uniqueFileName2 = string.Empty;

            if (model.NoiDungCT != null)
            {
                if (model.NoiDungCTDaTonTai != null)
                {
                    string filePath = Path.Combine(hostingEnviroment.WebRootPath, "Files", model.NoiDungCTDaTonTai);
                    System.IO.File.Delete(filePath);
                }
                uniqueFileName = LuuFileNoiDungCTVaoTrongRootUpdate(model);
            }

            if (model.QDBanHanh != null)
            {
                if (model.QuyDinhBanHanhDaTonTai != null)
                {
                    string filePath = Path.Combine(hostingEnviroment.WebRootPath, "Files", model.QuyDinhBanHanhDaTonTai);
                    System.IO.File.Delete(filePath);
                }
                uniqueFileName2 = LuuFileQuydinhVaoTrongRootUpdate(model);
            }


            CTDaoTao newCTDaoTao = new CTDaoTao()
            {
                CTDaoTao_Id = model.CTDaoTao_Id,
                MaCT = model.MaCT,
                TenCT = model.TenCT,
                ThoiGianDaotao = model.ThoiGianDaotao,
                HocPhi = model.HocPhi,
                NgayBanHanh = model.NgayBanHanh,
                NoiDungCT = uniqueFileName,
                QDBanHanh = uniqueFileName2,
                GhiChu = model.GhiChu
            };

            var url = $"{Common.Common.ApiUrl}/CTDaoTao/SuaCTDaoTao";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(newCTDaoTao);
                streamWriter.Write(json);
            }

            ResponseCTDaoTao ketQua = new ResponseCTDaoTao();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var resKetQua = streamReader.ReadToEnd();
                ketQua = JsonConvert.DeserializeObject<ResponseCTDaoTao>(resKetQua);
            }

            if (ketQua.Id != "0")
            {
                TempData["ThanhCong"] = ketQua.Message;
                ModelState.Clear();

            }
            else
            {
                TempData["Loi"] = "Câp nhật chương trình mới thất bại";
            }
            return View(new SuaCTDaoTaoView() { });
        }

        public IActionResult XoaCTDaoTao(int id)
        {
            int ketQua = 0;
            var url = $"{Common.Common.ApiUrl}/CTDaoTao/XoaCTDaoTao/{id}";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "DELETE";

            var response = httpWebRequest.GetResponse();
            {
                string responseData;
                Stream responseStream = response.GetResponseStream();
                try
                {
                    StreamReader streamReader = new StreamReader(responseStream);
                    try
                    {
                        responseData = streamReader.ReadToEnd();
                    }
                    finally
                    {
                        ((IDisposable)streamReader).Dispose();
                    }
                }
                finally
                {
                    ((IDisposable)responseStream).Dispose();
                }
                ketQua = JsonConvert.DeserializeObject<int>(responseData);
            }
           
            return RedirectToAction("Index","CTDaoTao");
        }

        public IActionResult KhoiPhucCTDaoTao(int id)
        {
            int ketQua = 0;
            var url = $"{Common.Common.ApiUrl}/CTDaoTao/KhoiPhucCTDaoTao/{id}";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "PUT";

            var response = httpWebRequest.GetResponse();
            {
                string responseData;
                Stream responseStream = response.GetResponseStream();
                try
                {
                    StreamReader streamReader = new StreamReader(responseStream);
                    try
                    {
                        responseData = streamReader.ReadToEnd();
                    }
                    finally
                    {
                        ((IDisposable)streamReader).Dispose();
                    }
                }
                finally
                {
                    ((IDisposable)responseStream).Dispose();
                }
                ketQua = JsonConvert.DeserializeObject<int>(responseData);
            }
            return RedirectToAction("DanhSachCTDaoTaoVoHieuLuc", "CTDaoTao");
        }





        private string LuuFileNoiDungCTVaoTrongRoot(TaoCTDaoTaoView model)
        {
            string uniqueFilename = null;
            if (model.NoiDungCT != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "Files");
                uniqueFilename = Guid.NewGuid() + "_" + model.NoiDungCT.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.NoiDungCT.CopyTo(filestream);
                }
            }
            return uniqueFilename;
        }

        private string LuuFileQuydinhVaoTrongRoot(TaoCTDaoTaoView model)
        {
            string uniqueFilename = null;
            if (model.QDBanHanh != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "Files");
                uniqueFilename = Guid.NewGuid() + "_" + model.QDBanHanh.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.QDBanHanh.CopyTo(filestream);
                }
            }
            return uniqueFilename;
        }

        private string LuuFileNoiDungCTVaoTrongRootUpdate(SuaCTDaoTaoView model)
        {
            string uniqueFilename = null;
            if (model.NoiDungCT != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "Files");
                uniqueFilename = Guid.NewGuid() + "_" + model.NoiDungCT.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.NoiDungCT.CopyTo(filestream);
                }
            }
            return uniqueFilename;
        }

        private string LuuFileQuydinhVaoTrongRootUpdate(SuaCTDaoTaoView model)
        {
            string uniqueFilename = null;
            if (model.QDBanHanh != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "Files");
                uniqueFilename = Guid.NewGuid() + "_" + model.QDBanHanh.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.QDBanHanh.CopyTo(filestream);
                }
            }
            return uniqueFilename;
        }



    }
}
