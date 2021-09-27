using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Models.ChuongTrinhDaoTao
{
    public class SuaCTDaoTaoView : TaoCTDaoTaoView
    {
        public int CTDaoTao_Id { get; set; }
              
        public string NoiDungCTDaTonTai { get; set; }
       
        public string QuyDinhBanHanhDaTonTai { get; set; }

    }
}
