<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EastlawUI_v2.Default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="google-site-verification" content="owdX5U3Q1j65A1efhDDMKcl0hM2FMjYvztYEuibaFo0" />
<title>EastLaw.pk – Pakistan's Largest Online Law Library</title>
     <meta name="description" content="Eastlaw.pk With a collection of over 100 databases, with more than 1 million documents and over 10.4 million pages, our legal research tool provides top quality information with an interface which makes research a quicker, easier and more effective process.Keep yourself up to date with a wide array of practice areas on eastlaw.pk. EastLaw provides its users the opportunity to research into their preferred area of practice, and the option to save it in their personal dashboard.Information, Media and Telecommunication,Banking & Insurance,Corporate Law,Contract Law,
Labour, Service & Employment,Criminal Law,Energy,Environment,Family Law,Intellectual Property,Land Revenue,Litigation,Public Law,Constitutional & Administrative,Income Tax, Sales Tax , Custom & Finance Law,Electrol Law" />

       <meta name="keywords" content="law, pakistan, statutes, statute, case, legal, notes, pakistani law, order, lawonline, lawsite, pld, law digest, rules, federal law, provincial law, federal,  provincial, taxation, service, tribunal, copyright, labour, essay, writing, document, legal document, pld, scmr, clc, pcrlj, ptd, plc, cld, ylr, lawsite, lawyer, judge, court, prosecution, opponent, appellant, caselaw search, advance legal search, statutes search, courtwise search, citation search, dictionary, legal dictionary, cyber, telecome,property, intellectual property laws,constitunal laws, constitution, constitution of pakistan,family laws,banking,family, banking laws,education, educational laws,election, politics,islam, islamic laws,muhammadan law, administrative, custum tarrif, circular, articles, double txation, notification, punjab, nwfp, balochistan, sindh, word, words & phrases, legal terms, topics, maxims, pakistanlawsite, lawsite, tax, library, law library, comprehensive law,Information, Media and Telecommunication,Banking & Insurance,Corporate Law,Contract Law,
Labour, Service & Employment,Criminal Law,Energy,Environment,Family Law,Intellectual Property,Land Revenue,Litigation,Public Law,Constitutional & Administrative,Income Tax, Sales Tax , Custom & Finance Law,Electrol Law,PLC, CLC, CLD, MLD, KLR, NLR, YLR, PCRLJ, SCMR, TAX, PCTLR, PLD, PTD, PTCL, Lahore High Court, CLR, PSC CIV, LHC, IHC, SC  AJK, Comp. C, Con. C, SCP, PHC, PLJ, PSC CRI, PSC, SHC, SECP (B.O), SECP (O), SCInd, Sindh Revenue Board Decisions." />

<link href="/style/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="/style/css/style.css" rel="stylesheet" type="text/css" />
<link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
type="text/css" rel="stylesheet" />
<link rel="stylesheet" type="text/css" href="/style/engine1/slider.css" />
<link href="/style/css/icons.css" rel="stylesheet" type="text/css" />

<link rel="shortcut icon" href="/style/img/fav.png">
<link href="/style/css/back-top.css" rel="stylesheet" type="text/css" />
<link href="/style/css/home.css" rel="stylesheet" type="text/css" />

<link href="/style/css/jquery-ui1.css" rel="stylesheet" type="text/css" />
    <!--  Tracking Code for https://eastlaw.pk -->
<script>
    (function(h,o,t,j,a,r){
        h.hj=h.hj||function(){(h.hj.q=h.hj.q||[]).push(arguments)};
        h._hjSettings={hjid:886368,hjsv:6};
        a=o.getElementsByTagName('head')[0];
        r=o.createElement('script');r.async=1;
        r.src=t+h._hjSettings.hjid+j+h._hjSettings.hjsv;
        a.appendChild(r);
    })(window,document,'https://static.hotjar.com/c/hotjar-','.js?sv=');
</script>
     <script>
         (function (i, s, o, g, r, a, m) {
             i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                 (i[r].q = i[r].q || []).push(arguments)
             }, i[r].l = 1 * new Date(); a = s.createElement(o),
             m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
         })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

         ga('create', 'UA-76303804-1', 'auto');
         ga('send', 'pageview');

</script>
    <link rel="manifest" href="/manifest.json" />
