using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace zalomini_app.Administrators
{
    public partial class add_answer : System.Web.UI.Page
    {
        LopDungChung ldc = new LopDungChung();



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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection connection = ldc.GetConnection();

            try
            {
                connection.Open();
                string CauHoi = Request["CauHoi"];
                string CauTraLoi1 = Request["CauTraLoi1"];
                string CauTraLoi2 = Request["CauTraLoi2"];
                string CauTraLoi3 = Request["CauTraLoi3"];
                string CauTraLoi4 = Request["CauTraLoi4"];
                string TenDotThi = tendt.SelectedValue;
                string DapAn = Request["selectedAnswer"];
                string NgayTao = Request["NgayTao"];
                DateTime ngaythangnam = DateTime.Parse(NgayTao);
                bool TrangThai = (Request["TrangThai"] == "on");


                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "INSERT INTO CDS2023_CauHoi ( CauHoi, CauTraLoi1, CauTraLoi2, CauTraLoi3, CauTraLoi4, IdDotThi, DapAn, NgayTao, TrangThai) VALUES ( @CauHoi, @CauTraLoi1, @CauTraLoi2, @CauTraLoi3, @CauTraLoi4, @TenDotThi, @DapAn, @ngaythangnam, @TrangThai)";

                    command.Parameters.AddWithValue("@CauHoi", CauHoi);
                    command.Parameters.AddWithValue("@CauTraLoi1", CauTraLoi1);
                    command.Parameters.AddWithValue("@CauTraLoi2", CauTraLoi2);
                    command.Parameters.AddWithValue("@CauTraLoi3", CauTraLoi3);
                    command.Parameters.AddWithValue("@CauTraLoi4", CauTraLoi4);
                    command.Parameters.AddWithValue("@TenDotThi", TenDotThi);
                    command.Parameters.AddWithValue("@DapAn", DapAn);
                    command.Parameters.AddWithValue("@ngaythangnam", ngaythangnam);
                    command.Parameters.AddWithValue("@TrangThai", TrangThai);
                    command.ExecuteNonQuery();


                    lblError.Style.Add("color", "#bbbb26");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-message-square-check'></i> Thêm Câu Hỏi Thành Công!";

                }
                connection.Close();


            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Mã lỗi SQL Server cho trường hợp nhập trùng khóa chính
                {
                    lblError.Style.Add("color", "#ef4f4f");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-error'></i> Mã Câu Hỏi Đã Tồn Tại!"; ;
                }
                else
                {
                    lblError.Style.Add("color", "#ef4f4f");
                    lblError.Style.Add("margin-bottom", "10px");
                    lblError.Style.Add("display", "block");
                    lblError.Style.Add("font-weight", "bold");
                    lblError.Text = "<i class='bx bx-error'></i> Có lỗi xảy ra khi thêm Câu Hỏi: " + ex.Message + "";
                }
            }
        }
    }
}