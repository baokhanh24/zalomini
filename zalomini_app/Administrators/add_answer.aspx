<%@ Page Title="" Language="C#" MasterPageFile="~/Administrators/Site1.Master" AutoEventWireup="true" CodeBehind="add_answer.aspx.cs" Inherits="zalomini_app.Administrators.add_answer" %>
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

        .answerall {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

            .answerall label {
                display: block;
                margin-bottom: 20px;
                font-weight: bold;
            }

            .answerall textarea {
                outline: none;
                border: 1px solid #ccc;
                font-size: 15px;
                border-radius: 10px;
                resize: none;
                padding: 10px;
                width: 560px;
            }

            .answerall input[type="radio"] {
                display: inline-block;
                margin-right: 10px;
            }

            .answerall input[type="text"] {
                outline: none;
                border: 1px solid #ccc;
                font-size: 15px;
                border-radius: 10px;
                width: 535px;
                padding: 10px;
                outline: none;
                border: 1px solid#f7f0f0;
            }


            .answerall button {
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

                .answerall button:hover {
                    background-color: #0069d9;
                }

        .ngaytao {
            margin-bottom: 10px;
            font-weight: bold;
        }

        p#date {
            font-weight: 300;
            margin: 5px;
            color: #5a51d6;
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

        .btnSubmit {
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

        .btnSubmit {
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
    <form class="answerall" method="POST" runat="server">
        <asp:Label ID="lblError" runat="server">
        </asp:Label>
        <div class="header">
            <a href="DSanswer.aspx" class="Dsanswer"><i class='bx bxs-chevron-left'></i> Quay lại</a>
            <h2>Tạo câu hỏi trắc nghiệm</h2>
        </div>


        <label for="question">Câu hỏi:
            <input type="text" id="CauHoi" name="CauHoi" required>
        </label>
        <label>Đáp án:

            <label for="answer1">
                A
                <input type="text" id="CauTraLoi1" name="CauTraLoi1" required>
            </label>
            <label for="answer2">
                B
                <input type="text" id="CauTraLoi2" name="CauTraLoi2" required>
            </label>
            <label for="answer3">
                C
                <input type="text" id="CauTraLoi3" name="CauTraLoi3" required>
            </label>
            <label for="answer4">
                D
                <input type="text" id="CauTraLoi4" name="CauTraLoi4" required>
            </label>
            
        </label>
        <label for="iddotthi">
            Đợt Thi
			<asp:DropDownList ID="tendt" name="tendt" runat="server" DataTextField="tendotthi" DataValueField="iddotthi">
                <asp:ListItem Text="Chọn đợt thi" />
            </asp:DropDownList>
        </label>
        <label for="dapan" class="DapAn">
            Đáp án
            <div class="Answer">
                <div>
                    <label for="optionA">A</label>
                    <input type="radio" id="optionA" name="DapAn" value="1">
                    
                </div>
                <div>
                    <label for="optionB">B</label>
                    <input type="radio" id="optionB" name="DapAn" value="2">
                    
                </div>
                <div>
                    <label for="optionC">C</label>
                    <input type="radio" id="optionC" name="DapAn" value="3">
                    
                </div>
                <div>
                    <label for="optionD">D</label>
                    <input type="radio" id="optionD" name="DapAn" value="4">
                    
                </div>
                <!-- Hidden field để lưu giá trị của đáp án được chọn -->
                <input type="hidden" id="selectedAnswer" name="selectedAnswer">
            </div>
        </label>
        <label class="ngaytao">
            Ngày Tạo
                
           <input type="text" id="NgayTao" name="NgayTao" class="form-control">
        </label>
        <label class="switch">
            Trạng Thái  
            
            <input id="TrangThai" name="TrangThai" type="checkbox">
            <span class="slider"></span>
        </label>

        <asp:Button ID="Submit" CssClass="btnSubmit" runat="server" Text="Tạo" OnClick="btnSubmit_Click" />
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
            document.getElementById("NgayTao").value = currentDateTime;
        };
    </script>
    <script>
        var selectedAnswerField = document.getElementById("selectedAnswer");
        var answerRadios = document.getElementsByName("DapAn");

        for (var i = 0; i < answerRadios.length; i++) {
            answerRadios[i].addEventListener("click", function () {
                selectedAnswerField.value = this.value;
            });
        }
    </script>
</asp:Content>
