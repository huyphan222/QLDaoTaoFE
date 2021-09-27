using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Models.ChuongTrinhDaoTao
{
    public class CTDaoTao
    {
        public int CTDaoTao_Id { get; set; }

        [Required]
        [Display(Name = "Mã chương tình")]
        public string MaCT { get; set; }

        [Required]
        [Display(Name = "Tên chương trình")]
        public string TenCT { get; set; }

        [Required]
        [Display(Name = "Thời gian đào tạo")]
        public int ThoiGianDaotao { get; set; }

        [Required]
        [Display(Name = "Học phí")]
        public long HocPhi { get; set; }

        [Display(Name = "Ngày ban hành")]
        public DateTime NgayBanHanh { get; set; }

        [Display(Name = "Nội dụng chương trình")]
        public string NoiDungCT { get; set; }

        [Display(Name = "Quy định ban hành")]
        public string QDBanHanh { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }
    }
}
