<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageEBookIndexes.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ebook.ManageEBookIndexes"
    MasterPageFile="~/adminpanel/Site1.Master" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <script type="text/javascript">   
function OnClientContextMenuItemClicking(sender, eventArgs)   
{   
    var item = eventArgs.get_menuItem();   
    if (item.get_text() == "Delete Index")
    {   
        if (!confirm("Do you really want to delete the item?"))   
        {   
            eventArgs.set_cancel(true);   
            sender.get_contextMenus()[0].hide();   
        }   
    }   
}   
</script>  
        
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                       E-Book & Statutes
                        <small>E-Book & Statutes Index Details</small>
                    </h1>
                    
                </section>
      
    <section class="content">
                    <div class="row">
                        <!-- left column -->
                        <div class="col-xs-3" >
                            <!-- general form elements disabled -->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Ebook/Statutes Index <asp:Label ID="lblCount" runat="server"></asp:Label> </h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                  
                                  
                                </div><!-- /.box-body -->
                                <div class="alert alert-danger alert-dismissable" id="div1" runat="server" style="display: none">
                                    <button type="button" class="close" data-dismiss="alert">
                                        ×</button>
                                    <strong>Transaction failed ... </strong>
                                </div>
                                <div class="alert alert-info alert-dismissable" id="div2" runat="server" style="display: none">
                                    <button type="button" class="close" data-dismiss="alert">
                                        ×</button>
                                    <strong>Transaction success !</strong>
                                </div>

                                <div style="overflow-y: scroll; height: 400px;">
                                  <telerik:RadTreeView RenderMode="Lightweight" runat="server" ID="tvDept" 
                                      OnNodeClick="tvDept_NodeClick" OnContextMenuItemClick="tvDept_ContextMenuItemClick" OnClientContextMenuItemClicking="OnClientContextMenuItemClicking" AllowNodeEditing="true"  OnNodeEdit="tvDept_NodeEdit" CausesValidation="false">
                                           <ContextMenus>
            <telerik:RadTreeViewContextMenu runat="server" ID="Caction" ClickToOpen="True"
                >
                <Items>
                    <telerik:RadMenuItem Text="Add New Index" Value="NewIndex">
                    </telerik:RadMenuItem>
                    <%-- <telerik:RadMenuItem Text="Edit / View" Value="Edit">
                    </telerik:RadMenuItem>--%>
                    <telerik:RadMenuItem Text="Delete Index" Value="Delete">
                    </telerik:RadMenuItem>
               
                </Items>
            </telerik:RadTreeViewContextMenu>
        </ContextMenus>
                                     
        </telerik:RadTreeView>
                                     
                                    </div>
                                <asp:Button id="btnRefreshIndex" runat="server" CssClass="btn btn-primary" Text="Refresh Index"   Width="150" OnClick="btnRefreshIndex_Click" CausesValidation="false"/>
   </div><!-- /.box -->
                        </div>
                        <!--/.col (left) -->
                        <!-- right column -->
                        
                        <div class="col-md-9" >
                            <!-- general form elements disabled -->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Index Details</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                   <div class="form-group">
                                    <label>
                                        EBook/Statute: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                     ControlToValidate="ddlEBook" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>
                                     <asp:DropDownList ID="ddlEBook" runat="server" class="form-control" >
                                     </asp:DropDownList>
                                </div>
                                        <div class="form-group">
                                            <label>Index Group: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtIndexGroup" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                <asp:Label ID="lblID" runat="server" Visible="false" Text="0"></asp:Label>
                                     </label>
                                            <asp:TextBox ID="txtIndexGroup" runat="server" class="form-control"> </asp:TextBox>
                                           </div>
                                     <div class="form-group">
                                            <label>Index Group Tag: 
                                                
                                                
                                     </label>
                                            <asp:TextBox ID="txtIndexGroupTag" runat="server" class="form-control"> </asp:TextBox>
                                           </div>
                                 <%--   <div class="form-group">
                                            <label>Parent Index: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="txtIndexTitle" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                
                                     </label>
                                            
                                        <asp:DropDownList ID="ddlParentIndex" runat="server" class="form-control">

                                        </asp:DropDownList>
                                           
                                         
                                           </div>--%>
                                    <div class="form-group">
                                            <label>Index Title: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="txtIndexTitle" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                
                                     </label>
                                            <asp:TextBox ID="txtIndexTitle" runat="server" class="form-control"> </asp:TextBox>
                                           
                                         
                                           </div>
                                    <div class="form-group">
                                            <label>Page No: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                        ControlToValidate="txtPageNo" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                
                                     </label>
                                            <asp:TextBox ID="txtPageNo" runat="server" class="form-control"> </asp:TextBox>
                                           
                                          <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" 
                                              FilterType="Numbers" TargetControlID="txtPageNo"></asp:FilteredTextBoxExtender>--%>
                                           </div>
                                    <div class="form-group">
                                            <label>Sort Order: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="txtSortOrder" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                
                                     </label>
                                            <asp:TextBox ID="txtSortOrder" runat="server" class="form-control" Text="0"> </asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txtSortOrder"></asp:FilteredTextBoxExtender>
                                           </div>
                                     <div class="form-group">
                                    <label>
                                        Data Mode: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="ddlDataMode" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>
                                     <asp:DropDownList ID="ddlDataMode" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDataMode_SelectedIndexChanged" >
                                         <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                         <asp:ListItem Value="Content" Text="Content"></asp:ListItem>
                                         <asp:ListItem Value="Word File" Text="Word File"></asp:ListItem>
                                     </asp:DropDownList>
                                </div>
                                    <div class="form-group"  id="divFile" runat="server" style="display:none">
                                    <label>
                                        Word File: *
                                         
                                    </label>
                                    
                                   <asp:FileUpload ID="fuploader" runat="server" class="form-control" />
                                        <asp:Label ID="lblFilename" runat="server"></asp:Label>
                                    
                                </div>
                                     <div class="form-group" id="divContent" runat="server" style="display:none">
                                    <label>
                                       Index Content: 
                                                
                                    </label>
                                    
                                     
                                    <telerik:RadEditor runat="server" ID="editorOIndexContent"  Width="100%" Height="450" >
    <ImageManager ViewPaths="/store/ebook/imgs" UploadPaths="/store/ebook/imgs" MaxUploadFileSize="1000000"/>
    <DocumentManager ViewPaths="/store/ebook/docs" UploadPaths="/store/ebook/docs" MaxUploadFileSize="10000000" />
    <MediaManager ViewPaths="/store/ebook/videos" UploadPaths="/store/ebook/videos" MaxUploadFileSize="10000000"/>
