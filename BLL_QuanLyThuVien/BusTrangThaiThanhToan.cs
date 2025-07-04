using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BusTrangThaiThanhToan
    {
        private DALTrangThaiThanhToan dal = new DALTrangThaiThanhToan();

        public List<TrangThaiThanhToan> GetAll()
        {
            return dal.SelectAll();
        }

        public TrangThaiThanhToan? GetById(string maTrangThai)
        {
            return dal.GetById(maTrangThai);
        }

        public string Add(TrangThaiThanhToan tttt)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tttt.MaTrangThai))
                    return "Mã trạng thái không được để trống.";

                if (dal.GetById(tttt.MaTrangThai) != null)
                    return "Mã trạng thái đã tồn tại.";

                dal.Insert(tttt);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string Update(TrangThaiThanhToan tttt)
        {
            return dal.Update(tttt);
        }

        public string Delete(string maTrangThai)
        {
            return dal.Delete(maTrangThai);
        }

        public List<TrangThaiThanhToan> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            return dal.Search(keyword);
        }
    }
}


