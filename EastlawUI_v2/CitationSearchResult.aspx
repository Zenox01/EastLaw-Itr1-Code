<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CitationSearchResult.aspx.cs" Inherits="EastlawUI_v2.CitationSearchResult"
    MasterPageFile="~/MemberMaster.Master" %>


    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content2" runat="server" contentplaceholderid="cntPlaceHolder">
    <link href="/style/css/accordian.css" rel="stylesheet" type="text/css" />
    <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    <li><a href="#" class="current">Search Result</a></li>
   
</ul>
  </div>
</div>
     <asp:UpdatePanel ID="uppnl" runat="server">
        <ContentTemplate>
    <div class="container">
    <div class="row">
    
    <!------------ Search Tabs Section ------------->
    
    	
   <div class="heading_style" style="display:none">
            
            	<h3>Search Result of <i style="color:#c4161c;"><asp:Label ID="lblSearchWords" runat="server" ForeColor="#D11515"></asp:Label></i></h3>
            
            </div>
    
    <!-------------- Left Side --------------->
    <div class="col-lg-3 col-md-3">
    
    
    	<h4 class="my_h4">Results : <b><asp:Label ID="lblCount" runat="server"></asp:Label></b></h4>
    
    	
  <div class="clearfix"></div>
  
  
  
  
  <div class="panel-group wrap" id="accordion" role="tablist" aria-multiselectable="true">
      <div class="panel">
        <div class="panel-heading" role="tab" id="headingOne">
          <h4 class="panel-title">
        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
          <i class="fa fa-gavel" aria-hidden="true" style="display:inline-block;"></i>
          <p class="sr_head">Court</p>
        </a>
      </h4>
        </div>
        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
          <div class="panel-body panel-body2">
          <div class="scrollbar" id="style-2" style="height:173px;">
              <asp:CheckBoxList ID="chkCourtLst"  class="force-overflow" runat="server" OnSelectedIndexChanged="chkCourtLst_SelectedIndexChanged" AutoPostBack="true" ></asp:CheckBoxList>
      <%--<div class="force-overflow">
      
            <ul>
                <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
          </ul>
          </div>--%>

          </div>
          </div>
        </div>
      </div>
      <!-- end of panel -->

      <div class="panel">
        <div class="panel-heading" role="tab" id="headingTwo">
          <h4 class="panel-title">
        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
         <i class="fa fa-calendar" aria-hidden="true" style="display:inline-block;"></i><p class="sr_head">Years</p>
        </a>
      </h4>
        </div>
        <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
          
      <div class="panel-body panel-body2">
      
          <div class="scrollbar" id="style-2" style="height:173px;">
          <asp:CheckBoxList ID="chkLstYear" runat="server"  class="force-overflow" OnSelectedIndexChanged="chkLstYear_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
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
          </div>--%></div>
          </div>
        </div>
      </div>
      <!-- end of panel -->
      
      
      <div class="panel" style="display:none">
        <div class="panel-heading" role="tab" id="headingTwo">
          <h4 class="panel-title">
        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
         <i class="fa fa-calendar" aria-hidden="true" style="display:inline-block;"></i><p class="sr_head">Narrow by Statute</p>
        </a>
      </h4>
        </div>
        <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
          
          <div class="scrollbar" id="style-2" style="height:173px;">
          
      <div class="force-overflow">
      <div class="panel-body panel-body2">
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
          </div></div>
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
            <asp:TextBox ID="txtSearchWithinResult" runat="server" class="form-control text_field4" placeholder="Search Within..." AutoPostBack="true" OnTextChanged="txtSearchWithinResult_TextChanged"></asp:TextBox>
                        <div class="input-group-btn">
                        <%--    <button class="btn btn-default btn_height" type="submit"><i class="fa fa-search"></i></button>--%>
                            <asp:Button ID="btnSearchWithinResult" runat="server" class="btn btn-default btn_height" Text="Search" OnClick="btnSearchWithinResult_Click" />
                        </div>
                    </div>
                    
                  <div class="pull-right" style="display:none">
                   <telerik:radbutton RenderMode="Lightweight" ID="RadbtnAddToFolder" runat="server" Text="Add to Folder" >
                                <Icon PrimaryIconCssClass="rbSave"></Icon>
                            </telerik:radbutton>
                  	<%--<button class="btn_3d"s>
                    	<i class="fa fa-folder-open-o" aria-hidden="true"></i>
                        Add to Folder
                          
                    </button>--%>
                      <telerik:radbutton RenderMode="Lightweight" ID="RadbtnCreateFolder" runat="server" Text="Create New Folder">
                                <Icon PrimaryIconCssClass="rbAdd"></Icon>
                            </telerik:radbutton>
             <%--       <button class="btn_3d">
                    	<i class="fa fa-plus" aria-hidden="true"></i>
                        Create New Folder
                    </button>--%>
                  
                  </div>  
    </div>
    </div>
    
    <div class="clearfix"></div>
    <div id="divNoResult" runat="server" style="display:none" >
                    <div style="width:80%;float:left">
                        <h3 style="color:#D11515">No documents satisfy your query.</h3>
                    <p>You may want to <span style="color:#D11515">Edit</span> your Search By 
                    </p>
                    <p>inserting <span style="color:red"><b>“ “</b></span> to search the exact phrase within a document; <span style="color:red"><b>OR</b></span> <br />
                        inserting  <span style="color:red"><b>AND</b></span> between your terms to find them anywhere in the same document; <span style="color:red"><b>OR</b></span><br />
                        by inserting  <span style="color:red"><b>OR</b></span> between your terms to find combination of two keywords existing together or separately.
                    </p>
                   
                    </div>
                    <div style="width:80%;float:right;">
                         <asp:Button ID="btnPopup" runat="server" Text="Report Missing Citations"  CssClass="btnstyle" Width="200"/>
                      <%--  <cc1:modalpopupextender ID="ModalPopupExtender1" BackgroundCssClass="modalBackground" PopupControlID="Panel1" TargetControlID="btnPopup" runat="server">
    </cc1:modalpopupextender>--%>
                         <asp:Panel ID="Panel1" runat="server">
    <div class="PopUpWindow">
        <table style="width:100%">
            <tr>
                <td>Enter Ciations/keywords: </td>
                <td><asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="300" Height="100"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment" ErrorMessage="*"
            ValidationGroup="AA"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:right">
                    <asp:Button  ID="btnClose" runat="server" Text="Close" CausesValidation="false"   CssClass="btnstyle"  Width="130"/>&nbsp; 
                    <asp:Button  ID="btnFill" runat="server" Text="Send Report" ValidationGroup="AA"  CssClass="btnstyle"  Width="130"/></td>
            </tr>
        </table>
    
        <br />
  
   
   
  </div>
    </asp:Panel>
                        </div>
                    
                </div>
    
          <div id="divResult" runat="server">
               <div class="table-container">
              <asp:GridView ID="gvLst" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-filter" DataKeyNames="Id"
                     GridLines="None" AllowPaging="true" AllowCustomPaging="true" PageSize="20" OnPageIndexChanging="gvLst_PageIndexChanging" OnRowCommand="gvLst_RowCommand">
                    <%--<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />--%>
                    <pagersettings mode="NumericFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"  
            position="TopAndBottom" />
                    <pagerstyle cssclass="gridview" >