<script src="https://cdn.onesignal.com/sdks/OneSignalSDK.js" async=""></script>
<script>
    var OneSignal = window.OneSignal || [];
    OneSignal.push(function() {
        OneSignal.init({
            appId: "8f46743a-24fc-45b6-b551-f9456c68c659",
        });
    });
</script>
    <script type='text/javascript'>
        window.__lo_site_id = 95423;
 
        (function() {
            var wa = document.createElement('script'); wa.type = 'text/javascript'; wa.async = true;
            wa.src = 'https://d10lpsik1i8c69.cloudfront.net/w.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(wa, s);
        })();
               </script>
  
      <script type="text/javascript">
          function ShowPopup(message) {
              $(function () {
                  $("#dialog").html(message);
                  $("#dialog").dialog({
                      title: "Information",
                      width: 400,
                      height: 250,
                      buttons: {
                          Close: function () {
                              $(this).dialog('close');
                          }
                      },
                      modal: true
                  });
              });
          };
</script>
       <script type="text/javascript">
    var yPos;
    var prm = Sys.WebForms.PageRequestManager.getInstance();    
    prm.add_endRequest(EndRequestHandler);
    $(window).scroll(function () { yPos = $(window).scrollTop(); });
    function EndRequestHandler(sender, args) {        
        $(window).scrollTop(yPos);
    }
</script>
    <!-- Google Tag Manager -->
<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
})(window,document,'script','dataLayer','GTM-TQLDCC6');</script>
<!-- End Google Tag Manager -->

    <script type="application/ld+json">
{
  "@context": "http://schema.org",
  "@type": "WebSite",
  "url": "https://www.eastlaw.pk/",
  "potentialAction": {
    "@type": "SearchAction",
    "target": "https://www.eastlaw.pk/search?q={search_term_string}",
    "query-input": "required name=search_term_string"
  }
}
</script>

 
    <style>
        .loader {
	position: fixed;
	left: 0px;
	top: 0px;
	width: 100%;
	height: 100%;
	z-index: 9999;
	background: url('page-loader.gif') 50% 50% no-repeat rgb(249,249,249);
}
    </style>
    <style type="text/css">

.modal1
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    /*background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;*/
}
.center1
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    /*background-color: White;*/
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center1 img
{
    height: 50px;
    width: 50px;
}
</style>
   
   
</head>

<body data-spy="scroll" data-target=".navbar" data-offset="30">
    <!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-TQLDCC6"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->
    <%--<div class="loader"></div>--%>
    <form runat="server">
        <asp:ScriptManager ID="scrpt" runat="server"></asp:ScriptManager>
             <asp:UpdatePanel ID="uppnl" runat="server">
            <ContentTemplate>
                    <div id="dialog" style="display: none" >
</div>
        
	 <!---------- Top Row ---------->
