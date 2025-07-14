using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;
namespace DAL_QuanLyThuVien
{
    public class DALChiTietNhapSach
    {
        private string connectionString = @"Data Source=HAHAHA\SQLEXPRESS;Initial Catalog=Xuong_QuanLyThuVien;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public List<ChiTietNhapSach> GetAll()
        {
            List<ChiTietNhapSach> list = new List<ChiTietNhapSach>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ChiTietNhapSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new ChiTietNhapSach
                    {
                        MaChiTietNhap = reader["MaChiTietNhap"].ToString(),
                        MaNhap = reader["MaNhap"].ToString(),
                        MaSach = reader["MaSach"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        DonGia = Convert.ToDecimal(reader["DonGia"])
                    });
                }
            }
            return list;
        }

        public bool Insert(ChiTietNhapSach ct)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ChiTietNhapSach VALUES (@MaChiTietNhap, @MaNhap, @MaSach, @SoLuong, @DonGia)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTietNhap", ct.MaChiTietNhap);
                cmd.Parameters.AddWithValue("@MaNhap", ct.MaNhap);
                cmd.Parameters.AddWithValue("@MaSach", ct.MaSach);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@DonGia", ct.DonGia);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string maChiTietNhap)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ChiTietNhapSach WHERE MaChiTietNhap = @MaChiTietNhap";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTietNhap", maChiTietNhap);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(ChiTietNhapSach ct)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE ChiTietNhapSach SET MaNhap=@MaNhap, MaSach=@MaSach, 
                                SoLuong=@SoLuong, DonGia=@DonGia WHERE MaChiTietNhap=@MaChiTietNhap";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTietNhap", ct.MaChiTietNhap);
                cmd.Parameters.AddWithValue("@MaNhap", ct.MaNhap);
                cmd.Parameters.AddWithValue("@MaSach", ct.MaSach);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@DonGia", ct.DonGia);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public ChiTietNhapSach GetByMa(string maChiTietNhap)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ChiTietNhapSach WHERE MaChiTietNhap = @MaChiTietNhap";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTietNhap", maChiTietNhap);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new ChiTietNhapSach
                    {
                        MaChiTietNhap = reader["MaChiTietNhap"].ToString(),
                        MaNhap = reader["MaNhap"].ToString(),
                        MaSach = reader["MaSach"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        DonGia = Convert.ToDecimal(reader["DonGia"])
                    };
                }
            }
            return null;
        }
    }
}
