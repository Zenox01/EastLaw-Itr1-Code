<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsLive.aspx.cs" Inherits="EastlawUI_v2.NewsLive" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>EastLaw.pk – Pakistan's Largest Online Law Library</title>
    <link href="style/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="style/css/style.css" rel="stylesheet" type="text/css" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
        type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="style/engine1/slider.css" />
    <link href="style/css/icons.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="img/fav.png">
    <link href="style/eNews/css/enewslive.css" rel="stylesheet" />

    <link href="style/css/news-slider.css" rel="stylesheet" type="text/css" />
    <!--<link href="css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />-->
    <link href="style/css/site.css" rel="stylesheet" type="text/css" />
    <link href="style/css/breadcrums.css" rel="stylesheet" type="text/css" />
    <link href="style/css/back-top.css" rel="stylesheet" type="text/css" />
    <link href="style/css/enewslive.css" rel="stylesheet" type="text/css" />
    <link href="style/css/responsive.css" rel="stylesheet" type="text/css" />
    <style>
        #myCarousel .list-group .active {
            background-color: #da2128 !important;
            color: #fff !important;
        }
    </style>
</head>
<body data-spy="scroll" data-target=".navbar" data-offset="30">
    <form id="form1" runat="server">
        <header>



            <!---------- Top Row End ---------->


        <%--    <div class="e-blog-header">
                <div class="container">
                    <a class="" href="#">
                        <img src="/style/img/logo2.jpg" alt="Eastlaw Logo" style="width: 108px;" /></a>
                </div>
            </div>--%>

            <nav class="navbar navbar-default hidden">
                <div class="container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>

                    </div>
                    <div id="navbar33" class="navbar-collapse collapse">
                        <ul class="nav navbar-nav">
                            <li><a href="index.html">Home</a></li>
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">News <span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="#">Income Tax</a></li>
                                    <li><a href="#">Sale Tax</a></li>
                                    <li><a href="#">Customs</a></li>
                                    <li><a href="#">Federal Excise</a></li>
                                    <li><a href="#">SECP</a></li>
                                    <li><a href="#">PRA</a></li>
                                    <li><a href="#">SRB</a></li>
                                </ul>
                            </li>
                            <li><a href="#about">Expert Corners</a></li>
                            <li><a href="#contact">Case Briefs</a></li>
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Legislation Updates <span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="#">Top Story</a></li>
                                    <li><a href="#">News</a></li>
                                    <li><a href="#">Legislation Updates</a></li>
                                    <li><a href="#">Notifications</a></li>
                                    <li><a href="#">National Assembly</a></li>
                                    <li><a href="#">Provincial Assemblies</a></li>
                                </ul>
                            </li>
                            <li><a href="#">Case Reported</a></li>
                            <li><a href="#">Law School News</a></li>
                            <li><a href="#">Interviews</a></li>
                            <li><a href="#">Subscribe</a></li>
                        </ul>

                    </div>
                    <!--/.nav-collapse -->
                </div>
            </nav>

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
        </header>

        <!----------- Main Nav End ------------->
        <div class="container">
            <div class="row breadcrum">

                <ul class="bc">
                    <li><a href="afterlogin.html" class="first">Home</a></li>

                    <li><a href="#" class="current">E-News live</a></li>
                </ul>
            </div>
        </div>

        <!--News Nav -->


        <!-------------- Content --------------->

        <div class="container">
            <div class="row">
                <div id="myCarousel" class="carousel slide margin_bot_20" data-ride="carousel">
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
                    </div>
                    <!-- End Carousel Inner -->
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

                </div>
                <!-- End Carousel -->


            </div>
        </div>

        <!-------------- Content End --------------->


        <!--cases-->

        <div class="container">
            <div class="row-org">

                <%
                    System.Data.DataTable dtLatest = new System.Data.DataTable();
                    EastLawBL.Cases objc = new EastLawBL.Cases();
                    dtLatest = objc.GetCasesByCourtFront("Supreme Court");
                    if (dtLatest != null)
                    {
                        for (int a = 0; a < dtLatest.Rows.Count; a++)
                        {
                            string imgs="";
                            if (dtLatest.Rows[a]["Court"].ToString() == "Supreme Court of Pakistan")
                                imgs = "/images/court_imgs/SCP.jpg";
                                 
                            Response.Write("<div class='col-lg-3 col-md-3' style='height:400px'>"
                                + "<div class='case-brief'>"
                                + "<img src='"+imgs+"'>"
                                + "<h5><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "'>" + dtLatest.Rows[a]["Court"].ToString() + "</a></h5>"
                                       // + "<td><b>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 2) + "... VS " + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 2) + "...</b><br />"
                                       + "<h4><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "'>"+EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString()).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-")+"</a></h4>"
                        + "<p>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(),5) + "</p>"
                            //+ "<b>Date:</b> " + dtLatest.Rows[a]["FormatedJdate"].ToString() + "<br /><b>Court:</b> " + dtLatest.Rows[a]["Court"].ToString()
                            //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                            // + "   <a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["ID"].ToString()) + "'>Read more...</a></td></tr></table></li>"
                            + "<h6><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "'>Continue Reading <i class='fa fa-long-arrow-right'></i></a></h6></div></div>");
                            if (a == 3)
                                break;
                        }
                    }

                %>

                <%--<div class="col-lg-3 col-md-3">
                    <div class="case-brief">
                        <img src="https://i0.wp.com/blog.scconline.com/wp-content/uploads/2016/04/GujHC.jpg?resize=440%2C293">
                        <h5><a href="#">Case Briefs</a></h5>
                        <h4><a href="#">Gujarat HC issues notice to Union & State Govt over ...</a></h4>
                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries,</p>
                        <h6><a href="#">Continue Reading <i class="fa fa-long-arrow-right"></i></a></h6>
                    </div>
                </div>--%>
            </div>
            <div class="row-org">

                <%
                    
                    dtLatest = objc.GetCasesByCourtFront("Lahore High Court");
                    if (dtLatest != null)
                    {
                        for (int a = 0; a < dtLatest.Rows.Count; a++)
                        {
                            string imgs="";
                            if (dtLatest.Rows[a]["Court"].ToString() == "Lahore High Court")
                                imgs = "/images/court_imgs/LHC.jpg";
                                 
                            Response.Write("<div class='col-lg-3 col-md-3' style='height:400px'>"
                                + "<div class='case-brief'>"
                                + "<img src='"+imgs+"'>"
                                + "<h5><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "'>" + dtLatest.Rows[a]["Court"].ToString() + "</a></h5>"
                                       // + "<td><b>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 2) + "... VS " + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 2) + "...</b><br />"
                                       + "<h4><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "'>" + EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString()).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "</a></h4>"
                        + "<p>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(),5) + "</p>"
                            //+ "<b>Date:</b> " + dtLatest.Rows[a]["FormatedJdate"].ToString() + "<br /><b>Court:</b> " + dtLatest.Rows[a]["Court"].ToString()
                            //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                            // + "   <a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["ID"].ToString()) + "'>Read more...</a></td></tr></table></li>"
                            + "<h6><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "'>Continue Reading <i class='fa fa-long-arrow-right'></i></a></h6></div></div>");
                            if (a == 3)
                                break;
                        }
                    }

                %>

                <%--<div class="col-lg-3 col-md-3">
                    <div class="case-brief">
                        <img src="https://i0.wp.com/blog.scconline.com/wp-content/uploads/2016/04/GujHC.jpg?resize=440%2C293">
                        <h5><a href="#">Case Briefs</a></h5>
                        <h4><a href="#">Gujarat HC issues notice to Union & State Govt over ...</a></h4>
                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries,</p>
                        <h6><a href="#">Continue Reading <i class="fa fa-long-arrow-right"></i></a></h6>
                    </div>
                </div>--%>
            </div>
            <div class="row-org">

                <%
                    
                    dtLatest = objc.GetCasesByCourtFront("Sindh High Court");
                    if (dtLatest != null)
                    {
                        for (int a = 0; a < dtLatest.Rows.Count; a++)
                        {
                            string imgs="";
                            if (dtLatest.Rows[a]["Court"].ToString() == "Sindh High Court")
                                imgs = "/images/court_imgs/SHC.jpg";
                                 
                            Response.Write("<div class='col-lg-3 col-md-3' style='height:400px'>"
                                + "<div class='case-brief'>"
                                + "<img src='"+imgs+"'>"
                                + "<h5><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "'>" + dtLatest.Rows[a]["Court"].ToString() + "</a></h5>"
                                       // + "<td><b>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 2) + "... VS " + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 2) + "...</b><br />"
                                       + "<h4><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "'>" + EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString()).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "</a></h4>"
                        + "<p>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(),5) + "</p>"
                            //+ "<b>Date:</b> " + dtLatest.Rows[a]["FormatedJdate"].ToString() + "<br /><b>Court:</b> " + dtLatest.Rows[a]["Court"].ToString()
                            //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                            // + "   <a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["ID"].ToString()) + "'>Read more...</a></td></tr></table></li>"
                            + "<h6><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "'>Continue Reading <i class='fa fa-long-arrow-right'></i></a></h6></div></div>");
                            if (a == 3)
                                break;
                        }
                    }

                %>

                <%--<div class="col-lg-3 col-md-3">
                    <div class="case-brief">
                        <img src="https://i0.wp.com/blog.scconline.com/wp-content/uploads/2016/04/GujHC.jpg?resize=440%2C293">
                        <h5><a href="#">Case Briefs</a></h5>
                        <h4><a href="#">Gujarat HC issues notice to Union & State Govt over ...</a></h4>
                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries,</p>
                        <h6><a href="#">Continue Reading <i class="fa fa-long-arrow-right"></i></a></h6>
                    </div>
                </div>--%>
            </div>
        </div>

        <!--cases End-->
        <div class="container">
            <div class="row-org">
                <div class="col-lg-8 col-md-7">
                    <div class="row-org">

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
                                        Response.Write("<div class='col-lg-6 col-md-3' style=><div class='case-brief set-img-e'>"
                                         // + "<a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'><img src='" + dt.Rows[a]["imgfile"].ToString() + "' alt='" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString()) + "' /></a>"
                                         + "<img src='" + imgfilename + "' alt='" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString()) + "' />"
                                         + "<h5><a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'>" + EastlawUI_v2.CommonClass.GetChracter(dt.Rows[a]["Title"].ToString(), 30) + "...</a> </h5>"
                                         + "<h4><a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'>" + EastlawUI_v2.CommonClass.GetWords(EastlawUI_v2.CommonClass.RemoveHTML(dt.Rows[a]["FullContent"].ToString()), 10) + "...</a> </h4>"                                         
                                      + "</div></div>");
                                    }
                                    if (a == 5)
                                        break;

                                }
                            }
                            catch { }
                        %>



                        <div class="col-lg-6 col-md-3 hidden">
                            <div class="case-brief set-img-e">
                                <img src="https://i1.wp.com/blog.scconline.com/wp-content/uploads/2016/03/KeralaHC-e1521442636157.jpg?resize=440%2C293">
                                <h5><a href="#">Case Briefs</a></h5>
                                <h4><a href="#">Gujarat HC issues notice to Union & State Govt over ...</a></h4>
                                <h3>- By <a href="#">John Doe</a></h3>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4 col-md-5">
                    <div class="e-sidebar">
                        <%--<div class="e-side-box">
                            <div class="e-head">
                                <h4>Newsletter Signup</h4>
                            </div>
                            <div class="newsletter-field">
                                <input type="email" class="form-control" placeholder="Email Address" />
                                <input type="button" value="Subscribe" />
                            </div>
                        </div>--%>
                        <div class="e-side-box">
                            <div class="e-head">
                                <h4>Latest Cases on Eastlaw</h4>
                            </div>
                            <div class="row pop-blogs">

                                <%
                    
                                    dtLatest = objc.GetLatestCasesFront();
                                    if (dtLatest != null)
                                    {
                                        for (int a = 0; a < dtLatest.Rows.Count; a++)
                                        {
                                            string imgs = "";
                                            if (dtLatest.Rows[a]["Court"].ToString() == "Sindh High Court")
                                                imgs = "/images/court_imgs/SHC.jpg";
                                            else if (dtLatest.Rows[a]["Court"].ToString() == "Lahore High Court")
                                                imgs = "/images/court_imgs/LHC.jpg";
                                            else if (dtLatest.Rows[a]["Court"].ToString() == "Supreme Court of Pakistan")
                                                imgs = "/images/court_imgs/SCP.jpg";
                                            

                                        //    Response.Write("<div class='col-lg-3 col-md-3' style='height:400px'>"
                                        //        + "<div class='case-brief'>"
                                        //        + "<img src='" + imgs + "'>"
                                        //        + "<h5><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["ID"].ToString()) + "'>" + dtLatest.Rows[a]["Court"].ToString() + "</a></h5>"
                                        //        // + "<td><b>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 2) + "... VS " + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 2) + "...</b><br />"
                                        //               + "<h4><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["ID"].ToString()) + "'>" + EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString()).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "</a></h4>"
                                        //+ "<p>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 5) + "</p>"
                                        //        //+ "<b>Date:</b> " + dtLatest.Rows[a]["FormatedJdate"].ToString() + "<br /><b>Court:</b> " + dtLatest.Rows[a]["Court"].ToString()
                                        //        //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                                        //        // + "   <a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["ID"].ToString()) + "'>Read more...</a></td></tr></table></li>"
                                        //    + "<h6><a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["ID"].ToString()) + "'>Continue Reading <i class='fa fa-long-arrow-right'></i></a></h6></div></div>");

                                            Response.Write("<div class='pop-blog-list'>"
                                   + "<div class='col-lg-3 col-md-3 col-sm-4 col-xs-4' style='padding: 0;'>");
                                   if(!string.IsNullOrEmpty(imgs))
                                        Response.Write("<img src='" + imgs + "'>");
                                            
                                    Response.Write("</div>"
                                    + "<div class='col-lg-9 col-md-9 col-sm-8 col-xs-8' style='padding-right: 0;'>"
                                        + "<h4>" + EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString()).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "</h4>"
                                        + "<a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "' class='blog-read-more'>Continue Reading <i class='fa fa-long-arrow-right'></i></a>"
                                    + "</div></div>");
                                            
                                            if (a == 9)
                                                break;
                                        }
                                    }
                    
                    %>
                              
                            </div>
                        </div>
                        <%--<div class="e-side-box">
                            <div class="e-head">
                                <h4>Related Blogs</h4>
                            </div>
                            <div class="row pop-blogs">
                                <div class="pop-blog-list">
                                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding: 0;">
                                        <img src="https://i1.wp.com/blog.scconline.com/wp-content/uploads/2017/02/IMG_3499-e1487871967209.jpg?resize=440%2C293">
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-right: 0;">
                                        <h4>Gujarat HC issues notice to Union & State Govt over ...</h4>
                                        <a href="#" class="blog-read-more">Continue Reading <i class="fa fa-long-arrow-right"></i></a>
                                    </div>
                                </div>
                                <div class="pop-blog-list">
                                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding: 0;">
                                        <img src="https://i1.wp.com/blog.scconline.com/wp-content/uploads/2017/02/IMG_3499-e1487871967209.jpg?resize=440%2C293">
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-right: 0;">
                                        <h4>Gujarat HC issues notice to Union & State Govt over ...</h4>
                                        <a href="#" class="blog-read-more">Continue Reading <i class="fa fa-long-arrow-right"></i></a>
                                    </div>
                                </div>
                                <div class="pop-blog-list">
                                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding: 0;">
                                        <img src="https://i1.wp.com/blog.scconline.com/wp-content/uploads/2017/02/IMG_3499-e1487871967209.jpg?resize=440%2C293">
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-right: 0;">
                                        <h4>Gujarat HC issues notice to Union & State Govt over ...</h4>
                                        <a href="#" class="blog-read-more">Continue Reading <i class="fa fa-long-arrow-right"></i></a>
                                    </div>
                                </div>
                                <div class="pop-blog-list">
                                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding: 0;">
                                        <img src="https://i1.wp.com/blog.scconline.com/wp-content/uploads/2017/02/IMG_3499-e1487871967209.jpg?resize=440%2C293">
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-right: 0;">
                                        <h4>Gujarat HC issues notice to Union & State Govt over ...</h4>
                                        <a href="#" class="blog-read-more">Continue Reading <i class="fa fa-long-arrow-right"></i></a>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>

            </div>
        </div>

        <div class="row blog-footer">
            <div class="container">
                
                <span>&copy; <% Response.Write(DateTime.Now.Year.ToString()); %>  Eastlaw.pk. All Rights Reserved.</span>
            </div>
        </div>


        <div class="buttons">

            <a href="#" class="bgcolor" data-placement="left" data-toggle="tooltip" title="User Manual">

                <i class="fa fa-book"></i>


            </a>

            <a href="#" class="bgcolor2" data-toggle="tooltip" title="Free Trial!" data-placement="left">

                <i class="fa fa-at"></i>


            </a>

            <a href="#" class="bgcolor3" data-toggle="tooltip" title="Subscribe" data-placement="left">

                <i class="fa fa-shopping-cart"></i>


            </a>


        </div>

        <a href="#0" class="cd-top">Top</a>
    </form>




    <script type="text/javascript" src="style/js/jquery.js"></script>
    <script type="text/javascript">
        var $p = $('#fos h4');
        var divh = $('#fos').height();
        while ($p.outerHeight() > divh) {
            $p.text(function (index, text) {
                return text.replace(/\W*\s(\S)*$/, '...');
            });
        }
    </script>
    <script type="text/javascript" src="style/engine1/slider.js"></script>
    <script type="text/javascript" src="style/engine1/script.js"></script>
    <script type="text/javascript" src="style/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="style/js/main.js"></script>
    <script type="text/javascript" src="style/js/jquery.bootstrap.newsbox.min.js"></script>
    <script type="text/javascript" src="style/js/breadcrums.js"></script>
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
