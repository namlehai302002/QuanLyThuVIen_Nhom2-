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
    public class DALTacGia
    {
        public List<TacGia> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<TacGia> list = new List<TacGia>();
            using (SqlDataReader reader = DBUtil.Query(sql, args))
            {
                while (reader.Read())
                {
                    list.Add(MapReaderToTacGia(reader));
                }
            }
            return list;
        }

        public List<TacGia> SelectAll()
        {
            string sql = "SELECT * FROM TacGia";
            return SelectBySql(sql, new List<object>());
        }

        public void InsertTacGia(TacGia tg)
        {
            string sql = @"INSERT INTO TacGia (MaTacGia, TenTacGia, QuocTich, TrangThai, NgayTao)
                           VALUES (@0, @1, @2, @3, @4)";
            List<object> parameters = new List<object>
            {
                tg.MaTacGia,
                tg.TenTacGia,
                tg.QuocTich,
                tg.TrangThai,
                tg.NgayTao
            };

            DBUtil.Update(sql, parameters);
        }

        public string UpdateTacGia(TacGia tg)
        {
            string sql = @"UPDATE TacGia 
                   SET TenTacGia = @0,
                       QuocTich = @1,
                       TrangThai = @2,
                       NgayTao = @3
                   WHERE MaTacGia = @4";

            List<object> parameters = new List<object>
    {
        tg.TenTacGia ?? (object)DBNull.Value,
        tg.QuocTich ?? (object)DBNull.Value,
        tg.TrangThai.HasValue ? (object)tg.TrangThai.Value : DBNull.Value,
        tg.NgayTao,
        tg.MaTacGia
    };

            try
            {
                int rowsAffected = DBUtil.Update(sql, parameters);
                if (rowsAffected == 0)
                    return "Không tìm thấy tác giả để cập nhật.";
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
        public string DeleteTacGia(string maTG)
        {
            try
            {
                string query = "DELETE FROM TacGia WHERE MaTacGia = @0";
                List<object> parameters = new List<object> { maTG };
                DBUtil.Update(query, parameters);
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public List<TacGia> SearchTacGia(string keyword)
        {
            string sql = @"SELECT * FROM TacGia 
                           WHERE MaTacGia LIKE @0 
                              OR TenTacGia LIKE @0 
                              OR QuocTich LIKE @0";
            List<object> parameters = new List<object> { "%" + keyword + "%" };
            return SelectBySql(sql, parameters);
        }

        private TacGia MapReaderToTacGia(SqlDataReader reader)
        {
            return new TacGia
            {
                MaTacGia = reader["MaTacGia"].ToString(),
                TenTacGia = reader["TenTacGia"].ToString(),
                QuocTich = reader["QuocTich"]?.ToString(),
                TrangThai = reader["TrangThai"] != DBNull.Value && Convert.ToBoolean(reader["TrangThai"]),
                NgayTao = reader["NgayTao"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTao"]) : DateTime.MinValue
            };
        }
    }
}