<header>
    <nav class="navbar navbar-default">
        <div class="row top_nav">
        
        	<div class="container">
            
            <div class="style1">
            
            
            	<div class="row">

     <a href="https://www.facebook.com/eastlaw.pk/" target="_blank">
    <i class="fa fa-facebook icon_style"></i></a>    
   
    
    
    <a href="https://www.linkedin.com/company/eastlawpk/" target="_blank" class="marginn">
    <i class="fa fa-linkedin icon_style"></i></a>
    
    <a href="https://twitter.com/EastLawpk" target="_blank">
    <i class="fa fa-twitter icon_style"></i></a>
    
  </div>
            
            
            </div>
                <div id="loginContainer" runat="server">

            	<ul class="pull-right hidden-xs" >
                	<li style="display:none"><a href="/en/careers">Careers</a></li>
                    <li><a href="/en/site-feedback">Site Feedback</a></li>
                    <li><a href="/en/privacy-policy">Privacy Policy</a></li>
                    <li><a href="/en/terms-of-use">Terms of Use</a></li>
                    <li><a href="/en/contact-us">Contact Us</a></li>
                    <li><a href="/member/member-register" class="a_style">Free Trial</a></li>
                    <li class="li_style"><a href="/member/member-login" class="a_style">Member Login</a></li>
                </ul>

                    <div class="dropdown visible-xs pull-right">
    <button class="btn btn-primary dropdown-toggle top" id="menu1" type="button" data-toggle="dropdown">Member Login
    <span class="caret"></span></button>
    <ul class="dropdown-menu" role="menu" aria-labelledby="menu1">
      <li role="presentation"><a role="menuitem" tabindex="-1" href="/en/careers">Careers</a></li>
      <li role="presentation"><a role="menuitem" tabindex="-1" href="/en/site-feedback">Site Feedback</a></li>
      <li role="presentation"><a role="menuitem" tabindex="-1" href="/en/privacy-policy">Privacy Policy</a></li>
      <li role="presentation"><a role="menuitem" tabindex="-1" href="/en/terms-of-use">Terms of Use</a></li>
      <li role="presentation"><a role="menuitem" tabindex="-1" href="/en/contact-us">Contact Us</a></li>
     
      <li role="presentation" class="divider" style="height:2px !important;"></li>
      <li role="presentation"><a role="menuitem" tabindex="-1" href="/member/member-register">Free Trial</a></li>    
      <li role="presentation"><a role="menuitem" tabindex="-1" href="/member/member-login">Member Login</a></li>    
    </ul>
  </div>
                    </div>

                <div  id="loginWelcome" runat="server" style="display: none">

                
                <ul class="pull-right">
                    <li><a href="/member/my-documents"><img src="/style/img/folder-4-xxl.png" height="17px" width="20px"/> My Documents</a></li>

                <li class="dropdown border_none"><a href="#" class="dropdown-toggle dropdown_style" data-toggle="dropdown-menu"><i class="fa fa-user margin-right"></i>
                    
                    <asp:Label ID="lblUserName" runat="server"></asp:Label>  <b class="caret"></b></a>

                     <ul class="dropdown-menu">
                        <li><a href="member/member-dashboard">Home</a></li>
                        <li><a href="/member/my-settings">Change Password</a></li>
                         <li><a href="/member/my-subscription">My Subscription</a></li>
                        <li><a href="/member/my-documents">My Documents</a></li>
                        <li><a href="/en/site-feedback">Site Feedback</a></li>
                         <li><a href="/en/privacy-policy">Privacy Policy</a></li>
                            <li><a href="/en/terms-of-use">Terms of Use</a></li>
                         <li><a href="/en/contact-us">Contact Us</a></li>
                        <li><a href="/?bG9nb3V0=do">Logout</a></li>
                    </ul>

                </li>

            </ul>
                    </div>
            
            </div>
        
        </div>
    
    
    <!---------- Top Row End ---------->
    
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <%--<a class="navbar-brand" href="#"><img src="/style/img/logo2.jpg" class="img-responsive img_style"/></a>--%>
        </div>
        <div id="navbar" class="navbar-collapse collapse">
          <ul class="nav navbar-nav"	>
           <li><a href="/">Home</a></li>
                	<li><a href="/en/features">Features</a></li>
                     <li><a href="/en/data-coverage">Data Coverage</a></li>
               <li >
          <a href="/statutes/legislations" class="dropdown-toggle">Legislation <b class="caret"></b></a>
          <ul class="dropdown-menu">
           <li><a href="/statutes/federal-legislation">Federal Legislation</a></li>
                    <li><a href="/statutes/provincial-legislation">Provincial Legislation</a></li>
                    <li><a href="/statutes/federal-amendment-acts">Federal Amendment Acts</a></li>
                    <li><a href="/statutes/provincial-amendment-acts">Provincial Amendment Acts</a></li>
                    <li><a href="/statutes/federal-rules-and-regulations">Federal Rules and Regulations</a></li>
                    <li><a href="/statutes/provincial-rules-and-regulations">Provincial Rules and Regulations</a></li>
                    <li><a href="/statutes/bill-by-national-assembly">Bill By National Assembly</a></li>
                    <li><a href="/statutes/bill-by-provincial-assembly">Bill By Provincial Assembly</a></li>
                    <li style="display:none"><a href="/statutes/circulars-s-r-o-schemes-etc">Circulars, S.R.O's Schemes, etc</a></li>
            
          </ul>
        </li>
                     <li><a href="/en/practice-area">Practice Areas</a></li>
                	<li ><a href="/en/Departments">Departments </a></li>
                	
                	
                    
                    <li><a href="/e-newslive" class="dropdown-toggle">E-News Live </a></li>
		<li><a href="/subscription">Subscribe</a></li>
              <%--<li class="li_style2" ><a href="/cases/latest-judgments-public">Latest Judgments</a></li>--%>
                    <li class="li_style2" style="display:none">
                    	<div class="input_style">
                            <asp:TextBox ID="txtSearch" runat="server" class="form-control text_style" placeholder="Search Here"  OnTextChanged="txtSearch_TextChanged" ValidationGroup="Search"></asp:TextBox>
                        <div class="input-group-btn">
                            <button class="btn btn-default btn_height" type="submit"><i class="fa fa-search"></i></button>
                        
                        </div>
                    </div>
                    
                    </li>
          </ul>
          
        </div><!--/.nav-collapse -->
      </div>
    </nav>
    
    <!----------- Main Nav ------------->
    
    	
        	<%--<nav class="navbar main_nav collapse navbar-collapse" data-spy="affix" data-offset-top="197">
            <div class="container">
             	<ul>
                	<a href="#"><img src="/style/img/logo2.jpg" class="img-responsive img_style"/></a>
                     
                </ul>
            	</div>
            </nav>--%>
    	</header>
    <!----------- Main Nav End ------------->
    
    
    <!-------------- Content --------------->
    
    <div class="container">
    <div class="row">
    
    <!------------ Left Side ------------->
    
    	<div class="col-lg-8 col-md-8 padding_left">
        <div class="img-main">
			<img src="/style/data1/images/eastlaw01.jpg" alt="" title="" id="" style="width:100%; "/>
            </div>
        	<%--<div id="wowslider-container1">
	<div class="ws_images"><ul>
		<li><img src="/style/data1/images/slider1.jpg" alt="" title="" id="wows1_0"/></li>
		<li><img src="/style/data1/images/slider2.jpg" alt="" title="" id="wows1_1"/></li>
		
		<li><img src="/style/data1/images/slider4.jpg" alt="" title="" id="wows1_3"/></li>
	</ul></div>
	<div class="ws_bullets"><div>
		<a href="#" title=""><span><img src="/style/data1/tooltips/slider1.jpg" alt=""/>1</span></a>
		<a href="#" title=""><span><img src="/style/data1/tooltips/slider2.jpg" alt=""/>2</span></a>
		
		<a href="#" title=""><span><img src="/style/data1/tooltips/slider4.jpg" alt=""/>4</span></a>
	</div></div>
	
	</div>--%>
        
        
        
        <!-------------- Slider End ----------------->
        
         
        <div class="jumbotron style_2">
        
        	<h2>Move Your Research to the Web with Eastlaw.pk</h2>
        	<p>With a collection of over 100 databases, with more than 1 million documents and over 10.4 million pages, our legal research tool provides top quality information with an interface which makes research a quicker, easier and more effective process.
