<%@ Page Title="" Language="C#" MasterPageFile="~/Administrators/Site1.Master" AutoEventWireup="true" CodeBehind="add_voucher.aspx.cs" Inherits="zalomini_app.Administrators.add_voucher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
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



            .voucher input[type="text"] {
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

        .voucher button {
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

       input#NgayCapMa 
       {
            display: block;
            margin: 8px 0;
            padding: 5px;
            border-radius: 5px;
            outline: none;
            border: 1px solid#ccc;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <form class="voucher" method="POST" runat="server">
         <asp:Label ID="lblError" runat="server">
         </asp:Label>
        <div class="header">
            <a href="DS_Voucher.aspx" class="Dsanswer"><i class='bx bxs-chevron-left'></i> Quay lại</a>
            <h2>Tạo Mã Thưởng</h2>
        </div>
        
        
        <label for="mathuong">Danh sách Mã Thưởng (cách nhau bằng dấu phẩy):
            <input type="text" id="MaThuongList" name="MaThuongList" required>
        </label>
        <label for="iddotthi">
            Đợt Thi
			<asp:DropDownList ID="tendt"  name="tendt" runat="server" DataTextField="tendotthi" DataValueField="iddotthi">
                 <asp:ListItem Text="Chọn đợt thi" />
            </asp:DropDownList>
        </label>
        <label for="DotCapMa">
            Đợt Cấp Mã
			<input type="text" id="DotCapMa" name="DotCapMa" required>
        </label>
        <label class="ngaytao">
            Ngày Cấp Mã
           <input type="text" id="NgayCapMa" name="NgayCapMa" class="form-control">
        </label>
        <label class="switch">
            Trạng Thái  
            
            <input ID="TrangThai" name="TrangThai" type="checkbox">
            <span class="slider"></span>
        </label>
        <asp:Button ID="Submit" CssClass="btn_voucher" runat="server" Text="Tạo" OnClick="btnaddvoucher_Click" />
    </form>
    <script>
        window.onload = function () {
            var currentDate = new Date();
            var day = ("0" + currentDate.getDate()).slice(-2);
            var month = ("0" + (currentDate.getMonth() + 1)).slice(-2);
            var year = currentDate.getFullYear();
            var hour = ("0" + currentDate.getHours()).slice(-2);
            var minute = ("0" + currentDate.getMinutes()).slice(-2);
            var second = ("0" + currentDate.getSeconds()).slice(-2);
            var currentDateTime = day + "/" + month + "/" + year + " " + hour + ":" + minute + ":" + second;
            document.getElementById("NgayCapMa").value = currentDateTime;
        };
    </script>
</asp:Content>
