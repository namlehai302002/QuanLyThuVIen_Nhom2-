using System;
using System.Collections.Generic;
using System.Linq;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BUSXuatSach
    {
        private readonly DALXuatSach dal = new DALXuatSach();

        public List<XuatSach> LayTatCaXuatSach()
        {
            return dal.GetAll();
        }

        public XuatSach LayTheoMa(string maXuat)
        {
            return dal.GetByMa(maXuat);
        }

        public void ThemXuatSach(XuatSach xs)
        {
            if (LayTheoMa(xs.MaXuat) != null)
                throw new Exception("Mã xuất đã tồn tại.");
            dal.Insert(xs);
        }

        public void CapNhatXuatSach(XuatSach xs)
        {
            dal.Update(xs);
        }

        public void XoaXuatSach(string maXuat)
        {
            if (!dal.Delete(maXuat))
                throw new Exception("Không tìm thấy hoặc không thể xóa mã xuất.");
        }

        public string TaoMaXuatTuDong()
        {
            var list = LayTatCaXuatSach();
            if (list.Count == 0) return "X001";
            string max = list.Max(x => x.MaXuat);
            int so = int.Parse(max.Substring(1)) + 1;
            return "X" + so.ToString("D3");
        }
    }
}
