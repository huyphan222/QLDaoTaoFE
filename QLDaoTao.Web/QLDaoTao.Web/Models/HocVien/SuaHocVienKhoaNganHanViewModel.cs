using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Models.HocVien
{
    public class SuaHocVienKhoaNganHanViewModel:TaoHocVienKhoaNganHanViewModel 
    {
        public int HocVien_Id { get; set; }

        public string HinhAnhDaTonTai { get; set; }

    }
}
