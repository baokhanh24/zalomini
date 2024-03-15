using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace zalomini_app.Admin
{
    public partial class edit_answer : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        DataTable table;
        string Id;
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

            this.Id = Request.QueryString.Get("Id");
            if (!IsPostBack)
            {
                LoadFrom(Id);
                if (!string.IsNullOrEmpty(selectedAnswer.Value))
                {
                    optionA.Checked = selectedAnswer.Value == "1";
                    optionB.Checked = selectedAnswer.Value == "2";
                    optionC.Checked = selectedAnswer.Value == "3";
                    optionD.Checked = selectedAnswer.Value == "4";
                }
            }
            this.Id = Request.QueryString.Get("Id");
            if (!IsPostBack)
            {
                LoadFrom(Id);
            }
        }
        protected void optionA_CheckedChanged(object sender, EventArgs e)
        {
            if (optionA.Checked)
            {
                selectedAnswer.Value = "1";
            }
        }

        protected void optionB_CheckedChanged(object sender, EventArgs e)
        {
            if (optionB.Checked)
            {
                selectedAnswer.Value = "2";
            }
        }

        protected void optionC_CheckedChanged(object sender, EventArgs e)
        {
            if (optionC.Checked)
            {
                selectedAnswer.Value = "3";
            }
        }

        protected void optionD_CheckedChanged(object sender, EventArgs e)
        {
            if (optionD.Checked)
            {
                selectedAnswer.Value = "4";
            }
        }

        private void LoadFrom(string id)
        {
            DataTable table = ldc.getDataID(id);

            if (table.Rows.Count > 0)
            {
                IdCauHoi.Text = id;
                CauHoi.Text = table.Rows[0]["CauHoi"].ToString();
                CauTraLoi1.Text = table.Rows[0]["CauTraLoi1"].ToString();
                CauTraLoi2.Text = table.Rows[0]["CauTraLoi2"].ToString();
                CauTraLoi3.Text = table.Rows[0]["CauTraLoi3"].ToString();
                CauTraLoi4.Text = table.Rows[0]["CauTraLoi4"].ToString();
                tendt.SelectedValue = table.Rows[0]["IdDotThi"].ToString();
                selectedAnswer.Value = table.Rows[0]["DapAn"].ToString();
                NgayTao.Text = table.Rows[0]["NgayTao"].ToString();
                TrangThai.Checked = Convert.ToBoolean(table.Rows[0]["TrangThai"]);

            }
            else
            {
                lblError.Style.Add("color", "#ef4f4f");
                lblError.Style.Add("margin-bottom", "10px");
                lblError.Style.Add("display", "block");
                lblError.Style.Add("font-weight", "bold");
                lblError.Text = "<i class='bx bx-error'></i> Không Tìm Thấy Dữ Liệu!";

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

                string IdCauHoi = Request.QueryString.Get("Id");
                string cauHoi = CauHoi.Text;
                string cauTraLoi1 = CauTraLoi1.Text;
                string cauTraLoi2 = CauTraLoi2.Text;
                string cauTraLoi3 = CauTraLoi3.Text;
                string cauTraLoi4 = CauTraLoi4.Text;
                string idDotThi = tendt.SelectedValue;
                string dapAn = selectedAnswer.Value;

                string ngayTao = NgayTao.Text;
                DateTime ngaythangnam = DateTime.Parse(ngayTao);

                string trangThaiString = TrangThai.Checked.ToString();
                bool trangThaiBool = bool.Parse(trangThaiString);
                byte trangThaiBit = trangThaiBool ? (byte)1 : (byte)0;

                SqlConnection connection = ldc.GetConnection();

                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    // Thực hiện update câu hỏi
                    string query = "UPDATE CDS2023_CauHoi SET CauHoi=@CauHoi, CauTraLoi1=@CauTraLoi1, CauTraLoi2=@CauTraLoi2, CauTraLoi3=@CauTraLoi3, CauTraLoi4=@CauTraLoi4, DapAn=@DapAn, IdDotThi=@IdDotThi, NgayTao=@ngaythangnam, TrangThai=@TrangThai WHERE Id=@IdCauHoi";
                    command.CommandText = query;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@CauHoi", cauHoi);
                    command.Parameters.AddWithValue("@CauTraLoi1", cauTraLoi1);
                    command.Parameters.AddWithValue("@CauTraLoi2", cauTraLoi2);
                    command.Parameters.AddWithValue("@CauTraLoi3", cauTraLoi3);
                    command.Parameters.AddWithValue("@CauTraLoi4", cauTraLoi4);
                    command.Parameters.AddWithValue("@DapAn", dapAn);
                    command.Parameters.AddWithValue("@IdDotThi", idDotThi);
                    command.Parameters.AddWithValue("@ngaythangnam", ngaythangnam);
                    command.Parameters.AddWithValue("@TrangThai", trangThaiBit);
                    command.Parameters.AddWithValue("@IdCauHoi", IdCauHoi);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Hiển thị thông báo thành công và chuyển về trang quản lý câu hỏi
                        Response.Redirect("~/Admin/DSanswer.aspx");
                    }
                    else
                    {

                        lblError.Style.Add("color", "#ef4f4f");
                        lblError.Style.Add("margin-bottom", "10px");
                        lblError.Style.Add("display", "block");
                        lblError.Style.Add("font-weight", "bold");
                        lblError.Text = "<i class='bx bx-error'></i> Không tìm thấy câu hỏi có Id = " + IdCauHoi + "";
                    }
                }

            }
            catch (SqlException ex)
            {

                lblError.Style.Add("color", "#ef4f4f");
                lblError.Style.Add("margin-bottom", "10px");
                lblError.Style.Add("display", "block");
                lblError.Style.Add("font-weight", "bold");
                lblError.Text = "<i class='bx bx-error'></i> Có lỗi xảy ra khi sửa câu hỏi: " + ex.Message + "";
            }


        }
    }
}