</p>

		<div class="row margin_top">
        
        	<div class="col-lg-4 col-md-4">
            
            	<div >
                    <%--<div class="text-center height">--%>
                
                	<img src="/style/img/item1.png" class="circle_red" />
                    
                    <h3>Quicker</h3>
                    
                    <p class="hover">Get results on the topic of your search in milliseconds. No more looking through indexes at libraries or waiting for webpages to load. The newest iteration of Eastlaw.pk is the quickest.</p>
                
                </div>
            
            </div>
            
            
            
            <div class="col-lg-4 col-md-4">
            
            	<%--<div class="text-center height">--%>
                <div >
                	<img src="/style/img/item2.png" class="circle_red" />
                    
                    <h3>Easier</h3>
                    
                    <p class="hover">Conducting quality legal research no longer requires extensive knowledge of the intricate legal system itself. Because of the intuitive interface and comprehensive topic guide, anyone who has access to the Internet can now conduct research independently, regardless of their location or time.
</p>
                
                </div>
            
            </div>
            
            
            <div class="col-lg-4 col-md-4">
            
            	<%--<div class="text-center height">--%>
                <div >
                	<img src="/style/img/item3.png" class="circle_red" />
                    
                    <h3>More Effective</h3>
                    
                    <p class="hover">Our content is updated daily, following comprehensive standards of quality control, thereby ensuring that your research is accurate and up-to-date. With options to filter your search results and ability to refine searches, you spend less time scrolling and more time building your argument.
