using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Models.LopHoc
{
    public class LopHocModel
    {
        public int LopHoc_Id { get; set; }

        public string MaLop { get; set; }

        public string TenLop { get; set; }

        public DateTime NgayKhaiGiang { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public int SoLuongHV { get; set; }

        public string QDMoLop { get; set;}

        public string ThoiKhoaBieu { get; set; }

        public string GhiChu { get; set; }

        public int CTDaoTao_Id { get; set; }
    }
}
