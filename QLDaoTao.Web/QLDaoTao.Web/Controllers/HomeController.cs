using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QLDaoTao.Web.Models;
using QLDaoTao.Web.Models.ChuongTrinhDaoTao;
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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
