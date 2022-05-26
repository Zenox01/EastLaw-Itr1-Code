<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyFolder.aspx.cs" Inherits="EastlawUI_v2.m.MyFolder"
    MasterPageFile="~/m/MemberMaster.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <form runat="server">
    
<div class="contentPage">
	<h1>
    <div class="container">
        <div class="margin">
            My Folders
        </div>
        
    </div>
    </h1>

	<div class="buttonRow">
    	<div class="container">
        	<div class="margin">
            	<%--<a href="#" class="redBtn">Add New Folder</a>--%>
                <asp:Button ID="btnAddFolder" runat="server" Text="Add New Folder" class="redBtn"   OnClick="btnAddFolder_Click"  />
                <%--<a href="#" class="redBtn">Edit Folder</a>
                <a href="#" class="redBtn">Delete Folder</a>--%>
                <asp:Button ID="btnDeleteFolder" runat="server" Text="Delete Folder" class="redBtn"  OnClick="btnDeleteFolder_Click" Visible="false" />
            </div>
        </div>
    </div>	
    
   <div class="resources grayBg mrBtm" id="divAddFolder" runat="server" style="display:none">
       <div class="container">
            <div class="margin">
                
                <div class="adSearch">
                	<div class="row">
                        <div class="find_legislation">
                        	
                            <div class="lft">
                                Parent Folder *
                                <asp:RequiredFieldValidator ID="rfvParentFolder" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red" Enabled="false" ControlToValidate="ddlParentFolder"></asp:RequiredFieldValidator>&nbsp;</label><%--<input type="text" value="" />--%>
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                    <%--<input type="text" class="input1" value="Acts, Ordinances, Rules, Regulations" class="srch" />--%>
                                    <asp:DropDownList ID="ddlParentFolder" runat="server" class="input1"  >
                           
                        </asp:DropDownList>
                                    
                                </div>
                                
                            </div>
                            
                            
                        </div>
                        
                        <div class="find_legislation">
                        	
                            <div class="lft">
                               Folder Name * 
                                <asp:RequiredFieldValidator ID="rfvFolderName" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red" Enabled="false" ControlToValidate="txtFolderName"></asp:RequiredFieldValidator>
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                     <asp:TextBox ID="txtFolderName" runat="server" class="input1" ></asp:TextBox>
                                    
                                </div>
                                <div class="searchBtn3"><%--<input name="Search" type="button" value="Search">--%>
                                

                                       <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="input3" Width="100" OnClick="btnCancel_Click"/>
                        <asp:Button ID="btnCreateFolder" runat="server" Text="Save"  class="input3" Width="150" ValidationGroup="A" OnClick="btnCreateFolder_Click" />
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            
                            
                        </div>
                    </div>
                </div>
                
            </div>
            
        </div>
        <div class="clear"></div>
   </div> 
    <div id="divFolderItems" runat="server" class="dictionary">
                    
                    <asp:Label ID="lblSelectedFolder" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblSelectedFolderID" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Delete Folder" Width="100" OnClick="btnDeleteFolder_Click" Visible="false" />
                    <asp:GridView ID="gvLst" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" AllowPaging="true" PageSize="20" 
                        OnPageIndexChanging="gvLst_PageIndexChanging" OnRowDeleting="gvLst_RowDeleting"  >
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
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("ItemType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>

                           
                            <div class="rows_ct" style="padding-left:10px">
                	<%--<div class="lft">Date:<strong><%# Eval("JDate") %></strong> </div>--%>
                    <div class="rgt">
                        
                    <%--<strong><a href='<%# "/Cases/" +  Eval("Appeallant").ToString().Replace(" ","-")+"VS"+Eval("Respondent").ToString().Replace(" ","-")+"."+Eval("ID") %>'> <%# Eval("Appeallant") %> VS <%# Eval("Respondent") %></a></strong><br />--%>
                    <%--<%# Eval("Title") %>--%>

                        <strong><a href='<%# "/"+  Eval("ItemType").ToString()+"/" + clsUtilities.RemoveSpecialCharacter( EastlawUI_v2.CommonClass.GetWords(Eval("Title").ToString(),6).ToString().Replace(" ","-")).ToString()+"."+EncryptDecryptHelper.Encrypt(Eval("ItemID").ToString()) %>'> <%# Eval("Title") %> </a></strong><br />
                        <%# Eval("Court") %>
                        
                             
                    </div>
                             
                </div>
                                <hr />
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete"
                                    ImageUrl="/adminpanel/media/img/Delete.png" Height="16" Width="16" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                </div>
	<div class="clear"></div>
    <div class="folder">
    	<div class="container">
        	<div class="margin" id="divParentFolders">
            <%
                EastLawBL.Users objusr = new EastLawBL.Users();
                System.Data.DataTable dtParentFolder = new System.Data.DataTable();
                dtParentFolder = objusr.GetUserParentFolderByUser(int.Parse(Session["MemberID"].ToString()));
                for (int a = 0; a < dtParentFolder.Rows.Count; a++)
                {
                    Response.Write("<div class='cols'><span>" + dtParentFolder.Rows[a]["FolderName"].ToString() + "</span></div>"); 
                }
                  
            %>
            	<%--<div class="cols">
                	<span>
                    	Folder1
                    </span>
                </div>--%>
                
              



            	


                
            </div>
        </div>
    </div>	


    
</div>
        </form>
</asp:Content>


