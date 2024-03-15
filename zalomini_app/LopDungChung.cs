using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace zalomini_app
{
    public class LopDungChung
    {
        SqlConnection conn;
        string path = @"Data Source=LAPTOP-6QOMN5VG\MSSQLSERVER01;Initial Catalog=ZaloMiniApp_Quiz;Integrated Security=True";

       
        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(path);
            return conn;
        }
        private void connect()
        {

            conn = new SqlConnection(@"Data Source=LAPTOP-6QOMN5VG\MSSQLSERVER01;Initial Catalog=ZaloMiniApp_Quiz;Integrated Security=True");
            conn.Open();
        }
        private void dongketnoi()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        public DataTable getData(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                connect();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            finally
            {
                dongketnoi();
            }
            return dt;
        }
        public DataTable getDataID(string ID)
        {
            DataTable dt = new DataTable();
            try
            {
                connect();
                string sql = "select * from CDS2023_CauHoi where Id = '" + ID + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            finally
            {
                dongketnoi();
            }
            return dt;
        }
        public int KiemTra(string q)
        {
            SqlCommand cm = new SqlCommand(q, conn);
            conn.Open();
            int kq = (int)cm.ExecuteScalar();
            conn.Close();
            return kq;
        }
        public int getPass(string sql)
        {
            int kq = 0;
            try
            {
                connect();
                SqlCommand cm = new SqlCommand(sql, conn);
                kq = (int)cm.ExecuteScalar();
            }
            catch
            {
                kq = 0;
            }
            finally
            {
                dongketnoi();
            }
            return kq;
        }
        public int themxoasua(string sql)
        {
            int kq = 0;
            try
            {
                connect();
                SqlCommand cm = new SqlCommand(sql, conn);
                kq = cm.ExecuteNonQuery();
            }
            catch
            {
                kq = 0;
            }
            finally
            {
                dongketnoi();
            }
            return kq;
        }
    }
}