using System;
using System.Collections.Generic;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BUSXuatSach
    {
        private readonly DALXuatSach dalXuat = new DALXuatSach();

        public List<XuatSach> LayTatCaXuatSach()
        {
            return dalXuat.GetAll();
        }

        public XuatSach LayXuatSachTheoMa(string maXuat)
        {
            return dalXuat.GetByMa(maXuat);
        }

        public void ThemXuatSach(XuatSach xs)
        {
            var existing = LayXuatSachTheoMa(xs.MaXuat);
            if (existing != null)
            {
                throw new Exception($"Mã xuất '{xs.MaXuat}' đã tồn tại.");
            }
            dalXuat.Insert(xs);
        }

        public void CapNhatXuatSach(XuatSach xs)
        {
            dalXuat.Update(xs);
        }

        public void XoaXuatSach(string maXuat)
        {
            bool deleted = dalXuat.Delete(maXuat);
            if (!deleted)
                throw new Exception($"Không tìm thấy mã xuất '{maXuat}' để xoá.");
        }

        public string TaoMaXuatTuDong()
        {
            var danhSach = LayTatCaXuatSach();
            if (danhSach.Count == 0) return "X001";

            string maxMa = danhSach.Max(x => x.MaXuat);
            int so = int.Parse(maxMa.Substring(1)) + 1;
            return "X" + so.ToString("D3");
        }
    }
}
