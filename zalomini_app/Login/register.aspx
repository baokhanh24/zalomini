<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="zalomini_app.Login.register" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=\, initial-scale=1.0">
    <link href='https://unpkg.com/boxicons@2.0.9/css/boxicons.min.css' rel='stylesheet'>
    <title>zalominiapp</title>

    <link rel="stylesheet" href="reponsivelogin.css">
    <style>
        * {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }

        html {
            font-family: Helvetica,Arial, sans-serif;
            height: 100%
        }

        body {
            height: 100%;
        }

        a {
            text-decoration: none;
        }

        .login {
            position: relative;
            background-image: url(../img/background.png);
            background-size: cover;
            background-position: center;
            height: 100%;
            display: flex;
            align-items: center;
            flex-direction: row-reverse;
            flex-wrap: wrap;
            justify-content: space-evenly;
            align-content: stretch;
            background-color: rgba(0, 0, 0, 0.5);
        }

        .login_container {
            position: absolute;
            width: 712px;
            max-height: 100%;
            background-color: transparent;
            backdrop-filter: blur(30px);
            border-radius: 30px;
            box-shadow: 0 0 17px -1px rgb(168 201 234);
            padding: 15px 100px;
        }

        .logo {
            height: 270px;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .logozalo {
            height: 100px;
            margin-top: 50px;
        }

        .login-box {
            display: flex;
        }

        .login_form {
            width: 100%
        }

            .login_form .input_login, .input_pass, .btn_login,.btnAdd {
                display: block;
                margin-bottom: 15px;
                height: 55px;
                width: 100%;
                outline: none;
                border: none;
                border-bottom: 2px solid #fff;
                background-color: transparent;
                backdrop-filter: blur(20px);
                padding: 10px 10px;
                color: #fff;
                border-radius: 15px;
                font-size: 16px;
                text-decoration: none;
            }

        .btn_login,.btnAdd {
            border: none;
        }

        ::placeholder {
            color: #fff;
            font-size: 16px;
        }

        .forgetpass {
            display: flex;
            justify-content: flex-end;
            color: #fff;
            margin-bottom: 14px;
            font-size: 15px;
        }
      

        .btn_login,.btnAdd {
            font-size: 18px;
            font-weight: bold;
            height: 50px;
            background-color: #64c5ff;
            color: #fff;
            cursor: pointer;
        }

        @media (max-width: 740px) {
            .login_form input {
                width: 350px !important;
            }

            input[value] {
                background-color: #64c5ff !important;
                text-transform: none !important;
                font-weight: unset !important;
            }
        }

        span#lblError,.lbl_error {
            display: block;
            margin: 0 70px;
            padding: 10px;
        }

        .login_eror {
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .forgetpass:hover {
            color: #64c5ff;
        }

        .ErrorUsername, .ErrorPass {
            color: #ef4f4f;
            font-weight: bold;
        }

        


        /*register*/
        .register {
           
        }
        .form-group {
            position: relative;
            margin-bottom: 1.5rem;
            width:100%;
        }

        .form-label {
            position: absolute;
            top: 50%;
            left: 0.5rem;
            transform: translateY(-50%);
            pointer-events: none;
            font-size: 0.875rem;
            color: #fff;
            transition: all 0.2s ease-out;
        }

        .form-control {
            outline: none;
            border: none;
            border-bottom: 2px solid #fff;
           width: 100%;
            background-color: transparent;
            backdrop-filter: blur(20px);
            padding: 10px 10px;
            color: #fff;
            border-radius: 15px;
            font-size: 16px;
            text-decoration: none;
            transition: all 0.2s ease-out;
        }

        .form-control:focus {
            outline: none;
               
        }

        .form-label.top {
            top: 0;
            font-size: 0.8rem;
            color: #0068FF;
            transform: translateY(-120%);
        }
        .form-label-datetime {
            left: 19px;
            font-size: 0.8rem;
            color: #0068FF;
            position: absolute;
            
            margin: -10px;
        }

        .btn {
            display: inline-block;
            font-weight: 400;
            color: #fff;
            text-align: center;
            vertical-align: middle;
            background-color: #007bff;
            border: 1px solid #007bff;
            padding: 0.375rem 0.75rem;
        }
        
        }
    </style>
</head>
<body>
    <form id="form1" class="login" method="POST" runat="server">
        
        <div id="register" class="login_container">
            <div class="logo">
                <img src="../img/Icon_of_Zalo.svg.png" alt="" class="logozalo">
            </div>
            <div class="login-box">

                <div class="login_form">
                    <div class="login_eror">
                        <div class="form-group">
                            <input type="text" id="name" name="name" class="form-control" required>
                            <label for="name" class="form-label">Họ và tên</label>
                        </div>
                        <div class="form-group">
                            <input type="text" id="username" name="username" class="form-control" required>
                            <label for="username" class="form-label">Tên đăng nhập</label>
                        </div>
                        <div class="form-group">
                            <input type="password" id="password" name="password" class="form-control" required>
                            <label for="password" class="form-label">Mật khẩu</label>
                        </div>
                        <div class="form-group">
                            <input type="password" id="confirm-password" name="confirm-password" class="form-control" required>
                            <label for="confirm-password" class="form-label">Nhập lại mật khẩu</label>
                        </div>
                        <div class="form-group">
                            <input type="text" id="date-created" name="date-created" class="form-control" required>
                            <label for="date-created" class="form-label-datetime">Ngày tạo</label>
                        </div>
                        <asp:Label ID="lbl_error" runat="server"></asp:Label>
                        <asp:Button ID="btnAdd" CssClass="btnAdd" runat="server" Text="Đăng Ký" OnClick="rigister_Click" />
                         <a href="login.aspx" id="loginLink" class="forgetpass">Đăng Nhập</a>
                </div>
            </div>
        </div>
       </div>
    </form>
</body>
    <script>
        const formControls = document.querySelectorAll('.form-control');
        const formLabels = document.querySelectorAll('.form-label');

        for (let i = 0; i < formControls.length; i++) {
            const formControl = formControls[i];
            const formLabel = formLabels[i];

            formControl.addEventListener('focus', () => {
                formLabel.classList.add('top');
            });

            formControl.addEventListener('blur', () => {
                if (formControl.value === '') {
                    formLabel.classList.remove('top');
                }
            });
        }
    </script>
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
            document.getElementById("date-created").value = currentDateTime;
        };
    </script>
   
</html>

