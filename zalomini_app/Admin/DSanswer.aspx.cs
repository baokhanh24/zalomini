using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace zalomini_app.Admin
{
    public partial class DSanswer : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        protected void Page_Load(object sender, EventArgs e)
        {

            loadTable();

            if (!IsPostBack)
            {
                // Gọi phương thức để lấy dữ liệu đợt thi
                SqlDataReader reader = GetDotThi();
                exam.Items.Clear();
                while (reader.Read())
                {
                    ListItem item = new ListItem();
                    item.Text = reader["TenDotThi"].ToString();
                    item.Value = reader["Id"].ToString();
                    exam.Items.Add(item);
                }
                reader.Close();
            }
            BindData();
           
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
        void loadTable()
        {
            SqlConnection connection = ldc.GetConnection();
            string query = "SELECT ch.Id, ch.CauHoi, ch.CauTraLoi1, ch.CauTraLoi2, ch.CauTraLoi3, ch.CauTraLoi4, ch.DapAn, ch.NgayTao, ch.TrangThai, dt.TenDotThi AS TenDotThi " +
                            "FROM CDS2023_CauHoi ch " +
                            "JOIN CDS2023_DotThi dt ON ch.IdDotThi = dt.Id " +
                            "ORDER BY ch.NgayTao DESC";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            GridView.DataSource = dataTable;
            GridView.DataBind();
        }
        protected void SortDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {


            BindData();
        }
        private void BindData()
        {
            SqlConnection connection = ldc.GetConnection();
            string selectedValue = Sort.SelectedValue;
            string query;

            switch (selectedValue)
            {
                case "Latest":
                    query = "SELECT CDS2023_CauHoi.*, CDS2023_DotThi.TenDotThi FROM CDS2023_CauHoi INNER JOIN CDS2023_DotThi ON CDS2023_CauHoi.IdDotThi = CDS2023_DotThi.Id ORDER BY NgayTao DESC";

                    break;
                case "Oldest":
                    query = "SELECT CDS2023_CauHoi.*, CDS2023_DotThi.TenDotThi FROM CDS2023_CauHoi INNER JOIN CDS2023_DotThi ON CDS2023_CauHoi.IdDotThi = CDS2023_DotThi.Id ORDER BY NgayTao ASC";
                    break;
                case "Alphabetical":
                    query = "SELECT CDS2023_CauHoi.*, CDS2023_DotThi.TenDotThi FROM CDS2023_CauHoi INNER JOIN CDS2023_DotThi ON CDS2023_CauHoi.IdDotThi = CDS2023_DotThi.Id ORDER BY CauHoi ASC";
                    break;

                default:
                    query = "SELECT CDS2023_CauHoi.*, CDS2023_DotThi.TenDotThi FROM CDS2023_CauHoi INNER JOIN CDS2023_DotThi ON CDS2023_CauHoi.IdDotThi = CDS2023_DotThi.Id ";
                    break;
            }


            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            GridView.DataSource = table;
            GridView.DataBind();

        }

        protected void btnAddClick(object sender, EventArgs e)
        {
            Server.Transfer("add_answer.aspx");
        }

        protected void btnSeach_Click(object sender, EventArgs e)
        {
            string searchTerm = Search.Text.Trim();
            string examId = exam.SelectedValue;
            DateTime? startDate = null;
            DateTime? endDate = null;

            // Kiểm tra xem StartDate và EndDate có giá trị hay không
            if (!string.IsNullOrEmpty(StartDate.Text) && !string.IsNullOrEmpty(EndDate.Text))
            {
                startDate = DateTime.Parse(StartDate.Text);
                endDate = DateTime.Parse(EndDate.Text);
            }

            // Xây dựng câu truy vấn SQL
            string sql = "SELECT ch.*, dt.TenDotThi FROM CDS2023_CauHoi ch JOIN CDS2023_DotThi dt ON ch.IdDotThi = dt.Id WHERE ch.CauHoi LIKE '%' + @searchTerm + '%'";

            // Thêm điều kiện cho StartDate và EndDate nếu có giá trị
            if (startDate != null && endDate != null)
            {
                sql += " AND dt.NgayBatDau BETWEEN @startDate AND @endDate";
            }

            // Thêm điều kiện cho examId nếu có giá trị
            if (!string.IsNullOrEmpty(examId))
            {
                sql += " AND (ch.IdDotThi = @examId OR @examId = -1)";
            }

            // Thực hiện truy vấn SQL
            SqlConnection connection = ldc.GetConnection();

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@searchTerm", searchTerm);
            if (startDate != null && endDate != null)
            {
                command.Parameters.AddWithValue("@startDate", startDate.Value);
                command.Parameters.AddWithValue("@endDate", endDate.Value);
            }
            if (!string.IsNullOrEmpty(examId))
            {
                command.Parameters.AddWithValue("@examId", examId);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            GridView.DataSource = table;
            GridView.DataBind();


        }
        protected void ReloadButton_Click(object sender, EventArgs e)
        {
            // Làm mới dữ liệu trong GridView
            loadTable();
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Kiểm tra nếu đây là hàng dữ liệu
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Lấy giá trị đáp án từ cột "Answer" trong hàng hiện tại
                int answerValue = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DapAn"));

                // Thay đổi giá trị đáp án thành ký tự tương ứng (A, B, C, D)
                string answerChar = "";
                switch (answerValue)
                {
                    case 1:
                        answerChar = "A";
                        break;
                    case 2:
                        answerChar = "B";
                        break;
                    case 3:
                        answerChar = "C";
                        break;
                    case 4:
                        answerChar = "D";
                        break;
                }

                // Gán giá trị mới cho cột "Answer" trong hàng hiện tại
                e.Row.Cells[7].Text = answerChar; // Giả sử cột "Answer" là cột thứ 2 trong GridView
            }
        }
        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataBind();
        }
        protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = this.GridView.DataKeys[e.RowIndex].Value.ToString();
            string sql = "delete from CDS2023_CauHoi where Id = " + id;
            int k = ldc.themxoasua(sql);
            loadTable();

        }
        
        
    }  
}