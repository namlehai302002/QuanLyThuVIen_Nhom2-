using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyThuVien
{
    public class DALChiTietXuatSach
    {
        private string connectionString = @"Data Source=HAHAHA\SQLEXPRESS;Initial Catalog=Xuong_QuanLyThuVien;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public List<ChiTietXuatSach> GetAll()
        {
            List<ChiTietXuatSach> list = new List<ChiTietXuatSach>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ChiTietXuatSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new ChiTietXuatSach
                    {
                        MaChiTietXuat = reader["MaChiTietXuat"].ToString(),
                        MaXuat = reader["MaXuat"].ToString(),
                        MaSach = reader["MaSach"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"])
                    });
                }
            }
            return list;
        }

        public bool Insert(ChiTietXuatSach ct)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ChiTietXuatSach VALUES (@MaChiTietXuat, @MaXuat, @MaSach, @SoLuong)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTietXuat", ct.MaChiTietXuat);
                cmd.Parameters.AddWithValue("@MaXuat", ct.MaXuat);
                cmd.Parameters.AddWithValue("@MaSach", ct.MaSach);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(ChiTietXuatSach ct)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE ChiTietXuatSach 
                                 SET MaXuat = @MaXuat, MaSach = @MaSach, SoLuong = @SoLuong 
                                 WHERE MaChiTietXuat = @MaChiTietXuat";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTietXuat", ct.MaChiTietXuat);
                cmd.Parameters.AddWithValue("@MaXuat", ct.MaXuat);
                cmd.Parameters.AddWithValue("@MaSach", ct.MaSach);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string maChiTietXuat)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ChiTietXuatSach WHERE MaChiTietXuat = @MaChiTietXuat";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTietXuat", maChiTietXuat);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public ChiTietXuatSach GetByMa(string maChiTietXuat)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ChiTietXuatSach WHERE MaChiTietXuat = @MaChiTietXuat";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTietXuat", maChiTietXuat);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new ChiTietXuatSach
                    {
                        MaChiTietXuat = reader["MaChiTietXuat"].ToString(),
                        MaXuat = reader["MaXuat"].ToString(),
                        MaSach = reader["MaSach"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"])
                    };
                }
            }
            return null;
        }
    }
}
