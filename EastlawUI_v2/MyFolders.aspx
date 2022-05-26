<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyFolders.aspx.cs" Inherits="EastlawUI_v2.MyFolders" 
    MasterPageFile="~/MemberMaster.Master"%>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
    <script type="text/javascript">
    function OnClientContextMenuItemClicking(sender, args)
     {
        var menuItem = args.get_menuItem();
        var treeNode = args.get_node();
        menuItem.get_menu().hide();
        switch (menuItem.get_value())
         {
            case "Edit":
                treeNode.startEdit();
                break;
         }
     }
</script>
    <style>
        @media (max-width:768px){

.table-filter tbody tr td{
display:inline-block !important;
}

}
    </style>
    <asp:UpdatePanel ID="upPnl" runat="server">
        <ContentTemplate>

        <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    
    <li><a href="#" class="current">My Documents</a></li>
</ul>
  </div>
</div>
    <div class="container">
        <div class="row">
             <div class="col-lg-3 col-md-3">
    
  
  <div class="panel-group wrap" id="accordion" role="tablist" aria-multiselectable="true">
      <div class="panel">
        <div class="panel-heading" role="tab" id="headingOne">
          <h4 class="panel-title">
        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
          <p class="sr_head">Folders</p>
        </a>
      </h4>
        </div>
        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
          <div class="panel-body panel-body2">
          <div class="scrollbar" id="style-2" style="height:173px;">
      <div class="force-overflow">
        <telerik:RadTreeView RenderMode="Lightweight" runat="server" ID="tvDept" 
                                      OnNodeClick="tvDept_NodeClick" OnContextMenuItemClick="tvDept_ContextMenuItemClick" AllowNodeEditing="true"  OnNodeEdit="tvDept_NodeEdit">
                                           <ContextMenus>
            <telerik:RadTreeViewContextMenu runat="server" ID="Caction" ClickToOpen="True"
                >
                <Items>
                    <telerik:RadMenuItem Text="Add New Folder" Value="NewFolder">
                    </telerik:RadMenuItem>
                     <%--<telerik:RadMenuItem Text="Edit / View" Value="Edit">
                    </telerik:RadMenuItem>--%>
                 <%--   <telerik:RadMenuItem Text="Delete Folder" Value="Delete">
                    </telerik:RadMenuItem>--%>
               
                </Items>
            </telerik:RadTreeViewContextMenu>
        </ContextMenus>
                                     
        </telerik:RadTreeView>
         
          </div></div>
          </div>
        </div>
      </div>
      <!-- end of panel -->

    
      <!-- end of panel -->

      </div>
      </div>
            <div class="col-lg-9 col-md-9 margin_bot_20">
    
   
    
    <div class="clearfix"></div>
    <div id="divFolderItems" runat="server">
                    
                    <asp:Label ID="lblSelectedFolder" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblSelectedFolderID" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                  
                    <%--<asp:Button ID="btnDeleteFolder" runat="server" Text="Delete Folder" Width="100" OnClick="btnDeleteFolder_Click" Visible="false" />--%>
                    <asp:GridView ID="gvLst" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" AllowPaging="true" PageSize="20" 
                        OnPageIndexChanging="gvLst_PageIndexChanging" OnRowDeleting="gvLst_RowDeleting"  CssClass="table table-filter" >
                    <pagersettings mode="NumericFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"  
            position="TopAndBottom" />
                    <pagerstyle cssclass="gridview" >

