<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageGlossaryList.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManageGlossaryList" 
    MasterPageFile="~/adminpanel/Site1.Master"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Master
                         <small>Glosssory Tree List</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Glosssory</li>
                    </ol>
                </section>
      
    <section class="content">
        <div class="col-xs-12" style="display:none">

            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Search</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->
                <table style="width:100%">
                    <tr>
                        <td style="width:24%">Category: </td>
                        <td style="width:25%"><asp:DropDownList ID="ddlCat" runat="server" class="form-control"></asp:DropDownList></td>
                        <td style="width:24%">Title: </td>
                        <td style="width:25%"><asp:TextBox ID="txtTitle" runat="server"  class="form-control"> </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:24%">Group: </td>
                        <td style="width:25%"><asp:DropDownList ID="ddlGroup" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList></td>
                        <td style="width:24%">Sub Group: </td>
                        <td style="width:25%"><asp:DropDownList ID="ddlSubGroup" runat="server" class="form-control"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td> </td>
                        <td> </td>
                        <td> </td>
                        <td style="text-align:right">
                             <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPnl">
                                            <ProgressTemplate>
                                                <img src="../images/ajax-loader.gif"  />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                            <asp:Button ID="btnAll" runat="server" Text="Show All" class="btn btn-block btn-primary"  Width="100"   />
                            &nbsp;&nbsp; &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-block btn-primary"  Width="100" />
                            </td>
                    </tr>
                </table>
                

            </div>
            <!-- /.box -->
        </div>
                    <div class="row">
                        <!-- left column -->
                        <!--/.col (left) -->
                        <!-- right column -->
                        <%--<div class="col-md-6">--%> <asp:UpdateProgress ID="upProcessReg" runat="server" AssociatedUpdatePanelID="updPnl">
                                            <ProgressTemplate>
                                                <img src="../images/ajax-loader.gif"  />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                       
                        <div class="col-xs-12">
                            <!-- general form elements disabled -->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Glossary List (<asp:Label ID="lblCount" runat="server"></asp:Label>) </h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                  
                                      



                                  
                                </div><!-- /.box-body -->
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
                                 <telerik:RadTreeView RenderMode="Lightweight" runat="server" ID="tvDept" OnContextMenuItemClick="tvDept_ContextMenuItemClick" >
                              <ContextMenus>
        <telerik:RadTreeViewContextMenu runat="server" ID="Caction" ClickToOpen="True"
            >
            <Items>
                <telerik:RadMenuItem Text="Edit / View" Value="Edit">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Add New" Value="Add New">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Delete" Value="Delete">
                </telerik:RadMenuItem>
               
            </Items>
        </telerik:RadTreeViewContextMenu>
    </ContextMenus>
        </telerik:RadTreeView>
                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"  
                    Width="100%" AllowPaging="True" PageSize="20" class="table table-bordered table-hover"
                     onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound" onrowdeleting="gv_RowDeleting" 
                        onrowediting="gv_RowEditing"  >

                     <Columns>
                        <asp:TemplateField HeaderText="Glossory Name">
                            <ItemTemplate>
                                <asp:Label ID="lblGlossoryName" runat="server" Text='<%# Eval("GlossoryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No. of Keywords Tag">
                            <ItemTemplate>
                                <asp:Label ID="lblKeywordsTag" runat="server" Text='<%# Eval("KeywordsTag") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No. of LawsTag">
                            <ItemTemplate>
                                <asp:Label ID="lblLawsTag" runat="server" Text='<%# Eval("LawsTag") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No. of CitationsTag">
                            <ItemTemplate>
                                <asp:Label ID="lblCitationsTag" runat="server" Text='<%# Eval("CitationsTag") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         
                     
                      
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label ID="lblActive" runat="server" Text='<%# Eval("strActive") %>'></asp:Label>
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
                </asp:GridView>     </div><!-- /.box -->
                        </div><!--/.col (right) -->
                         
                    </div>   
                </section>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



