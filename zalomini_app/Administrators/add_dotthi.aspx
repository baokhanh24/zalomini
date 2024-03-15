<%@ Page Title="" Language="C#" MasterPageFile="~/Administrators/Site1.Master" AutoEventWireup="true" CodeBehind="add_dotthi.aspx.cs" Inherits="zalomini_app.Administrators.add_dotthi" %>
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

        .dotthi{
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

            .dotthi label {
                display: block;
                margin-bottom: 10px;
                font-weight: bold;
            }

            .dotthi textarea {
                outline: none;
                border: 1px solid #ccc;
                font-size: 15px;
                border-radius: 10px;
                resize: none;
                padding: 10px;
                width: 560px;
            }



            .dotthi input[type="text"] {
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

        .dotthi button {
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

       /* .ngaytao {
            margin-bottom: 10px;
            font-weight: bold;
        }
*/


        /*select {
            width: 142px;
            padding: 5px 9px;
            margin: 8px 0;
            border: 1px solid #ccc;
            border-radius: 10px;
            box-sizing: border-box;
            display: block;
        }*/


        .btn_dotthi {
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

       .form-control 
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
      <form class="dotthi" method="POST" runat="server">
        <asp:Label ID="lblError" runat="server">
        </asp:Label>
        <div class="header">
            <a href="DS_DotThi.aspx" class="Dsanswer"><i class='bx bxs-chevron-left'></i> Quay lại</a>
            <h2>Tạo Đợt Thi</h2>
        </div>

        
        <label for="TenDotthi">
            Tên Đợt Thi
        <input type="text" id="TenDotthi" name="TenDotthi" required>
        </label>


        <label class="NgayBatDau">
            Ngày Bắt Đầu
           <input type="datetime-local" id="NgayBatDau" name="NgayBatDau" class="form-control">
        </label>
        <label class="NgayKetThuc">
            Ngày Kết thúc
           <input type="datetime-local" id="NgayKetThuc" name="NgayKetThuc" class="form-control">
        </label>
        <label class="switch">
            Trạng Thái  
            
            <input id="TrangThai" name="TrangThai" type="checkbox">
            <span class="slider"></span>
        </label>
        <asp:Button ID="Submit" CssClass="btn_dotthi" runat="server" Text="Tạo" OnClick="btnadd_dotthi_Click" />
    </form>
</asp:Content>
