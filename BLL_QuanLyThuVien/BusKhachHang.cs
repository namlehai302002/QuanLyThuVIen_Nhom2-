using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BusKhachHang
    {
        private DALKhachHang dalKhachHang = new DALKhachHang();

        public List<KhachHang> GetKhachHangList()
        {
            return dalKhachHang.SelectAll();
        }

        public string AddKhachHang(KhachHang kh)
        {
            if (string.IsNullOrWhiteSpace(kh.MaKhachHang))
                return "Mã khách hàng không được để trống.";

            return dalKhachHang.InsertKhachHang(kh);
        }

        public string UpdateKhachHang(KhachHang kh)
        {
            return dalKhachHang.UpdateKhachHang(kh);
        }

        public string DeleteKhachHang(string maKH)
        {
            return dalKhachHang.DeleteKhachHang(maKH);
        }

        public List<KhachHang> SearchKhachHang(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetKhachHangList();

            return dalKhachHang.SearchKhachHang(keyword);
        }
    }
}

