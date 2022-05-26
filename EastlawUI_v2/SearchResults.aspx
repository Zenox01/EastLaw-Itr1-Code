<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="EastlawUI_v2.SearchResults" 
    MasterPageFile="~/MemberMaster.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">

    <script type="text/javascript">
          function ShowPopup() {
              $(function () {
                //  $("#dialog").html(message);
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
    <link href="/style/css/accordian.css" rel="stylesheet" type="text/css" />
    
    <asp:UpdatePanel ID="uppnl" runat="server">
        <ContentTemplate>

    <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    <li><a href="#">Search Result</a></li>
    <li><a href="#" class="current"><asp:Label ID="lblSearchWordsCrumbs" runat="server"></asp:Label></a></li>
    <li id="liSearchWithin" runat="server"><a href="#" class="current"><asp:Label ID="lblSearchWithinWordsCrumbs" runat="server"></asp:Label></a></li>
   
</ul>
  </div>
</div>
               
    <div class="container">
            <div class="row">
            </div>
            <!------------ Search Tabs Section ------------->
            <div class="heading_style">
                <h3>Search Result of <i style="color:#c4161c;">
                    <asp:Label ID="lblSearchWords" runat="server" ForeColor="#D11515"></asp:Label>
                    </i><span id="spanSearchWithin" runat="server" style="display:none">Search within Results of <i style="color:#c4161c;">
                    <asp:Label ID="lblSearchWithinWord" runat="server" ForeColor="#D11515"></asp:Label>
                    <asp:Label ID="lblNoofSearchwithrecord" runat="server" ForeColor="#D11515"></asp:Label>
                    </i></span></h3>
            </div>
            <!-------------- Left Side --------------->
            <div class="col-lg-3 col-md-3">
                <h4 class="my_h4">Results : <b>
                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                    </b></h4>
                <div class="clearfix">
                </div>
                <div id="accordion" aria-multiselectable="true" class="panel-group wrap" role="tablist">
                    <div class="panel">
                        <div id="headingOne" class="panel-heading" role="tab">
                            <h4 class="panel-title"><a aria-controls="collapseOne" aria-expanded="true" data-parent="#accordion" data-toggle="collapse" href="#collapseOne" role="button"><i aria-hidden="true" class="fa fa-gavel" style="display:inline-block;"></i>
                                <p class="sr_head">
                                    Court</p>
                                </a>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                            </h4>
                        </div>
                        <div id="collapseOne" aria-labelledby="headingOne" class="panel-collapse collapse in" role="tabpanel">
                            <div class="panel-body panel-body2">
                                <div id="style-2" class="scrollbar" style="height:173px;">
                                    <div class="force-overflow">
                                        <asp:CheckBoxList ID="chkCourtLst" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkCourtLst_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end of panel -->
                    <div class="panel">
                        <div id="headingTwo" class="panel-heading" role="tab">
                            <h4 class="panel-title"><a aria-controls="collapseTwo" aria-expanded="false" class="collapsed" data-parent="#accordion" data-toggle="collapse" href="#collapseTwo" role="button"><i aria-hidden="true" class="fa fa-calendar" style="display:inline-block;"></i>
                                <p class="sr_head">
                                    Years</p>
                                </a>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                            </h4>
                        </div>
                        <div id="collapseTwo" aria-labelledby="headingTwo" class="panel-collapse collapse" role="tabpanel">
                            <div class="panel-body panel-body2">
                                <div id="style-2" class="scrollbar" style="height:173px;">
                                    <asp:CheckBoxList ID="chkLstYear" runat="server" AutoPostBack="true" class="force-overflow" OnSelectedIndexChanged="chkLstYear_SelectedIndexChanged">
                                    </asp:CheckBoxList>
                                    <%--<div class="force-overflow">
            <ul>
                <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">2016 - 2017</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">2011 - 2015</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">2005 - 2010</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">1999 - 2004</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">1993 - 1998</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">1987 - 1992</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">1981 - 1986</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">1975 - 1981</p>
                </li>
                <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">1969 - 1974</p>
                </li>
                <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">1963 - 1968</p>
                </li>
                <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">1957 - 1962</p>
                </li>
                <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">1951 - 1956</p>
                </li>
                <li>
                    <input type="checkbox" class="checkbox2 margin_rgt_5">
                    <p class="checbox-p">1948 - 1950</p>
                </li>
               
          </ul>
          </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end of panel -->
                    <div class="panel" style="display:none">
                        <div id="headingTwo" class="panel-heading" role="tab">
                            <h4 class="panel-title"><a aria-controls="collapseThree" aria-expanded="false" class="collapsed" data-parent="#accordion" data-toggle="collapse" href="#collapseThree" role="button"><i aria-hidden="true" class="fa fa-calendar" style="display:inline-block;"></i>
                                <p class="sr_head">
                                    Narrow by Statute</p>
                                </a>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                                <h4></h4>
                            </h4>
                        </div>
                        <div id="collapseThree" aria-labelledby="headingTwo" class="panel-collapse collapse" role="tabpanel">
                            <div id="style-2" class="scrollbar" style="height:173px;">
                                <div class="force-overflow">
                                    <div class="panel-body panel-body2">
                                        <ul>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    2016 - 2017</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    2011 - 2015</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    2005 - 2010</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    1999 - 2004</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    1993 - 1998</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    1987 - 1992</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    1981 - 1986</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    1975 - 1981</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    1969 - 1974</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    1963 - 1968</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    1957 - 1962</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    1951 - 1956</p>
                                                </input></li>
                                            <li>
                                                <input class="checkbox2 margin_rgt_5" type="checkbox">
                                                <p class="checbox-p">
                                                    1948 - 1950</p>
                                                </input></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-------------- Left Side End --------------->
            <!-------------- right Side --------------->
            <div class="col-lg-9 col-md-9 margin_bot_20">
            
            <div class="row">
          
            <div class="style0">
                <div class="pull-left topper">
                    <%--<input class="form-control text_field4" placeholder="Search Within..." name="srch-term" id="srch-term" type="text">--%>
                    <asp:TextBox ID="txtSearchWithinResult" runat="server" AutoPostBack="false" class="form-control text_field4" OnTextChanged="txtSearchWithinResult_TextChanged" placeholder="Search Within..."></asp:TextBox>
                    <div class="input-group-btn">
                        <%--    <button class="btn btn-default btn_height" type="submit"><i class="fa fa-search"></i></button>--%>
                        <asp:Button ID="btnSearchWithinResult" runat="server" class="btn btn-default btn_height" OnClick="btnSearchWithinResult_Click" Text="Search" />
                    </div>
                </div>
                <div class="pull-right">
                    <%--<button class="btn_3d"s>
                    	<i class="fa fa-folder-open-o" aria-hidden="true"></i>
                        Add to Folder
                          
                    </button>--%><%--       <button class="btn_3d">
                    	<i class="fa fa-plus" aria-hidden="true"></i>
                        Create New Folder
                    </button>--%>
                </div>
            </div>
            <div id="divSearchWithBreadCrumb" runat="server" class="row breadcrum" style="display:none">
                <ul class="bc">
                    <li id="liSearchWithBreadCrumb1" runat="server" style="display:none">
                        <asp:LinkButton ID="lblSearchWithinText1" runat="server" Text="" OnClick="lblSearchWithinText1_Click" >
                        </asp:LinkButton>
                        <asp:LinkButton ID="lblSearchWithinCount1" runat="server" Text=""></asp:LinkButton>
                        </li>
                    <li id="liSearchWithBreadCrumb2" runat="server" style="display:none">
                        <asp:LinkButton ID="lblSearchWithinText2" runat="server" Text="" OnClick="lblSearchWithinText2_Click" ></asp:LinkButton>
                        <asp:LinkButton ID="lblSearchWithinCount2" runat="server" Text=""></asp:LinkButton>
                        </li>
                    <li id="liSearchWithBreadCrumb3" runat="server" style="display:none">
                        <asp:LinkButton ID="lblSearchWithinText3" runat="server" Text="" OnClick="lblSearchWithinText3_Click"></asp:LinkButton>
                        <asp:LinkButton ID="lblSearchWithinCount3" runat="server" Text=""></asp:LinkButton>
                        </li>
                    <li id="liSearchWithBreadCrumb4" runat="server" style="display:none">
                        <asp:LinkButton ID="lblSearchWithinText4" runat="server" Text="" OnClick="lblSearchWithinText4_Click"></asp:LinkButton>
                        <asp:LinkButton ID="lblSearchWithinCount4" runat="server" Text=""></asp:LinkButton>
                        </li>
                    <li id="liSearchWithBreadCrumb5" runat="server" style="display:none">
                        <asp:LinkButton ID="lblSearchWithinText5" runat="server" Text=""></asp:LinkButton>
                        <asp:LinkButton ID="lblSearchWithinCount5" runat="server" Text=""></asp:LinkButton>
                        </li>
                </ul>
            </div>
            
            <div class="clearfix">
            </div>
            <div id="divNoResult" runat="server" style="display:none">
                <div class="col-lg-10 col-md-10 center">
                    <div class="row">
                        <div class="login">
                            <div style="width:80%;float:left">
                                <h3 style="color:#D11515">No documents satisfy your query.</h3>
                                <p>
                                    You may want to <span style="color:#D11515">Edit</span> your Search By
                                </p>
                                <p>
                                    inserting <span style="color:red"><b>“ “</b></span> to search the exact phrase within a document; <span style="color:red"><b>OR</b></span>
                                    <br />
                                    inserting <span style="color:red"><b>AND</b></span> between your terms to find them anywhere in the same document; <span style="color:red"><b>OR</b></span><br /> by inserting <span style="color:red"><b>OR</b></span> between your terms to find combination of two keywords existing together or separately.
                                </p>
                            </div>
                            <br />
                            <div>
                                <%--<input type="text" class="form-control" placeholder="Email Address" />--%>:
                                <asp:TextBox ID="txtComment" runat="server" Height="100" placeholder="Enter Ciations/keywords" TextMode="MultiLine" ValidationGroup="AA" Width="300"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment" ErrorMessage="*" ValidationGroup="AA"></asp:RequiredFieldValidator>
                            </div>
                            <div>
                                <asp:Button ID="btnFill" runat="server" CssClass="btn btn-danger btn_style" OnClick="btnFill_Click" Text="Send Report" ValidationGroup="AA" Width="130" />
                                </td>
                                <asp:Label ID="lblMsgRportMsging" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divResult" runat="server">
                <div class="table-container">
                    <asp:GridView ID="gvLst" runat="server" AllowCustomPaging="true" AllowPaging="true" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table table-filter" GridLines="None" OnPageIndexChanging="gvLst_PageIndexChanging" OnRowDataBound="gvLst_RowDataBound" PageSize="20" Width="100%" OnRowCommand="gvLst_RowCommand">
                        <%--<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />--%>
                        <pagersettings firstpagetext="First" lastpagetext="Last" mode="NumericFirstLast" nextpagetext="Next" position="TopAndBottom" previouspagetext="Prev" />
                        <pagerstyle cssclass="gridview" />
                        <Columns>
                            <asp:TemplateField ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <div class="ckbox">
                                    </div>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <a class="star" href="javascript:;"><i class="glyphicon glyphicon-star"></i></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="media">
                                        <div class="media-body">
                                            <a href='<%# Eval("Link") %>'>
                                            <h4 class="title"><%# Eval("Title") %><span class="pull-right">[ <%# Eval("Court") %>] [ <%# Eval("noofjudges") %>] </span></h4>
                                            </a>
                                            <div class="row details_data">
                                                <div class="col-lg-7 col-md-7" style="padding-left:0;">
                                                    <p class="pull-left">
                                                        <strong>Where Reported : </strong><%#  Eval("Citation") %>
                                                    </p>
                                                </div>
                                                <div class="col-lg-5 col-md-5">
                                                    <p class="pull-left">
                                                        <strong>Dated : </strong><%# Eval("JDate") %>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="row details_data" style="margin-top:0;">
                                                <div class="col-lg-7 col-md-7" style="padding-left:0;">
                                                    <p class="pull-left">
                                                        <strong>Appeal : </strong><%# EastlawUI_v2.CommonClass.GetChracter(Eval("Appeal").ToString(),20).ToString() %>...</p>
                                                </div>
                                                <div class="col-lg-5 col-md-5">
                                                    <p class="pull-left">
                                                        <strong>Result : </strong><%# Eval("Result") %>
                                                    </p>
                                                </div>
                                            </div>
                                            <br />
                                            <strong>Practice Area:</strong> <%# Eval("CasePracticeArea") %>
                                            <br />
                                            <strong>Tagged Statutes:</strong> <%# Eval("CaseTaggedStatutes") %>
                                            <div class="row">
                                                <asp:Button ID="btnShowSummary" runat="server" CssClass="btn_show" Text="Show Case Summary" CommandName="ViewSummary" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                               
                                            </div>
                                            <p class="summary">
                                                <br />
                                                <%# Eval("Desc") %>
                                                <br />
                                                <br />
                                                <span class="media-meta pull-right"><a href='<%# Eval("Link") %>'>View Full</a></span>
                                            </p>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                    <asp:HiddenField ID="hdSummary" runat="server" Value='<%# Eval("CaseSummary") %>' />
                                    <asp:HiddenField ID="hdResultType" runat="server" Value='<%# Eval("ResultType") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            </div>
            <!-------------- right Side End --------------->
            </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uppnl">
                <ProgressTemplate>
                    <div class="modal1">
                        <div class="center1">
                            <img alt="" src="/style/img/ajax_loader_big_search.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" id="divModelHeader" runat="server">Modal Header</h4>
                    </div>
                    <div class="modal-body" id="divModelBody" runat="server">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    
                </div>
            </div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>

     
    <script type="text/javascript">
        function showModal() {
            $("#myModal").modal('show');
        }

        $(function () {
            $("#btnShow").click(function () {
                showModal();
            });
        });
    </script>
</asp:Content>


