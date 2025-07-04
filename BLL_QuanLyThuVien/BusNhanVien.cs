using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DAL_QUANLYTHUVIEN;
using DTO_QuanLyThuVien;

namespace BLL_QUANLYTHUVIEN
{
    public class BusNhanVien
    {
        DALNhanVien dalNhanVien = new DALNhanVien();

        public NhanVien? DangNhap(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            return dalNhanVien.getNhanVien1(username, password);
        }

        public List<NhanVien> GetNhanVienList()
        {
            return dalNhanVien.SelectAll();
        }

        public string AddNhanVien(NhanVien nv)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nv.MaNhanVien))
                    return "Mã nhân viên không được để trống.";

                if (dalNhanVien.GetNhanVienByEmail(nv.Email) != null)
                    return "Email đã tồn tại.";

                dalNhanVien.InsertNhanVien(nv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateNhanVien(NhanVien nv)
        {
            try
            {
                // Lấy nhân viên khác có cùng email
                var existingNV = dalNhanVien.GetNhanVienByEmail(nv.Email);

                // Nếu đã tồn tại email này và mã nhân viên khác => trùng
                if (existingNV != null && existingNV.MaNhanVien != nv.MaNhanVien)
                {
                    return "Email đã tồn tại.";
                }

                return dalNhanVien.UpdateNhanVien(nv);
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteNhanVien(string maNV)
        {
            try
            {
                return dalNhanVien.DeleteNhanVien(maNV);
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public List<NhanVien> SearchNhanVien(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return GetNhanVienList();

            var danhSach = GetNhanVienList();

            return danhSach.Where(nv =>
            (nv.MaNhanVien ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            (nv.Ten ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            (nv.Email ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            (nv.MatKhau ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            (nv.SoDienThoai ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            nv.VaiTro.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            nv.TrangThai.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            (nv.NgayTao?.ToString("dd/MM/yyyy") ?? "").Contains(keyword)
        ).ToList();
        }
    }

}
