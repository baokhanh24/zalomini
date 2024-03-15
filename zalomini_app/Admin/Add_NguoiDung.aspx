<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site1.Master" AutoEventWireup="true" CodeBehind="Add_NguoiDung.aspx.cs" Inherits="zalomini_app.Admin.Add_NguoiDung" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        h2 {
            text-align: center;
            margin-top: 10px;
        }

        .Dsanswer {
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

        .voucher {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

            .voucher label {
                display: block;
                margin-bottom: 10px;
                font-weight: bold;
            }

            .voucher textarea {
                outline: none;
                border: 1px solid #ccc;
                font-size: 15px;
                border-radius: 10px;
                resize: none;
                padding: 10px;
                width: 560px;
            }



            .voucher input[type="text"],#matkhau {
                outline: none;
                border: 1px solid #ccc;
                font-size: 15px;
                border-radius: 10px;
                width: 535px;
                padding: 10px;
                outline: none;
                border: 1px solid#f7f0f0;
            }

        }

        .voucher #btnAdd {
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

        .voucher button:hover {
            background-color: #0069d9;
        }

        .ngaytao {
            margin-bottom: 10px;
            font-weight: bold;
        }



        select {
            width: 142px;
            padding: 5px 9px;
            margin: 8px 0;
            border: 1px solid #ccc;
            border-radius: 10px;
            box-sizing: border-box;
            display: block;
        }


        .btn_voucher {
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
            outline: none;
            display: block;
        }

        i.bx.bx-book-bookmark {
            margin-right: 5px;
        }

        .Dsanswer:hover {
            opacity: 0.8;
        }

        input#NgayTao {
            display: block;
            margin: 8px 0;
            padding: 5px;
            border-radius: 5px;
            outline: none;
            border: 1px solid#ccc;
        }

        .btnAdd {
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

        .switch {
            position: relative;
            display: inline-block;
            width: 135px;
            height: 28px;
            margin-top: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0px;
            left: 96px;
            right: -15px;
            bottom: 0px;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
            border-radius: 34px;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 20px;
                width: 20px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
                border-radius: 50%;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
            .header{
                margin-top:10px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="voucher" >
        <asp:Label ID="lblError" runat="server">
        </asp:Label>
        <div class="header">
            <a href="DS_NguoiDung.aspx" class="Dsanswer"><i class='bx bxs-chevron-left'></i> Quay lại</a>
            <h2>Tạo Người Dùng</h2>
        </div>

        
        <label for="HoTen">
            Họ Tên:
        <input type="text" id="HoTen" name="HoTen" required>
        </label>
        <label for="DangNhap">
            Tên Đăng Nhập:
        <input type="text" id="dangnhap" name="dangnhap" required>
        </label>
        <label for="matkhau">
            Mật khẩu:
        <input type="password" id="matkhau" name="matkhau" required>
        </label>
        <label class="ngaytao">
            Ngày Tạo
           <input type="text" id="NgayTao" name="NgayTao" class="form-control">
        </label>
        <label for="iddotthi">
            Vai Trò
			<asp:DropDownList ID="Role" name="Role" runat="server" DataTextField="tenRole" DataValueField="idRole">
                <asp:ListItem Text="Chọn đợt thi" />
            </asp:DropDownList>
        </label>


         <label class="switch">
            Trạng Thái  
            
            <input ID="TrangThai" name="TrangThai" type="checkbox">
            <span class="slider"></span>
        </label>
        <asp:Button ID="btnAdd" CssClass="btnAdd" runat="server" Text="Tạo" OnClick="btnAddRole_Click" />
    </div>
    <script>
        window.onload = function () {
            var currentDate = new Date();
            var day = ("0" + currentDate.getDate()).slice(-2);
            var month = ("0" + (currentDate.getMonth() + 1)).slice(-2);
            var year = currentDate.getFullYear();
            var hour = ("0" + currentDate.getHours()).slice(-2);
            var minute = ("0" + currentDate.getMinutes()).slice(-2);
            var second = ("0" + currentDate.getSeconds()).slice(-2);
            var currentDateTime = day + "-" + month + "-" + year + "-" + hour + ":" + minute + ":" + second;
            document.getElementById("NgayTao").value = currentDateTime;
        };
    </script>
</asp:Content>
