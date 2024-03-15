using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace zalomini_app.Administrators
{
    public partial class add_dotthi : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnadd_dotthi_Click(object sender, EventArgs e)
        {
            SqlConnection connection = ldc.GetConnection();

            try
            {

                connection.Open();


                string tenDotThi = Request.Form["TenDotthi"];
                DateTime ngayBatDau = DateTime.Parse(Request.Form["NgayBatDau"]);
                DateTime ngayKetThuc = DateTime.Parse(Request.Form["NgayKetThuc"]);
                bool TrangThai = (Request["TrangThai"] == "on");

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;

                    // Bật identity insert trước khi thêm dữ liệu


                    // Lấy IdDotThi từ CDS2023_DotThi

                    // Thêm câu hỏi mới vào CDS2023_CauHoi
                    command.CommandText = "INSERT INTO CDS2023_DotThi ( TenDotThi, NgayBatDau, NgayKetThuc, TrangThai) VALUES ( @TenDotThi, @NgayBatDau, @NgayKetThuc, @TrangThai)";

                    command.Parameters.AddWithValue("@TenDotThi", tenDotThi);
                    command.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                    command.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                    command.Parameters.AddWithValue("@TrangThai", TrangThai);


                    command.ExecuteNonQuery();



                    // Hiển thị thông báo thành công 
                    lblError.Style.Add("color", "#bbbb26");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-message-square-check'></i> Thêm Đợt Thi Thành Công!";



                }
                connection.Close();


            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Mã lỗi SQL Server cho trường hợp nhập trùng khóa chính
                {
                    lblError.Style.Add("color", "#ef4f4f");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-error'></i> Mã Đợt Thi Đã Tồn Tại!"; ;
                }
                else
                {
                    lblError.Style.Add("color", "#ef4f4f");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-error'></i> Có Lỗi Xảy Ra Khi Thêm Đợt Thi: " + ex.Message + "";
                }
            }
        }
    }
}