</p>
                
                </div>
            
            </div>
            
        
        </div>
        
        <!-- Row End -->
        
        
        
            
        
        
        </div>
        
    </div>
    <!------------ Left Side End ------------->
    
    <!------------ Right Side ------------->
   
    	<div class="col-lg-4 col-md-4 padding_left">
        
        	<div class="row">
           
        		<div class="login">
                
                	<div class="style_3">
                    	<i class="fa fa-user icon_color"></i><span class="span_style">Login</span>
                    </div>
                    
                    <div>
                    
                        <asp:TextBox ID="txtEmailIDLogin" runat="server" class="form-control" placeholder="Email Address" ValidationGroup="Login"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="Login" runat="server" ErrorMessage="Email ID Required" ForeColor="Red" ControlToValidate="txtEmailIDLogin" Display="Dynamic" ></asp:RequiredFieldValidator>
                        
                        <asp:TextBox ID="txtPasswordLogin" runat="server" TextMode="Password" class="form-control" placeholder="Password" AutoPostBack="False" ValidationGroup="Login" OnTextChanged="txtPasswordLogin_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="Login" runat="server" ErrorMessage="Password Required" ForeColor="Red" ControlToValidate="txtPasswordLogin"  Display="Dynamic"></asp:RequiredFieldValidator>
                    
                	
                    </div>
                    
                    <div class="row">
                    
                    <div class="pull-left">
                    	
                    	<span><asp:CheckBox ID="chkRem" runat="server" Text="Remember Me" /></span>
                    </div>
                    
                    <div class="pull-right">
                    	<a  href="/member/forget-password" class="forgot_pass">Forgot Password ?</a>
                    </div>
                    
                    
                    </div>
                    
                    <div>
                    <span style="font-size:10pt;line-height:14pt;text-align:left"><br />
                            
                             <asp:CheckBox ID="chkTNC" runat="server" Checked="true" />
                         Use of this service is subject to <a href="/en/Terms-of-Use" target="_blank">Terms & Conditions</a> and <a href="/en/Privacy-Policy" target="_blank">Privacy Policy</a>. Please review this information before proceeding. You must accept the terms and conditions to use the service
                            </span>
                    	<br />
                        <asp:Label ID="lblMsg" runat="server" Text="Please enter search words" Visible="false" ForeColor="Red"></asp:Label>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-danger btn_style" OnClick="btnLogin_Click" ValidationGroup="Login" data-step="3" data-intro="Click Here to login"/>
                          
                        
                        <br />
                    
                    </div>
                    
                    <div class="line" style="display:none">
                    	
                        <div class="login_with">
                        	<span>or login using</span>
                        </div>
                        
                    </div>
                    
                    <div class="row" style="display:none">
                    
                    	<div class="text-center">

    <a href="http://facebook.com" class="btn btn-social-icon btn-facebook">
    <i class="fa fa-facebook"></i></a>    
   
    <a class="btn btn-social-icon btn-google-plus"><i class="fa fa-google-plus"></i></a>
    
    <a class="btn btn-social-icon btn-linkedin"><i class="fa fa-linkedin"></i></a>
    
    <a class="btn btn-social-icon btn-twitter"><i class="fa fa-twitter"></i></a>
    
  </div>
                        
                    
                    </div>
                    
                    
                    <div class="row subscription">
                    
                    	<ul>
                    		<li class="padding-left"><a href="/subscription">Subscribe</a></li>
                            <li><a href="/member/member-login?req=/member/subscription">Renew Subcription</a></li>
                            <li><a href="/member/member-register" class="a_style2">Free Trial</a></li>
                        </ul>
                    
                    </div>
                
                
                </div>
               
                
                
                
                <div class="row margin-top2">
                    <a href="/member/member-register"><img src="/images/Eastlaw_Signup.jpeg"  width="360"/></a>
    	<asp:UpdatePanel ID="upPnlLatestNews" runat="server">
    <ContentTemplate>
        
        <asp:Timer ID="Timer1" runat="server" OnTick="TimerTick" Interval="1" Enabled="false">
        </asp:Timer>
        

         <asp:Image ID="imgLoader" runat="server" ImageUrl="/page-loader.gif" Visible="false" />
      <div class="carousel-inner" style="border:1px solid;border-color:lightgray;display:none">   
        <telerik:RadRotator RenderMode="Lightweight" ID="RadRotator1" runat="server" Width="330" Height="400" CssClass="horizontalRotator"   Visible="false"
             ScrollDirection="Left,Right"
                ScrollDuration="500" FrameDuration="4000" ItemHeight="230" ItemWidth="330" RotatorType="AutomaticAdvance" SlideShowAnimation-Type="Fade">
                <ItemTemplate>
                  <div class="item active" style="background-color:white"> 
                  
                    	<a href='<%# Eval("SourceLink") %>' target='_blank'><img class="thumbnail" src='<%# Eval("imgURL") %>' alt='<%# Eval("Title") %>' height="220px" width="100%"></a>
                        <div class="caption" style="background-color:white">
                       	  <h4><a href='<%# Eval("SourceLink") %>' target='_blank'> <%# EastlawUI_v2.CommonClass.GetChracter(Eval("Title").ToString(),100) %>...</a> </h4><br />
                            Date: <%# Eval("NDate") %> <br />
                            <i><%# Eval("Source") %>,<%# Eval("PracticeAreaSubCatName") %>,<%# Eval("StatutesCategories") %>,<%# Eval("Author") %></i>
                            <%--<p>Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. </p>--%>
                        </div>
                        
                        <%--<div class="channel_logo">
                        
                        	<img src="img/channel1.png" />
                        
                        </div>--%>
                      </div>
                
                </ItemTemplate>
             <ControlButtons LeftButtonID="prevButton" RightButtonID="nextButton"></ControlButtons>
            </telerik:RadRotator>
        <div class="navigation" style="float:right;padding-right:7px;padding-bottom:5px;display:none">
            <asp:Image ImageUrl="/images/ArrowsLeftcircular.ico" ID="prevButton" AlternateText="Previous Slide"
                       runat="Server" Height="34" Width="33" Style="border: 0px"></asp:Image>
            <asp:Image ImageUrl="/images/ArrowsRightcircular.ico" ID="nextButton" AlternateText="Next Slide"
                       runat="Server" Height="34" Width="31" Style="border: 0px"></asp:Image>
        </div>
          </div>
                                
            
            </ContentTemplate>
