using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLDaoTao.Web.Models.ChuongTrinhDaoTao
{
    public class CTDaoTaoView
    {
        public int CTDaoTao_Id { get; set; }

        [Display(Name = "Mã chương tình")]
        public string MaCT { get; set; }

        [Display(Name = "Tên chương trình")]
        public string TenCT { get; set; }

        [Display(Name = "Thời gian đào tạo")]
        public float ThoiGianDaotao { get; set; }

        [Display(Name ="Ngày ban hành")]
        [DataType(DataType.Date)]
        public DateTime NgayBanHanh { get; set; }

        [Display(Name ="Nội dụng CT")]
        public string NoiDungCT { get; set; }

        [Display(Name ="Quy định")]
        public string QDBanHanh { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }
    }
}
