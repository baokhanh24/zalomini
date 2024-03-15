using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace zalomini_app.Administrators
{
    public partial class DS_DotThi : System.Web.UI.Page
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
            string query = "SELECT * FROM CDS2023_DotThi ORDER BY NgayBatDau DESC";

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
                    query = "SELECT * FROM CDS2023_DotThi ORDER BY NgayBatDau DESC";
                    break;
                case "Oldest":
                    query = "SELECT * FROM CDS2023_DotThi ORDER BY NgayBatDau";
                    break;
                case "Alphabetical":
                    query = "SELECT * FROM CDS2023_DotThi ORDER BY TenDotThi ASC";
                    break;
                default:
                    query = "SELECT * FROM CDS2023_DotThi";
                    break;
            }

           
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                GridView.DataSource = table;
                GridView.DataBind();
            
        }
        protected void btnaddClick(object sender, EventArgs e)
        {
            Server.Transfer("add_dotthi.aspx");
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
            string sql = "SELECT * FROM CDS2023_DotThi WHERE TenDotThi LIKE '%' + @searchTerm + '%'";

            // Thêm điều kiện cho StartDate và EndDate nếu có giá trị
            if (startDate != null && endDate != null)
            {
                sql += " AND NgayBatDau BETWEEN @startDate AND @endDate";
            }

            // Thêm điều kiện cho examId nếu có giá trị
            if (!string.IsNullOrEmpty(examId))
            {
                sql += " AND (Id = @examId OR @examId = -1)";
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
            LoadData();
        }
        private void LoadData()
        {
            // Khởi tạo kết nối đến database và truy vấn dữ liệu
            SqlConnection connection = ldc.GetConnection();
          
            SqlCommand cmd = new SqlCommand("SELECT * FROM CDS2023_DotThi", connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Gán dữ liệu cho GridView
            GridView.DataSource = dt;
            GridView.DataBind();
        }
        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataBind();
        }
        protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = this.GridView.DataKeys[e.RowIndex].Value.ToString();
            string sql = "delete from CDS2023_DotThi where Id = " + id;
            int k = ldc.themxoasua(sql);
            loadTable();
        }
    }
}