</asp:UpdatePanel>
           
    	
    </div>
                
        
    		</div>
            
    	</div>
                 
    
    <!------------ Right Side End ------------->
    
    	
    </div>  
    </div>
    
    <!-------------- Content End --------------->
    
    
    <div class="container">
    	<div class="jumbotron margin_bottom">
    
    		
            
            
            	<div class="row">
        
        	<h2 class="margin_bottom2">Why EastLaw?</h2>
        
        	<div class="col-lg-4 col-md-4">
        		
               <div class="text-center border">
                	<i class="fa fa-search fa-4x new_icon"></i>
                    <h3>Powerful Search</h3>
               		
                    
                    <div class="overlay">
                    
                    	<i class="fa fa-search new_icon2"></i>
                    
                    	<h5>Powerful Search</h5>
                    	<p>Our software logic is optimized to vastly improve the relevance of your search results and our various search and filter options allow you to make the process more efficient. Make searches in specific databases, or search by topic, section, party names, citations or statutes. Filter your results by date, party names or Coram to find exactly what you’re looking for.
</p>
                    
                    </div>
        	
            </div>
            
            </div>
            
            
            <div class="col-lg-4 col-md-4">
        		
               <div class="text-center border">
                	<i class="fa fa-file-text-o fa-5x new_icon"></i>
                    <h3>Content Coverage</h3>
             
                    
                    <div class="overlay">
                    
                    	<i class="fa fa-file-text-o new_icon2"></i>
                    
                    	<h5>Content Coverage</h5>
                    	<p>The depth and breadth of content helps you to excel at every stage of your research and analysis...

</p>
                    
                    </div>
        	
            </div>
            
            
            
            </div>
            
            
            
            <div class="col-lg-4 col-md-4">
        		
               <div class="text-center border">
                	<i class="fa fa-laptop fa-5x new_icon"></i>
                    <h3>Accessblity</h3>
               		</i>
                    
                    <div class="overlay">
                    
                    	<i class="fa fa-laptop new_icon2"></i>
                    
                    	<h5>Accessblity</h5>
                    	<p>Eastlaw.pk Web Edition is hosted on the Internet, and allows for access to our databases from anywhere in the world. The Web Edition is device independent and platform independent and usable on – Windows PC, Macintosh, Linux or a tablet or mobile phone with internet capability. 


