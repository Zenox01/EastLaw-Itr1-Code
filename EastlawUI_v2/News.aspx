<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="EastlawUI_v2.News"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
<title>EastLaw.pk – Pakistan's Largest Online Law Library</title>
<link href="/style/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="/style/css/style.css" rel="stylesheet" type="text/css" />
<link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
type="text/css" rel="stylesheet" />
<link rel="stylesheet" type="text/css" href="/style/engine1/slider.css" />
<link href="/style/css/icons.css" rel="stylesheet" type="text/css" />
 <link rel="shortcut icon" href="/style/img/fav.png">


<link href="/style/css/news-slider.css" rel="stylesheet" type="text/css" />
<!--<link href="css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />-->
<link href="/style/css/site.css" rel="stylesheet" type="text/css" />
<link href="/style/css/breadcrums.css" rel="stylesheet" type="text/css" />
<link href="/style/css/back-top.css" rel="stylesheet" type="text/css" />
<link href="/style/css/enewslive.css" rel="stylesheet" type="text/css" />
    <link href="/style/css/responsive.css" rel="stylesheet" type="text/css" />
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
   <script type='text/javascript'>
       window.__lo_site_id = 95423;
 
       (function() {
           var wa = document.createElement('script'); wa.type = 'text/javascript'; wa.async = true;
           wa.src = 'https://d10lpsik1i8c69.cloudfront.net/w.js';
           var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(wa, s);
       })();
               </script>
     <style>
        .loader {
	position: fixed;
	left: 0px;
	top: 0px;
	width: 100%;
	height: 100%;
	z-index: 9999;
	background: url('/page-loader.gif') 50% 50% no-repeat rgb(249,249,249);
}
    </style>
</head>

