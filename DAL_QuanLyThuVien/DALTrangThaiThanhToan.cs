using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QUANLYTHUVIEN;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyThuVien
{
    public class DALTrangThaiThanhToan
    {
        public List<TrangThaiThanhToan> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<TrangThaiThanhToan> list = new List<TrangThaiThanhToan>();
            using (SqlDataReader reader = DBUtil.Query(sql, args, cmdType))
            {
                while (reader.Read())
                {
                    list.Add(MapReaderToTrangThai(reader));
                }
            }
            return list;
        }

        public List<TrangThaiThanhToan> SelectAll()
        {
            string sql = "SELECT * FROM TrangThaiThanhToan";
            return SelectBySql(sql, new List<object>());
        }

        public TrangThaiThanhToan? GetById(string maTrangThai)
        {
            string sql = "SELECT * FROM TrangThaiThanhToan WHERE MaTrangThai = @0";
            var args = new List<object> { maTrangThai };

            using (SqlDataReader reader = DBUtil.Query(sql, args))
            {
                if (reader.HasRows && reader.Read())
                {
                    return MapReaderToTrangThai(reader);
                }
            }
            return null;
        }

        public void Insert(TrangThaiThanhToan tttt)
        {
            string sql = @"INSERT INTO TrangThaiThanhToan (MaTrangThai, TenTrangThai, NgayTao) 
                           VALUES (@0, @1, @2)";
            var args = new List<object> { tttt.MaTrangThai, tttt.TenTrangThai, tttt.NgayTao };
            DBUtil.Update(sql, args);
        }

        public string Update(TrangThaiThanhToan tttt)
        {
            string sql = @"UPDATE TrangThaiThanhToan 
                           SET TenTrangThai = @0,
                               NgayTao = @1 
                           WHERE MaTrangThai = @2";
            var args = new List<object> { tttt.TenTrangThai, tttt.NgayTao, tttt.MaTrangThai };

            try
            {
                DBUtil.Update(sql, args);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string Delete(string ma)
        {
            try
            {
                // Bước 1: Xoá dữ liệu liên quan trong bảng MuonTraSach trước
                string sql1 = "DELETE FROM MuonTraSach WHERE MaTrangThai = @0";
                DBUtil.Update(sql1, new List<object> { ma });

                // Bước 2: Xoá dữ liệu trong bảng TrangThaiThanhToan
                string sql2 = "DELETE FROM TrangThaiThanhToan WHERE MaTrangThai = @0";
                DBUtil.Update(sql2, new List<object> { ma });

                return ""; // Thành công
            }
            catch (Exception ex)
            {
                return "Lỗi khi xoá: " + ex.Message;
            }
        }


        public List<TrangThaiThanhToan> Search(string keyword)
        {
            string sql = @"SELECT * FROM TrangThaiThanhToan 
                           WHERE MaTrangThai LIKE @0 OR TenTrangThai LIKE @0";
            var args = new List<object> { "%" + keyword + "%" };
            return SelectBySql(sql, args);
        }

        private TrangThaiThanhToan MapReaderToTrangThai(SqlDataReader reader)
        {
            return new TrangThaiThanhToan
            {
                MaTrangThai = reader["MaTrangThai"].ToString(),
                TenTrangThai = reader["TenTrangThai"].ToString(),
                NgayTao = reader["NgayTao"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTao"]) : (DateTime?)null
            };
        }
    }
}

