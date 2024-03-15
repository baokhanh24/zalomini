using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace zalomini_app.Administrators
{
    public partial class edit_DotThi : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        string Id;
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Id = Request.QueryString.Get("Id");
            if (!IsPostBack)
            {
                LoadFrom(Id);
            }
        }
        private void LoadFrom(string id)
        {
            SqlConnection connection = ldc.GetConnection();
            string query = "SELECT * FROM CDS2023_DotThi WHERE Id = @Id";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IdDotThi.Text = id;
                    Dotthi.Text = reader["TenDotThi"].ToString();
                    NgayBatDau.Text = reader["NgayBatDau"].ToString();
                    NgayKetThuc.Text = reader["NgayKetThuc"].ToString();

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
        protected void btnedit_Click(object sender, EventArgs e)
        {


            try
            {
                string id = Request.QueryString.Get("Id");
                string dotthi = Dotthi.Text;
                DateTime ngaybatdau = DateTime.Parse(NgayBatDau.Text);
                DateTime ngayketthuc = DateTime.Parse(NgayKetThuc.Text);

                string trangThaiString = TrangThai.Checked.ToString();
                bool trangThaiBool = bool.Parse(trangThaiString);
                byte trangThaiBit = trangThaiBool ? (byte)1 : (byte)0;

                SqlConnection connection = ldc.GetConnection();

                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    // Thực hiện update câu hỏi
                    string query = "UPDATE CDS2023_DotThi SET TenDotThi=@TenDotThi, NgayBatDau=@NgayBatDau, NgayKetThuc=@NgayKetThuc, TrangThai=@TrangThai WHERE Id=@IdDotThi";
                    command.CommandText = query;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@TenDotThi", dotthi);
                    command.Parameters.AddWithValue("@NgayBatDau", ngaybatdau);
                    command.Parameters.AddWithValue("@NgayKetThuc", ngayketthuc);

                    command.Parameters.AddWithValue("@TrangThai", trangThaiBit);
                    command.Parameters.AddWithValue("@IdDotThi", id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Hiển thị thông báo thành công và chuyển về trang quản lý câu hỏi
                        Response.Redirect("~/Admin/DS_DotThi.aspx");
                    }
                    else
                    {

                        lblError.Style.Add("color", "#ef4f4f");
                        lblError.Style.Add("margin-bottom", "10px");
                        lblError.Style.Add("display", "block");
                        lblError.Style.Add("font-weight", "bold");
                        lblError.Text = "<i class='bx bx-error'></i> Không tìm thấy câu hỏi có Id = " + id + "";
                    }
                }

            }
            catch (SqlException ex)
            {

                lblError.Style.Add("color", "#ef4f4f");
                lblError.Style.Add("margin-bottom", "10px");
                lblError.Style.Add("display", "block");
                lblError.Style.Add("font-weight", "bold");
                lblError.Text = "<i class='bx bx-error'></i> Có lỗi xảy ra khi sửa Đơt Thi: " + ex.Message + "";
            }

        }
    }
}