<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShERPa360net.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShERPa-360</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="icon" href="img/favicon.png" sizes="32x32" type="image/x-icon"/>
    <link rel="icon" href="../img/favicon.png" sizes="32x32" type="image/x-icon"/>
    <!-- END META SECTION -->

   
    <!-- EOF CSS INCLUDE -->
     <!-- Custom Stylesheet -->
    <link type="text/css" rel="stylesheet" href="css/bootstrap.min.log.css"/>
    <link type="text/css" rel="stylesheet" href="css/login.css"/>
    <style>
        .q_logo {
            background: url(img/qarmatek_bottom-1.png) top center no-repeat;
            width: 100%;
            height: 50px;
            float: left;
            margin-bottom: 10px;
        }
    </style>
     
</head>
<body>
    <div class="page_loader"></div>
 
    
<!-- Login 6 start -->
<div class="login-6">
    <div id="particles-js"></div>
    <div class="container">
        <div class="row d-flex">
            <div class="col-lg-4 col-md-4">
                <div class="form-section">
                    <div class="logo">
                        <a href="login-6.html">
                            <img src="Dashboardimg/login-logo%20(1).png" alt="logo">
                        </a>
                    </div>
                    <div class="typing">
                        <h1>LOG IN TO YOUR ACCOUNT</h1>
                    </div>
                    <div class="login-inner-form">
                        <form action="#"  runat="server">
                            <div class="form-group clearfix">
                                <div class="form-box">
                                     <asp:TextBox name="email" runat="server" class="form-control" ID="txtUserName" placeholder="User Name"></asp:TextBox>
                                    <i class="flaticon-mail-2"></i>
                                </div>
                            </div>
                            <div class="form-group clearfix">
                                <div class="form-box">
                                   <asp:TextBox TextMode="Password" runat="server" class="form-control" autocomplete="off" ID="txtPassword" placeholder="Password"></asp:TextBox>
                                    <i class="flaticon-password"></i>
                                </div>
                            </div>
                            <div class="checkbox form-group clearfix">
                                <a href="forgot-password-6.html" class="link-light float-end forgot-password">Forgot your password?</a>
                            </div>
                            <div class="form-group clearfix">
                               <asp:Button runat="server" ID="btnLogin" class="btn btn-primary btn-lg btn-theme" OnClick="btnLogin_Click" Text="Log In" />
                            </div>
                        </form>
                        
                    </div>
                 
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Login 6 end -->
    <script src="js/jquery-1.7.1.min.js"></script>
    
<script src="js/app.log.js"></script>
<!-- Custom JS Script -->
</body>
</html>
