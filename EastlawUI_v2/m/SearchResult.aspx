<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchResult.aspx.cs" Inherits="EastlawUI_v2.m.SearchResult"
    MasterPageFile="~/m/MemberMaster.Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <form runat="server">
        <asp:ScriptManager ID="scrpt" runat="server"></asp:ScriptManager>
<script type="text/javascript" src="/m/js/accordion.custom.29473.js"></script>
<script src="/m/js/showHide.js" type="text/javascript"></script>
  
<script type="text/javascript">

$(document).ready(function(){


   $('.show_hide').showHide({			 
		speed: 800,  // speed you want the toggle to happen	
		easing: '',  // the animation effect you want. Remove this line if you dont want an effect and if you haven't included jQuery UI
		changeText: 1, // if you dont want the button text to change, set this to 0
		showText: 'Show Filters',// the button text to show when a div is closed
		hideText: 'Hide Filters' // the button text to show when a div is open
					 
	}); 


});

</script>

    
   <asp:UpdatePanel ID="upPnlTop" runat="server">
              <ContentTemplate>

<div class="contentPage">
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
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="modalBackground" PopupControlID="Panel1" TargetControlID="btnPopup" runat="server">
    </cc1:ModalPopupExtender>
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
                    <asp:Button  ID="btnClose" runat="server" Text="Close" CausesValidation="false" OnClick="btnClose_Click"  CssClass="btnstyle"  Width="130"/>&nbsp; 
                    <asp:Button OnClick="btnFill_Click" ID="btnFill" runat="server" Text="Send Report" ValidationGroup="AA"  CssClass="btnstyle"  Width="130"/></td>
            </tr>
        </table>
    
        <br />
  
   
   
  </div>
    </asp:Panel>
                        </div>
                    
                </div>
	<div class="comingSoon" id="divResult" runat="server">
    	<div class="container">
        	<div class="margin">
            
            	<div class="srchRow">
                	<div class="lft"><asp:TextBox ID="txtSearchWithinResult" runat="server"  placeholder="Search Within" AutoPostBack="true" OnTextChanged="txtSearchWithinResult_TextChanged"></asp:TextBox>
                        </div>
                    <div class="rgt"><asp:Button ID="btnSearchWithinResult" runat="server" Text="Search Within" OnClick="btnSearchWithinResult_Click" />
                        </div>
                </div>
                
                <div class="RowFilter">
                
                	<div class="lft">
                    	<h6>Showing <asp:Label ID="lblCount" runat="server"></asp:Label></h6>
                        <span class="sp"><a href="#" class="show_hide showfilterBtn1" rel="#slidingDiv"> Show Filters </a></span>
                        <span class="sp"><a href="#" class="showfilterBtn1" rel="#slidingDiv"> Clear Filters </a></span>
                    </div>
                    
                    <div class="mid2">
                        <div class="ResultShow">
                        	Showing Results for  <span> <asp:Label ID="lblSearchWords" runat="server" ForeColor="#D11515"></asp:Label></span>
                        </div>
                    </div>
                    
                    <div class="rgt" id="spanMyFolder" runat="server">
                    	<h6>Add to folder</h6>
                        <span>
                        	<asp:DropDownList ID="ddlFolders" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFolders_SelectedIndexChanged"></asp:DropDownList> 
                        </span>

                        <h6 style="display:none">Create New Folder</h6>
                        <span style="display:none">
                        	Parent Folder *
                                <asp:RequiredFieldValidator ID="rfvParentFolder" ValidationGroup="CreateFolder" runat="server" ErrorMessage="Required" ForeColor="Red" Enabled="false" ControlToValidate="ddlParentFolder"></asp:RequiredFieldValidator>&nbsp;</label><%--<input type="text" value="" />--%><asp:DropDownList ID="ddlParentFolder" runat="server" AutoPostBack="false" CssClass="form-control" ></asp:DropDownList>
                           
                        </span>
                        <span style="display:none"> 
                        	<b>Folder Name *</b> 
                                <asp:RequiredFieldValidator ID="rfvFolderName" ValidationGroup="CreateFolder" runat="server" ErrorMessage="Required" ForeColor="Red" Enabled="false" ControlToValidate="txtFolderName"></asp:RequiredFieldValidator>
                        	 <asp:TextBox ID="txtFolderName" runat="server" CssClass="form-control" ></asp:TextBox>         
                        </span>
                        <asp:Button runat="server" Visible="false" ID="btnCreateFolder"  class="btn btn-black register-page-btn" ValidationGroup="CreateFolder" Text="Create Folder" OnClick="btnCreateFolder_Click"></asp:Button>
                         
                    </div>
                    
                </div>
                
                <div class="resultRow">
                
                <div id="slidingDiv" class="lft1">
                	
                    
                    
                    	<section class="ac-container">
                             <div class="lftBlock">
                                <div>
                                    <input id="ac-0" name="accordion-1" type="radio" checked />
                                    <label for="ac-0">Content Type</label>
                                    <article class="ac-small">
                                        <asp:CheckBoxList ID="chkContentType" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="chkContentType_SelectedIndexChanged"></asp:CheckBoxList>
                                        
                                    </article>
                                </div>
                            </div>
                       
                            <div class="lftBlock" id="AccordionPaneCourts" runat="server">
                                <div>
                                    <input id="ac-1" name="accordion-1" type="radio" checked />
                                    <label for="ac-1">Courts</label>
                                    <article class="ac-small">
                                       <%-- <ul>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>1998</strong>                                            </li>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>1999</strong>                                            </li>
                                            
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>2000</strong>                                            </li>
                                            
                                            </ul>
                                        --%>
                                    <asp:CheckBoxList ID="chkCourtLst" runat="server" OnSelectedIndexChanged="chkCourtLst_SelectedIndexChanged"  AutoPostBack="true" ></asp:CheckBoxList>
                                    </article>
                                    
                                </div>
                            </div>
                            
                        	 <div class="lftBlock" id="AccordionPaneYears" runat="server">
                            	<div>
                                <input id="ac-2" name="accordion-1" type="radio" />
                                <label for="ac-2">Years</label>
                                <article class="ac-medium">
                                    <asp:CheckBoxList ID="chkLstYear" runat="server" OnSelectedIndexChanged="chkLstYear_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                                    <%--<ul>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>1998</strong>                                            </li>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>1999</strong>                                            </li>
                                            
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>2000</strong>                                            </li>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>2001</strong>                                            </li>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>2002</strong>                                            </li>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>2003</strong>                                            </li>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>2004</strong>                                            </li>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>2005</strong>                                            </li>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>2006</strong>                                            </li>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>2015</strong>                                            </li>
                                            <li>
                                                <span><input name="" type="checkbox" value=""></span> 
                                                <strong>2016</strong>                                            </li>
                                            <li>
                                           
                                            <div class="clear"></div>
                                        </ul>--%>
                                </article>
                            </div>
                            </div>
                            <div class="lftBlock" id="AccordionPane2" runat="server">
                            	<div>
                                <input id="ac-3" name="accordion-1" type="radio" />
                                <label for="ac-3">TOPICS</label>
                                <article class="ac-medium">
                                    <asp:TreeView ID="tv" runat="server" >

            </asp:TreeView>
                                    
                                </article>
                            </div>
                            </div>
            
                        </section>
                    
                    
                    
                    	
                    
                </div>
                
               <div class="rgt1">
               	<span>
                	   <asp:GridView ID="gvLst" runat="server" AutoGenerateColumns="false" Width="100%"
                     GridLines="None" AllowPaging="true" AllowCustomPaging="true" PageSize="20" OnPageIndexChanging="gvLst_PageIndexChanging">
                    <pagersettings mode="NumericFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"  
            position="TopAndBottom" />
                    <pagerstyle cssclass="" >