<body data-spy="scroll" data-target=".navbar" data-offset="30">
     <div class="loader"></div>
    <form runat="server">	
         <!---------- Top Row ---------->
            <header>
        <div class="row top_nav">
        
        	<div class="container">
            
            <div class="style1">
            
            
            	<div class="row">

     <a href="https://www.facebook.com/eastlaw.pk/" target="_blank">
    <i class="fa fa-facebook icon_style"></i></a>    
   
    
    
    <a href="https://www.linkedin.com/in/east-law-506aaa148/" target="_blank" class="marginn">
    <i class="fa fa-linkedin icon_style"></i></a>
    
    <a href="https://twitter.com/Eastlaw_pk?s=08" target="_blank">
    <i class="fa fa-twitter icon_style"></i></a>
    
  </div>
            
            
            </div>
            	<ul class="pull-right hidden-xs" id="loginContainer" runat="server">
                	<li><a href="/en/careers">Careers</a></li>
                    <li><a href="/en/site-feedback">Site Feedback</a></li>
                    <li><a href="/en/privacy-policy">Privacy Policy</a></li>
                    <li><a href="/en/terms-of-use">Terms of Use</a></li>
                    <li><a href="/en/contact-us">Contact Us</a></li>
                    <li><a href="/member/member-register" class="a_style">Free Trial</a></li>
                    <li class="li_style"><a href="/member/member-login" class="a_style">Member Login</a></li>
                </ul>

                <ul class="pull-right" id="loginWelcome" runat="server" style="display: none">
                    <li><a href="/member/my-documents"><img src="/style/img/folder-4-xxl.png" height="17px" width="20px"/> My Documents</a></li>

                <li class="dropdown border_none"><a href="#" class="dropdown-toggle dropdown_style" data-toggle="dropdown-menu"><i class="fa fa-user margin-right"></i>
                    
                    <asp:Label ID="lblUserName" runat="server"></asp:Label>  <b class="caret"></b></a>

                     <ul class="dropdown-menu">
                        <li><a href="/member/member-dashboard">Home</a></li>
                        <li><a href="/member/my-settings">My Account</a></li>
                        <li><a href="/member/my-documents">My Documents</a></li>
                         <li><a href="/member/my-subscription">My Subscription</a></li>
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
    
    
    <!---------- Top Row End ---------->
    
    <!----------- Main Nav ------------->
    
    	
        	   <nav class="navbar navbar-default" id="navWithoutLogin" runat="server">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="#"><img src="/style/img/logo2.jpg" class="img-responsive img_style"/></a>
        </div>
        <div id="navbar" class="navbar-collapse collapse">
          <ul class="nav navbar-nav"	>
           <li><a href="/">Home</a></li>
                	<li><a href="/en/features">Features</a></li>
                     <li><a href="/en/data-coverage">Data Coverage</a></li>
                     <li><a href="/en/practice-area">Practice Areas</a></li>
                	<li ><a href="/en/Departments">Departments </a></li>
                	
                	
                    
                    <li><a href="/e-newslive" class="dropdown-toggle">E-Newslive </a></li>
		<li><a href="/subscription">Subscribe</a></li>
                  
          </ul>
          
        </div><!--/.nav-collapse -->
      </div>
    </nav>
            <nav class="navbar navbar-default" id="navWithLogin" runat="server">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="#"><img src="/style/img/logo2.jpg" class="img-responsive img_style"/></a>
        </div>
        <div id="navbar" class="navbar-collapse collapse">
          <ul class="nav navbar-nav"	>
         
                	<li><a href="/member/member-dashboard">Home</a></li>
                	<%--<li><a href="/cases/advance-search">Case Law</a></li>--%>
               <li class="dropdown">
          <a href="#" class="dropdown-toggle">Case Law<b class="caret"></b></a>
          <ul class="dropdown-menu">
           <li><a href="/cases/advance-search">Advance Case Law</a></li>
                    <li><a href="/cases/latest-judgments">Latest Case Law</a></li>
          </ul>
        </li>

                	<%--<li><a href="#">Case Law by Section</a></li>--%>
                    <li class="dropdown">
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
                    <li class="dropdown"><a href="#"  class="dropdown-toggle">Practice Areas <b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <%
                            EastLawBL.PracticeAreas objpa = new EastLawBL.PracticeAreas();
                            System.Data.DataTable dtPA = new System.Data.DataTable();
                            dtPA = objpa.GetActivePracticeAreaSubCategoriesByCategory(3);
                            for (int a = 0; a < dtPA.Rows.Count; a++)
                            {
                                Response.Write("<li><a href='/practice-area/" + dtPA.Rows[a]["PracticeAreaSubCatName"].ToString().Replace(" ", "-").Replace("&","-").ToLower() + "?param=" + EncryptDecryptHelper.Encrypt(dtPA.Rows[a]["ID"].ToString()) + "&trans=" + dtPA.Rows[a]["PracticeAreaSubCatName"].ToString().Replace(" ", "-") + "'>" + dtPA.Rows[a]["PracticeAreaSubCatName"].ToString() + "</a></li>");
                            }
                            %>
                          
            
                            </ul>
                    </li>
                	<li class="dropdown">
          <a href="/departments/departments-home" class="dropdown-toggle">Departments <b class="caret"></b></a>
          <ul class="dropdown-menu">
               <%
     if (Session["MemberID"] != null)
     {
          EastLawBL.Departments objdept = new EastLawBL.Departments();
         System.Data.DataTable dtlevel1 = objdept.GetActiveDeptByParent(0);

         if (dtlevel1 != null)
         {
             for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
             {
                 Response.Write("<li><a href='/departments/departments-home?dptn=" + dtlevel1.Rows[l1]["DeptName"].ToString().ToLower().Replace(" ", "-") + "&dptkp=" + EncryptDecryptHelper.Encrypt(dtlevel1.Rows[l1]["ID"].ToString()) + "'> " + dtlevel1.Rows[l1]["DeptName"].ToString() + "</a></li>"); 
             } 
         }
     }
     %>
            <%--<li><a href="#">Income Tax</a></li>
            <li><a href="#">Sales Tax</a></li>
            <li><a href="#">Customs</a></li>
            
            <li><a href="#">Federal Excise</a></li>
            
            <li><a href="#">SECP</a></li>
            <li><a href="#">PRA</a></li>
            <li><a href="#">SRB</a></li>
              <li><a href="#">SBP </a></li>--%>
          </ul>
        </li>
                    
                    <li class="dropdown">
          <a href="/e-newslive" class="dropdown-toggle">E-News Live </a>
        </li>
                         <li class="dropdown">
          <a href="#" class="dropdown-toggle">Legal Maxims & Dictionary <b class="caret"></b></a>
          <ul class="dropdown-menu">
            <li><a href="/dictionary/dictionary-all">Dictionay</a></li>
            <li><a href="/dictionary/legal-maxims">Legal Maxims</a></li>
          </ul>
        </li>
          </ul>
          
        </div><!--/.nav-collapse -->
      </div>
    </nav>
    </header>
    <!----------- Main Nav End ------------->
    <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/" class="first">Home</a></li>
 
    <li><a href="#" class="current">E-News live</a></li>
