using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace zalomini_app.Admin
{
    public partial class Edit_NguoiDung : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        string Id;
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
            this.Id = Request.QueryString.Get("Id");
            if (!IsPostBack)
            {
                LoadFrom(Id);
            }

        }
        private void LoadFrom(string id)
        {
            SqlConnection connection = ldc.GetConnection();
            string query = "SELECT * FROM CDS2023_NguoiDung WHERE Id = @Id";

            
            
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        IdNguoiDung.Text = id;
                        HoTen.Text = reader["HoTen"].ToString();
                        TenDangNhap.Text = reader["TenDangNhap"].ToString();
                        MatKhau.Text = reader["MatKhau"].ToString();
                        NgayTao.Text = reader["NgayTao"].ToString();
                        Role.Text = reader["IdRole"].ToString();
                        TrangThai.Checked = Convert.ToBoolean(reader["TrangThai"].ToString());
                }
                    else
                    {

                        lblError.Style.Add("color", "#ef4f4f");
                        lblError.Style.Add("margin-bottom", "10px");
                        lblError.Style.Add("display", "block");
                        lblError.Style.Add("font-weight", "bold");
                        lblError.Text = "<i class='bx bx-error'></i> Không Tìm Thấy Dữ Liệu!";
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    lblError.Style.Add("color", "#ef4f4f");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-error'></i> Có lỗi xảy ra: " + ex.Message + "";
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
        protected void btnedit_Click(object sender, EventArgs e)
        {


            try
            {
                string id = Request.QueryString.Get("Id");
                string hoten = HoTen.Text;
                string dangnhap = TenDangNhap.Text;
                string matkhau = MatKhau.Text;
                string ngaytao = NgayTao.Text;
                DateTime ngaythangnam = DateTime.Parse(ngaytao);
                string role = Role.SelectedValue;
                string trangThaiString = TrangThai.Checked.ToString();
                bool trangThaiBool = bool.Parse(trangThaiString);
                byte trangThaiBit = trangThaiBool ? (byte)1 : (byte)0;

                SqlConnection connection = ldc.GetConnection();
                
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        // Thực hiện update câu hỏi
                        string query = "UPDATE CDS2023_NguoiDung SET HoTen=@HoTen, TenDangNhap=@TenDangNhap, MatKhau=@MatKhau, NgayTao=@NgayTao, IdRole=@IdRole, TrangThai=@TrangThai WHERE Id=@IdNguoiDung";
                        command.CommandText = query;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("@HoTen", hoten);
                        command.Parameters.AddWithValue("@TenDangNhap", dangnhap);
                        command.Parameters.AddWithValue("@MatKhau", matkhau);
                        command.Parameters.AddWithValue("@NgayTao", ngaythangnam);
                        command.Parameters.AddWithValue("@IdRole", role);
                        command.Parameters.AddWithValue("@TrangThai", trangThaiBit);
                        command.Parameters.AddWithValue("@IdNguoiDung", id);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Hiển thị thông báo thành công và chuyển về trang quản lý câu hỏi
                            Response.Redirect("~/Admin/DS_NguoiDung.aspx");
                        }
                        else
                        {

                            lblError.Style.Add("color", "#ef4f4f");
                            lblError.Style.Add("margin-bottom", "10px");
                            lblError.Style.Add("display", "block");
                            lblError.Style.Add("font-weight", "bold");
                            lblError.Text = "<i class='bx bx-error'></i> Không tìm thấy Người Dùng có Id = " + id + "";
                        }
                    }
                
            }
            catch (SqlException ex)
            {

                lblError.Style.Add("color", "#ef4f4f");
                lblError.Style.Add("margin-bottom", "10px");
                lblError.Style.Add("display", "block");
                lblError.Style.Add("font-weight", "bold");
                lblError.Text = "<i class='bx bx-error'></i> Có lỗi xảy ra khi sửa Người Dùng: " + ex.Message + "";
            }

        }
    }
}