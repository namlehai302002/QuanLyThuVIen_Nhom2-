using System;
using System.Collections.Generic;
using System.Linq;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BusChiTietNhapSach
    {
        private readonly DALChiTietNhapSach dalCT = new DALChiTietNhapSach();

        public List<ChiTietNhapSach> LayTatCa()
        {
            return dalCT.GetAll();
        }

        public ChiTietNhapSach LayTheoMa(string maChiTiet)
        {
            return dalCT.GetByMa(maChiTiet);
        }

        public void Them(ChiTietNhapSach ct)
        {
            if (LayTheoMa(ct.MaChiTietNhap) != null)
                throw new Exception("Mã chi tiết đã tồn tại.");
            dalCT.Insert(ct);
        }

        public void CapNhat(ChiTietNhapSach ct)
        {
            dalCT.Update(ct);
        }

        public void Xoa(string maChiTiet)
        {
            if (!dalCT.Delete(maChiTiet))
                throw new Exception("Không thể xóa hoặc mã không tồn tại.");
        }

        public string TaoMaTuDong()
        {
            var danhSach = LayTatCa();
            if (danhSach.Count == 0) return "CTN001";
            string maxMa = danhSach.Max(ct => ct.MaChiTietNhap);
            int so = int.Parse(maxMa.Substring(3)) + 1;
            return "CTN" + so.ToString("D3");
        }
    }
}