</p>
                    
                    </div>
        	
            </div>
            
            
            
            </div>
            
            
            <div class="col-lg-4 col-md-4">
        		
               <div class="text-center border">
                	<i class="fa fa-mobile fa-5x new_icon"></i>
                    <h3>Mobile Application</h3>
               		</i>
                    
                    <div class="overlay">
                    
                    	<i class="fa fa-mobile new_icon2"></i>
                    
                    	<h5>Mobile Application</h5>
                    	<p>Log on to WEB APP or download the App to access eastlaw.pk on your phone. 
Complimentary with your online subscription.


</p>
                    
                    </div>
        	
            </div>
            
            
            
            </div>
            
            
            <div class="col-lg-4 col-md-4">
        		
               <div class="text-center border">
                	<i class="fa fa-cogs fa-5x new_icon"></i>
                    <h3>Powerful Features</h3>
               		</i>
                    
                    <div class="overlay">
                    
                    	<i class="fa fa-cogs new_icon2"></i>
                    
                    	<h5>Powerful Features</h5>
                    	<p>Eastlaw.pk was developed with everyone in mind, but it is also meant just for you. With the introduction of eastlaw.pk you can now store your selected research results in folders on your very own account, classified and categorized into your own self-created folders, allowing you to pick up where you left off or to access results easily without having to print everything out.

</p>
                    
                    </div>
        	
            </div>
            
            
            
            </div>
            
            
            <div class="col-lg-4 col-md-4">
        		
               <div class="text-center border">
                	<i class="fa fa-money fa-5x new_icon"></i>
                    <h3>Pay Online</h3>
               		</i>
                    
                    <div class="overlay">
                    
                    	<i class="fa fa-money new_icon2"></i>
                    
                    	<h5>Pay Online</h5>
                    	<p>Our online payment gateway also makes it simple and convenient to purchase, renew or upgrade your subscription at any point of time.

</p>
                    
                    </div>
        	
            </div>
            </div>
            
            
            </div>
            
            </div>
    
    	</div>
        
        
        <div class="container">
        
        	<div class="row newsletter">
            
            	<div class="col-lg-1 col-md-1"></div>
        	
            	<div class="col-lg-4 col-md-4 heading">
            	
                	<h2>get the newsletter</h2>
                
                </div>
                
                <div class="col-lg-6 col-md-5 subcribe">
            	
                	<div>
                        <asp:UpdatePanel ID="updSubscribe" runat="server">
                            <ContentTemplate>

                        <asp:TextBox ID="txtNewslettersubscribeemail" runat="server" CssClass="form-control text_style2" placeholder="Enter your email id" ValidationGroup="Subscribe"></asp:TextBox>
                        
                        <div class="input-group-btn">
                            <asp:Button ID="btnSubscribe" runat="server" CssClass="btn btn-default btn-style" Text="Subscribe" ValidationGroup="Subscribe" OnClick="btnSubscribe_Click" />
                            <asp:Label ID="lblSubscribeMsg" runat="server" ForeColor="Green" Text="Thanks, Email ID added for newsletters!!"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvSubscribeEmail" runat="server" ErrorMessage="Enter Email ID" ControlToValidate="txtNewslettersubscribeemail"
                                Display="Dynamic" ValidationGroup="Subscribe" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNewslettersubscribeemail" Display="Dynamic" ValidationGroup="Subscribe"
                                 ErrorMessage="Invalid Email ID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </div>
                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                </div>
                
            
            </div>
        
        </div>
        </div>
        
        
        
        <div class="container">
        
        	<div class="row footer1">
            
            	<div class="col-lg-4 col-md-4">
                
                	<div class="cover">
                    
                    	<div class="footer_parant">
                        	<i class="fa fa-phone fa-3x"></i><h2>Call Now</h2>
                        </div>
                        
                        <div class="footer_inn">
                        
                        	<div>
                        	<span class="span_style2">0304-1116670</span>	
                                		
    						</div>
                            
                            <div class="margin-top3">
                        	<span class="span_style3"><i class="fa fa-whatsapp icon_style2"></i>Whatsapp: </span><span class="font">0304-1116670</span>
    
    	<span class="span_style4"> (Monday to Friday - 10:00am – 5:00pm )</span>
                                <span class="span_style4"> (Saturday - 10:00am – 3:00pm )</span>
    						
                            </div>
    						
                        </div>
                    
                    </div>
                
                </div>
                
                
                
                <div class="col-lg-4 col-md-4">
                
                	<div class="cover">
                    
                    	<div class="footer_parant">
                        	<i class="fa fa-envelope fa-3x"></i><h2>Email Us</h2>
                        </div>
                        
                        <div class="footer_inn">
                        
                        	<div>
                       <b> General Queries: </b><span class="font"><a href="mailto:info@eastlaw.pk">info@eastlaw.pk</a></span>			
    						</div>
                            
                            
                        </div>
                       <%-- <div class="footer_inn">
                        
                        	<div>
                        <b>Technical/Legal Queries: </b><span class="font"><a href="mailto:support@eastlaw.pk">support@eastlaw.pk</a></span>			
    						</div>
                            
                            
                        </div>--%>
                    
                    </div>
                
                </div>
                
                
                
                <div class="col-lg-4 col-md-4">
                
                	<div class="cover border_none">
                    
                    	<div class="footer_parant">
                        	<i class="fa fa-address-book fa-3x"></i><h2>Address</h2>
                        </div>
                        
                        <div class="footer_inn">
                        <h3>Eastlaw (Pvt.) Ltd.</h3>
                        	<div>
                        <span class="font">39 Link Farid Kot Road,
    <br />Lahore, Pakistan.</span>			
    						</div>
                            
                            
                        </div>
                    
                    </div>
                
                </div>
                 
                
                
                
            
            </div>
            <br />
            <div class="col-lg-12 col-md-12">
                     Technology Partner: <a href="http://www.widerangedigital.com" target="_blank"> Wide Range Digital Services</a>
                     </div>
        
        </div>
        
        <%--<div class="buttons">
        
        		<a href="#" class="bgcolor" data-placement="left" data-toggle="tooltip" title="User Manual">
                
                	<i class="fa fa-book"></i>
                	
                    
                </a>
                
                <a href="#" class="bgcolor2" data-toggle="tooltip" title="Free Trial!" data-placement="left">
                
                	<i class="fa fa-at"></i>
                	
                    
                </a>
                
                <a href="#" class="bgcolor3" data-toggle="tooltip" title="Subscribe" data-placement="left">
                
                	<i class="fa fa-shopping-cart"></i>
                	
                    
                </a>
        
        
        </div>--%>
           <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uppnl">
                    <ProgressTemplate>
                     
                           <div class="modal1">
        <div class="center1">
           <%--<img alt="" src="/style/img/ajax_loader_big.gif" />--%>
        </div>
    </div>
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                 </ContentTemplate>
                   </asp:UpdatePanel>      
        </form>
    
 <a href="#0" class="cd-top">Top</a>