</pagerstyle>
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
                                <a href="javascript:;" class="star">
												<i class="glyphicon glyphicon-star"></i>
											</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="media">
												
												<div class="media-body">
													
													<a href='<%# "/cases/" +  clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.GetWords(Eval("Appeallant").ToString(),3).ToString()).Replace(" ","-")+"VS"+clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.GetWords(Eval("Respondent").ToString(),3).ToString()).Replace(" ","-")+"."+ EncryptDecryptHelper.Encrypt(Eval("ID").ToString()) %>'><h4 class="title">
														<%# EastlawUI_v2.CommonClass.MakeFirstCap((string)Eval("Appeallant")) %> VS <%# EastlawUI_v2.CommonClass.MakeFirstCap((string)Eval("Respondent")) %> <span class="pull-right">[ <%# Eval("Court") %> ] </span>
														
													</h4>
                                                    </a>
                                                    <div class="row details_data">
                                                    <div class="col-lg-7 col-md-7" style="padding-left:0;">
                                                    <p class="pull-left"><strong>Where Reported : </strong> <%# Eval("Citation") %></p>
                                                    </div>
                                                    <div class="col-lg-5 col-md-5">
                                                    <p class="pull-left"><strong>Dated : </strong> <%# Eval("JDate") %></p>
                                                    </div>
                                                    </div>
                                                    <div class="row details_data" style="margin-top:0;">						
                                                    <div class="col-lg-7 col-md-7" style="padding-left:0;">
                                                    <p class="pull-left"><strong>Appeal : </strong> <%# EastlawUI_v2.CommonClass.GetChracter(Eval("Appeal").ToString(),20).ToString() %>...</p>
                                                    </div>
                                                    <div class="col-lg-5 col-md-5">
                                                    <p class="pull-left"><strong>Result : </strong> <%# Eval("Result") %></p>
                                                         
                                                    </div>
                                                    </div>
                                                    <strong>Practice Area:</strong> <%# Eval("CasePracticeArea") %>
                                            <br />
                                            <strong>Tagged Statutes:</strong> <%# Eval("CaseTaggedStatutes") %>
                                                    <div class="row">
                                                <asp:Button ID="btnShowSummary" runat="server" CssClass="btn_show" Text="Show Case Summary" CommandName="ViewSummary" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                               
                                            </div>
                                                    
                                                    
													<p class="summary">
                                                    <br />
                                                   <%-- <%# Eval("Desc") %>--%>
                                                        <br />
                                                        <br />
                                                        <span class="media-meta pull-right">
                                                        <a href='<%# "/cases/" +  clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.GetWords(Eval("Appeallant").ToString(),3).ToString()).Replace(" ","-")+"VS"+clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.GetWords(Eval("Respondent").ToString(),3).ToString()).Replace(" ","-")+"."+ EncryptDecryptHelper.Encrypt(Eval("ID").ToString()) %>'>View Full ...</a></span>
												</div>
											</div>

                             <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                            
                                <%--<asp:HiddenField ID="hdResultType" runat="server" Value='<%# Eval("ResultType") %>' />--%>
     
                             
               
                               
                                </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
                </asp:GridView>
                   </div>
              </div>
       
  
    
    
    </div>
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
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




