using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BusTheLoaiSach
    {
        private DALTheLoaiSach dal = new DALTheLoaiSach();

        public List<TheLoaiSach> GetAll()
        {
            return dal.SelectAll();
        }

        public TheLoaiSach? GetById(string ma)
        {
            return dal.GetById(ma);
        }

        public string Add(TheLoaiSach tls)
        {
            if (string.IsNullOrWhiteSpace(tls.MaTheLoai))
                return "Mã thể loại không được để trống.";

            if (dal.GetById(tls.MaTheLoai) != null)
                return "Mã thể loại đã tồn tại.";

            try
            {
                dal.Insert(tls);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string Update(TheLoaiSach tls)
        {
            return dal.Update(tls);
        }

        public string Delete(string ma)
        {
            return dal.Delete(ma);
        }

        public List<TheLoaiSach> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            return GetAll().Where(tls =>
                (tls.MaTheLoai ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                (tls.TenTheLoai ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                (tls.NgayTao?.ToString("dd/MM/yyyy") ?? "").Contains(keyword) ||
                tls.TrangThai.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }
}