<script type="text/javascript" src="/style/js/jquery.js"></script>
<script type="text/javascript" src="/style/engine1/slider.js"></script>
<script type="text/javascript" src="/style/engine1/script.js"></script>
<script type="text/javascript" src="/style/js/bootstrap.min.js"></script>
<script type="text/javascript" src="/style/js/main.js"></script>

     
     <script type="text/javascript">
         $(window).load(function() {
             $(".loader").fadeOut("slow");
         })
</script>
<script>
	
	    $(function(){
    $(".dropdown").hover(            
            function() {
                $('.dropdown-menu', this).stop( true, true ).fadeIn("fast");
                $(this).toggleClass('open');
                $('b', this).toggleClass("caret caret-up");                
            },
            function() {
                $('.dropdown-menu', this).stop( true, true ).fadeOut("fast");
                $(this).toggleClass('open');
                $('b', this).toggleClass("caret caret-up");                
            });
    });
    

</script>
<script type="text/javascript">
$(function() {

    $('#tootip').tooltip();

});​
</script> 
<script>

$('#myCarousel').carousel({
    	interval:   4000
	});
	
	$(".navbar").affix({
  offset: {
    top: 0
  }
});



</script>
   <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
<script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>


    <!--Start of Tawk.to Script-->
<script type="text/javascript">
    var Tawk_API=Tawk_API||{}, Tawk_LoadStart=new Date();
    (function(){
        var s1=document.createElement("script"),s0=document.getElementsByTagName("script")[0];
        s1.async=true;
        s1.src='https://embed.tawk.to/5a9d02b34b401e45400d675c/default';
        s1.charset='UTF-8';
        s1.setAttribute('crossorigin','*');
        s0.parentNode.insertBefore(s1,s0);
    })();
</script>
<!--End of Tawk.to Script-->
</body>
</html>
