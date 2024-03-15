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
    public partial class detail_answer : System.Web.UI.Page
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
    }
}