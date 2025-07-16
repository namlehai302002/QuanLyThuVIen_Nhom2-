using System;
using System.Collections.Generic;
using System.Data;
using DAL_QUANLYTHUVIEN;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyThuVien
{
    public class DALNhapSach
    {
        public List<NhapSach> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<NhapSach> list = new List<NhapSach>();
            using (SqlDataReader reader = DBUtil.Query(sql, args, cmdType))
            {
                while (reader.Read())
                {
                    list.Add(MapReaderToNhapSach(reader));
                }
            }
            return list;
        }

        public List<NhapSach> SelectAll()
        {
            string sql = "SELECT * FROM NhapSach";
            return SelectBySql(sql, new List<object>());
        }

        public NhapSach GetByMa(string maNhap)
        {
            string sql = "SELECT * FROM NhapSach WHERE MaNhap = @0";
            List<NhapSach> list = SelectBySql(sql, new List<object> { maNhap });
            return list.Count > 0 ? list[0] : null;
        }

        public void InsertNhapSach(NhapSach ns)
        {
            string sql = @"INSERT INTO NhapSach (MaNhap, MaNhanVien, NgayNhap, GhiChu, MaKho) 
                           VALUES (@0, @1, @2, @3, @4)";
            List<object> parameters = new List<object>
            {
                ns.MaNhap,
                ns.MaNhanVien,
                ns.NgayNhap,
                ns.GhiChu ?? (object)DBNull.Value,
                ns.MaKho
            };

            DBUtil.Update(sql, parameters);
        }

        public string UpdateNhapSach(NhapSach ns)
        {
            string sql = @"UPDATE NhapSach 
                           SET MaNhanVien = @0,
                               NgayNhap = @1,
                               GhiChu = @2,
                               MaKho = @3
                           WHERE MaNhap = @4";
            List<object> parameters = new List<object>
            {
                ns.MaNhanVien,
                ns.NgayNhap,
                ns.GhiChu ?? (object)DBNull.Value,
                ns.MaKho,
                ns.MaNhap
            };

            try
            {
                int rows = DBUtil.Update(sql, parameters);
                if (rows == 0)
                    return "Không tìm thấy phiếu nhập để cập nhật.";
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteNhapSach(string maNhap)
        {
            try
            {
                // Xóa chi tiết nhập sách trước
                string sqlChiTiet = "DELETE FROM ChiTietNhapSach WHERE MaNhap = @0";
                DBUtil.Update(sqlChiTiet, new List<object> { maNhap });

                // Xóa nhập sách
                string sql = "DELETE FROM NhapSach WHERE MaNhap = @0";
                DBUtil.Update(sql, new List<object> { maNhap });

                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public List<NhapSach> Search(string keyword)
        {
            string sql = @"SELECT * FROM NhapSach 
                           WHERE MaNhap LIKE @0 
                              OR MaNhanVien LIKE @0 
                              OR MaKho LIKE @0 
                              OR GhiChu LIKE @0";
            List<object> parameters = new List<object> { "%" + keyword + "%" };
            return SelectBySql(sql, parameters);
        }

        private NhapSach MapReaderToNhapSach(SqlDataReader reader)
        {
            return new NhapSach
            {
                MaNhap = reader["MaNhap"].ToString(),
                MaNhanVien = reader["MaNhanVien"].ToString(),
                NgayNhap = Convert.ToDateTime(reader["NgayNhap"]),
                GhiChu = reader["GhiChu"]?.ToString(),
                MaKho = reader["MaKho"].ToString()
            };
        }
    }
}