</pagerstyle>
                    <Columns>
                        <asp:TemplateField ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>

                                <div class="rows">
                    	<h6><a href='<%# Eval("Link") %>'><span style="text-transform:capitalize"> <%# Eval("Title") %></span></a></h6>
                        <%--<p>
                        	<strong>Date:</strong> 13th July 2015 <br />
                            <strong>Court:</strong> Sindh High Court <br />
                            <strong>Appeal:</strong> J.M. Nos. 8, 9, 10 & 11 of 2014 <br />
                            <strong>Result:</strong> Order Accordingly
                        </p>--%>
                                     
                                                    <div class="row">
                                                    
                                                   
                                                    	<button type="button" class="btn_show" data-toggle="collapse" data-target='<%# "#demo"+Eval("ID") %>' runat="server" id="btnshowsumary">Show Excerpt</button>
                                                   

                                                          <div id='<%# "demo"+Eval("ID") %>' class="collapse" style="background-color: #f1e9e9;">
                        
                   <%# Eval("CaseSummary").ToString().Replace("</p>","</p><br><br>----") %>
                    </div>
                                                    
                                                    </div>
                        <div class="result">
                             <%# Eval("OtherContent") %>
                        	
                        </div>
                        <div class="viewMore"><a href='<%# Eval("Link") %>'>View Full...</a></div>
                                    <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:HiddenField ID="hdResultType" runat="server" Value='<%# Eval("ResultType") %>' />
                    </div>

                       
                                </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                 
                    
                   
                    
                 </span>
                </div>
                    
                </div>
                
            </div>
        </div>
        <div class="clear"></div>
    </div>

</div>
                   <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlTop">
                    <ProgressTemplate>
                     
                           <div class="modal1">
        <div class="center1">
           <img alt="" src="/m/images/ajax_loader_big.gif" />
        </div>
    </div>
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
              </ContentTemplate>
          </asp:UpdatePanel>
        </form>
</asp:Content>


