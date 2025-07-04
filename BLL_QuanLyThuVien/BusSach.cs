using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DAL_QUANLYTHUVIEN;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BUSSach
    {
        private readonly DALSach dalsach = new DALSach();


        public List<Sach> LayTatCaSach()
        {
            return dalsach.GetAllSach();
        }



        public Sach LaySachTheoMa(string maSach)
        {
            return dalsach.GetSachByMa(maSach);
        }

        public void ThemSach(Sach sach)
        {
            var existing = LaySachTheoMa(sach.MaSach);
            if (existing != null)
            {
                throw new Exception($"Mã sách '{sach.MaSach}' đã tồn tại, vui lòng nhập mã khác.");
            }
            dalsach.InsertSach(sach);
        }



        public void CapNhatSach(Sach sach)
        {
            dalsach.UpdateSach(sach);
        }




        public void XoaSach(string maSach)
        {
            int affectedRows = dalsach.DeleteSach(maSach);
            if (affectedRows == 0)
            {
                throw new Exception($"Không tìm thấy sách với mã '{maSach}' hoặc sách đang được sử dụng.");
            }
        }



        public List<Sach> TimKiemSach(string tuKhoa)
        {
            return dalsach.TimKiemSach(tuKhoa);
        }




        public bool KiemTraTheLoaiTonTai(string maTheLoai)
        {
            string sql = "SELECT COUNT(*) FROM TheLoaiSach WHERE MaTheLoai = @0";
            object result = DBUtil.ExecuteScalar(sql, new List<object> { maTheLoai });
            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                return count > 0;
            }
            return false;
        }
        public string TaoMaSachTuDong()
        {
            var danhSach = LayTatCaSach();
            if (danhSach.Count == 0) return "S001";

            // Lấy mã lớn nhất
            string maxMa = danhSach.Max(s => s.MaSach);
            int so = int.Parse(maxMa.Substring(1)) + 1;
            return "S" + so.ToString("D3"); // S001, S002,...
        }
    }
}
