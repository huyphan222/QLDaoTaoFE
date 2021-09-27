using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Models.HocVien
{
    public class HocVienViewModel
    {
        [Key]
        public int HocVien_Id { get; set; }

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
        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }

        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
      
        [Display(Name = "Trình độ")]
        public string TrinhDo { get; set; }

        public int LopHoc_Id { get; set; }

    }
}


//  suaHocVien (id)

//  ViewBag.danhSachLopHoc().where(e => e.LopHoc_Id)
