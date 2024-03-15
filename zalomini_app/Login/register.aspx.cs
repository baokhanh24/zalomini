using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace zalomini_app.Login
{
    public partial class register : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void rigister_Click(object sender, EventArgs e)
        {
            if (Request["username"].Length > 0 && (Request["username"].Length < 8 || Request["username"].Length > 20))
            {
                lbl_error.Style.Add("color", "#ef4f4f");
                lbl_error.Style.Add("font-weight", "bold");
                lbl_error.Text = "<i class='bx bx-error'></i> Tên đăng nhập phải có độ dài từ 8 đến 20 ký tự!";
                return;
            }
            else if (Request["username"].Trim() != "" && !Regex.IsMatch(Request["username"], @"\d"))
            {
                lbl_error.Style.Add("color", "#ef4f4f");
                lbl_error.Style.Add("font-weight", "bold");
                lbl_error.Text = "<i class='bx bx-error'></i> Tên đăng nhập phải chứa ít nhất một chữ số!";
                return;
            }
            else if (!string.IsNullOrEmpty(Request["password"]) && !Regex.IsMatch(Request["password"], @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,12}$"))
            {
                lbl_error.Style.Add("color", "#ef4f4f");
                lbl_error.Style.Add("font-weight", "bold");
                lbl_error.Text = "<i class='bx bx-error'></i> Mật khẩu phải chứa ít nhất một ký tự in hoa, một ký tự thường, một số và một ký tự đặc biệt và có độ dài từ 8 đến 12 ký tự!";
                return;
            }
            else
            {
                SqlConnection connection = ldc.GetConnection();
                try
                {
                    connection.Open();
                    string dangnhap = Request["username"];
                    SqlCommand checkUsernameCmd = new SqlCommand("SELECT COUNT(*) FROM CDS2023_NguoiDung WHERE TenDangNhap = @TenDangNhap", connection);
                    checkUsernameCmd.Parameters.AddWithValue("@TenDangNhap", dangnhap);
                    int userCount = (int)checkUsernameCmd.ExecuteScalar();

                    if (userCount > 0)
                    {
                        lbl_error.Style.Add("color", "#ef4f4f");
                        lbl_error.Style.Add("margin-bottom", "10px");
                        lbl_error.Style.Add("display", "block");
                        lbl_error.Style.Add("font-weight", "bold");
                        lbl_error.Text = "<i class='bx bx-error'></i> Tên đăng nhập đã tồn tại!";
                        return;
                    }

                    // If username is not duplicated, proceed with user registration

                    string HoTen = Request["name"];
                    string MatKhau = Request["password"];
                    string nhaplaiMatKhau = Request["confirm-password"];
                    string NgayTao = Request["date-created"];
                    DateTime ngaythangnam = DateTime.Parse(NgayTao);

                    if (nhaplaiMatKhau != MatKhau)
                    {
                        lbl_error.Style.Add("color", "#ef4f4f");
                        lbl_error.Style.Add("margin-bottom", "10px");
                        lbl_error.Style.Add("display", "block");
                        lbl_error.Style.Add("font-weight", "bold");
                        lbl_error.Text = "<i class='bx bx-error'></i> Mật Khẩu Không Trùng Khớp!";
                        return;
                    }
                    else
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "INSERT INTO CDS2023_NguoiDung ( HoTen, TenDangNhap, MatKhau, NgayTao, TrangThai, IdRole) VALUES ( @HoTen, @TenDangNhap, @MatKhau, @NgayTao, @TrangThai, @IdRole)";
                            command.Parameters.AddWithValue("@HoTen", HoTen);
                            command.Parameters.AddWithValue("@TenDangNhap", dangnhap);
                            command.Parameters.AddWithValue("@MatKhau", MatKhau);
                            command.Parameters.AddWithValue("@NgayTao", ngaythangnam);
                            command.Parameters.AddWithValue("@TrangThai", 1);
                            command.Parameters.AddWithValue("@IdRole", 2);
                            command.ExecuteNonQuery();

                            lbl_error.Style.Add("color", "#bbbb26");
                            lbl_error.Style.Add("margin-bottom", "10px");
                            lbl_error.Style.Add("display", "block");
                            lbl_error.Style.Add("font-weight", "bold");
                            lbl_error.Text = "<i class='bx bx-message-square-check'></i> Đăng ký Thành công !";
                        }
                    }
                    connection.Close();


                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) // Mã lỗi SQL Server cho trường hợp nhập trùng khóa chính
                    {
                        lbl_error.Style.Add("color", "#ef4f4f");
                        lbl_error.Style.Add("margin-bottom", "10px");
                        lbl_error.Style.Add("display", "block");
                        lbl_error.Style.Add("font-weight", "bold");
                        lbl_error.Text = "<i class='bx bx-error'></i> Id Người Dùng Đã Tồn Tại!";
                    }
                    else
                    {
                        lbl_error.Style.Add("color", "#ef4f4f");
                        lbl_error.Style.Add("margin-bottom", "10px");
                        lbl_error.Style.Add("display", "block");
                        lbl_error.Style.Add("font-weight", "bold");
                        lbl_error.Text = "<i class='bx bx-error'></i> Có Lỗi Xảy Ra Khi Thêm Người Dùng: " + ex.Message + "";
                    }
                }
            }
        }
    }
}