</ul>
  </div>
</div>
    
    <!--News Nav -->
    
    
     
    <!-------------- Content --------------->
    
    <div class="container">
    <div class="row">
    
    
    
    	<div id="myCarousel" class="carousel slide margin_bot_20" data-ride="carousel">
    
      <!-- Wrapper for slides -->
      <div class="carousel-inner">
      
          <%
              try
              {
                  System.Data.DataTable dt = new System.Data.DataTable();
                  EastLawBL.News objn = new EastLawBL.News();
                  dt = objn.GetActiveNews();
                  for (int a = 0; a < dt.Rows.Count; a++)
                  {
                      if (!string.IsNullOrEmpty(dt.Rows[a]["imgfile"].ToString()))
                      {
                          string imgfilename = "";
                          if (dt.Rows[a]["ImageType"].ToString() == "Local")
                              imgfilename = "/store/news/imgs/" + dt.Rows[a]["imgfile"].ToString();
                          else if (dt.Rows[a]["ImageType"].ToString() == "URL")
                              imgfilename = dt.Rows[a]["imgfile"].ToString();
                          else
                              imgfilename = dt.Rows[a]["imgfile"].ToString();
                          if (a == 0)
                          {
                              Response.Write("<div class='item active'>"
                                  // + "<a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'><img src='" + dt.Rows[a]["imgfile"].ToString() + "' alt='" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString()) + "' /></a>"
                               + "<img src='" + imgfilename + "' alt='" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString()) + "' />"
                               + "<div class='carousel-caption'>"
                               + "<h5><a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'>" + EastlawUI_v2.CommonClass.GetChracter(dt.Rows[a]["Title"].ToString(), 30) + "...</a> </h5>"
                                 + "<p>" + EastlawUI_v2.CommonClass.GetWords(EastlawUI_v2.CommonClass.RemoveHTML(dt.Rows[a]["FullContent"].ToString()), 50) + " ... </p>"
                            + "</div></div>");


                          }
                          else
                          {
                              Response.Write("<div class='item'>"
                                  //+ "<a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'><img src='" + dt.Rows[a]["imgfile"].ToString() + "' alt='" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString()) + "' /></a>"
                              + "<img src='" + imgfilename + "' alt='" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString()) + "' />"
                              + "<div class='carousel-caption'>"
                              + "<h5><a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'>" + EastlawUI_v2.CommonClass.GetChracter(dt.Rows[a]["Title"].ToString(), 30) + "...</a> </h5>"
                                + "<p>" + EastlawUI_v2.CommonClass.GetWords(EastlawUI_v2.CommonClass.RemoveHTML(dt.Rows[a]["FullContent"].ToString()), 50) + " ... </p>"
                           + "</div></div>");


                          }
                      }
                      if (a == 5)
                          break;

                  }
              }
              catch { }
                  %>
    
        
        
                
      </div><!-- End Carousel Inner -->


    <ul class="list-group col-sm-4">
                <%
                        try
                        {
                            System.Data.DataTable dtCount = new System.Data.DataTable();
                            EastLawBL.News objn1 = new EastLawBL.News();
                            dtCount = objn1.GetActiveNews();
                            for (int a = 0; a < dtCount.Rows.Count; a++)
                            {
                                if (a == 0)
                                {
                                    Response.Write("<li data-target='#myCarousel' data-slide-to='" + a.ToString() + "' class='list-group-item active'><h4>" + dtCount.Rows[a]["Title"].ToString() + " </h4></li>");
                                }
                                else
                                {
                                    Response.Write("<li data-target='#myCarousel' data-slide-to='" + a.ToString() + "' class='list-group-item'><h4>" + dtCount.Rows[a]["Title"].ToString() + " </h4></li>");
                                }
                                if (a == 5)
                                    break;
                            }

                        }

                        catch { }
       %> 
    </ul>

      <!-- Controls -->
      <div class="carousel-controls">
          <a class="left carousel-control" href="#myCarousel" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left"></span>
          </a>
          <a class="right carousel-control" href="#myCarousel" data-slide="next">
            <span class="glyphicon glyphicon-chevron-right"></span>
          </a>
      </div>

    </div><!-- End Carousel -->
    
            
    </div>
    </div>
    
    <!-------------- Content End --------------->
    
    
    <!--Judments-->
    
    <div class="container">

        <div class="row">

            <div class="col-lg-8">

                <div class="panel panel-default">
                    <div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Judgements</b></div>
                    <div class="panel-body" style="padding-bottom:0;">
                        <div class="row">

                            <div class="col-lg-6">

                                <div class="panel panel-default">
                                    <div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Supreme Court of Pakistan</b></div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-12" style="padding:0;">
                                                <ul class="demo1">
                                                    <%
                                                    System.Data.DataTable dtLatest = new System.Data.DataTable();
                                                    EastLawBL.Cases objc = new EastLawBL.Cases();
                                                    dtLatest = objc.GetCasesByCourt("Supreme Court");
                                                    if (dtLatest != null)
                                                    {
                                                    for (int a = 0; a < dtLatest.Rows.Count; a++)
                                                    {
                                                    Response.Write("<li class='news-item'>"
                                                        + "<table cellpadding='4'><tr>"
                                                                + "<td><b>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 2) + "... VS " + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 2) + "...</b><br />"
                                                                    + "<b>Date:</b> " + dtLatest.Rows[a]["FormatedJdate"].ToString() + "<br /><b>Court:</b> " + dtLatest.Rows[a]["Court"].ToString()
                                                                    //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                                                                    + "   <a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["ID"].ToString())+"'>Read more...</a></td></tr></table></li>");
                                                    if (a == 7)
                                                    break;
                                                    }
                                                    }

                                                    %>


                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer"><a href="/cases/supreme-court-of-pakistan">View More</a></div>
                                </div>

                            </div>

                            <!--panel 1 end -->

                            <div class="col-lg-6">

                                <div class="panel panel-default">
                                    <div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>High Court</b></div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-12" style="padding:0;">
                                                <ul class="demo1">
                                                    <%
                                                    // System.Data.DataTable dtHigh = new System.Data.DataTable();
                                                    // EastLawBL.Cases objc = new EastLawBL.Cases();
                                                    dtLatest = objc.GetCasesByCourt("high court");
                                                    if (dtLatest != null)
                                                    {
                                                    for (int a = 0; a < dtLatest.Rows.Count; a++)
                                                    {
                                                    Response.Write("<li class='news-item'>"
                                                        + "<table cellpadding='4'><tr>"
                                                                + "<td><b>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 2) + "... VS " + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 2) + "...</b><br />"
                                                                    + "<b>Date:</b> " + dtLatest.Rows[a]["FormatedJdate"].ToString() + "<br /><b>Court:</b> " + dtLatest.Rows[a]["Court"].ToString()
                                                                    //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                                                                    + "   <a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["ID"].ToString())+"'>Read more...</a></td></tr></table></li>");
                                                    if (a == 7)
                                                    break;
                                                    }
                                                    }

                                                    %>

                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer"><a href="/cases/high-court">View More</a></div>
                                </div>

                            </div>

                        </div>
                    </div>
                    <div class="panel-footer"> </div>
                </div>

            </div>

            <div class="col-lg-4" style="display:none">

                <div class="panel panel-default">
                    <div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Notifications</b></div>
                    <div class="panel-body" style="padding-bottom:0;">
                        <div class="row">

                            <div class="col-lg-12" style="padding:0;">

                                <div class="panel panel-default">
                                    <div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Lastest</b></div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-12" style="padding:0;">
                                                <ul class="demo1">
                                                    <li class="news-item">
                                                        <table cellpadding="4">
                                                            <tr>

                                                                <td>
                                                                    <b>Shahid Parveen VS Muhammad Nawaz</b><br />


                                                                    <b>Date:</b> July 26, 2017<br />
                                                                    Lorem ipsum dolor sit amet, consectetur adipiscing
                                                                    <a href="#">Read more...</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </li>
                                                    <li class="news-item">
                                                        <table cellpadding="4">
                                                            <tr>
                                                                <td>
                                                                    <b>John Cena VS Triple H</b><br />


                                                                    <b>Date:</b> June 19, 2017<br />
                                                                    Lorem ipsum dolor sit amet, consectetur adipiscing
                                                                    <a href="#">Read more...</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </li>
                                                    <li class="news-item">
                                                        <table cellpadding="4">
                                                            <tr>
                                                                <td>
                                                                    <b>Jeff Hardy VS CM Punk</b><br />


                                                                    <b>Date:</b> August 11, 2017<br />
                                                                    Lorem ipsum dolor sit amet, consectetur adipiscing
                                                                    <a href="#">Read more...</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </li>
                                                    <li class="news-item">
                                                        <table cellpadding="4">
                                                            <tr>
                                                                <td>
                                                                    <b>Undertaker VS Edge</b><br />


                                                                    <b>Date:</b> July 26, 2017<br />
                                                                    Lorem ipsum dolor sit amet, consectetur adipiscing
                                                                    <a href="#">Read more...</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </li>
                                                    <li class="news-item">
                                                        <table cellpadding="4">
                                                            <tr>
                                                                <td>
                                                                    <b>Brock Lesnar VS The Rock</b><br />


                                                                    <b>Date:</b> Oct 26, 2017<br />
                                                                    Lorem ipsum dolor sit amet, consectetur adipiscing
                                                                    <a href="#">Read more...</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </li>
                                                    <li class="news-item">
                                                        <table cellpadding="4">
                                                            <tr>
                                                                <td>
                                                                    <b>Ronnie VS Selby</b><br />


                                                                    <b>Date:</b> July 26, 2017<br />
                                                                    Lorem ipsum dolor sit amet, consectetur adipiscing
                                                                    <a href="#">Read more...</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </li>
                                                    <li class="news-item">
                                                        <table cellpadding="4">
                                                            <tr>
                                                                <td>
                                                                    <b>Kane VS Nasir Khan Jaan</b><br />


                                                                    <b>Date:</b> Jan 10, 2017<br />
                                                                    Lorem ipsum dolor sit amet, consectetur adipiscing
                                                                    <a href="#">Read more...</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer"> </div>
                                </div>

                            </div>

                            <!--panel 1 end -->



                        </div>
                    </div>
                    <div class="panel-footer"> </div>
                </div>

            </div>

        </div>

    </div>
    
    <!--Judgements End-->
    
    
    <!-- Supreme Court -->
    
    
    	<div class="container" style="display:block">
        
        	<div class="row">
            <%
                EastLawBL.Statutes objstate = new EastLawBL.Statutes();
                string cri = "";
                System.Data.DataTable dtUpdatePanel = new System.Data.DataTable();
                cri = cri + " AND A.Pri_Sec='PRIMARY' and A.GroupID=1";

                dtUpdatePanel = objstate.GetLatestStatutesSearch(cri);
                                                        
                                                            if (dtUpdatePanel != null)
                                                            {
                                                                if (dtUpdatePanel.Rows.Count > 0)
                                                                {
%>
        <div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>National Assembly</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1">
<%
                                                                  
                                                                        for (int a = 0; a < dtUpdatePanel.Rows.Count; a++)
                                                                        {
                                                                            Response.Write("<li class='news-item'>"
                                                                               + "<table cellpadding='4' width=310px><tr>"
                                                                                + "<td><b>" + dtUpdatePanel.Rows[a]["Title"].ToString().Replace("_", " ") + " ...</b><br />"
                                                                                
                                                                                + "<b>Date:</b> " + dtUpdatePanel.Rows[a]["Date"].ToString() + "<br /><b>Category:</b> " + dtUpdatePanel.Rows[a]["PracticeArea"].ToString() + "/" + dtUpdatePanel.Rows[a]["CatName"].ToString()
                                                                                //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                                                                                + "  <br><span style='float:right'> <a href='/statutes/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtUpdatePanel.Rows[a]["Title"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtUpdatePanel.Rows[a]["ID"].ToString()) + "'>Read more...</a></span></td></tr></table></li>");
                                                                            if (a == 15)
                                                                                break;
                                                                        }
                                                                   // }
                                                            
                                                        %>
</ul>
</div>
</div>
</div>
<div class="panel-footer"> 
    <%
                                                                   
                                                                    Response.Write("<a href='/statutes/provincial-legislation'>View More</a>");
                                                                    
         %>
</div>
</div>
        
        </div>
            <%}
                                                            } %>

        
        	
        
        
        
           <%
                
                
               
                cri = "  AND A.Pri_Sec='PRIMARY' and A.GroupID=2";

                dtUpdatePanel = objstate.GetLatestStatutesSearch(cri);
                                                        
                                                            if (dtUpdatePanel != null)
                                                            {
                                                                if (dtUpdatePanel.Rows.Count > 0)
                                                                {
%>
        <div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>National Assembly</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1">
<%
                                                                  
                                                                        for (int a = 0; a < dtUpdatePanel.Rows.Count; a++)
                                                                        {
                                                                            Response.Write("<li class='news-item'>"
                                                                               + "<table cellpadding='4' width=310px><tr>"
                                                                                + "<td><b>" + dtUpdatePanel.Rows[a]["Title"].ToString().Replace("_", " ") + " ...</b><br />"
                                                                                
                                                                                + "<b>Date:</b> " + dtUpdatePanel.Rows[a]["Date"].ToString() + "<br /><b>Category:</b> " + dtUpdatePanel.Rows[a]["PracticeArea"].ToString() + "/" + dtUpdatePanel.Rows[a]["CatName"].ToString()
                                                                                //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                                                                                + "  <br><span style='float:right'> <a href='/statutes/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtUpdatePanel.Rows[a]["Title"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtUpdatePanel.Rows[a]["ID"].ToString()) + "'>Read more...</a></span></td></tr></table></li>");
                                                                            if (a == 15)
                                                                                break;
                                                                        }
                                                                   // }
                                                            
                                                        %>
</ul>
</div>
</div>
</div>
<div class="panel-footer"> 
    <%
                                                                   
                                                                    Response.Write("<a href='/statutes/provincial-legislation'>View More</a>");
                                                                    
         %>
</div>
</div>
        
        </div>
            <%}
                                                            } %>
        
        
        
        
        
         <%
             EastLawBL.Departments objd = new EastLawBL.Departments();
                                                                dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(0, "Circular");
                                                            if (dtUpdatePanel != null)
                                                            {
                                                                if (dtUpdatePanel.Rows.Count > 0)
                                                                {
%>
        <div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Circular</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1">
<%
                                                                    
                                                                        for (int a = 0; a < dtUpdatePanel.Rows.Count; a++)
                                                                        {
                                                                            Response.Write("<li class='news-item'>"
                                                                               + "<table cellpadding='4' width=310px><tr>"
                                                                                + "<td><b>" + dtUpdatePanel.Rows[a]["Title"].ToString().Replace("_", " ") + " ...</b><br />"
                                                                                + "<b>Date:</b> " + dtUpdatePanel.Rows[a]["DDate"].ToString() + "<br />"
                                                                                + "<b>Department:</b> " + dtUpdatePanel.Rows[a]["ParentDeptName"].ToString() + "<br />"
                                                                                //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                                                                                + "  <br><span style='float:right'>  <a href='/departments/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtUpdatePanel.Rows[a]["DeptName"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtUpdatePanel.Rows[a]["Title"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtUpdatePanel.Rows[a]["ID"].ToString()) + "'>Read more...</a></span></td></tr></table></li>");
                                                                            if (a == 15)
                                                                                break;
                                                                        }
                                                                   // }
                                                            
                                                        %>
</ul>
</div>
</div>
</div>
<div class="panel-footer"> 
    <%
                                                                    if (Request.QueryString["dptkp"] != null)
                                                                        Response.Write("<a href='/departments/latest-circular/?param=" + Request.QueryString["dptkp"].ToString() + "&trans=" + Request.QueryString["dptn"].ToString() + "'>View More</a>");
                                                                    else
                                                                        Response.Write("<a href='/departments/latest-circular/'>View More</a>");
                                                                    
         %>
</div>
</div>
        
        </div>
            <%}
                                                            } %>
        
        
        		
        	
            </div>
        
        </div>
    
    
    <!-- Supreme Court End -->
    
        
        
        
       
        <div class="container">
        
        	<div class="row newsletter">
            
            	<div class="col-lg-1 col-md-1"></div>
        	
            	<div class="col-lg-4 col-md-4 heading">
            	
                	<h2>get the newsletter</h2>
                
                </div>
                
                <div class="col-lg-6 col-md-5 subcribe">
            	
                	<div>
                        <input class="form-control text_style2" placeholder="Search" name="srch-term" id="srch-term" type="text">
                        <div class="input-group-btn">
                            <button class="btn btn-default btn-style" type="submit">Subscribe	</button>
                        </div>
                
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
                        	<span class="span_style2">04237311670 / 04237311671</span>			
    						</div>
                            
                            <div class="margin-top3">
                        	<span class="span_style3"><i class="fa fa-whatsapp icon_style2"></i>Whatsapp: </span><span class="font">0310-8131610</span>
    
    	<span class="span_style4"> (Monday to Friday - 10:00am – 6:00pm )</span>
    						
                            </div>
    						
                        </div>
                    
                    </div>
                
                </div>
                
                
                
                <div class="col-lg-4 col-md-4">
                
                	<div class="cover">
                    
                    	<div class="footer_parant">
                        	<i class="fa fa-envelope fa-3x"></i><h2>Send Us Email</h2>
                        </div>
                        
                        <div class="footer_inn">
                        
                        	<div>
                        	<span class="font">info@eastlaw.pk</span>			
    						</div>
                            
                            
                        </div>
                    
                    </div>
                
                </div>
                
                
                
                <div class="col-lg-4 col-md-4">
                
                	<div class="cover border_none">
                    
                    	<div class="footer_parant">
                        	<i class="fa fa-address-book fa-3x"></i><h2>Address</h2>
                        </div>
                        
                        <div class="footer_inn">
                        
                        	<div>
                        <span class="font">39 Link Farid Kot Road.
    <br />Lahore , Pakistan.</span>			
    						</div>
                            
                            
                        </div>
                    
                    </div>
                
                </div>
                
                
                
            
            </div>
        
        </div>
        
        </form>

    
<a href="#0" class="cd-top">Top</a>
<script type="text/javascript" src="/style/js/jquery.js"></script>
<script type="text/javascript" src="/style/engine1/slider.js"></script>
<script type="text/javascript" src="/style/engine1/script.js"></script>
<script type="text/javascript" src="/style/js/bootstrap.min.js"></script>
<script type="text/javascript" src="/style/js/main.js"></script>
<script type="text/javascript" src="/style/js/jquery.bootstrap.newsbox.min.js"></script>
<script type="text/javascript" src="/style/js/breadcrums.js"></script>
     <script type="text/javascript">
         $(window).load(function() {
             $(".loader").fadeOut("slow");
         })
</script>
<script type="text/javascript">
    $(function () {
        $(".demo1").bootstrapNews({
            newsPerPage: 3,
            autoplay: true,
            pauseOnHover:true,
            direction: 'up',
            newsTickerInterval: 4000,
            onToDo: function () {
                //console.log(this);
            }
        });
		
        $(".demo2").bootstrapNews({
            newsPerPage: 4,
            autoplay: true,
            pauseOnHover: true,
            navigation: false,
            direction: 'down',
            newsTickerInterval: 2500,
            onToDo: function () {
                //console.log(this);
            }
        });

        $("#demo3").bootstrapNews({
            newsPerPage: 3,
            autoplay: false,
            
            onToDo: function () {
                //console.log(this);
            }
        });
    });
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
    $(document).ready(function(){
    
        var clickEvent = false;
        $('#myCarousel').carousel({
            interval:   4000	
        }).on('click', '.list-group li', function() {
            clickEvent = true;
            $('.list-group li').removeClass('active');
            $(this).addClass('active');		
        }).on('slid.bs.carousel', function(e) {
            if(!clickEvent) {
                var count = $('.list-group').children().length -1;
                var current = $('.list-group li.active');
                current.removeClass('active').next().addClass('active');
                var id = parseInt(current.data('slide-to'));
                if(count == id) {
                    $('.list-group li').first().addClass('active');	
                }
            }
            clickEvent = false;
        });
    })

    $(window).load(function() {
        var boxheight = $('#myCarousel .carousel-inner').innerHeight();
        var itemlength = $('#myCarousel .item').length;
        var triggerheight = Math.round(boxheight/itemlength+1);
        $('#myCarousel .list-group-item').outerHeight(triggerheight);
    });


</script>


</body>
</html>