using System;
using System.Collections.Generic;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BUSNhapSach
    {
        private readonly DALNhapSach dalNhap = new DALNhapSach();

        public List<NhapSach> LayTatCaNhapSach()
        {
            return dalNhap.GetAll();
        }

        public NhapSach LayNhapSachTheoMa(string maNhap)
        {
            return dalNhap.GetByMa(maNhap);
        }

        public void ThemNhapSach(NhapSach nhap)
        {
            var existing = LayNhapSachTheoMa(nhap.MaNhap);
            if (existing != null)
            {
                throw new Exception($"Mã nhập '{nhap.MaNhap}' đã tồn tại. Vui lòng chọn mã khác.");
            }
            dalNhap.Insert(nhap);
        }

        public void CapNhatNhapSach(NhapSach nhap)
        {
            dalNhap.Update(nhap);
        }

        public void XoaNhapSach(string maNhap)
        {
            bool deleted = dalNhap.Delete(maNhap);
            if (!deleted)
            {
                throw new Exception($"Không tìm thấy mã nhập '{maNhap}' hoặc đang được sử dụng.");
            }

        }

        public string TaoMaNhapTuDong()
        {
            var danhSach = LayTatCaNhapSach();
            if (danhSach.Count == 0) return "N001";

            string maxMa = danhSach.Max(n => n.MaNhap);
            int so = int.Parse(maxMa.Substring(1)) + 1;
            return "N" + so.ToString("D3");
        }
    }
}
