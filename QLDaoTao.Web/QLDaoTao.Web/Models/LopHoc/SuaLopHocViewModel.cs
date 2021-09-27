using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Models.LopHoc
{
    public class SuaLopHocViewModel : TaoLopHocViewModel
    {
        public int LopHoc_Id { get; set; }

        public string QDMoLopDaTonTai { get; set; }

        public string ThoiKhoaBieuDaTonTai { get; set; }


    }
}
