using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Models.LopHoc
{
    public class TaoLopHocViewModel
    {
        [Display(Name = "Mã lớp")]
        [Required(ErrorMessage =  "Không được để trống")]
        public string MaLop { get; set; }

        [Display(Name = "Tên lớp")]
        [Required(ErrorMessage = "Không được để trống")]
        public string TenLop { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày khai giảng")]
        public DateTime NgayKhaiGiang { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày kết thúc")]
        public DateTime NgayKetThuc { get; set; }

        [Display(Name = "Số lượng học viên")]
        public int SoLuongHV { get; set; }

        [Display(Name = "Quy định mở lớp")]
        public IFormFile QDMoLop { get; set; }

        [Display(Name = "Thời khóa biểu")]
        public IFormFile ThoiKhoaBieu { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [Display(Name = "Chương trình đào tạo")]
        public int CTDaoTao_Id { get; set; }

    }
}
