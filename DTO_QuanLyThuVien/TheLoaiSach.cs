using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLyThuVien
{
    public class TheLoaiSach
    {
        public string MaTheLoai { get; set; }
        public string TenTheLoai { get; set; }
        public bool? TrangThai { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