</telerik:RadEditor>
                                </div>
                                
                                  
                                       
                                            
                                              <div class="form-group">
                                            <label>Active:
                                            </label>
                                                  <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
                                            </div>
                                  
                                         
                                     <asp:Button ID="btnCancel" runat="server" Text="Cancel"   CssClass="btn btn-primary" Width="150" OnClick="btnCancel_Click"/>
                &nbsp;&nbsp;
                <asp:Button id="btnSave" runat="server" CssClass="btn btn-primary" Text="Save"   Width="150" OnClick="btnSave_Click"/>

                                   
                                    
                                  
                                     
                                     <div class="alert alert-danger alert-dismissable" id="divError" runat="server" style="display: none">
                            <button type="button" class="close" data-dismiss="alert">
                                ×</button>
                            <strong>Transaction failed ... </strong>
                        </div>
                        <div class="alert alert-info alert-dismissable" id="divSuccess" runat="server" style="display: none">
                            <button type="button" class="close" data-dismiss="alert">
                                ×</button>
                            <strong>Transaction success !</strong>
                        </div>
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->
                        </div><!--/.col (right) -->
                            
                         
                    </div>   
                        <div class="col-xs-12">

            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Index List</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->

                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" PageSize="20" class="table table-bordered table-hover"
                     onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound" onrowdeleting="gv_RowDeleting" 
                        onrowediting="gv_RowEditing">

                    <Columns>
                          <asp:TemplateField HeaderText="Parent Title">
                            <ItemTemplate>
                                <asp:Label ID="lblParentIndex" runat="server" Text='<%# Eval("ParentIndex") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Book Title">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Author">
                            <ItemTemplate>
                                <asp:Label ID="lblAuthor" runat="server" Text='<%# Eval("Author") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Index Group">
                            <ItemTemplate>
                                <asp:Label ID="lblIndexGroup" runat="server" Text='<%# Eval("IndexGroup") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Index Title">
                            <ItemTemplate>
                                <asp:Label ID="lblIndexTitle" runat="server" Text='<%# Eval("IndexTitle") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Page No">
                            <ItemTemplate>
                                <asp:Label ID="lblPageNo" runat="server" Text='<%# Eval("PageNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sort Order">
                            <ItemTemplate>
                                <asp:Label ID="lblSortOrder" runat="server" Text='<%# Eval("SortOrder") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    
                      
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("strActive") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                    ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" />
                                <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete" Visible="true"
                                    ImageUrl="~/adminpanel/media/img/Delete.png" Height="16" Width="16" CausesValidation="false" />
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>



                    </Columns>
                </asp:GridView>
            </div>
            <!-- /.box -->
        </div>
       
                </section>
            
        </ContentTemplate>
        <Triggers>
               <%--<asp:PostBackTrigger ControlID="btnUpload"  />--%>
               <asp:PostBackTrigger ControlID="btnSave" />
           </Triggers>
    </asp:UpdatePanel>
</asp:Content>

