using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLyThuVien
{
    public class TacGia
    {
        public string MaTacGia { get; set; }
        public string TenTacGia { get; set; }
        public string? QuocTich { get; set; }
        public bool? TrangThai { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
