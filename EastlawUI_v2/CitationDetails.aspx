<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CitationDetails.aspx.cs" Inherits="EastlawUI_v2.CitationDetails"
    MasterPageFile="~/MemberMaster.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cntPlaceHolder">
    <style type="text/css">
        #sticknotes ul, li {
            list-style: none;
        }

        #sticknotes ul {
            overflow: hidden;
            padding: 3em;
        }

            #sticknotes ul li a {
                text-decoration: none;
                color: #000;
                background: #ffc;
                display: block;
                height: 80px;
                width: 80px;
                padding: 1em;
                -moz-box-shadow: 5px 5px 7px rgba(33,33,33,1);
                -webkit-box-shadow: 5px 5px 7px rgba(33,33,33,.7);
                box-shadow: 5px 5px 7px rgba(33,33,33,.7);
                -moz-transition: -moz-transform .15s linear;
                -o-transition: -o-transform .15s linear;
                -webkit-transition: -webkit-transform .15s linear;
            }

            #sticknotes ul li {
                margin: 1em;
                float: left;
            }

                #sticknotes ul li h2 {
                    font-size: 140%;
                    font-weight: bold;
                    padding-bottom: 10px;
                }

                #sticknotes ul li p {
                    font-family: "Reenie Beanie",arial,sans-serif;
                    font-size: 12pt;
                }

                #sticknotes ul li a {
                    -webkit-transform: rotate(-6deg);
                    -o-transform: rotate(-6deg);
                    -moz-transform: rotate(-6deg);
                }

                #sticknotes ul li:nth-child(even) a {
                    -o-transform: rotate(4deg);
                    -webkit-transform: rotate(4deg);
                    -moz-transform: rotate(4deg);
                    position: relative;
                    top: 5px;
                    background: #cfc;
                }

                #sticknotes ul li:nth-child(3n) a {
                    -o-transform: rotate(-3deg);
                    -webkit-transform: rotate(-3deg);
                    -moz-transform: rotate(-3deg);
                    position: relative;
                    top: -5px;
                    background: #ccf;
                }

                #sticknotes ul li:nth-child(5n) a {
                    -o-transform: rotate(5deg);
                    -webkit-transform: rotate(5deg);
                    -moz-transform: rotate(5deg);
                    position: relative;
                    top: -10px;
                }

                #sticknotes ul li a:hover, ul li a:focus {
                    box-shadow: 10px 10px 7px rgba(0,0,0,.7);
                    -moz-box-shadow: 10px 10px 7px rgba(0,0,0,.7);
                    -webkit-box-shadow: 10px 10px 7px rgba(0,0,0,.7);
                    -webkit-transform: scale(1.25);
                    -moz-transform: scale(1.25);
                    -o-transform: scale(1.25);
                    position: relative;
                    z-index: 5;
                }

        ol {
            text-align: center;
        }

            ol li {
                display: inline;
                padding-right: 1em;
            }

                ol li a {
                    color: #fff;
                }
    </style>
    <style>
        /* Tooltip container */
        .tooltip {
            position: relative;
            display: inline-block;
            border-bottom: 1px dotted black; /* If you want dots under the hoverable text */
            font-size: 14px;
            opacity: 1;
            padding: 5px 7px;
            box-shadow: 0 0 5px #000;
            z-index: 99;
        }

            /* Tooltip text */
            .tooltip .tooltiptext {
                visibility: hidden;
                width: 120px;
                background-color: black;
                color: #fff;
                text-align: center;
                padding: 5px 0;
                border-radius: 6px;
                /* Position the tooltip text - see examples below! */
                position: absolute;
                z-index: 1;
            }

            /* Show the tooltip text when you mouse over the tooltip container */
            .tooltip:hover .tooltiptext {
                visibility: visible;
            }
    </style>

    <%-- <asp:UpdatePanel ID="upnl" runat="server" >
     <ContentTemplate>--%>


    <% 
        try
        {
            if (Session["MemberID"] != null)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                EastLawBL.Cases objcases = new EastLawBL.Cases();

                dt = objcases.GetCases(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                string citation = dt.Rows[0]["Citation"].ToString();


    %>
    <div class="container">
        <div class="row breadcrum">

            <ul class="bc">
                <li><a href="/member/member-dashboard" class="first">Home</a></li>

                <li><a href="#">Search Result</a></li>
                <li class="current"><%  Response.Write(citation.ToString()); %></li>
                <span style="float: right; display: none">

                    <telerik:RadButton RenderMode="Lightweight" ID="btnDownladPDF" runat="server" Text="Download PDF" OnClick="btnDownladPDF_Click">
                        <Icon PrimaryIconCssClass="rbDownload"></Icon>
                    </telerik:RadButton>
                    <%--  <telerik:RadButton RenderMode="Lightweight" ID="btnPrint" runat="server" Text="Print">
                                <Icon PrimaryIconCssClass="rbPrint"></Icon>
                            </telerik:RadButton>--%>
                   
                </span>
            </ul>
        </div>
    </div>

    <div class="container">
        <div class="row margin_top">

            <!-------------- Left Side --------------->

            <div class="col-lg-4 col-md-12">


                <div class="panel panel-default style">
                    <div class="panel-heading panel-heading2">Documents on EastLaw<i class="fa fa-file-text-o" aria-hidden="true" style="float: right; font-size: 19px;"></i></div>
                    <div class="panel-body my_panel">

                        <ul>

                            <% 
                                Response.Write(LinkedCasesLst());
                                //try
                                //{
                                //    if (Session["MemberID"] != null)
                                //    {

                                //////System.Data.DataTable dtLinkedCases = new System.Data.DataTable();
                                EastLawBL.Cases objcase1 = new EastLawBL.Cases();
                                //////dtLinkedCases = objcase1.GetLinkedCasesWithDetails(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                                //////if (dtLinkedCases != null)
                                //////{
                                //////    if (dtLinkedCases.Rows.Count > 0)
                                //////    { 



                                //////        //for (int a = 0; a < dtLinkedCases.Rows.Count; a++)
                                //////        //{
                                //////        //    if (!EastLawUI.CommonClass.ExactMatch(dt.Rows[0]["Judgment"].ToString().Replace("--", " "), dtLinkedCases.Rows[a]["word"].ToString()))
                                //////        //    {
                                //////        //        dtLinkedCases.Rows[a].Delete();
                                //////        //    }
                                //////        //}
                                //////        //dtLinkedCases.AcceptChanges();

                                //////       // Response.Write("<li><span style='font-style:normal;font-weight:bold;color:black'> Linked Cases</span><ul>");
                                //////        for (int a = 0; a < dtLinkedCases.Rows.Count; a++)
                                //////        {

                                //////            //Response.Write("<li><a target='_blank' href='/Cases/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.GetWords(dtLinkedCases.Rows[a]["Appeallant"].ToString(), 3).ToString()).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.GetWords(dtLinkedCases.Rows[a]["Respondent"].ToString(), 3).ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtLinkedCases.Rows[a]["ID"].ToString()) + "'>" + dtLinkedCases.Rows[a]["Word"].ToString() + "</a></li>");
                                //////            string link = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLinkedCases.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLinkedCases.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLinkedCases.Rows[a]["ID"].ToString());
                                //////            Response.Write("<li> <a href='"+link+"' target='_blank'>" + dtLinkedCases.Rows[a]["Citation"].ToString() + "</a></li>");

                                //////            //Response.Write("<li>" + dtLinkedCases.Rows[a]["Word"].ToString() + "</li>");
                                //////        }
                                //////       // Response.Write("</ul></li>");
                                //////    }
                                //////}

                                System.Data.DataTable dtLawLinkedCases = new System.Data.DataTable();
                                dtLawLinkedCases = objcase1.GetLinkedStatutes(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                                if (dtLawLinkedCases != null)
                                {
                                    if (dtLawLinkedCases.Rows.Count > 0)
                                    {
                                        dtLawLinkedCases.Columns.Add("link");
                                        for (int a = 0; a < dtLawLinkedCases.Rows.Count; a++)
                                        {
                                            dtLawLinkedCases.Rows[a]["link"] = "<a href='/statutes/" + dtLawLinkedCases.Rows[a]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtLawLinkedCases.Rows[a]["ID"].ToString()) + "' target='_blank'>" + EastlawUI_v2.CommonClass.MakeFirstCap(dtLawLinkedCases.Rows[a]["Title"].ToString()) + "</a>";
                                        }

                                        // Response.Write("<li><span style='font-style:normal;font-weight:bold;color:black'> Linked Laws</span><ul>");
                                        for (int a = 0; a < dtLawLinkedCases.Rows.Count; a++)
                                        {
                                            //Response.Write("<li><a target='_blank' href='/Cases/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.GetWords(dtLinkedCases.Rows[a]["Appeallant"].ToString(), 3).ToString()).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.GetWords(dtLinkedCases.Rows[a]["Respondent"].ToString(), 3).ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtLinkedCases.Rows[a]["ID"].ToString()) + "'>" + dtLinkedCases.Rows[a]["Word"].ToString() + "</a></li>");
                                            Response.Write("<li>" + dtLawLinkedCases.Rows[a]["link"].ToString() + "</li>");
                                            //Response.Write("<li>" + dtLinkedCases.Rows[a]["Word"].ToString() + "</li>");
                                        }
                                        // Response.Write("</ul></li>");
                                    }
                                }


                            %>
                            <%--<li><a href="#">Constitution Of Pakistan</a></li>
        <li><a href="#">The Sindh Sales Tax On Services Act, 2011</a></li>
        <li><a href="#">The Sindh Sales Tax Ordinance, 2000</a></li>
        <li><a href="#">Punjab Sales Tax Ordinance 2000</a></li>
        <li><a href="#">Sindh Sales Tax Ordinance, 2000</a></li>
        <li><a href="#">Federal Excise Act, 2005</a></li>
        <li><a href="#">Central Excises Act, 1944</a></li>
        <li><a href="#">Sales Tax Act, 1990</a></li>
        <li><a href="#">Provincial Act,2011</a></li>
        <li><a href="#">Government Of India Act, 1935</a></li>
        <li><a href="#">Central Provinces And Berar Sales Of Motor </a></li>
        <li><a href="#">Spirit And Lubricants Taxation Act, 1938</a></li>
        <li><a href="#">British North America Act, 1867</a></li>
        <li><a href="#">Finance Act, 1989</a></li>
        <li><a href="#">Finance Act, 1985</a></li>
        <li><a href="#">Income Tax Act, 1922</a></li>
        <li><a href="#">Finance Ordinance, 1969</a></li>
        <li><a href="#">Provincial Ordinance,2000</a></li>
        <li><a href="#">Sindh Sales Tax On Services Rules, 2011</a></li>
        <li><a href="#">Octroi Rules, 1964</a></li>
        <li><a href="#">Provisional Constitution Order, 1981</a></li>
        <li><a href="#">Legal Framework Order, 2002</a></li>--%>
                        </ul>
                    </div>

                </div>
                <div class="panel panel-default style">
                    <div class="panel-heading panel-heading2">Alternate Citations<i class="fa fa-file-text-o" aria-hidden="true" style="float: right; font-size: 19px;"></i></div>
                    <div class="panel-body my_panel">

                        <ul>
                            <% 
                                //try
                                //{
                                //    if (Session["MemberID"] != null)
                                //    {

                                System.Data.DataTable dtAlternateCitations = new System.Data.DataTable();
                                EastLawBL.Cases objcase2 = new EastLawBL.Cases();
                                dtAlternateCitations = objcase2.GetAlternateCitationByCaseID(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                                if (dtAlternateCitations != null)
                                {
                                    if (dtAlternateCitations.Rows.Count > 0)
                                    {

                                        for (int a = 0; a < dtAlternateCitations.Rows.Count; a++)
                                        {

                                            //Response.Write("<li><a target='_blank' href='/Cases/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.GetWords(dtLinkedCases.Rows[a]["Appeallant"].ToString(), 3).ToString()).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.GetWords(dtLinkedCases.Rows[a]["Respondent"].ToString(), 3).ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtLinkedCases.Rows[a]["ID"].ToString()) + "'>" + dtLinkedCases.Rows[a]["Word"].ToString() + "</a></li>");
                                            //string link = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLinkedCases.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLinkedCases.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLinkedCases.Rows[a]["ID"].ToString());
                                            //Response.Write("<li> <a href='"+link+"' target='_blank'>" + dtLinkedCases.Rows[a]["Citation"].ToString() + "</a></li>");
                                            Response.Write("<li> " + dtAlternateCitations.Rows[a]["Citation"].ToString() + "</li>");

                                            //Response.Write("<li>" + dtLinkedCases.Rows[a]["Word"].ToString() + "</li>");
                                        }
                                        // Response.Write("</ul></li>");
                                    }
                                }




                            %>
                        </ul>
                    </div>

                </div>
                <div id="sticknotes" style="display: none">
                    <ul>
                        <li>
                            <a href="#">
                                <p>Text Content #1 This is another comments ....</p>
                            </a>
                        </li>
                        <li>
                            <a href="#">

                                <p>Text Content #2</p>
                            </a>
                        </li>
                        <li>
                            <a href="#">

                                <p>Text Content #3</p>
                            </a>
                        </li>
                        <li>
                            <a href="#">

                                <p>Text Content #4</p>
                            </a>
                        </li>
                        <li>
                            <a href="#">

                                <p>Text Content #5</p>
                            </a>
                        </li>
                        <li>
                            <a href="#">

                                <p>Text Content #6</p>
                            </a>
                        </li>
                        <li>
                            <a href="#">

                                <p>Text Content #2</p>
                            </a>
                        </li>
                        <li>
                            <a href="#">

                                <p>Text Content #7</p>
                            </a>
                        </li>
                        <li>
                            <a href="#">

                                <p>Text Content #8</p>
                            </a>
                        </li>
                    </ul>
                </div>

            </div>
            <div style="position: fixed; padding-top: 200px">
            </div>

            <!-------------- Left Side End --------------->



            <!-------------- right Side --------------->

            <div class="col-lg-8 ccol-md-8">

                <div class="row">
                    <%

                                if (dt.Rows.Count > 0)
                                {
                                    Response.Write("<div class='row text-center details'><p><strong>" + dt.Rows[0]["Court"].ToString() + "</strong></p></div>");
                                    System.Data.DataTable dtJugesName = new System.Data.DataTable();
                                    dtJugesName = objcases.GetListofJudgesByCaseNew(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                                    if (dtJugesName != null && dtJugesName.Rows.Count > 0)
                                    {
                                        Response.Write("<div class='row text-center details'> Judge(s)");
                                        Response.Write("<p><strong>");
                                        for (int a = 0; a < dtJugesName.Rows.Count; a++)
                                        {
                                            Response.Write(dtJugesName.Rows[a]["JudgeName"].ToString() + ", ");
                                        }
                                        Response.Write("</strong></p>");
                                        Response.Write("</div>");
                                    }
                                    else
                                    {
                                        Response.Write("<div class='row text-center details'>Judge(s) <p><strong>" + dt.Rows[0]["JudgeName"].ToString() + "</strong></p></div>");
                                    }
                                    Response.Write("<div class='row text-center details footer_inn'><p><strong>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Appeallant"].ToString()) + "</strong></p><p style='color:#DA2128;'><strong>VS</strong></p><p><strong>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Respondent"].ToString()) + "</strong></p></div>");
                                    Response.Write("<div class='row text-center details footer_inn'><p><strong>" + dt.Rows[0]["Appeal"].ToString() + " <br />" + dt.Rows[0]["JDate"].ToString() + "<br /><span style='color:#DA2128;'>Reported As</span> [" + dt.Rows[0]["Citation"].ToString() + "] " + GetAlternateCitations(int.Parse(HttpContext.Current.Items["caseid"].ToString())) + "<br />Result: " + dt.Rows[0]["Result"].ToString() + "<br /></strong></p></div>");
                                    if (Session["MemberID"].ToString() == "3" || Session["MemberID"].ToString() == "65" || Session["MemberID"].ToString() == "1959" || Session["MemberID"].ToString() == "2369" || Session["MemberID"].ToString() == "2627" || Session["MemberID"].ToString() == "2881" || Session["MemberID"].ToString() == "3078")
                                    {
                                        Response.Write("<div><br /><span style='color:#DA2128;'>Counsel(s) for Appellant(s):</span> " + dt.Rows[0]["AdvA"].ToString() + "<br /><span style='color:#DA2128;'>Counsel(s) for Respondent(s):</span> " + dt.Rows[0]["AdvR"].ToString() + "<br /></strong></p></div>");
                                    }
                                    Response.Write("<div><br /><span style='color:#DA2128;'>Practice Area:</span> " + dt.Rows[0]["CasePracticeArea1"].ToString() + "<br /><span style='color:#DA2128;'>Tagged Statutes:</span> " + dt.Rows[0]["CaseTaggedStatutesNew"].ToString() + "<br /></strong></p></div>");
                                    //  if (dt.Rows[0]["CitedIn"].ToString() !="0")
                                    Response.Write("<div><br /><span style='color:#DA2128;'>Cited In:</span> " + dt.Rows[0]["CitedIn"].ToString() + "<span style='color:#DA2128;padding-left:52px'>Cited By:</span> " + dt.Rows[0]["CitedBy"].ToString() + "<br /></strong></p></div>");

                                    Response.Write("<div class='row margin-top-25'>");

                                    if (Session["MemberID"] != null)
                                    {
                                        if (!string.IsNullOrEmpty(dt.Rows[0]["CaseSummary"].ToString()))
                                        {
                                            Response.Write(" <div class='row'>");
                                            Response.Write("<button type='button' class='btn_show' data-toggle='collapse' data-target='#demo1' >Show Case Summary</button>");
                                            Response.Write("<div id='demo1' class='collapse' style='background-color: #f1e9e9;'>");
                                            Response.Write(EastlawUI_v2.CommonClass.AutoCloseHtmlTags(dt.Rows[0]["CaseSummary"].ToString()).ToString().Replace("</p>", "</p><br><br>----") + "</div></div>");
                                            //Response.Write(EastlawUI_v2.CommonClass.AutoCloseHtmlTags(dt.Rows[0]["CaseSummary"].ToString().ToString()) + "</div></div>");
                                        }
                                        if (Session["MemberID"].ToString() == "1959" || Session["MemberID"].ToString() == "3" || Session["MemberID"].ToString() == "26" || Session["MemberID"].ToString() == "2365" || Session["MemberID"].ToString() == "2369" || Session["MemberID"].ToString() == "2378" || Session["MemberID"].ToString() == "2627" || Session["MemberID"].ToString() == "2838"
                                            || Session["MemberID"].ToString() == "2825" || Session["MemberID"].ToString() == "2954" || Session["MemberID"].ToString() == "2955" || Session["MemberID"].ToString() == "2746" || Session["MemberID"].ToString() == "2520" || Session["MemberID"].ToString() == "2435" || Session["MemberID"].ToString() == "3078"
                                            || Session["MemberID"].ToString() == "65" || Session["MemberID"].ToString() == "2520" || Session["MemberID"].ToString() == "3128" || Session["MemberID"].ToString() == "3387" || Session["MemberID"].ToString() == "3891")
                                        {
                                            //Response.Write("<div class='row text-justify order'>" + EastlawUI_v2.CommonClass.AutoCloseHtmlTags(dt.Rows[0]["HeadNotes"].ToString()) + "<br><br>" + "</div>");
                                            Response.Write("<div class='row text-justify order'>" + dt.Rows[0]["HeadNotes"].ToString() + "<br><br>" + "</div>");

                                        }

                                        //if (Session["MemberID"].ToString() == "3" || Session["MemberID"].ToString() == "1959" || Session["MemberID"].ToString() == "26" || Session["MemberID"].ToString() == "2365" || Session["MemberID"].ToString() == "2369" || Session["MemberID"].ToString() == "2378" || Session["MemberID"].ToString() == "2729" || Session["MemberID"].ToString() == "2627" || Session["MemberID"].ToString() == "2838")
                                        //{
                                        //    Response.Write("<div class='row text-justify order'>"+EastlawUI_v2.CommonClass.AutoCloseHtmlTags(dt.Rows[0]["CaseSummary"].ToString()) + "<br><br>"+"</div>");

                                        //}
                                    }

                                    //   Response.Write("<p class='text-center'><strong>Order,</strong></p>");
                                    //Response.Write("<div class='row text-justify order'>"+ HighlightText(HighlightTextWithin(LinkedCases(LinkedStatutes(GetAnnotation(EastlawUI_v2.CommonClass.AutoCloseHtmlTags(FormatContent(dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("#TBS", "").Replace("#TBE", ""))))))))+"</div>");
                                    // Response.Write("<div class='row text-justify order'>" + FormatContent(GetAnnotation(HighlightText(HighlightTextWithin(LinkedCases(LinkedStatutes(EastlawUI_v2.CommonClass.AutoCloseHtmlTags(FormatContent(dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("#TBS", "").Replace("#TBE", ""))))))))) + "</div>");
                                    Response.Write("<div class='row text-justify order'>" + FormatContent(GetAnnotation(HighlightText(LinkedCases(LinkedStatutes(HighlightTextWithin(dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("#TBS", "").Replace("#TBE", ""))))))) + "</div>");
                                    Response.Write("</div>");
                                }
                            }
                        }
                        catch { }
                    %>
                </div>

            </div>

            <!-------------- right Side End --------------->




        </div>
    </div>
    <div class="buttons">

        <a href="#" id="tooltip" class="btn_bgcolor" data-toggle="modal" data-target="#myModal3" title="Save into Folder" data-placement="left">

            <i class="fa fa-folder-open-o"></i>


        </a>


        <%--  <a href="#" class="btn_bgcolor2"  data-toggle="tooltip" title="Create New Folder" data-placement="left">
                
                	<i class="fa fa-plus"></i>
                	
                    
                </a>--%>

        <a href="#" class="btn_bgcolor3" data-toggle="modal" data-target="#myModal1" id="btnComments" title="Add Comment" data-placement="left">

            <i class="fa fa-comment-o"></i>


        </a>

        <a href="#" class="btn_bgcolor4" data-toggle="modal" data-target="#myModal2" title="Report Error" data-placement="left">

            <i class="fa fa-exclamation-triangle"></i>


        </a>
        <asp:LinkButton ID="lnkDownload" runat="server" class="btn_bgcolor5" ToolTip="Download Judgement" data-toggle="tooltip" OnClick="lnkDownload_Click">
                    <i class="fa fa-download"></i>
        </asp:LinkButton>
        <%-- <a href="#" class="btn_bgcolor5" data-toggle="tooltip" title="Download Judgement" data-placement="left">
                
                	
                	
                    
                </a>--%>
        <br />
        <a id="copy1" class="btn btn-primary" data-toggle="modal" data-target="#myModal4">Ant</a>


    </div>



    <div id="myModal1" class="modal fade" role="dialog">

        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Send us your comments</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upnlSendComment" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <table style="width: 100%">
                                <tr id="tr1" runat="server">
                                    <%-- <td>Comment: </td>--%>
                                    <td>
                                        <asp:TextBox ID="txtUserComment" runat="server" TextMode="MultiLine" Width="300" Height="100" placeholder="Enter your comments"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserComment" ErrorMessage="Please Enter Your Comments" ForeColor="Red"
                                            ValidationGroup="AB"></asp:RequiredFieldValidator>

                                        <br />

                                        <asp:Button ID="btnAddComent" runat="server" Text="Add Comment" ValidationGroup="AB" CssClass="btn btn-danger btn_style" OnClick="btnAddComent_Click" />
                                        <asp:Label ID="lblCommentThanks" runat="server" ForeColor="Green"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="tr2" runat="server" style="display: none">

                                    <td>Thanks for your valuable comments, we will definatily review your comment.
                                    </td>

                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>

    </div>



    <div id="myModal2" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Report Error</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="uppPnlReportError" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr id="trErrorReportFields" runat="server">
                                    <%-- <td>Comment: </td>--%>
                                    <td>
                                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="300" Height="100" placeholder="Enter your comments"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment" ErrorMessage="Please enter your comments" ForeColor="Red" Display="Dynamic"
                                            ValidationGroup="AA"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:Button OnClick="btnFill_Click" ID="btnFill" runat="server" Text="Report Error" ValidationGroup="AA" CssClass="btn btn-danger btn_style" />
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Green"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trErrorReport" runat="server" style="display: none">

                                    <td>Thanks for your valuable comments, we will definatily review your comment.
                                    </td>

                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <div id="myModal3" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add To Folder</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanelFolder" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr id="tr3" runat="server">
                                    <%-- <td>Comment: </td>--%>
                                    <td>
                                        <telerik:RadTreeView RenderMode="Lightweight" runat="server" ID="tvDept"
                                            OnContextMenuItemClick="tvDept_ContextMenuItemClick" AllowNodeEditing="true" OnNodeEdit="tvDept_NodeEdit">
                                            <ContextMenus>
                                                <telerik:RadTreeViewContextMenu runat="server" ID="Caction" ClickToOpen="True">
                                                    <Items>
                                                        <telerik:RadMenuItem Text="Add New Folder" Value="NewFolder">
                                                        </telerik:RadMenuItem>
                                                        <%--<telerik:RadMenuItem Text="Edit / View" Value="Edit">
                    </telerik:RadMenuItem>--%>
                                                        <%--  <telerik:RadMenuItem Text="Delete Folder" Value="Delete">
                    </telerik:RadMenuItem>--%>
                                                    </Items>
                                                </telerik:RadTreeViewContextMenu>
                                            </ContextMenus>

                                        </telerik:RadTreeView>
                                        <br />
                                        <asp:Button OnClick="btnSaveInFolder_Click" ID="btnAddToFolder" runat="server" Text="Add To Selected Folder" CssClass="btn btn-danger btn_style" />
                                        <asp:Label ID="lblAddToFolder" runat="server" ForeColor="Green"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="tr4" runat="server" style="display: none">

                                    <td>Thanks for your valuable comments, we will definatily review your comment.
                                    </td>

                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
    <div id="myModal4" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add Annotation</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanelAnnotation" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr id="tr5" runat="server">
                                    <%-- <td>Comment: </td>--%>
                                    <td>
                                        <div id="newDiv1" runat="server"></div>
                                        <br />
                                        <asp:TextBox ID="txtText" runat="server" Width="300"></asp:TextBox>
                                        <asp:Button ID="btn" runat="server" Text="Add Annotation" CssClass="btn btn-danger btn_style" OnClick="btn_Click" />
                                        <asp:HiddenField ID="hd" runat="server" />
                                        <br />
                                        <asp:Label ID="lblAannotationAdded" runat="server" ForeColor="Green"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="tr6" runat="server" style="display: none">

                                    <td></td>

                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <%--    <asp:UpdatePanel ID="upNewUpdatePanel" UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server">
            <ContentTemplate>--%>
    <div id="dialog" style="display: none">
    </div>
    <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>

    <%--   <script src="/style/js/jquery.js"></script>
    <script src="/style/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="/style/js/bs3/js/bootstrap.min.js"></script>--%>

    <%--  <link href="/js/jquery-ui/jquery-ui-1.10.1.custom.min.css" rel="stylesheet" type="text/css" /> --%>
    <script>
        function CloseCommentBox() {
            $('#myModal1').modal('close');

        }
    </script>




    <%-- </ContentTemplate>
 </asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cntFooter">

    <script type="text/javascript">



        if (!window.Kolich) {
            Kolich = {};
        }

        Kolich.Selector = {};
        // getSelected() was borrowed from CodeToad at
        // http://www.codetoad.com/javascript_get_selected_text.asp
        Kolich.Selector.getSelected = function () {
            var t = '';
            if (window.getSelection) {
                t = window.getSelection();
            } else if (document.getSelection) {
                t = document.getSelection();
            } else if (document.selection) {
                t = document.selection.createRange().text;
            }
            return t;
        }

        Kolich.Selector.mouseup = function () {
            var st = Kolich.Selector.getSelected();
            if (st != '') {

                document.getElementById('cntPlaceHolder_newDiv1').innerHTML = st;
                // document.getElementById('txtText').value = st;

                var myHidden = document.getElementById('<%=hd.ClientID %>');
                if (myHidden)//checking whether it is found on DOM, but not necessary
                {

                    myHidden.value = st;
                }

                //$(function () {
                //    //  $("#dialog").html(st);
                //    $("#dialog").dialog({
                //        title: "Add Annotation",
                //        draggable: true,
                //        width: 600,
                //        height: 500,
                //        open: function (type, data) {
                //            $(this).parent().appendTo("form");
                //        },
                //        buttons: {
                //            Close: function () {
                //                $(this).dialog('close');
                //            }
                //        },

                //        modal: true,
                //        zIndex: 1000000
                //    });
                //});
                //  alert("You selected:\n" + st);


                // ShowPopup(st);
            }
        }

        $(document).ready(function () {
            //$(document).bind("mouseup", Kolich.Selector.mouseup);
            $("#copy1").bind("click", Kolich.Selector.mouseup);
        });

    </script>

</asp:Content>


