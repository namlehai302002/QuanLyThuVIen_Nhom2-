using System;
using System.Collections.Generic;
using System.Linq;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BUSNhapSach
    {
        private readonly DALNhapSach dal = new DALNhapSach();

        public List<NhapSach> LayTatCaNhapSach()
        {
            return dal.SelectAll();
        }

        public NhapSach LayNhapSachTheoMa(string maNhap)
        {
            return dal.GetByMa(maNhap);
        }

        public void ThemNhapSach(NhapSach nhap)
        {
            if (LayNhapSachTheoMa(nhap.MaNhap) != null)
                throw new Exception($"Mã nhập '{nhap.MaNhap}' đã tồn tại.");

            dal.InsertNhapSach(nhap);
        }

        public void CapNhatNhapSach(NhapSach nhap)
        {
            string error = dal.UpdateNhapSach(nhap);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
        }

        public void XoaNhapSach(string maNhap)
        {
            string error = dal.DeleteNhapSach(maNhap);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
        }

        public List<NhapSach> TimKiem(string keyword)
        {
            return dal.Search(keyword);
        }

        public string TaoMaNhapTuDong()
        {
            var danhSach = LayTatCaNhapSach();
            if (danhSach.Count == 0) return "N001";

            string maxMa = danhSach.Max(x => x.MaNhap);
            int so = int.Parse(maxMa.Substring(1)) + 1;
            return "N" + so.ToString("D3");
        }
    }
}
