using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLDaoTao.Web.Models.HocVien;
using QLDaoTao.Web.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Controllers
{
    public class HocVienController : Controller
    {
        private readonly IWebHostEnvironment hostingEnviroment;

        public HocVienController(IWebHostEnvironment hostingEnviroment)
        {
            this.hostingEnviroment = hostingEnviroment;
        }

        public IActionResult Index()
        {
            var DanhSachHocVien = new List<HocVienViewModel>();
            var url = $"{Common.Common.ApiUrl}/HocVien/DanhSachHocVien";
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
                DanhSachHocVien = JsonConvert.DeserializeObject<List<HocVienViewModel>>(responseData);
            }
            ViewBag.LopHoc = DanhSachLopHoc();
            return View(DanhSachHocVien);
        }

        public IActionResult DanhSachHocVienTheoLopHoc(int id)
        {
            var DanhSachHocVien = new List<HocVienViewModel>();
            var url = $"{Common.Common.ApiUrl}/HocVien/DanhSachHocVienTheoLopHoc/{id}";
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
                DanhSachHocVien = JsonConvert.DeserializeObject<List<HocVienViewModel>>(responseData);

            }

            ViewBag.TenLopHoc = DanhSachLopHoc().Where(e => e.LopHoc_Id == id).FirstOrDefault().TenLop;
            return View(DanhSachHocVien);
        }

        public IActionResult TaoHocVien()
        {
            ViewBag.DanhSachCTDaoTaoHienThi = DanhSachCTDaoTaoHienThi();
            return View();
        }

        [HttpPost]
        public IActionResult TaoHocVien(TaoHocVienViewModel model)
        {
            HocVienModel newHocVien = new HocVienModel()
            {
                MaHV = model.MaHV,
                HoTen = model.HoTen,
                GioiTinh = model.GioiTinh,
                NgaySinh = model.NgaySinh,
                NoiSinh = model.NoiSinh,
                SDT = model.SDT,
                DiaChi = model.DiaChi,
                DanToc = model.DanToc,
                ChucVu = model.ChucVu,
                ChuyenNganh = model.ChuyenNganh,
                Email = model.Email,
                HinhAnh = model.HinhAnh,
                NgheNghiep = model.NgheNghiep,
                NguyenQuan = model.NguyenQuan,
                SoQDHocNghe = model.SoQDHocNghe,
                HoTenNBT = model.HoTenNBT,
                TrinhDo = model.TrinhDo,
                SDTNBT = model.SDTNBT,
                SoBHXH = model.SoBHXH,
                TenCoQuan = model.TenCoQuan,
                GhiChu = model.GhiChu,
                LopHoc_Id = model.LopHoc_Id
            };

            var url = $"{Common.Common.ApiUrl}/HocVien/TaoHocVien";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(newHocVien);
                streamWriter.Write(json);
            }

            ResponseHocVien ketQua = new ResponseHocVien();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var resKetQua = streamReader.ReadToEnd();

                ketQua = JsonConvert.DeserializeObject<ResponseHocVien>(resKetQua);
            }

            if (ketQua.Id != "0")
            {
                TempData["ThanhCong"] = ketQua.Message;
                ModelState.Clear();
                ViewBag.DanhSachLopHoc = DanhSachLopHoc();
            }
            else
            {
                TempData["Loi"] = "Tên chương trình không được trùng";
            }
            return View(new TaoHocVienViewModel() { });
        }



        public IActionResult TaoHocVienAptech()
        {  
            ViewBag.DanhSachLopHoc = DanhSachLopHoc();
            TempData["ThanhCong"] = null;
            TempData["Loi"] = null;
            return View();
        }

        [HttpPost]
        public IActionResult TaoHocVienAptech(TaoHocVienAptechViewModel model)
        {
            string uniqueFilename = LuuFileHinhAnhHVApTechVaoTrongRoot(model);
            

            HocVienModel newHocVien = new HocVienModel()
            {
                MaHV = model.MaHV,
                HoTen = model.HoTen,
                NgaySinh = model.NgaySinh,
                NoiSinh = model.NoiSinh,
                SDT = model.SDT,
                GioiTinh = model.GioiTinh,
                DiaChi =  model.DiaChi,
                Email = model.Email,
                HinhAnh = uniqueFilename,
                TrinhDo = model.TrinhDo,
                NgheNghiep = model.NgheNghiep,
                HoTenNBT = model.HoTenNBT,
                SDTNBT = model.SDTNBT,
                GhiChu = model.GhiChu,
                LopHoc_Id = model.LopHoc_Id,
             
            };

            
            var url = $"{Common.Common.ApiUrl}/HocVien/TaoHocVien";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(newHocVien);
                streamWriter.Write(json);
            }

            ResponseHocVien ketQua = new ResponseHocVien();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var resKetQua = streamReader.ReadToEnd();

                ketQua = JsonConvert.DeserializeObject<ResponseHocVien>(resKetQua);
            }

            if (ketQua.Id != "0")
            {
                TempData["ThanhCong"] = ketQua.Message;
                ModelState.Clear();
                ViewBag.DanhSachLopHoc = DanhSachLopHoc();
            }
            else
            {
                TempData["Loi"] = "Tên chương trình không được trùng";
            }
            return View(new TaoHocVienAptechViewModel() { });
        }

        public IActionResult TaoHocVienKhoaNganHan()
        {
            ViewBag.DanhSachLopHoc = DanhSachLopHoc().Where(e => e.CTDaoTao_Id == 1058).ToList();
            TempData["ThanhCong"] = null;
            TempData["Loi"] = null;
            return View();
        }

        [HttpPost]
        public IActionResult TaoHocVienKhoaNganHan(TaoHocVienKhoaNganHanViewModel model)
        {         
            string uniqueFilename = LuuFileHinhAnhHVNganHanVaoTrongRoot(model);
            HocVienModel newHocVien = new HocVienModel()
            {
                MaHV = model.MaHV,
                HoTen = model.HoTen,
                NgaySinh = model.NgaySinh,
                NoiSinh = model.NoiSinh,
                SDT = model.SDT,
                GioiTinh = model.GioiTinh,
                DiaChi = model.DiaChi,
                Email = model.Email,
                HinhAnh = uniqueFilename,
                NgheNghiep = model.NgheNghiep,
                GhiChu = model.GhiChu,
                LopHoc_Id = model.LopHoc_Id,
            };


            var url = $"{Common.Common.ApiUrl}/HocVien/TaoHocVien";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(newHocVien);
                streamWriter.Write(json);
            }

            ResponseHocVien ketQua = new ResponseHocVien();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var resKetQua = streamReader.ReadToEnd();

                ketQua = JsonConvert.DeserializeObject<ResponseHocVien>(resKetQua);
            }

            if (ketQua.Id != "0")
            {
                TempData["ThanhCong"] = ketQua.Message;
                ModelState.Clear();
                ViewBag.DanhSachLopHoc = DanhSachLopHoc();
            }
            else
            {
                TempData["Loi"] = "Tên chương trình không được trùng";
            }
            return View(new TaoHocVienKhoaNganHanViewModel() { });

        }

        public IActionResult SuaHocVienKhoaNganHan(int id)
        {
            var HocVien = new HocVienModel();
            var url = $"{Common.Common.ApiUrl}/HocVien/LayHocVienBangId/{id}";
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
                HocVien = JsonConvert.DeserializeObject<HocVienModel>(responseData);
            }
            TempData["ThanhCong"] = null;

            SuaHocVienKhoaNganHanViewModel suaHocVienKhoaNganHan = new SuaHocVienKhoaNganHanViewModel
            {                          
                HocVien_Id = HocVien.HocVien_Id,
                MaHV = HocVien.MaHV,
                HoTen = HocVien.HoTen,
                NgaySinh = HocVien.NgaySinh,
                NoiSinh = HocVien.NoiSinh,
                DiaChi = HocVien.DiaChi,
                GioiTinh = HocVien.GioiTinh,
                SDT = HocVien.SDT,
                HinhAnhDaTonTai = HocVien.HinhAnh,
                Email = HocVien.Email,
                NgheNghiep = HocVien.NgheNghiep,
                GhiChu = HocVien.GhiChu,
                LopHoc_Id = HocVien.LopHoc_Id    
            };
            return View(suaHocVienKhoaNganHan);
        }

        [HttpPost]
        public IActionResult SuaHocVienKhoaNganHan(SuaHocVienKhoaNganHanViewModel model)
        {
            string uniqueFileName = string.Empty;

            if (model.HinhAnh != null)
            {
                if (model.HinhAnhDaTonTai != null)
                {
                    string filePath = Path.Combine(hostingEnviroment.WebRootPath, "Files", model.HinhAnhDaTonTai);
                    System.IO.File.Delete(filePath);
                }
                uniqueFileName = LuuFileHiAnhHVNganHangVaoTrongRootUpdate(model);
            }

            HocVienModel newHVKhoaNganHan = new HocVienModel()
            {
                HocVien_Id = model.HocVien_Id,
                MaHV = model.MaHV,
                HoTen = model.HoTen,
                NgaySinh = model.NgaySinh,
                NoiSinh = model.NoiSinh,
                DiaChi = model.DiaChi,
                GioiTinh = model.GioiTinh,
                SDT = model.SDT,
                HinhAnh = uniqueFileName,
                Email = model.Email,
                NgheNghiep = model.NgheNghiep,
                GhiChu = model.GhiChu,
                LopHoc_Id = model.LopHoc_Id
            };

            var url = $"{Common.Common.ApiUrl}/HocVien/SuaHocVien";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(newHVKhoaNganHan);
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
            return View(new SuaHocVienKhoaNganHanViewModel() { });
        }



        private List<LopHocItem> DanhSachLopHoc()
        {
            var DanhSachLopHoc = new List<LopHocItem>();
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
                DanhSachLopHoc = JsonConvert.DeserializeObject<List<LopHocItem>>(responseData);
            }
            return DanhSachLopHoc;
        }

        private List<CTDaoTaoHienThiItem> DanhSachCTDaoTaoHienThi()
        {
            var DanhSachCTDaoTaoHienThi = new List<CTDaoTaoHienThiItem>();
            var url = $"{Common.Common.ApiUrl}/LopHoc/DanhSachCTDaoTaoHienThi";
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
                DanhSachCTDaoTaoHienThi = JsonConvert.DeserializeObject<List<CTDaoTaoHienThiItem>>(responseData);
            }
            return DanhSachCTDaoTaoHienThi;
        }




        private string LuuFileHinhAnhHVApTechVaoTrongRoot(TaoHocVienAptechViewModel model)
        {
            string uniqueFilename = null;
            if (model.HinhAnh != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "FilesHocVien");
                uniqueFilename = Guid.NewGuid() + "_" + model.HinhAnh.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.HinhAnh.CopyTo(filestream);
                }
            }
            return uniqueFilename;
        }

        private string LuuFileHinhAnhHVNganHanVaoTrongRoot(TaoHocVienKhoaNganHanViewModel model)
        {
            string uniqueFilename = null;
            if (model.HinhAnh != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "FilesHocVien");
                uniqueFilename = Guid.NewGuid() + "_" + model.HinhAnh.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.HinhAnh.CopyTo(filestream);
                }
            }
            return uniqueFilename;
        }

        private string LuuFileHiAnhHVNganHangVaoTrongRootUpdate(SuaHocVienKhoaNganHanViewModel model)
        {
            string uniqueFilename = null;
            if (model.HinhAnh != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "Files");
                uniqueFilename = Guid.NewGuid() + "_" + model.HinhAnh.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.HinhAnh.CopyTo(filestream);
                }
            }
            return uniqueFilename;
        }
    }
}
