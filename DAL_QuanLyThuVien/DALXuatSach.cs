using System;
using System.Collections.Generic;
using System.Data;
using DAL_QUANLYTHUVIEN;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyThuVien
{
    public class DALXuatSach
    {
        public List<XuatSach> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<XuatSach> list = new List<XuatSach>();
            using (SqlDataReader reader = DBUtil.Query(sql, args, cmdType))
            {
                while (reader.Read())
                {
                    list.Add(MapReaderToXuatSach(reader));
                }
            }
            return list;
        }

        public List<XuatSach> SelectAll()
        {
            string sql = "SELECT * FROM XuatSach";
            return SelectBySql(sql, new List<object>());
        }

        public void InsertXuatSach(XuatSach xs)
        {
            string sql = @"INSERT INTO XuatSach (MaXuat, MaNhanVien, NgayXuat, LyDo, MaKho)
                           VALUES (@0, @1, @2, @3, @4)";
            List<object> parameters = new List<object>
            {
                xs.MaXuat,
                xs.MaNhanVien,
                xs.NgayXuat,
                xs.LyDo ?? (object)DBNull.Value,
                xs.MaKho
            };

            DBUtil.Update(sql, parameters);
        }

        public string UpdateXuatSach(XuatSach xs)
        {
            string sql = @"UPDATE XuatSach 
                           SET MaNhanVien = @0,
                               NgayXuat = @1,
                               LyDo = @2,
                               MaKho = @3
                           WHERE MaXuat = @4";
            List<object> parameters = new List<object>
            {
                xs.MaNhanVien,
                xs.NgayXuat,
                xs.LyDo ?? (object)DBNull.Value,
                xs.MaKho,
                xs.MaXuat
            };

            try
            {
                int rows = DBUtil.Update(sql, parameters);
                if (rows == 0)
                    return "Không tìm thấy phiếu xuất để cập nhật.";
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteXuatSach(string maXuat)
        {
            try
            {
                // Xóa chi tiết trước
                string sqlChiTiet = "DELETE FROM ChiTietXuatSach WHERE MaXuat = @0";
                DBUtil.Update(sqlChiTiet, new List<object> { maXuat });

                // Xóa phiếu xuất
                string sql = "DELETE FROM XuatSach WHERE MaXuat = @0";
                DBUtil.Update(sql, new List<object> { maXuat });

                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public XuatSach GetByMa(string maXuat)
        {
            string sql = "SELECT * FROM XuatSach WHERE MaXuat = @0";
            List<XuatSach> list = SelectBySql(sql, new List<object> { maXuat });
            return list.Count > 0 ? list[0] : null;
        }

        public List<XuatSach> SearchXuatSach(string keyword)
        {
            string sql = @"SELECT * FROM XuatSach 
                           WHERE MaXuat LIKE @0 
                              OR MaNhanVien LIKE @0 
                              OR MaKho LIKE @0 
                              OR LyDo LIKE @0";
            List<object> parameters = new List<object> { "%" + keyword + "%" };
            return SelectBySql(sql, parameters);
        }

        private XuatSach MapReaderToXuatSach(SqlDataReader reader)
        {
            return new XuatSach
            {
                MaXuat = reader["MaXuat"].ToString(),
                MaNhanVien = reader["MaNhanVien"].ToString(),
                NgayXuat = reader["NgayXuat"] != DBNull.Value ? Convert.ToDateTime(reader["NgayXuat"]) : DateTime.MinValue,
                LyDo = reader["LyDo"]?.ToString(),
                MaKho = reader["MaKho"]?.ToString()
            };
        }
    }
}
