using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Models.HocVien
{
    public class HocVienModel
    {
        [Key]
        public int HocVien_Id { get; set; }

        [Required]
        public string MaHV { get; set; }

        [Required]
        public string HoTen { get; set; }

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        public string NoiSinh { get; set; }

        [Required]
        public string DiaChi { get; set; }

        [Required]
        public string GioiTinh { get; set; }

        public string SDT { get; set; }

        public string HinhAnh { get; set; }

        public string Email { get; set; }

        public string HoTenNBT { get; set; }

        public string SDTNBT { get; set; }

        public string TrinhDo { get; set; }

        public string ChuyenNganh { get; set; }

        public string NgheNghiep { get; set; }

        public string NguyenQuan { get; set; }

        public string DanToc { get; set; }

        public string SoQDHocNghe { get; set; }

        public string SoBHXH { get; set; }

        public string TenCoQuan { get; set; }

        public string ChucVu { get; set; }

        public string GhiChu { get; set; }

        public int LopHoc_Id { get; set; }

    }
}
