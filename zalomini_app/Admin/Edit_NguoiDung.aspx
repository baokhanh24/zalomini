<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site1.Master" AutoEventWireup="true" CodeBehind="Edit_NguoiDung.aspx.cs" Inherits="zalomini_app.Admin.Edit_NguoiDung" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        h2 {
            text-align: center;
            margin-top: 10px;
        }

        .edit_DsVoucher {
            background-color: #4CAF50;
            color: white;
            padding: 8px 7px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-right: 10px;
            font-weight: bold;
            font-size: 15px;
        }

        .edit_Voucher {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

            .edit_Voucher label {
                display: block;
                margin-bottom: 10px;
                font-weight: bold;
            }

            .edit_Voucher textarea {
                outline: none;
                border: 1px solid #ccc;
                font-size: 15px;
                border-radius: 10px;
                resize: none;
                padding: 10px;
                width: 560px;
            }

            .edit_Voucher input[type="radio"] {
                display: inline-block;
                margin-right: 10px;
            }

            .edit_Voucher input[type="text"] {
                outline: none;
                border: 1px solid #ccc;
                font-size: 15px;
                border-radius: 10px;
                width: 535px;
                padding: 10px;
            }

            .edit_Voucher button {
                background-color: #007bff;
                color: #fff;
                padding: 10px 20px;
                border: none;
                border-radius: 5px;
                cursor: pointer;
                transition: all 0.3s ease;
                font-weight: bold;
                font-size: 15px;
                margin-top: 10px;
                margin-left: 30px;
            }

        .edit_answerall button:hover {
            background-color: #0069d9;
        }

        /*     .ngaytao {
            margin-bottom: 10px;
            font-weight: bold;
        }

        p#date {
            font-weight: 300;
            margin: 5px;
            color: #5a51d6;
        }*/



        select {
            width: 142px;
            padding: 5px 9px;
            margin: 8px 0;
            border: 1px solid #ccc;
            border-radius: 10px;
            box-sizing: border-box;
            display: block;
        }


        .btn_edit {
            background-color: #4CAF50;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-right: 10px;
            font-weight: bold;
            font-size: 15px;
            margin: 5px auto;
            display: flex;
        }

        select {
            width: 30%;
            padding: 5px 9px;
            margin: 8px 0;
            border: 1px solid #ccc;
            border-radius: 10px;
            box-sizing: border-box;
            display: block;
        }

        /*i.bx.bx-book-bookmark {
            margin-right: 5px;
        }*/
        .edit_DsVoucher:hover {
            opacity: 0.8;
        }

        .toggle-checkbox {
            position: relative;
            display: inline-block;
            width: 57px;
            height: 29px;
        }

            .toggle-checkbox input {
                display: none;
            }

            .toggle-checkbox label {
                display: block;
                width: 100%;
                height: 100%;
                border-radius: 34px;
                background-color: #ccc;
                cursor: pointer;
                position: relative;
                transition: background-color 0.3s;
            }

            .toggle-checkbox input:checked + label {
                background-color: #2196F3;
            }

            .toggle-checkbox label::before {
                content: "";
                position: absolute;
                width: 20px;
                height: 20px;
                border-radius: 50%;
                background-color: white;
                top: 4px;
                left: 4px;
                transition: transform 0.3s;
            }

            .toggle-checkbox input:checked + label::before {
                transform: translateX(26px);
            }

        .switch label {
            color: transparent;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="edit_Voucher">
        <asp:Label ID="lblError" runat="server">
        </asp:Label>
        <div class="header">
            <a href="DS_NguoiDung.aspx" class="edit_DsVoucher"><i class='bx bxs-chevron-left'></i> Quay lại</a>
            <h2>Sửa Thông Tin Người Dùng</h2>
        </div>

        <label for="id"style="display:none;">
            ID Người Dùng
               <asp:TextBox type="text" ID="IdNguoiDung" name="IdNguoiDung" runat="server" ReadOnly="true" />
        </label>
        <label for="HoTen">
            Họ Tên
                 <asp:TextBox type="text" ID="HoTen" name="HoTen" runat="server" />
        </label>
        <label for="TenDangNhap">
            Tên Đăng Nhập
                 <asp:TextBox type="text" ID="TenDangNhap" name="TenDangNhap" runat="server" />
        </label>
        <label for="MatKhau">
            Mật Khẩu
                 <asp:TextBox type="text" ID="MatKhau" name="MatKhau" runat="server" />
        </label>
        <label class="NgayTao">
            Ngày Tạo
               <asp:TextBox type="text" ID="NgayTao" name="NgayTao" runat="server" />
        </label>
        <label for="iddotthi">
            Vai Trò
			    <asp:DropDownList ID="Role" name="Roke" runat="server" DataTextField="tendotthi" DataValueField="iddotthi">
                    <asp:ListItem Text="Chọn đợt thi" Value="0"></asp:ListItem>
                </asp:DropDownList>
        </label>
        <label class="switch">
            Trạng Thái  
            <asp:CheckBox ID="TrangThai" runat="server" CssClass="toggle-checkbox" Text="." />
            <label for="chkToggle"></label>

        </label>
        <asp:Button ID="Edit" CssClass="btn_edit" runat="server" Text="Lưu" OnClick="btnedit_Click" />
    </div>
</asp:Content>
