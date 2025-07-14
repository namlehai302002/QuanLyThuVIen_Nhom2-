using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyThuVien
{
    public class DALXuatSach
    {
        private string connectionString = @"Data Source=HAHAHA\SQLEXPRESS;Initial Catalog=Xuong_QuanLyThuVien;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public List<XuatSach> GetAll()
        {
            List<XuatSach> list = new List<XuatSach>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM XuatSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new XuatSach
                    {
                        MaXuat = reader["MaXuat"].ToString(),
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        NgayXuat = reader["NgayXuat"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["NgayXuat"]),
                        LyDo = reader["LyDo"].ToString(),
                        MaKho = reader["MaKho"].ToString()
                    });
                }
            }
            return list;
        }

        public bool Insert(XuatSach xs)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO XuatSach VALUES (@MaXuat, @MaNhanVien, @NgayXuat, @LyDo, @MaKho)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaXuat", xs.MaXuat);
                cmd.Parameters.AddWithValue("@MaNhanVien", xs.MaNhanVien);
                cmd.Parameters.AddWithValue("@NgayXuat", (object)xs.NgayXuat ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LyDo", (object)xs.LyDo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaKho", xs.MaKho);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(XuatSach xs)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE XuatSach SET MaNhanVien = @MaNhanVien, NgayXuat = @NgayXuat, 
                                 LyDo = @LyDo, MaKho = @MaKho WHERE MaXuat = @MaXuat";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaXuat", xs.MaXuat);
                cmd.Parameters.AddWithValue("@MaNhanVien", xs.MaNhanVien);
                cmd.Parameters.AddWithValue("@NgayXuat", (object)xs.NgayXuat ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LyDo", (object)xs.LyDo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaKho", xs.MaKho);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string maXuat)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM XuatSach WHERE MaXuat = @MaXuat";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaXuat", maXuat);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public XuatSach GetByMa(string maXuat)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM XuatSach WHERE MaXuat = @MaXuat";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaXuat", maXuat);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new XuatSach
                    {
                        MaXuat = reader["MaXuat"].ToString(),
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        NgayXuat = reader["NgayXuat"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["NgayXuat"]),
                        LyDo = reader["LyDo"].ToString(),
                        MaKho = reader["MaKho"].ToString()
                    };
                }
            }
            return null;
        }
    }
}
