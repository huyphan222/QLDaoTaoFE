using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Models.HocVien
{
    public class TaoHocVienKhoaNganHanViewModel
    {
        [Required]
        [Display(Name = "Mã học viên")]
        public string MaHV { get; set; }

        [Required]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime NgaySinh { get; set; }

        [Required]
        [Display(Name = "Nơi sinh")]
        public string NoiSinh { get; set; }

        [Required]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Required]
        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }

        [Phone]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }

        [Display(Name = "Hình ảnh")]
        public IFormFile HinhAnh { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Nghề nghiệp")]
        public string NgheNghiep { get; set; }

        [Display(Name = "Ghi chú")]
        [MaxLength(500)]
        public string GhiChu { get; set; }

        [Display(Name = "Lớp")]
        public int LopHoc_Id { get; set; }
    }
}
