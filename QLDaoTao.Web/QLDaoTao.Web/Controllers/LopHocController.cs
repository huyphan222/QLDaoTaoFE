using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLDaoTao.Web.Models.ChuongTrinhDaoTao;
using QLDaoTao.Web.Models.LopHoc;
using QLDaoTao.Web.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Controllers
{
    public class LopHocController : Controller
    {
        private readonly IWebHostEnvironment hostingEnviroment;

        public LopHocController(IWebHostEnvironment hostingEnviroment)
        {
            this.hostingEnviroment = hostingEnviroment;
        }

        public IActionResult Index()
        {
            var DanhSachLopHoc = new List<LopHocViewModel>();
            var url = $"{Common.Common.ApiUrl}/LopHoc/DanhSachLopHoc";
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
                DanhSachLopHoc = JsonConvert.DeserializeObject<List<LopHocViewModel>>(responseData);
            }
            return View(DanhSachLopHoc);
        }

        public IActionResult XemLopHoc(int id)
        {
            var LopHoc = new LopHocViewModel();
            var url = $"{Common.Common.ApiUrl}/LopHoc/LayLopHocBangId/{id}";
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
                LopHoc = JsonConvert.DeserializeObject<LopHocViewModel>(responseData);
            }

            return View(LopHoc);
        }


        public IActionResult DanhSachLopHocTheoCTDaoTao(int id)
        {
            var DanhSachLopHoc = new List<LopHocViewModel>();
            var url = $"{Common.Common.ApiUrl}/LopHoc/DanhSachLopHocTheoCTDaoTao/{id}";
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
                DanhSachLopHoc = JsonConvert.DeserializeObject<List<LopHocViewModel>>(responseData);

            }

            ViewBag.TenCTDaoTao = DanhSachCTDaoTao().Where(e => e.CTDaoTao_Id == id).FirstOrDefault().TenCT;
            return View(DanhSachLopHoc);
        }

        public IActionResult TaoLopHoc()
        {
            ViewBag.DanhSachCTDaoTao = DanhSachCTDaoTao();

            TempData["ThanhCong"] = null;
            TempData["Loi"] = null;
            return View();
        }

        [HttpPost]
        public IActionResult TaoLopHoc(TaoLopHocViewModel model)
        {
            string uniqueFilename = LuuFileQDMoLopVaoTrongRoot(model);
            string uniqueFilename2 = LuuFileTKBVaoTrongRoot(model);

            LopHocModel newLopHoc = new LopHocModel()
            {
                MaLop  = model.MaLop,
                TenLop = model.TenLop,
                NgayKhaiGiang = model.NgayKhaiGiang,
                NgayKetThuc = model.NgayKetThuc,
                SoLuongHV = model.SoLuongHV,
                QDMoLop = uniqueFilename,
                ThoiKhoaBieu = uniqueFilename2,
                GhiChu = model.GhiChu,
                CTDaoTao_Id = model.CTDaoTao_Id
            };

            ResponseLopHoc ketQua = new ResponseLopHoc();
            var url = $"{Common.Common.ApiUrl}/LopHoc/TaoLopHoc";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(newLopHoc);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var resKetQua = streamReader.ReadToEnd();

                ketQua = JsonConvert.DeserializeObject<ResponseLopHoc>(resKetQua);
            }

            if (ketQua.Id != "0")
            {
                ViewBag.DanhSachCTDaoTao = DanhSachCTDaoTao();
                TempData["ThanhCong"] = ketQua.Message;
                ModelState.Clear();
                
            }
            else
            {
                TempData["Loi"] = ketQua.Message;
            }


            return View(new TaoLopHocViewModel() { });
        }

        public IActionResult SuaLopHoc(int id)
        {
            var LopHoc = new LopHocModel();
            var url = $"{Common.Common.ApiUrl}/LopHoc/LayLopHocBangId/{id}";
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
                LopHoc = JsonConvert.DeserializeObject<LopHocModel>(responseData);
            }
            TempData["ThanhCong"] = null;

            SuaLopHocViewModel suaLopHoc = new SuaLopHocViewModel
            {
                LopHoc_Id = LopHoc.LopHoc_Id,
                MaLop  = LopHoc.MaLop,
                TenLop = LopHoc.TenLop,
                NgayKhaiGiang = LopHoc.NgayKhaiGiang,
                NgayKetThuc = LopHoc.NgayKetThuc,
                SoLuongHV = LopHoc.SoLuongHV,
                QDMoLopDaTonTai = LopHoc.QDMoLop,
                ThoiKhoaBieuDaTonTai = LopHoc.ThoiKhoaBieu,
                GhiChu = LopHoc.GhiChu,
                CTDaoTao_Id = LopHoc.CTDaoTao_Id
            };

            ViewBag.DanhSachCTDaoTao = DanhSachCTDaoTao();

            return View(suaLopHoc);
        }

        [HttpPost]
        public IActionResult SuaLopHoc(SuaLopHocViewModel model)
        {
            string uniqueFileName = string.Empty;
            string uniqueFileName2 = string.Empty;

            if (model.QDMoLop != null)
            {
                if (model.QDMoLopDaTonTai != null)
                {
                    string filePath = Path.Combine(hostingEnviroment.WebRootPath, "Files", model.QDMoLopDaTonTai);
                    System.IO.File.Delete(filePath);
                }
                uniqueFileName = LuuFileQDMoLopVaoTrongRootUpdate(model);
            }

            if (model.ThoiKhoaBieu != null)
            {
                if (model.ThoiKhoaBieuDaTonTai != null)
                {
                    string filePath = Path.Combine(hostingEnviroment.WebRootPath, "Files", model.ThoiKhoaBieuDaTonTai);
                    System.IO.File.Delete(filePath);
                }
                uniqueFileName2 = LuuFileTKBVaoTrongRootUpdate(model);
            }


            LopHocModel newLopHoc = new LopHocModel()
            {
                LopHoc_Id = model.LopHoc_Id,
                MaLop = model.MaLop,
                TenLop = model.TenLop,
                NgayKhaiGiang = model.NgayKhaiGiang,
                NgayKetThuc = model.NgayKetThuc,
                SoLuongHV = model.SoLuongHV,
                QDMoLop = uniqueFileName,
                ThoiKhoaBieu = uniqueFileName2,
                GhiChu = model.GhiChu,
                CTDaoTao_Id = model.CTDaoTao_Id
            };


            ResponseLopHoc ketQua = new ResponseLopHoc();
            var url = $"{Common.Common.ApiUrl}/LopHoc/SuaLopHoc";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(newLopHoc);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var resKetQua = streamReader.ReadToEnd();
                ketQua = JsonConvert.DeserializeObject<ResponseLopHoc>(resKetQua);
            }

            if (ketQua.Id != "0")
            {
                ViewBag.DanhSachCTDaoTao = DanhSachCTDaoTao();
                TempData["ThanhCong"] = ketQua.Message;
                ModelState.Clear();
            }
            else
            {
                TempData["Loi"] = "Cập nhật lớp học thất bại";
            }
            return View(new SuaLopHocViewModel() { });
        }

        public IActionResult XoaLopHoc(int id)
        {
            int ketQua = 0;
            var url = $"{Common.Common.ApiUrl}/LopHoc/XoaLopHoc/{id}";
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

            return RedirectToAction("Index", "LopHoc");


        }







        private List<CTDaoTaoItem> DanhSachCTDaoTao()
        {
            var DanhSachCTDaoTao = new List<CTDaoTaoItem>();
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
                DanhSachCTDaoTao = JsonConvert.DeserializeObject<List<CTDaoTaoItem>>(responseData);
            }
            return DanhSachCTDaoTao;
        }

        private string LuuFileQDMoLopVaoTrongRoot(TaoLopHocViewModel model)
        {
            string uniqueFilename = null;
            if (model.QDMoLop != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "FilesLopHoc");
                uniqueFilename = Guid.NewGuid() + "_" + model.QDMoLop.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.QDMoLop.CopyTo(filestream);
                }
            }
            return uniqueFilename;
        }

        private string LuuFileTKBVaoTrongRoot(TaoLopHocViewModel model)
        {
            string uniqueFilename = null;
            if (model.ThoiKhoaBieu != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "FilesLopHoc");
                uniqueFilename = Guid.NewGuid() + "_" + model.ThoiKhoaBieu.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.ThoiKhoaBieu.CopyTo(filestream);
                }
            }
            return uniqueFilename;
        }

        private string LuuFileQDMoLopVaoTrongRootUpdate(SuaLopHocViewModel model)
        {
            string uniqueFilename = null;
            if (model.QDMoLop != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "FilesLopHoc");
                uniqueFilename = Guid.NewGuid() + "_" + model.QDMoLop.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.QDMoLop.CopyTo(filestream);
                }
            }
            return uniqueFilename;
        }

        private string LuuFileTKBVaoTrongRootUpdate(SuaLopHocViewModel model)
        {
            string uniqueFilename = null;
            if (model.ThoiKhoaBieu != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "FilesLopHoc");
                uniqueFilename = Guid.NewGuid() + "_" + model.ThoiKhoaBieu.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.ThoiKhoaBieu.CopyTo(filestream);
                }
            }
            return uniqueFilename;
        }

    }
}
