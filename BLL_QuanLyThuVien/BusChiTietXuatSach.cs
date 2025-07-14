using System;
using System.Collections.Generic;
using System.Linq;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BUSChiTietXuatSach
    {
        private readonly DALChiTietXuatSach dalCTXS = new DALChiTietXuatSach();

        public List<ChiTietXuatSach> LayTatCa()
        {
            return dalCTXS.GetAll();
        }

        public ChiTietXuatSach LayTheoMa(string ma)
        {
            return dalCTXS.GetByMa(ma);
        }

        public void Them(ChiTietXuatSach ct)
        {
            if (LayTheoMa(ct.MaChiTietXuat) != null)
                throw new Exception("Mã chi tiết đã tồn tại!");

            dalCTXS.Insert(ct);
        }

        public void CapNhat(ChiTietXuatSach ct)
        {
            dalCTXS.Update(ct);
        }

        public void Xoa(string ma)
        {
            if (!dalCTXS.Delete(ma))
                throw new Exception("Không tìm thấy mã hoặc đang được sử dụng!");
        }

        public string TaoMaTuDong()
        {
            var danhSach = LayTatCa();
            if (danhSach.Count == 0) return "CTX001";
            string maxMa = danhSach.Max(c => c.MaChiTietXuat);
            int so = int.Parse(maxMa.Substring(3)) + 1;
            return "CTX" + so.ToString("D3");
        }
    }
}
