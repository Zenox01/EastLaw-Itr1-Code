<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatutesListAutoFilter.aspx.cs" Inherits="EastlawUI_v2.m.StatutesListAutoFilter" 
     MasterPageFile="~/m/MemberMaster.Master"%>

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
                    <asp:Button  ID="btnClose" runat="server" Text="Close" CausesValidation="false"  CssClass="btnstyle"  Width="130"/>&nbsp; 
                    <asp:Button  ID="btnFill" runat="server" Text="Send Report" ValidationGroup="AA"  CssClass="btnstyle"  Width="130"/></td>
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
                    
                    <div class="rgt" id="spanMyFolder"  runat="server">
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
                        <%--<asp:Button runat="server" Visible="false" ID="btnCreateFolder"  class="btn btn-black register-page-btn" ValidationGroup="CreateFolder" Text="Create Folder" OnClick="btnCreateFolder_Click"></asp:Button>--%>
                         
                    </div>
                    
                </div>
                
                <div class="resultRow">
                
                <div id="slidingDiv" class="lft1">
                	
                    
                    
                    	<section class="ac-container">
                           
                       
                            <div class="lftBlock" id="AccordionPaneCourts" runat="server">
                                <div>
                                    <input id="ac-1" name="accordion-1" type="radio" checked />
                                    <label for="ac-1">Categories</label>
                                    <article class="ac-small">
                                    
                                        <asp:CheckBoxList ID="chkCat" runat="server" OnSelectedIndexChanged="chkCat_SelectedIndexChanged" AutoPostBack="true" ></asp:CheckBoxList>
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
                    	<h6>
                            <a href='<%# "/m/Statutes/" +  clsUtilities.RemoveSpecialChars(Eval("Title").ToString()).Replace(" ","-")+"."+ EncryptDecryptHelper.Encrypt(Eval("ID").ToString()) %>'> <%# EastlawUI_v2.CommonClass.MakeFirstCap((string)Eval("Title")) %></a>
                              <br />        
                                <span style="font-size:8pt">
 <%# Eval("ChildStatutestxt") %></span>
                    	</h6>
                      
                        <div class="result">
                               <%# Eval("FormatedDateAct") %>
                        <br /><br />
                            
                        	
                        </div>
                        <div class="viewMore"><a href='<%# "/m/Statutes/" +  clsUtilities.RemoveSpecialChars(Eval("Title").ToString()).Replace(" ","-")+"."+ EncryptDecryptHelper.Encrypt(Eval("ID").ToString()) %>'>View Full ...</a></div>
                                    <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
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
        </form>
    </asp:Content>