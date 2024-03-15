<%@ Page Title="" Language="C#" MasterPageFile="~/Administrators/Site1.Master" AutoEventWireup="true" CodeBehind="DS_DotThi.aspx.cs" Inherits="zalomini_app.Administrators.DS_DotThi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <style>
        body {
            font-size: 15px;
        }

        .container {
            width: 100%;
            margin: 0 auto;
            padding: 20px;
        }

        h1 {
            font-size: 36px;
            text-align: center;
            margin-bottom: 30px;
            color: #1e59ca;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }


        .d-flex td {
            padding: 7px 12px;
            text-align: left;
        }

            .d-flex td:hover {
                opacity: 0.8;
            }


        .add-btn,
        .edit-btn,
        .delete-btn {
            background-color: #4CAF50;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-right: 10px;
            font-weight: bold;
            font-size: 15px;
        }

        .search-box {
            width: 300px;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 5px;
            margin-right: 10px;
        }

        .pagination {
            display: inline-block;
        }

            .pagination a {
                color: black;
                float: left;
                padding: 8px 16px;
                text-decoration: none;
                border: 1px solid #ddd;
                margin: 0 4px;
            }

                .pagination a.active {
                    background-color: #4CAF50;
                    color: white;
                    border: 1px solid #4CAF50;
                }

                .pagination a:hover:not(.active) {
                    background-color: #ddd;
                }

        .sort-box {
            display: inline-block;
            margin-right: 10px;
        }

            .sort-box select, #category {
                padding: 5px;
                border: 1px solid #ddd;
                border-radius: 5px;
                background-color: #f1f1f1;
                font-size: 16px;
                cursor: pointer;
            }

        .text-center {
            width: 100%;
            text-align: center;
        }

        .d-flex table {
        }

        .numbercenter tr {
            display: flex;
            justify-content: center;
        }
        .add-btn:hover{
            opacity:0.8;
        }
        .Edit{
           background-color: #507CD1;
           color:#fff;
           padding: 0px 10px;
        }
        .sort_search{
            background-color: #fff;
            border: 1px solid #ccc;
            border-radius: 0px;
            padding: 10px;
            margin: 10px 0;
            display:flex;
            justify-content:space-between;
            align-items:center;
        }
        /*.sort_search label{
            padding:0 5px;
        }*/
        .sort_search #search
        {
            border: 1px solid#ccc;
            border-radius: 5px;
            padding: 8px;
            outline:none
        }
       input#ContentPlaceHolder1_Search,
       select#ContentPlaceHolder1_exam,
       input#ContentPlaceHolder1_StartDate,
       input#ContentPlaceHolder1_EndDate{
            padding: 5px;
		    margin: 10px 0;
            outline:none;
		    border: 1px solid #ccc;
		    border-radius: 5px;
        }
        .actions{
            display:flex;
            justify-content:space-between;
            align-items:center;
        }
        input#ContentPlaceHolder1_btn_search,
        input#ContentPlaceHolder1_ReloadButton{
            padding: 10px 15px;
			font-size: 16px;
			background-color: #4CAF50;
			color: #fff;
			border: none;
			border-radius: 5px;
			cursor: pointer;
        }
        input#ContentPlaceHolder1_btn_search:hover,
        input#ContentPlaceHolder1_ReloadButton:hover{
            opacity:0.8;
        }
        th,td {
            border: 1px solid #ccc;
        }
        

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="container" method="POST" runat="server">
        <h1>Danh Sách Đợt Thi</h1>
        <div class="actions">
            <asp:Button ID="btnadd" CssClass="add-btn" runat="server" Text="Thêm câu hỏi" OnClick="btnaddClick" />
            <div class="sort-box">
                <label for="sort">Sắp xếp theo:</label>
                <asp:DropDownList ID="Sort" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SortDropDownList_SelectedIndexChanged">
                <asp:ListItem Text="Mới Nhất" Value="Latest" Selected="True" />
                <asp:ListItem Text="Cũ Nhất" Value="Oldest" />
                <asp:ListItem Text="A-Z" Value="Alphabetical" />
            </asp:DropDownList>
            </div>
        </div>
        <div class="sort_search">
            <label for="search">Tìm kiếm:
               <asp:TextBox ID="Search" runat="server" placeholder="Tìm kiếm đợt thi"></asp:TextBox>
            </label>

            <label for="start-date">Thời gian bắt đầu:
                <asp:TextBox ID="StartDate" runat="server" TextMode="Date"></asp:TextBox>
            </label>

            <label for="end-date">Thời gian kết thúc:
                <asp:TextBox ID="EndDate" runat="server" TextMode="Date"></asp:TextBox>
            </label>

            <label for="exam">Đợt thi:
                <asp:DropDownList ID="exam" name="exam" runat="server" >
                    <asp:ListItem Value="-1"  Text="Chọn đợt thi"/>
		        </asp:DropDownList>
            </label>
	  
            <asp:Button ID="btn_search" CssClass="btn_search" runat="server" Text="Tìm kiếm" OnClick="btnSeach_Click" />
            <asp:Button ID="ReloadButton" runat="server" Text="Tải lại dữ liệu" OnClick="ReloadButton_Click" />

        </div>
        <div class="d-flex">
          <asp:GridView CssClass="text-center" ID="GridView" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" marrgin-top="10px" AllowPaging="True" OnPageIndexChanging="GridView_PageIndexChanging" DataKeyNames="Id" OnRowDeleting="GridView_RowDeleting">
            <Columns>

                <asp:BoundField DataField="Id" HeaderText="ID" />
                <asp:BoundField DataField="TenDotThi" HeaderText="Tên đợt thi" />
                <asp:BoundField DataField="NgayBatDau" HeaderText="Ngày bắt đầu" />
                <asp:BoundField DataField="NgayKetThuc" HeaderText="Ngày kết thúc" />
                
                <asp:TemplateField HeaderText="Trạng thái">
                <ItemTemplate>
                     <%# (Eval("TrangThai") != DBNull.Value && Convert.ToBoolean(Eval("TrangThai")) ? "Sử dụng" : "Không sử dụng") %>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True"  >
                <ControlStyle BackColor="#ce6363" ForeColor="White"  />
                </asp:CommandField>               
                <asp:TemplateField>
                    <ItemTemplate>
                        <a class="Edit" href="edit_DotThi.aspx?id=<%# Eval("Id") %>">Edit</a>
                    </ItemTemplate>
                </asp:TemplateField>
               
            </Columns>
            <%--Xét thuộc tính classic cho GridView--%>
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" CssClass="numbercenter" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        </div>
	</form>
</asp:Content>
