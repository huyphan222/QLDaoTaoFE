using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Models.LopHoc
{
    public class LopHocViewModel
    {
        public int LopHoc_Id { get; set; }

        [Display(Name = "Mã lớp")]
        public string MaLop { get; set; }

        [Display(Name = "Tên lớp")]
        public string TenLop { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày khai giảng")]
        public DateTime NgayKhaiGiang { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày kết thúc")]
        public DateTime NgayKetThuc { get; set; }

        [Display(Name = "Số lượng học viên")]
        public int SoLuongHV { get; set; }

        [Display(Name = "QĐ mở lớp")]
        public string QDMoLop { get; set; }

        [Display(Name = "Thời khoa biểu")]
        public string ThoiKhoaBieu { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

    }
}
