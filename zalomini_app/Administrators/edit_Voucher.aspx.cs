using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace zalomini_app.Administrators
{
    public partial class edit_Voucher : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        string Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Gọi phương thức để lấy dữ liệu đợt thi
                SqlDataReader reader = GetDotThi();
                tendtVoucher.Items.Clear();
                while (reader.Read())
                {
                    ListItem item = new ListItem();
                    item.Text = reader["TenDotThi"].ToString();
                    item.Value = reader["Id"].ToString();
                    tendtVoucher.Items.Add(item);
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
            string query = "SELECT * FROM CDS2023_MaThuong WHERE Id = @Id";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IdMathuong.Text = id;
                    MaT.Text = reader["MaThuong"].ToString();
                    tendtVoucher.Text = reader["IdDotThi"].ToString();
                    DotCapMa.Text = reader["DotCapMa"].ToString();
                    NgayCM.Text = reader["NgayCapMa"].ToString();
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

        protected SqlDataReader GetDotThi()
        {
            SqlConnection connection = ldc.GetConnection();
            string query = "SELECT Id, TenDotThi FROM CDS2023_DotThi ORDER BY NgayBatDau DESC";

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
                string mathuong = MaT.Text;
                string dotthi = tendtVoucher.SelectedValue;
                string dotcapma = DotCapMa.Text;
                string ngaycapma = NgayCM.Text;
                DateTime ngaythangnam = DateTime.Parse(ngaycapma);
                string trangThaiString = TrangThai.Checked.ToString();
                bool trangThaiBool = bool.Parse(trangThaiString);
                byte trangThaiBit = trangThaiBool ? (byte)1 : (byte)0;

                SqlConnection connection = ldc.GetConnection();

                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    // Thực hiện update câu hỏi
                    string query = "UPDATE CDS2023_MaThuong SET MaThuong=@MaThuong, IdDotThi=@IdDotThi, DotCapMa=@DotCapMa, NgayCapMa=@ngaycapma, TrangThai=@TrangThai WHERE Id=@IdVoucher";
                    command.CommandText = query;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@MaThuong", mathuong);
                    command.Parameters.AddWithValue("@IdDotThi", dotthi);
                    command.Parameters.AddWithValue("@DotCapMa", dotcapma);
                    command.Parameters.AddWithValue("@ngaycapma", ngaythangnam);
                    command.Parameters.AddWithValue("@TrangThai", trangThaiBit);
                    command.Parameters.AddWithValue("@IdVoucher", id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Hiển thị thông báo thành công và chuyển về trang quản lý câu hỏi
                        Response.Redirect("~/Admin/DS_Voucher.aspx");
                    }
                    else
                    {

                        lblError.Style.Add("color", "#ef4f4f");
                        lblError.Style.Add("margin-bottom", "10px");
                        lblError.Style.Add("display", "block");
                        lblError.Style.Add("font-weight", "bold");
                        lblError.Text = "<i class='bx bx-error'></i> Không tìm thấy Mã Thưởng có Id = " + id + "";
                    }
                }

            }
            catch (SqlException ex)
            {

                lblError.Style.Add("color", "#ef4f4f");
                lblError.Style.Add("margin-bottom", "10px");
                lblError.Style.Add("display", "block");
                lblError.Style.Add("font-weight", "bold");
                lblError.Text = "<i class='bx bx-error'></i> Có lỗi xảy ra khi sửa Mã Thưởng: " + ex.Message + "";
            }

        }
    }
}