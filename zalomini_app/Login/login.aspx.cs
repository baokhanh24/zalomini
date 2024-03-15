using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace zalomini_app.Login
{
    public partial class login : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["logout"] == "true")
            {
                Response.Write("Bạn đã đăng xuất thành công!");
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            SqlConnection connection = ldc.GetConnection();
            string query = "SELECT nd.Id, nd.HoTen, nd.TenDangNhap, nd.MatKhau, nd.NgayTao, nd.TrangThai, r.Role FROM CDS2023_NguoiDung nd JOIN CDS2023_Role r ON nd.IdRole = r.Id WHERE nd.TenDangNhap = @username AND nd.MatKhau = @password";
            string dangnhap = Request["username"];
            string MatKhau = Request["password"];

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", dangnhap);
                command.Parameters.AddWithValue("@password", MatKhau);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {


                            dangnhap = reader["TenDangNhap"].ToString();
                            MatKhau = reader["MatKhau"].ToString();
                            Session["TenDangNhap"] = reader["TenDangNhap"].ToString();
                            Session["HoTen"] = reader["HoTen"].ToString();
                            string role = reader["Role"].ToString();
                            Session["Role"] = role;

                            switch (role)
                            {
                                case "Admin":
                                    Response.Redirect("~/Admin/home.aspx");
                                    break;
                                case "Quản Trị Viên":
                                    Response.Redirect("~/Administrators/home.aspx");
                                    break;

                            }
                        }
                        else
                        {
                            lblError.Style.Add("color", "#ef4f4f");
                            lblError.Style.Add("font-weight", "bold");
                            lblError.Text = "<i class='bx bx-error'></i> Tên đăng nhập hoặc mật khẩu không đúng!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblError.Style.Add("color", "#ef4f4f");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-error'></i> Có lỗi xảy ra: " + ex.Message;

                }
            }


        }
       

    }
}
