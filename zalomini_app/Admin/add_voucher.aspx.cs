using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace zalomini_app.Admin
{
    public partial class add_voucher : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Gọi phương thức để lấy dữ liệu đợt thi
                SqlDataReader reader = GetDotThi();
                tendt.Items.Clear();
                while (reader.Read())
                {
                    ListItem item = new ListItem();
                    item.Text = reader["TenDotThi"].ToString();
                    item.Value = reader["Id"].ToString();
                    tendt.Items.Add(item);
                }
                reader.Close();
            }

        }
        protected SqlDataReader GetDotThi()
        {
            SqlConnection connection = ldc.GetConnection();
            string query = "SELECT Id, TenDotThi FROM CDS2023_DotThi ORDER BY NgayBatDau DESC";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }
        protected void btnaddvoucher_Click(object sender, EventArgs e)
        {
            SqlConnection connection = ldc.GetConnection();

            try
            {
                connection.Open();
                string[] mathuong_arr = Request["MaThuongList"].Split(',');
                string tenDotThi = tendt.SelectedValue;
                string DotCapMa = Request["DotCapMa"];
                string NgayCapMa = Request["NgayCapMa"];
                DateTime ngaythangnam = DateTime.Parse(NgayCapMa);
                bool TrangThai = (Request["TrangThai"] == "on");

                using (SqlCommand command = new SqlCommand())
                {
                    int i = 0;
                    command.Connection = connection;
                    foreach (string mathuong in mathuong_arr)
                    {
                        // Thêm câu hỏi mới vào CDS2023_CauHoi
                        command.CommandText = "INSERT INTO CDS2023_MaThuong (MaThuong, IdDotThi, DotCapMa, NgayCapMa, TrangThai) VALUES (@MaThuong" + i + ", @TenDotThi, @DotCapMa, @NgayCapMa, @TrangThai)";

                        command.Parameters.AddWithValue("@MaThuong" + i, mathuong);
                        command.Parameters.AddWithValue("@TenDotThi", tenDotThi);
                        command.Parameters.AddWithValue("@DotCapMa", DotCapMa);
                        command.Parameters.AddWithValue("@NgayCapMa", ngaythangnam);
                        command.Parameters.AddWithValue("@TrangThai", TrangThai);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear(); // Xóa các tham số để sử dụng lại cho lần thêm dữ liệu tiếp theo
                        i++;
                    }

                    // Hiển thị thông báo thành công và chuyển về trang quản lý câu hỏi
                    lblError.Style.Add("color", "#bbbb26");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-message-square-check'></i> Thêm Mã Thưởng Thành Công!";
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
                    lblError.Text = "<i class='bx bx-error'></i> Id Mã Thưởng Đã Tồn Tại!";
                }
                else
                {
                    lblError.Style.Add("color", "#ef4f4f");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-error'></i> Có Lỗi Xảy Ra Khi Thêm Mã Thưởng: " + ex.Message + "";
                }
            }
        }
    }
}   