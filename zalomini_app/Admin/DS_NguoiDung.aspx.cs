using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace zalomini_app.Admin
{
    public partial class DS_NguoiDung : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Gọi phương thức để lấy dữ liệu đợt thi
                SqlDataReader reader = GetRole();
                exam.Items.Clear();
                while (reader.Read())
                {
                    ListItem item = new ListItem();
                    item.Text = reader["Role"].ToString();
                    item.Value = reader["Id"].ToString();
                    exam.Items.Add(item);
                }
                reader.Close();
            }
            loadTable();
        }
        void loadTable()
        {
            SqlConnection connection = ldc.GetConnection();
            string query = "SELECT ch.Id, ch.HoTen, ch.TenDangNhap, ch.MatKhau, ch.NgayTao, ch.TrangThai, dt.Role AS Role " +
                            "FROM CDS2023_NguoiDung ch " +
                            "JOIN CDS2023_Role dt ON ch.IdRole = dt.Id " +
                            "ORDER BY ch.NgayTao DESC";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            GridView.DataSource = dataTable;
            GridView.DataBind();
        }
        protected SqlDataReader GetRole()
        {
            SqlConnection connection = ldc.GetConnection();
            string query = "SELECT * FROM CDS2023_Role";

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
                    query = "SELECT CDS2023_NguoiDung.*, CDS2023_Role.Role FROM CDS2023_NguoiDung INNER JOIN CDS2023_Role ON CDS2023_NguoiDung.IdRole = CDS2023_Role.Id ORDER BY NgayTao DESC";

                    break;
                case "Oldest":
                    query = "SELECT CDS2023_NguoiDung.*, CDS2023_Role.Role FROM CDS2023_NguoiDung INNER JOIN CDS2023_Role ON CDS2023_NguoiDung.IdRole = CDS2023_Role.Id ORDER BY NgayTao ASC";
                    break;
                case "Alphabetical":
                    query = "SELECT CDS2023_NguoiDung.*, CDS2023_Role.Role FROM CDS2023_NguoiDung INNER JOIN CDS2023_Role ON CDS2023_NguoiDung.IdRole = CDS2023_Role.Id ORDER BY HoTen ASC";
                    break;

                default:
                    query = "SELECT CDS2023_NguoiDung.*, CDS2023_Role.Role FROM CDS2023_NguoiDung INNER JOIN CDS2023_Role ON CDS2023_NguoiDung.IdRole = CDS2023_Role.Id ";
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
            

            // Kiểm tra xem StartDate và EndDate có giá trị hay không
            if (!string.IsNullOrEmpty(StartDate.Text) )
            {
                startDate = DateTime.Parse(StartDate.Text);
                
            }

            // Xây dựng câu truy vấn SQL
            string sql = "SELECT ch.*, dt.Role FROM CDS2023_NguoiDung ch JOIN CDS2023_Role dt ON ch.IdRole = dt.Id WHERE ch.HoTen LIKE '%' + @searchTerm + '%'";

            // Thêm điều kiện cho StartDate và EndDate nếu có giá trị
            if (startDate != null )
            {
                sql += " AND ch.NgayTao = @createdDate";
            }

            // Thêm điều kiện cho examId nếu có giá trị
            if (!string.IsNullOrEmpty(examId))
            {
                sql += " AND (ch.IdRole = @examId OR @examId = -1)";
            }

            // Thực hiện truy vấn SQL
            SqlConnection connection = ldc.GetConnection();
            
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@searchTerm", searchTerm);
                if (startDate != null)
                {
                    command.Parameters.AddWithValue("@createdDate", startDate.Value);
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
            Server.Transfer("Add_NguoiDung.aspx");
        }
        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataBind();
        }
        protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = this.GridView.DataKeys[e.RowIndex].Value.ToString();
            string sql = "delete from CDS2023_NguoiDung where Id = " + id;
            int k = ldc.themxoasua(sql);
            loadTable();
        }

    }
    
}