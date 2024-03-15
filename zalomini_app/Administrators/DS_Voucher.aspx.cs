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
    public partial class DS_Voucher : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        protected void Page_Load(object sender, EventArgs e)
        {


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
            loadTable();
        }
        void loadTable()
        {
            SqlConnection connection = ldc.GetConnection();
            string query = "SELECT ch.Id, ch.MaThuong, ch.DotCapMa, ch.NgayCapMa, ch.TrangThai, dt.TenDotThi AS TenDotThi " +
                          "FROM CDS2023_MaThuong ch " +
                          "JOIN CDS2023_DotThi dt ON ch.IdDotThi = dt.Id " +
                          "ORDER BY ch.NgayCapMa DESC";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            GridView.DataSource = dataTable;
            GridView.DataBind();
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
                    query = "SELECT CDS2023_MaThuong.*, CDS2023_DotThi.TenDotThi FROM CDS2023_MaThuong INNER JOIN CDS2023_DotThi ON CDS2023_MaThuong.IdDotThi = CDS2023_DotThi.Id ORDER BY NgayCapMa DESC";

                    break;
                case "Oldest":
                    query = "SELECT CDS2023_MaThuong.*, CDS2023_DotThi.TenDotThi FROM CDS2023_MaThuong INNER JOIN CDS2023_DotThi ON CDS2023_MaThuong.IdDotThi = CDS2023_DotThi.Id ORDER BY NgayCapMa ASC";
                    break;
                case "Alphabetical":
                    query = "SELECT CDS2023_MaThuong.*, CDS2023_DotThi.TenDotThi FROM CDS2023_MaThuong INNER JOIN CDS2023_DotThi ON CDS2023_MaThuong.IdDotThi = CDS2023_DotThi.Id ORDER BY MaThuong ASC";
                    break;
                default:
                    query = "SELECT CDS2023_MaThuong.*, CDS2023_DotThi.TenDotThi FROM CDS2023_MaThuong INNER JOIN CDS2023_DotThi ON CDS2023_MaThuong.IdDotThi = CDS2023_DotThi.Id ";
                    break;

            }

            
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                GridView.DataSource = table;
                GridView.DataBind();

            
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
            string sql = "SELECT ch.*, dt.TenDotThi FROM CDS2023_MaThuong ch JOIN CDS2023_DotThi dt ON ch.IdDotThi = dt.Id WHERE ch.MaThuong LIKE '%' + @searchTerm + '%'";

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
                if (!string.IsNullOrEmpty(examId))
                {
                    sql += " AND (dt.Id = @examId OR @examId = -1)";
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
        //private void LoadData()
        //{
        //    // Khởi tạo kết nối đến database và truy vấn dữ liệu
        //    string connectionString = @"Data Source=LAPTOP-6QOMN5VG\MSSQLSERVER01;Initial Catalog=ZaloMiniApp_Quiz;Integrated Security=True";
        //    SqlConnection conn = new SqlConnection(connectionString);
        //    SqlCommand cmd = new SqlCommand("SELECT * FROM CDS2023_MaThuong, CDS2023_DotThi", conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    // Gán dữ liệu cho GridView
        //    GridView.DataSource = dt;
        //    GridView.DataBind();
        //}
        protected void btnaddClick(object sender, EventArgs e)
        {
            Server.Transfer("add_voucher.aspx");
        }
        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataBind();
        }
        protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = this.GridView.DataKeys[e.RowIndex].Value.ToString();
            string sql = "delete from CDS2023_MaThuong where Id = " + id;
            int k = ldc.themxoasua(sql);
            loadTable();
        }
    }
}