</pagerstyle>
                    <Columns>
                        <asp:TemplateField ItemStyle-VerticalAlign="Top" >
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("ItemType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="650px">
                            <ItemTemplate>

                           
                          <%--  <div class="rows_ct" style="padding-left:10px">
                	<div class="lft">Date:<strong><%# Eval("JDate") %></strong> </div>
                    <div class="rgt">--%>
                        
                    <%--<strong><a href='<%# "/Cases/" +  Eval("Appeallant").ToString().Replace(" ","-")+"VS"+Eval("Respondent").ToString().Replace(" ","-")+"."+Eval("ID") %>'> <%# Eval("Appeallant") %> VS <%# Eval("Respondent") %></a></strong><br />--%>
                    <%--<%# Eval("Title") %>--%>
                        
                        <strong><a href='<%# "/"+  Eval("ItemType").ToString().Replace("Department","departments")+"/" +  EastlawUI_v2.CommonClass.RemoveSomeCharacters(EastlawUI_v2.CommonClass.GetWords(Eval("Title").ToString(),8).ToString().Replace(" ","-").Replace(".","").Replace("'","").ToString().ToLower())+"."+EncryptDecryptHelper.Encrypt(Eval("ItemID").ToString()) %>'> <%# Eval("Title") %> </a></strong><br />
                        <%# Eval("Court") %>
                        
                             
                   <%-- </div>
                             
                </div>--%>
                                <%--<hr />--%>
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
        
  
    
    
    </div>

        <span style="float:right;display:none" >
        <asp:Button ID="btnAddFolder" runat="server" Text="Add New Folder" CssClass="btnstyle"  Width="130" OnClick="btnAddFolder_Click"  />
            </span>
       
        <div id="divAddFolder" runat="server" style="display:none">
                    <div class="law-forms" style="width:50%">
                	<h1>Manage Folder</h1>
                    
                    
                    <div class="f_row">
                    	
                        	<label><b>Parent Folder *</b> 
                                <asp:RequiredFieldValidator ID="rfvParentFolder" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red" Enabled="false" ControlToValidate="ddlParentFolder"></asp:RequiredFieldValidator>&nbsp;</label><%--<input type="text" value="" />--%>
                           <asp:DropDownList ID="ddlParentFolder" runat="server" AutoPostBack="false" >
                           
                        </asp:DropDownList>
                       
                    </div>
                         <div class="f_row">
                             <label><b>Folder Name *</b> 
                                <asp:RequiredFieldValidator ID="rfvFolderName" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red" Enabled="false" ControlToValidate="txtFolderName"></asp:RequiredFieldValidator></label>
                        	 <asp:TextBox ID="txtFolderName" runat="server" ></asp:TextBox>
                             </div>
                    
                    
                    
                    
                    
                    <div class="f_row3">
                     
                        <span style="float:right">
                               
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100" OnClick="btnCancel_Click"/>
                        <asp:Button ID="btnCreateFolder" runat="server" Text="Save" Width="150" ValidationGroup="A" OnClick="btnCreateFolder_Click" />
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </span>
                        
                    </div>
                     
                    
                </div>
            
                </div>
    <div class="row diff" style="margin:25px 0;display:none">
    
 <%--	<ul class="nav-pills nav-stacked col-md-2 folder">
  <li class="active"><a href="#tab_a" data-toggle="pill"><i class="fa fa-folder"></i> <p>My Folder</p></a></li>
  <li><a href="#tab_b" data-toggle="pill"><i class="fa fa-folder"></i> <p>My Folder</p></a></li>
  <li><a href="#tab_c" data-toggle="pill"><i class="fa fa-folder"></i> <p>New Folder</p></a></li>
  <li><a href="#tab_d" data-toggle="pill"><i class="fa fa-folder"></i> <p>Add Folder</p></a></li>
          
         
</ul>--%>
        <div class="col-md-2 folder">
            <h5>Folders</h5>
                                  
        <%--    <asp:TreeView ID="tvUserFolder" runat="server"  OnSelectedNodeChanged="tvUserFolder_SelectedNodeChanged"
                ExpandImageUrl="/Images/Folder My Documents.png" im
             CollapseImageUrl="/Images/Folder My Documents.png"
                >

            </asp:TreeView>--%>

        </div>
        
<div class="col-lg-9 col-md-9 margin_bot_20">
    <%--<div class="tab-pane">--%>
     
       <%-- </div>--%>

     
</div><!-- tab content -->
    
    	
    </div>  
    </div>
        </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>