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
            return dal.SelectAll();
        }

        public XuatSach LayTheoMa(string maXuat)
        {
            return dal.GetByMa(maXuat);
        }

        public void ThemXuatSach(XuatSach xs)
        {
            if (LayTheoMa(xs.MaXuat) != null)
                throw new Exception("Mã xuất đã tồn tại.");

            dal.InsertXuatSach(xs);
        }

        public void CapNhatXuatSach(XuatSach xs)
        {
            string error = dal.UpdateXuatSach(xs);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
        }

        public void XoaXuatSach(string maXuat)
        {
            string error = dal.DeleteXuatSach(maXuat);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
        }

        public List<XuatSach> TimKiemXuatSach(string keyword)
        {
            return dal.SearchXuatSach(keyword);
        }

        public string TaoMaXuatTuDong()
        {
            var list = LayTatCaXuatSach();
            if (list.Count == 0) return "X001";

            // Tìm mã lớn nhất và tăng số
            string maxMa = list.Max(x => x.MaXuat);
            int so = int.Parse(maxMa.Substring(1)) + 1;
            return "X" + so.ToString("D3");
        }
    }
}
