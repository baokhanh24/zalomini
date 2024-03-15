using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace zalomini_app.Administrators
{
    public partial class home : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTotalQuestions.Text = GetTotalQuestions().ToString();
                lblVoucher.Text = GetTotalVoucher().ToString();
                lblDotthi.Text = GetTotalDotThi().ToString();
            }


        }
        protected int GetTotalQuestions()
        {
            int totalQuestions = 0;
           
            using (SqlConnection conn = ldc.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CDS2023_CauHoi", conn);
                conn.Open();
                totalQuestions = (int)cmd.ExecuteScalar();
            }
            return totalQuestions;
        }
        protected int GetTotalVoucher()
        {
            int totalVoucher = 0;
            
            using (SqlConnection conn = ldc.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CDS2023_MaThuong", conn);
                conn.Open();
                totalVoucher = (int)cmd.ExecuteScalar();
            }
            return totalVoucher;
        }
        protected int GetTotalDotThi()
        {
            int totaldotthi = 0;    
           
            using (SqlConnection conn = ldc.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CDS2023_DotThi", conn);
                conn.Open();
                totaldotthi = (int)cmd.ExecuteScalar();
            }
            return totaldotthi;
        }
    }
}