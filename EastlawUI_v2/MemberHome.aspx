<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberHome.aspx.cs" Inherits="EastlawUI_v2.MemberHome"
    MasterPageFile="~/MemberMaster.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cntPlaceHolder">
    <%--      <asp:UpdatePanel ID="upPnlTop" runat="server">
            <ContentTemplate>--%>
    <head>
        <!-- Tracking Code for https://eastlaw.pk -->


        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"
            type="text/javascript"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
            type="text/javascript"></script>
        <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
            rel="Stylesheet" type="text/css" />
        <style>
            .rrClipRegion {
                border: 0px !important;
            }

            .btn {
                background-color: #da2128;
                color: #fff;
            }

                .btn:hover {
                    background-color: #222;
                    color: #fff;
                }

            .padding_left_z {
                padding-left: 0;
            }



            .tab-content {
                height: auto;
            }

            @media (max-width:767px) {
                .padAllZeroRes {
                    padding: 0 !important;
                    margin-bottom: 10px;
                }

                .roww {
                    width: 93%;
                    margin: 26px 0;
                }
            }
        </style>

        <!-- Add IntroJs styles -->
        <link href="/style/css/introjs.css" rel="stylesheet">

        <script type="text/javascript">
            $(document).ready(function () {
                $("#<%=txtStatutesTitle.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("/Service.asmx/GetStatutesTitle") %>',
                            data: "{ 'prefix': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                //response($.map(data.d, function (item)
                                //{
                                //    return {
                                //        label: item.split('-')[0],
                                //        val: item.split('-')[1]
                                //    }
                                //}))

                                response($.map(data.d, function (item) {
                                    return {
                                        //label: __highlight(item, request.term),
                                        //label: item,// __highlight(item, request.term),
                                        //value: item

                                        label: __highlight(item, request.term),
                                        value: item
                                    };
                                }))
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    minLength: 1
                }).data("autocomplete")._renderItem = function (ul, item) {
                    // only change here was to replace .text() with .html()
                    return $("<li></li>")
                        .data("item.autocomplete", item)
                        .append($("<a></a>").html(item.label))
                        .appendTo(ul);
                };

            });


            function __highlight(s, t) {
                var matcher = new RegExp("(" + $.ui.autocomplete.escapeRegex(t) + ")", "ig");
                return s.replace(matcher, " <strong style='color:#eb1b33'>$1</strong> ");
            }

        </script>



        <style>
            .ui-autocomplete {
                max-height: 200px;
                overflow-y: auto;
                /* prevent horizontal scrollbar */
                overflow-x: hidden;
                border: 1px solid #222;
                position: absolute;
                width: 500px;
            }
        </style>
    </head>
    <div class="container">
        <div class="row">

            <!------------ Search Tabs Section ------------->


            <!-- Nav tabs -->
            <div class="card">

                <ul class="nav nav-tabs" role="tablist">
                    <li role="presentation" class="active" data-step="1" data-intro="Main General Case Search Area"><a href="#main_search" aria-controls="main_search" role="tab" data-toggle="tab">Main Search Area</a></li>
                    <li role="presentation" data-step="2" data-intro="Legislation Search Area"><a href="#find_legislation" aria-controls="find_legislation" role="tab" data-toggle="tab">Find Legislation</a></li>
                    <li role="presentation" data-step="3" data-intro="Specific Citation Search"><a href="#srch_by_citizen" aria-controls="srch_by_citizen" role="tab" data-toggle="tab">Search by Citation</a></li>

                </ul>

                <!-- Tab panes -->
                <div class="tab-content">

                    <div role="tabpanel" class="tab-pane active" id="main_search">
                        <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="research_text">

                                        <h2><span style="color: #717671;">Re</span>Search+</h2>

                                    </div>

                                    <div class="research_text2">

                                        <h6>The best way to legal research</h6>
                                        <p>
                                            Use the <span>Advanced Search Options</span> below to research caselaw.
                                    <br />
                                            <%--or the <span>Search Tools</span> to search by Topic, Case, Citation, or Statute.--%>
                                        </p>
                                    </div>
                                </div>
                                <!--row-->

                                <div class="row margin_top">

                                    <div class="col-lg-8 col-md-8">

                                        <div class="row">

                                            <asp:TextBox ID="txtSearch" runat="server" class="form-control login_text" placeholder="Search" AutoPostBack="false" data-step="4" data-intro="Enter Search word (Use AND, OR and NEAR)" MaxLength="200"></asp:TextBox>
                                            <div class="input-group-btn">
                                                <%--<button class="btn btn-default btn_height" type="submit"><i class="fa fa-search"></i></button>--%>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-default btn_height" OnClick="btnSearch_Click" data-step="8" data-intro="After input into search text are please press enter button and wait for desired search">
                                                    <%-- <i class="fa fa-search"></i>--%>
                                                </asp:Button>

                                                <asp:Label ID="lblMsg" runat="server" Text="Please enter search words" Visible="false" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-lg-4 col-md-4">

                                        <div class="my_radio">

                                            <div class="radio_parant" data-step="5" data-intro="Choose Pre-Define Selection (All Word)">
                                                <asp:RadioButton ID="radioALlWord" runat="server" GroupName="A" Checked="true" AutoPostBack="True" OnCheckedChanged="radioALlWord_CheckedChanged" />

                                                <span>All Words</span>
                                            </div>

                                            <div data-step="6" data-intro="Choose Pre-Define Selection (Exact word) - This will auto fill double quotes">
                                                <asp:RadioButton ID="radioExactPhrase" runat="server" GroupName="A" AutoPostBack="True" OnCheckedChanged="radioExactPhrase_CheckedChanged" />

                                                <span>Exact Phrase</span>
                                            </div>

                                            <div class="radio_parant2" data-step="7" data-intro="Choose Pre-Define Selection (More than one phrace) - This will auto fill double quotes and AND condistion">
                                                <asp:RadioButton ID="radioMoreThan" runat="server" GroupName="A" AutoPostBack="True" OnCheckedChanged="radioMoreThan_CheckedChanged" />

                                                <span>More than one phrase </span>
                                            </div>



                                        </div>

                                    </div>
                                    <div class="row roww mobile-cong-row" style="margin: 70px">

                                        <div class="col-lg-8 col-xs-12 col-md-12 mb-spacings">
                                            <div class="col-md-12 col-lg-12">
                                                <h4 style="color: #fff;">Search by Citation</h4>
                                            </div>
                                            <div class="col-lg-2 col-md-2 padding_left_z padAllZeroRes">
                                                <div class="text_boxes">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Year" Display="Dynamic"
                                                        ForeColor="Red" ControlToValidate="txtByCitationYearMain" ValidationGroup="CitationMain">
                                                    </asp:RequiredFieldValidator>

                                                    <asp:TextBox ID="txtByCitationYearMain" runat="server" class="form-control" ToolTip="Year" placeholder="Year" ValidationGroup="CitationMain"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-lg-4 col-md-4 padAllZeroRes">
                                                <div class="text_boxes">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Journal" ForeColor="Red"
                                                        ControlToValidate="ddlCitationJournalMain" InitialValue="0" ValidationGroup="CitationMain" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlCitationJournalMain" runat="server" class="form-control" ValidationGroup="CitationMain"></asp:DropDownList>



                                                </div>
                                            </div>

                                            <div class="col-lg-2 col-md-2 padAllZeroRes ctrl-pr-mb" style="padding-right: 0;">
                                                <div class="text_boxes">



                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="No."
                                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtCitationPageMain" ValidationGroup="CitationMain">
                                                    </asp:RequiredFieldValidator>

                                                    <asp:TextBox ID="txtCitationPageMain" runat="server" class="form-control" placeholder="Page" ToolTip="Number" ValidationGroup="CitationMain"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="col-lg-3 col-md-3 padAllZeroRes" style="padding-right: 0;">
                                                <div class="text_boxes">
                                                    <label>&nbsp;</label>
                                                    <asp:Button ID="btnCitationSearchMain" runat="server" Text="Submit" class="btn_style" Style="margin-top: 1px; float: left;" OnClick="btnCitationSearchMain_Click" ValidationGroup="CitationMain" />
                                                    <%--                 <button class="btn_style" style="margin-top:10px;float:left;">

                                                Search&nbsp;&nbsp;&nbsp;
                                                <i class="fa fa-search"></i>
                                            </button>--%>
                                                    <asp:Label ID="lblCitaionSearchMain" runat="server" ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>


                                        </div>

                                    </div>

                                </div>

                                <div class="row margin_top">

                                    <div class="col-lg-3 col-md-3">

                                        <div class="tabs_inner">
                                            <p>
                                                Use AND, OR and NEAR<br />

                                                [all capitals] between words for<br />

                                                boolean search.
                                            </p>

                                        </div>

                                    </div>

                                    <div class="col-lg-5 col-md-5">

                                        <asp:DropDownList ID="ddlRecentSearch" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlRecentSearch_SelectedIndexChanged"></asp:DropDownList>
                                        <%-- <select class="form-control" style="width: 92%;">

                                    <option>Select Previous Option</option>
                                    <option>Income tax ordinance 2001</option>
                                    <option>Charging provision prospective in nature</option>
                                    <option>Smugling</option>
                                    <option>Court</option>
                                    <option>Tax on resale of software</option>
                                    <option>Re seller softwares</option>
                                    <option>System development</option>
                                    <option>Information system cunsultant</option>
                                    <option>I.T bases system development</option>
                                    <option>Neuraceutical</option>
                                    <option>Software is considered a good in sale of goods</option>

                                </select>--%>
                                    </div>

                                </div>

                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlMain">
                                    <ProgressTemplate>

                                        <div class="modal1">
                                            <div class="center1">
                                                <img alt="" src="/style/img/ajax_loader_big_search.gif" />
                                            </div>
                                        </div>



                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                    <div role="tabpanel" class="tab-pane" id="find_legislation">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="overlay">
                            <div class="center">
                                <img alt="" src="/images/ajax-loader3.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
                                <div class="row roww">
                                    <div class="col-lg-3">
                                        <div class="text_boxes">
                                            <label>Title</label>
                                            <%--<input type="text" class="form-control" placeholder="Title" />--%>

                                            <asp:TextBox ID="txtStatutesTitle" runat="server" ToolTip="Title" class="form-control" placeholder="Acts, Oridinances, Rules, Regulations" ValidationGroup="Legislation" AutoPostBack="false" OnTextChanged="txtStatutesTitle_TextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ServiceMethod="SearchStatuteTitle"
                                                MinimumPrefixLength="2"
                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                TargetControlID="txtStatutesTitle"
                                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                            </cc1:AutoCompleteExtender>
                                            <%-- <telerik:RadAutoCompleteBox RenderMode="Lightweight" runat="server" ID="RadAutoCompleteBox1"   EmptyMessage="Please type here"
                 InputType="Token" HighlightFirstMatch="true" Width="200" DropDownWidth="300px">
            </telerik:RadAutoCompleteBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="text_boxes">
                                            <label>Year</label>
                                            <%--<input type="text" class="form-control" placeholder="Year" />--%>
                                            <asp:TextBox ID="txtYear" runat="server" ToolTip="Year" class="form-control" placeholder="Acts, Oridinances, Rules, Regulations By Year"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="text_boxes">
                                            <label>Subject</label>
                                            <asp:DropDownList ID="ddlStatutesCat" runat="server" class="form-control">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <%--   <div class="text_boxes">
                            <label>&nbsp;</label>--%>
                                        <asp:Button ID="btnStatutesSearch" runat="server" Text="Search" class="btn_style" OnClick="btnStatutesSearch_Click" ValidationGroup="Legislation" />

                                        <asp:Label ID="lblLegisLationMsg" runat="server" Visible="false"></asp:Label>
                                        <%--</div>--%>
                                    </div>
                                </div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>

                                        <div class="modal1">
                                            <div class="center1">
                                                <img alt="" src="/style/img/ajax_loader_big_search.gif" />
                                            </div>
                                        </div>



                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>


                    <div role="tabpanel" class="tab-pane" id="srch_by_citizen">
                        <asp:UpdatePanel ID="uppnl" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uppnl">
                    <ProgressTemplate>
                        <div class="overlay">
                            <div class="center">
                                <img alt="" src="/images/ajax-loader3.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
                                <div class="row roww">
                                    <div class="col-lg-3">
                                        <div class="text_boxes">

                                            <label>
                                                Year *:    
                                                <asp:RequiredFieldValidator ID="rfvCitationYear" runat="server" ErrorMessage="Year" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCitationYear" ValidationGroup="Citation">
                                                </asp:RequiredFieldValidator></label>
                                            <%--<input type="text" class="form-control" placeholder="Year" />--%>
                                            <asp:TextBox ID="txtCitationYear" runat="server" class="form-control" ToolTip="Year" placeholder="Year"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="text_boxes">
                                            <label>
                                                Select Journal *:    
                                                <asp:RequiredFieldValidator ID="rvfJournals" runat="server" ErrorMessage="Journal" ForeColor="Red"
                                                    ControlToValidate="ddlJournals" InitialValue="0" ValidationGroup="Citation" Display="Dynamic">
                                                </asp:RequiredFieldValidator></label>
                                            <asp:DropDownList ID="ddlJournals" runat="server" class="form-control"></asp:DropDownList>


                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="text_boxes">

                                            <label>
                                                Page *:<asp:RequiredFieldValidator ID="rfvNumber" runat="server" ErrorMessage="No." Display="Dynamic" ForeColor="Red" ControlToValidate="txtCitationNumber" ValidationGroup="Citation">
                                                </asp:RequiredFieldValidator></label>
                                            <%--<input type="text" class="form-control" placeholder="Page" />--%>
                                            <asp:TextBox ID="txtCitationNumber" runat="server" class="form-control" placeholder="Page" ToolTip="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="text_boxes">
                                            <label>&nbsp;</label>
                                            <asp:Button ID="btnCitationSearch" runat="server" Text="Search" class="btn_style" OnClick="btnCitationSearch_Click" ValidationGroup="Citation" />
                                            <%--<button class="btn_style">
            
                                    Search&nbsp;&nbsp;&nbsp;
                                    <i class="fa fa-search"></i>
                                </button>--%>



                                            <asp:Label ID="lblCitationMsg" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="uppnl">
                                    <ProgressTemplate>

                                        <div class="modal1">
                                            <div class="center1">
                                                <img alt="" src="/style/img/ajax_loader_big_search.gif" />
                                            </div>
                                        </div>



                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>



            <!------------ Search Tabs Section End ------------->

        </div>
    </div>

    <!-------------- Content End --------------->


    <!-------------- Panels --------------->

    <div class="container">

        <div class="row margin4">


            <div class="col-lg-4" style="display:none">
                <asp:UpdatePanel ID="upPnlLatestNews" runat="server">
                    <ContentTemplate>

                        <asp:Timer ID="TimerNews" runat="server" OnTick="TimerTickNews" Enabled="false" Interval="1">
                        </asp:Timer>


                       
                        <div class="carousel-inner" style="border: 1px solid; border-color: lightgray">
                             
                            <asp:Image ID="imgNews" runat="server" ImageUrl="/page-loader.gif" />
                            <telerik:RadRotator RenderMode="Lightweight" ID="RadRotator3" runat="server" Width="330" Height="400" CssClass="horizontalRotator"
                                ScrollDirection="Left,Right"
                                ScrollDuration="500" FrameDuration="4000" ItemHeight="230" ItemWidth="330" RotatorType="AutomaticAdvance" SlideShowAnimation-Type="Fade">
                                <ItemTemplate>
                                    <div class="item active">

                                        <a href='<%# Eval("SourceLink") %>' target='_blank'>
                                            <img class="thumbnail" src='<%# Eval("imgURL") %>' alt='<%# Eval("Title") %>' height="220px" width="100%"></a>
                                        <div class="caption" style="background-color: white">
                                            <h4><a href='<%# Eval("SourceLink") %>' target='_blank'><%# EastlawUI_v2.CommonClass.GetChracter(Eval("Title").ToString(),100) %>...</a> </h4>
                                            <br />
                                            Date: <%# Eval("NDate") %>
                                            <br />
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
                            <div class="navigation" style="float: right; padding-right: 7px; padding-bottom: 5px; display: none">
                                <asp:Image ImageUrl="/images/ArrowsLeftcircular.ico" ID="prevButton" AlternateText="Previous Slide"
                                    runat="Server" Height="34" Width="33" Style="border: 0px"></asp:Image>
                                <asp:Image ImageUrl="/images/ArrowsRightcircular.ico" ID="nextButton" AlternateText="Next Slide"
                                    runat="Server" Height="34" Width="31" Style="border: 0px"></asp:Image>
                            </div>
                        </div>


                    </ContentTemplate>
                </asp:UpdatePanel>



            </div>
            <asp:UpdatePanel ID="upPnlLatestCase" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Timer ID="Timer1" runat="server" OnTick="TimerTick" Interval="1" Enabled="false" >
                    </asp:Timer>
                    <div class="col-lg-4" style="display:none">

                        <div class="panel panel-default">
                            <div class="panel-heading"><span class="glyphicon glyphicon-list-alt"></span><b>Latest Judgements</b></div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-12 padding_all_z">

                                        <asp:Image ID="imgLoader" runat="server" ImageUrl="/page-loader.gif" />
                                        <telerik:RadRotator RenderMode="Lightweight" ID="RadRotator1" runat="server" Width="330" Height="300" CssClass="horizontalRotator"
                                            ScrollDirection="Up,Down"
                                            ScrollDuration="500" FrameDuration="2000" ItemHeight="130" ItemWidth="300" RotatorType="Buttons" SlideShowAnimation-Type="Fade">
                                            <ItemTemplate>

                                                <p>
                                                    <b><a href='<%# Eval("Link") %>'><%# Eval("Title") %></a></b><br />
                                                    <b>Date:</b> <%# Eval("FormatedJdate") %><br />
                                                    <b>Court:</b> <%# Eval("Court") %><br />
                                                    <b>Practice Area:</b> <%# Eval("CasePracticeArea") %><br />
                                                </p>
                                                <hr></hr>
                                            </ItemTemplate>
                                            <ControlButtons UpButtonID="caseUpButton" DownButtonID="caseDownButton"></ControlButtons>
                                        </telerik:RadRotator>


                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <a href="/cases/latest-judgments">View More</a>
                                <div class="navigation" style="float: right; padding-right: 7px; padding-bottom: 5px">
                                    <asp:Image ImageUrl="/images/arrow_up.png" ID="caseUpButton" AlternateText="Previous Slide"
                                        runat="Server" Height="20" Width="20" Style="border: 0px"></asp:Image>
                                    <asp:Image ImageUrl="/images/arrow_down.png" ID="caseDownButton" AlternateText="Next Slide"
                                        runat="Server" Height="20" Width="20" Style="border: 0px"></asp:Image>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="upPnlLatestLegislation" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Timer ID="Timer2" runat="server" OnTick="TimerTickLegislation" Interval="1" Enabled="false"></asp:Timer>
                    <div class="col-lg-4" style="display:none">

                        <div class="panel panel-default">
                            <div class="panel-heading"><span class="glyphicon glyphicon-list-alt"></span><b>Latest Legislations</b></div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-12 padding_all_z">


                                        <asp:Image ID="imgLoaderLegislation" runat="server" ImageUrl="/page-loader.gif" />

                                        <telerik:RadRotator RenderMode="Lightweight" ID="RadRotator2" runat="server" Width="330" Height="300" CssClass="horizontalRotator" BorderStyle="None"
                                            ScrollDirection="Up,Down"
                                            ScrollDuration="500" FrameDuration="2000" ItemHeight="130" ItemWidth="300" RotatorType="Buttons" SlideShowAnimation-Type="Fade">
                                            <ItemTemplate>

                                                <p>
                                                    <b><a href='<%# Eval("Link") %>'><%# Eval("Title") %></a></b><br />
                                                    <b>Date:</b> <%# Eval("Date") %><br />
                                                    <b>Category:</b> <%# Eval("PracticeArea") %>/<%# Eval("CatName") %><br />
                                                </p>
                                                <hr></hr>
                                            </ItemTemplate>
                                            <ControlButtons UpButtonID="lawUpButton" DownButtonID="lawDownButton"></ControlButtons>
                                        </telerik:RadRotator>



                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <a href="/statutes/latest-legislations">View More</a>
                                <div class="navigation" style="float: right; padding-right: 7px; padding-bottom: 5px">
                                    <asp:Image ImageUrl="/images/arrow_up.png" ID="lawUpButton" AlternateText="Previous Slide"
                                        runat="Server" Height="20" Width="20" Style="border: 0px"></asp:Image>
                                    <asp:Image ImageUrl="/images/arrow_down.png" ID="lawDownButton" AlternateText="Next Slide"
                                        runat="Server" Height="20" Width="20" Style="border: 0px"></asp:Image>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>







        </div>

    </div>

    <div class="buttons">
        <a class="btn btn-large btn-success" href="javascript:void(0);" onclick="javascript:introJs().start();">Quick Help</a>
        <%-- <a href="#" class="bgcolor" data-placement="left" data-toggle="tooltip" title="User Manual">

            <i class="fa fa-book"></i>


        </a>--%>

        <%-- <a href="#" class="bgcolor2" data-toggle="tooltip" title="Free Trial!" data-placement="left">

            <i class="fa fa-at"></i>


        </a>

        <a href="#" class="bgcolor3" data-toggle="tooltip" title="Subscribe" data-placement="left">

            <i class="fa fa-shopping-cart"></i>


        </a>--%>
    </div>
    <%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlTop">
                    <ProgressTemplate>
                     
                           <div class="modal1">
        <div class="center1">
           <img alt="" src="/style/img/ajax_loader_big.gif" />
        </div>
    </div>
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
    <%-- </ContentTemplate>
           </asp:UpdatePanel>--%>
</asp:Content>

<%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"
type = "text/javascript"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
type = "text/javascript"></script> 
<link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
rel = "Stylesheet" type="text/css" /> --%>
    


