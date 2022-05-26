<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageDepartments.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManageDepartments"
    MasterPageFile="~/adminpanel/Site1.Master" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cntplc">
    <%--    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>

    <section class="content-header">
        <h1>Departments
                         <small>Departments List</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Departments</li>
        </ol>
    </section>

    <section class="content">

        <div class="row">
            <!-- left column -->
            <!--/.col (left) -->
            <!-- right column -->
            <%--<div class="col-md-6">--%>
            <%-- <asp:UpdateProgress ID="upProcessReg" runat="server" AssociatedUpdatePanelID="updPnl">
                                            <ProgressTemplate>
                                                <img src="../images/ajax-loader.gif"  />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>--%>

            <div class="col-xs-4">
                <!-- general form elements disabled -->
                <div class="box box-warning">
                    <div class="box-header">
                        <h3 class="box-title">Departments List (<asp:Label ID="lblCount" runat="server"></asp:Label>) </h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                    </div>
                    <!-- /.box-body -->
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


                    <telerik:RadTreeView RenderMode="Lightweight" runat="server" ID="tvDept" OnNodeClick="tvDept_NodeClick" OnContextMenuItemClick="tvDept_ContextMenuItemClick">
                        <ContextMenus>
                            <telerik:RadTreeViewContextMenu runat="server" ID="Caction" ClickToOpen="True">
                                <Items>
                                    <telerik:RadMenuItem Text="Upload" Value="Upload">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem Text="View Files" Value="View Files">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem Text="Add New" Value="Add New">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem Text="Edit / View" Value="Edit">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem Text="Delete" Value="Delete">
                                    </telerik:RadMenuItem>

                                </Items>
                            </telerik:RadTreeViewContextMenu>
                        </ContextMenus>

                    </telerik:RadTreeView>

                    <asp:TreeView ID="tv" runat="server" OnSelectedNodeChanged="tv_SelectedNodeChanged">
                    </asp:TreeView>


                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                        Width="100%" AllowPaging="True" PageSize="20" class="table table-bordered table-hover"
                        OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting"
                        OnRowEditing="gv_RowEditing">

                        <Columns>
                            <asp:TemplateField HeaderText="Title">
                                <ItemTemplate>
                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDDate" runat="server" Text='<%# Eval("DDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblDType" runat="server" Text='<%# Eval("DType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                        ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" />
                                    <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete"
                                        ImageUrl="~/adminpanel/media/img/Delete.png" Height="16" Width="16" CausesValidation="false" />
                                    <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>




                        </Columns>
                    </asp:GridView>
                </div>
                <!-- /.box -->
            </div>
            <!--/.col (right) -->
            <div class="col-md-8" runat="server" id="divFileUpload" style="display: none">
                <!-- general form elements disabled -->
                <div class="box box-warning">
                    <div class="box-header">
                        <h3 class="box-title">Upload Files</h3>
                    </div>


                    <!-- /.box-header -->
                    <div class="box-body">

                        <div class="form-group">
                            <label>
                                Title: 
                                         
                            </label>
                            <asp:TextBox ID="txtTitle" runat="server" class="form-control"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <label>
                                Year
                                         
                            </label>
                            <asp:TextBox ID="txtYear" runat="server" class="form-control"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <label>
                                Date:  (dd/mm/yyyy)
                                         
                            </label>
                            <asp:TextBox ID="txtDate" runat="server" class="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regexpName" runat="server"
                                ErrorMessage="Invalid Format."
                                ControlToValidate="txtDate" ForeColor="Red"
                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" />
                        </div>
                        <div class="form-group">
                            <label>
                                No: 
                                         
                            </label>
                            <asp:TextBox ID="txtNo" runat="server" class="form-control"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <label>
                                Type 
                                         
                            </label>
                            <%--<asp:TextBox ID="txtType" runat="server" class="form-control"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlDeptTypeGroups" runat="server" class="form-control"></asp:DropDownList>

                        </div>
                        <div class="form-group">
                            <label>
                                Date Formated 
                                         
                            </label>

                            <asp:CheckBox ID="chkDateFormated" runat="server" Checked="true" />
                        </div>
                        <div class="form-group">
                            <label>
                                File Type 
                                         
                            </label>
                            <%--<asp:TextBox ID="txtType" runat="server" class="form-control"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlFileType" runat="server" class="form-control">
                                <asp:ListItem Value="Word" Text="Word"></asp:ListItem>
                                <asp:ListItem Value="PDF" Text="PDF"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="form-group">
                            <label>
                                Word / PDF File: *
                                         
                            </label>

                            <asp:FileUpload ID="fuploader" runat="server" class="form-control" />
                            <br />
                            <br />
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" Width="150" OnClick="btnAdd_Click" />
                            <asp:Label ID="lblDeptFileAdd" runat="server"></asp:Label>

                        </div>



                    </div>

                </div>
                <!-- /.box-body -->
            </div>
            <div class="col-md-8">
                <!-- general form elements disabled -->
                <div class="box box-warning">
                    <div class="box-header">
                        <h3 class="box-title">Files</h3>
                    </div>
                    <div>
                        <h2>Search</h2>
                        <table width="100%">
                            <tr>
                                <td>Title:</td>
                                <td>
                                    <asp:TextBox ID="txtSTitle" runat="server"></asp:TextBox></td>
                                <td>Type:</td>
                                <td>
                                    <asp:DropDownList ID="ddlSType" runat="server">
                                    </asp:DropDownList>
                            </tr>
                            <tr>
                                <td>Number:</td>
                                <td>
                                    <asp:TextBox ID="txtSNumber" runat="server"></asp:TextBox></td>
                                <td>Year:</td>
                                <td>
                                    <asp:DropDownList ID="ddlSYear" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: right"></td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" /></td>

                            </tr>
                        </table>
                    </div>

                    <!-- /.box-header -->
                    <div class="box-body">

                        <asp:GridView ID="gvFile" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowDeleting="gvFile_RowDeleting" OnRowEditing="gvFile_RowEditing">
                            <Columns>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                        <hr />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDDate" runat="server" Text='<%# Eval("DDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDType" runat="server" Text='<%# Eval("DType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="File">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                               
                                                
                                
                                                
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("WordFile") %>'></asp:Label>
                                                <hr />
                            </itemtemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="View">
                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <a href='<%# "../store/departments/wordfiles/"+Eval("WordFile") %>' target="_blank">View</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                            ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" />
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Visible="false" Width="20" Height="20" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>





                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </div>


        </div>
    </section>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>




