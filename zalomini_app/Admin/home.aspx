<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site1.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="zalomini_app.Admin.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="panel panel-container">
            <div class="row home_container">
                <div class="home_colum4">
                    <a href="DSanswer.aspx" class="home_click">
                        <div class="panel panel-teal panel-widget border-right blue">
                            <div class="row no-padding">
                                <i class='bx bx-question-mark'></i>
                                <div class="large">
                                    <asp:Label ID="lblTotalQuestions" runat="server" Text='<%# GetTotalQuestions() %>'></asp:Label>

                                </div>
                                <div class="text-muted text-blue">Câu Hỏi</div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="home_colum4">
                    <a href="DS_Voucher.aspx" class="home_click">
                        <div class="panel panel-blue panel-widget border-right red">
                            <div class="row no-padding">
                                <i class='bx bx-credit-card-front'></i>
                                <div class="large">
                                    <asp:Label ID="lblVoucher" runat="server" Text='<%# GetTotalVoucher() %>'></asp:Label></div>
                                <div class="text-muted">Voucher</div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="home_colum4">
                    <a href="DS_DotThi.aspx" class="home_click">
                        <div class="panel panel-orange panel-widget border-right green">
                            <div class="row no-padding">
                                <i class='bx bx-history'></i>
                                <div class="large">
                                    <asp:Label ID="lblDotthi" runat="server" Text='<%# GetTotalDotThi() %>'></asp:Label>
                                </div>
                                <div class="text-muted">Đợt Thi</div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="home_colum4">
                    <a href="DS_NguoiDung.aspx" class="home_click">
                        <div class="panel panel-red panel-widget yelow">
                            <div class="row no-padding">
                               <i class='bx bxs-user'></i>
                                <div class="large">
                                    <asp:Label ID="lblNguoiDung" runat="server" Text='<%# GetTotalNguoiDung() %>'></asp:Label>
                                </div>
                                <div class="text-muted">Người Dùng</div>
                            </div>
                        </div>
                    </a>
                </div>

            </div>
            <!--/.row-->
        </div>

    </main>
</asp:Content>
