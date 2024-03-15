using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace zalomini_app.Admin
{
    public partial class Add_NguoiDung : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Gọi phương thức để lấy dữ liệu đợt thi
                SqlDataReader reader = GetRole();
                Role.Items.Clear();
                while (reader.Read())
                {
                    ListItem item = new ListItem();
                    item.Text = reader["Role"].ToString();
                    item.Value = reader["Id"].ToString();
                    Role.Items.Add(item);
                }
                reader.Close();
            }
        }
        protected SqlDataReader GetRole()
        {
            SqlConnection connection = ldc.GetConnection();
            string query = "SELECT Id, Role FROM CDS2023_Role";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }
        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            if (Request["dangnhap"].Length > 0 && (Request["dangnhap"].Length < 8 || Request["dangnhap"].Length > 12))
            {
                lblError.Style.Add("color", "#ef4f4f");
                lblError.Style.Add("font-weight", "bold");
                lblError.Text = "<i class='bx bx-error'></i> Tên đăng nhập phải có độ dài từ 8 đến 12 ký tự!";
                return;
            }
            else if (Request["dangnhap"].Trim() != "" && !Regex.IsMatch(Request["dangnhap"], @"\d"))
            {
                lblError.Style.Add("color", "#ef4f4f");
                lblError.Style.Add("font-weight", "bold");
                lblError.Text = "<i class='bx bx-error'></i> Tên đăng nhập phải chứa ít nhất một chữ số!";
                return;
            }
            if (!string.IsNullOrEmpty(Request["matkhau"]) && !Regex.IsMatch(Request["matkhau"], @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,12}$"))
            {
                lblError.Style.Add("color", "#ef4f4f");
                lblError.Style.Add("font-weight", "bold");
                lblError.Text = "<i class='bx bx-error'></i> Mật khẩu phải chứa ít nhất một ký tự in hoa, một ký tự thường, một số và một ký tự đặc biệt và có độ dài từ 8 đến 12 ký tự!";
                return;
            }
            SqlConnection connection = ldc.GetConnection();

            try
            {
                connection.Open();
                string dangnhap = Request["dangnhap"];
                SqlCommand checkUsernameCmd = new SqlCommand("SELECT COUNT(*) FROM CDS2023_NguoiDung WHERE TenDangNhap = @TenDangNhap", connection);
                checkUsernameCmd.Parameters.AddWithValue("@TenDangNhap", dangnhap);
                int userCount = (int)checkUsernameCmd.ExecuteScalar();

                if (userCount > 0)
                {
                    lblError.Style.Add("color", "#ef4f4f");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-error'></i> Tên đăng nhập đã tồn tại!";
                    return;
                }

                string HoTen = Request["HoTen"];

                string MatKhau = Request["matkhau"];
                string NgayTao = Request["NgayTao"];
                DateTime ngaythangnam = DateTime.Parse(NgayTao);
                string TenRole = Role.SelectedValue;
                bool TrangThai = (Request["TrangThai"] == "on");


                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;

                    // Bật identity insert trước khi thêm dữ liệu


                    // Lấy IdDotThi từ CDS2023_DotThi

                    // Thêm câu hỏi mới vào CDS2023_CauHoi
                    command.CommandText = "INSERT INTO CDS2023_NguoiDung ( HoTen, TenDangNhap, MatKhau,NgayTao, TrangThai, IdRole) VALUES ( @HoTen, @TenDangNhap, @MatKhau, @NgayTao, @TrangThai, @IdRole)";

                    command.Parameters.AddWithValue("@HoTen", HoTen);
                    command.Parameters.AddWithValue("@TenDangNhap", dangnhap);
                    command.Parameters.AddWithValue("@MatKhau", MatKhau);
                    command.Parameters.AddWithValue("@NgayTao", ngaythangnam);
                    command.Parameters.AddWithValue("@TrangThai", TrangThai);
                    command.Parameters.AddWithValue("@IdRole", TenRole);
                    command.ExecuteNonQuery();

                    // Tắt identity insert sau khi thêm dữ liệu


                    // Hiển thị thông báo thành công và chuyển về trang quản lý câu hỏi
                    lblError.Style.Add("color", "#bbbb26");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-message-square-check'></i> Thêm Người Dùng Thành Công!";



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
                    lblError.Text = "<i class='bx bx-error'></i> Id Người Dùng Đã Tồn Tại!";
                }
                else
                {
                    lblError.Style.Add("color", "#ef4f4f");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-error'></i> Có Lỗi Xảy Ra Khi Thêm Người Dùng: " + ex.Message + "";
                }
            }
        }



    }

}