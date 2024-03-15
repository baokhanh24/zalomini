<%@ Page Title="" Language="C#" MasterPageFile="~/Administrators/Site1.Master" AutoEventWireup="true" CodeBehind="detail_answer.aspx.cs" Inherits="zalomini_app.Administrators.detail_answer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <style>
        h2 {
            text-align: center;
            margin-top: 10px;
        }

        .edit_Dsanswer {
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

        .edit_answerall {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

            .edit_answerall label {
                display: block;
                margin-bottom: 10px;
                font-weight: bold;
            }

            .edit_answerall textarea {
                outline: none;
                border: 1px solid #ccc;
                font-size: 15px;
                border-radius: 10px;
                resize: none;
                padding: 10px;
                width: 560px;
            }

            .edit_answerall input[type="radio"] {
                display: inline-block;
                margin-right: 10px;
            }

            .edit_answerall input[type="text"] {
                outline: none;
                border: 1px solid #ccc;
                font-size: 15px;
                border-radius: 10px;
                width: 535px;
                padding: 10px;
            }

            .edit_answerall button {
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
            ackground-color: #0069d9;
        }

        .btn_edit {
            background-color: #3d5af1;
            padding: 17px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 15px;
            color: #fff;
            font-weight: bold;
            margin: 5px auto;
            display: flex;
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

        
        .edit_Dsanswer:hover {
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
            .switch label{
                color:transparent;
            }
             label.DapAn {
            display: flex;
        }
       .Answer{
           display:flex;
           padding:0 10px;
       }
       .Answer div{
           padding:0 10px;
           display:flex;
           justify-content:center;
           align-items:center;
       }
       .Answer label{
           margin:0;
           padding:0 10px;
       }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="edit_answerall" method="POST" runat="server">
        <asp:Label ID="lblError" runat="server">
        </asp:Label>
        <div class="header">
            <a href="DSanswer.aspx" class="edit_Dsanswer"><i class='bx bx-book-bookmark'></i> Quay Lại</a>
            <h2>Câu Hỏi</h2>
        </div>

        <label for="id" style="display:none;">
            ID Câu Hỏi
           <asp:TextBox type="text" ID="IdCauHoi" name="Id" runat="server" ReadOnly="true" />
        </label>
        <label for="question">Câu hỏi:</label>
        <asp:TextBox type="text" ID="CauHoi" name="CauHoi" runat="server" />
        <label>Đáp án:</label>

        <label for="answer1">
            A
            <asp:TextBox type="text" ID="CauTraLoi1" name="CauTraLoi1" runat="server" />
        </label>
        <label for="answer2">
           B
            <asp:TextBox type="text" ID="CauTraLoi2" name="CauTraLoi2" runat="server" />
        </label>
        <label for="answer3">
           C
            <asp:TextBox type="text" ID="CauTraLoi3" name="CauTraLoi3" runat="server" />
        </label>
        <label for="answer4">
           D
            <asp:TextBox type="text" ID="CauTraLoi4" name="CauTraLoi4" runat="server" />
        </label>
        <label for="iddotthi">
            Đợt Thi
			<asp:DropDownList ID="tendt" name="tendt" runat="server" DataTextField="tendotthi" DataValueField="iddotthi">
                <asp:ListItem Text="Chọn đợt thi" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </label>
        <label for="dapan" class="DapAn">
            Đáp án
            <div class="Answer">
                <div>
                   <label for="optionA">A</label>
                   <asp:RadioButton ID="optionA" runat="server"  GroupName="DapAn" Value="1"  AutoPostBack="True" />
                    
                </div>
                <div>
                    <label for="optionB">B</label>
                   <asp:RadioButton ID="optionB" runat="server" GroupName="DapAn" Value="2"  AutoPostBack="True" />
                    
                </div>
                <div>
                    <label for="optionC">C</label>
                   <asp:RadioButton ID="optionC" runat="server" GroupName="DapAn" Value="3"  AutoPostBack="True"/>
                    
                </div>
                <div>
                    <label for="optionD">D</label>
                   <asp:RadioButton ID="optionD" runat="server" GroupName="DapAn" Value="4" AutoPostBack="True"/>
                    
                </div>
                <!-- Hidden field để lưu giá trị của đáp án được chọn -->
                <asp:HiddenField ID="selectedAnswer" runat="server" />
            </div>
        </label>
        <label class="ngaytao">
            Ngày Tạo
           <asp:TextBox type="text" ID="NgayTao" name="NgayTao" runat="server" />
        </label>
        <label class="switch">
            Trạng Thái  
            <asp:CheckBox ID="TrangThai" runat="server" CssClass="toggle-checkbox" Text="." />
            <label for="chkToggle"></label>

        </label>
        
    </form>
</asp:Content>
