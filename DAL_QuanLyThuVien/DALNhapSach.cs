using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;

public class DALNhapSach
{
    private string connectionString = @"Data Source=HAHAHA\SQLEXPRESS;Initial Catalog=Xuong_QuanLyThuVien;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

    public List<NhapSach> GetAll()
    {
        List<NhapSach> list = new List<NhapSach>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM NhapSach";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new NhapSach
                {
                    MaNhap = reader["MaNhap"].ToString(),
                    MaNhanVien = reader["MaNhanVien"].ToString(),
                    NgayNhap = Convert.ToDateTime(reader["NgayNhap"]),
                    GhiChu = reader["GhiChu"].ToString(),
                    MaKho = reader["MaKho"].ToString()
                });
            }
        }
        return list;
    }

    public bool Insert(NhapSach ns)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO NhapSach VALUES (@MaNhap, @MaNhanVien, @NgayNhap, @GhiChu, @MaKho)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNhap", ns.MaNhap);
            cmd.Parameters.AddWithValue("@MaNhanVien", ns.MaNhanVien);
            cmd.Parameters.AddWithValue("@NgayNhap", ns.NgayNhap);
            cmd.Parameters.AddWithValue("@GhiChu", (object)ns.GhiChu ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaKho", ns.MaKho);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public bool Delete(string maNhap)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM NhapSach WHERE MaNhap = @MaNhap";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNhap", maNhap);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public bool Update(NhapSach ns)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = @"UPDATE NhapSach SET MaNhanVien=@MaNhanVien, NgayNhap=@NgayNhap, 
                            GhiChu=@GhiChu, MaKho=@MaKho WHERE MaNhap=@MaNhap";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNhap", ns.MaNhap);
            cmd.Parameters.AddWithValue("@MaNhanVien", ns.MaNhanVien);
            cmd.Parameters.AddWithValue("@NgayNhap", ns.NgayNhap);
            cmd.Parameters.AddWithValue("@GhiChu", (object)ns.GhiChu ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaKho", ns.MaKho);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
    public NhapSach GetByMa(string maNhap)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM NhapSach WHERE MaNhap = @MaNhap";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNhap", maNhap);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new NhapSach
                {
                    MaNhap = reader["MaNhap"].ToString(),
                    MaNhanVien = reader["MaNhanVien"].ToString(),
                    NgayNhap = Convert.ToDateTime(reader["NgayNhap"]),
                    GhiChu = reader["GhiChu"].ToString(),
                    MaKho = reader["MaKho"].ToString()
                };
            }
        }
        return null;
    }

}
