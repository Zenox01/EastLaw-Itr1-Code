<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EastlawUI_v2.adminpanel.Default" %>

<!DOCTYPE html>

<html class="bg-black">
    <head>
        <meta charset="UTF-8">
         <title>East Law - Admin Panel</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        <!-- bootstrap 3.0.2 -->
        <link href="media/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- font Awesome -->
        <link href="media/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <!-- Theme style -->
        <link href="media/css/AdminLTE.css" rel="stylesheet" type="text/css" />

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
          <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
    </head>
    <body class="bg-black">
   <%-- <center>
    <img src="img/fabbylogo.png" width="400" height="150">
    </center>--%>
        <div class="form-box" id="login-box">
            <div class="header">Sign In to East Law - Admin Panel</div>
            <form id="Form1" runat="server">
                <div class="body bg-gray">
                    <div class="form-group">
                      
                        <asp:TextBox runat="server" class="form-control" ID="txtusrName" placeholder="enter your user name"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtusrName" ForeColor="Red"
                    ErrorMessage="Required"></asp:RequiredFieldValidator>
                       
                    </div>
                    <div class="form-group">
                       <asp:TextBox runat="server" class="form-control" ID="txtpwd" TextMode="Password" placeholder="enter password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpwd" ForeColor="Red"
                    ErrorMessage="Required"></asp:RequiredFieldValidator>
                        
                    </div>          
                  <div class="form-group">
                       <div class="alert alert-error" id="divError" runat="server" style="display:none">
							<button type="button" class="close" data-dismiss="alert">×</button>
                            
							<strong>User Name or Password Incorrect ... </strong> 
						</div>
                    </div>
                </div>
                <div class="footer">                                                               
                   
                  
                    <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn bg-olive btn-block" OnClick="btnLogin_Click"   />
                    	
					<asp:Label ID="lblCon" runat="server" ForeColor="Red" Text="invalid Username or Password." Visible="false"></asp:Label>
                   <%-- <p><a href="#">I forgot my password</a></p>
                    
                    <a href="register.html" class="text-center">Register a new membership</a>--%>
                </div>
            </form>

            <div class="margin text-center">
                
                <br/>
               <%-- <button class="btn bg-light-blue btn-circle"><i class="fa fa-facebook"></i></button>
                <button class="btn bg-aqua btn-circle"><i class="fa fa-twitter"></i></button>
                <button class="btn bg-red btn-circle"><i class="fa fa-google-plus"></i></button>--%>

            </div>
        </div>


        <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="media/js/bootstrap.min.js" type="text/javascript"></script>        

    </body>
</html>

