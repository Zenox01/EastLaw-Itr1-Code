<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminMain.aspx.cs" Inherits="EastlawUI_v2.adminpanel.AdminMain" 
    MasterPageFile="~/adminpanel/Site1.Master"%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <section class="content-header">
                    <h1>
                        Dashboard
                        <small>Control panel</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Dashboard</li>
                    </ol>
                </section>



                <!-- Main content -->
                <section class="content">

                    

                    <!-- top row -->
                    <div class="row">
                        <div class="col-xs-12 connectedSortable">
                            
                        </div><!-- /.col -->
                    </div>
                    <!-- /.row -->

                    <!-- Main row -->
                    <div class="row">
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
                        <section class="col-xs-12">
                            <!-- Map box -->
                            <div class="box box-primary">
                                <div class="box-header">
                                    
                                    <h3 class="box-title">
                                        Missing Data Report
                                    </h3>
                                </div>
                                <div class="box-body no-padding">
                                     
                                     <div class="alert alert-danger alert-dismissable" id="div3" runat="server" style="display: none">
                            <button type="button" class="close" data-dismiss="alert">
                                ×</button>
                            <strong>Transaction failed ... </strong>
                        </div>
                        <div class="alert alert-info alert-dismissable" id="div4" runat="server" style="display: none">
                            <button type="button" class="close" data-dismiss="alert">
                                ×</button>
                            <strong>Transaction success !</strong>
                        </div>
                                    <asp:GridView ID="gvMissingDataGeneral" runat="server" class="table table-bordered table-hover"
                                        Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageIndex="7" OnRowEditing="gvMissingDataGeneral_RowEditing" OnPageIndexChanging="gvMissingDataGeneral_PageIndexChanging">
                                        <Columns>
                                             <asp:TemplateField HeaderText="Reported On">
                            <ItemTemplate>
                                <asp:Label ID="lblFCreatedOn" runat="server" Text='<%# Eval("FCreatedOn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Error Reported">
                            <ItemTemplate>
                                <asp:Label ID="lblComment" runat="server" Text='<%# Eval("UserComment") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="User">
                            <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserFullName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("ItemType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            
                                             
                                            <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlWorkflow" runat="server">
                                    <asp:ListItem Value="1" Text="Pending"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Approve"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Rejected"></asp:ListItem>
                                </asp:DropDownList>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Admin Comments">
                            <ItemTemplate>
                               <asp:TextBox ID="txtAdminComments" runat="server" TextMode="MultiLine"></asp:TextBox>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Update Status">
                            <ItemTemplate>
                               <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" CssClass="btn btn-primary" Width="100" CommandName="Edit"/>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                                             
                                        </Columns>
                                        <RowStyle CssClass="table table-striped" />
                                    </asp:GridView>
                                    
                                </div><!-- /.box-body-->
                                <%--<div class="box-footer">
                                    <button class="btn btn-info"><i class="fa fa-download"></i> Generate PDF</button>
                                    <button class="btn btn-warning"><i class="fa fa-bug"></i> Report Bug</button>
                                </div>--%>
                            </div>
                            <!-- /.box -->

                          

                           

                        </section>
                       <section class="col-xs-4" style="display:none">
                            <!-- Map box -->
                            <div class="box box-primary">
                                <div class="box-header">
                                    
                                    <h3 class="box-title">
                                        Pending Users Comments
                                    </h3>
                                </div>
                                <div class="box-body no-padding">
                                     
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
                                    <asp:GridView ID="gvPendingUsersComments" runat="server" class="table table-bordered table-hover"
                                        Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageIndex="15" OnRowEditing="gvPendingUsersComments_RowEditing" OnPageIndexChanging="gvPendingUsersComments_PageIndexChanging">
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="User">
                            <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                         
                                            <asp:TemplateField HeaderText="Citation">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Citation") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <asp:Label ID="lblComment" runat="server" Text='<%# Eval("UserComment") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="View">
                            <ItemTemplate>
                                <a href='<%# Eval("CustomLink") %>' target="_blank">View</a>
                                
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlWorkflow" runat="server">
                                    <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
                                    <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                    <asp:ListItem Value="Rejected" Text="Rejected"></asp:ListItem>
                                </asp:DropDownList>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Update Status">
                            <ItemTemplate>
                               <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" CssClass="btn btn-primary" Width="100" CommandName="Edit"/>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                                             
                                        </Columns>
                                        <RowStyle CssClass="table table-striped" />
                                    </asp:GridView>
                                    
                                </div><!-- /.box-body-->
                                <%--<div class="box-footer">
                                    <button class="btn btn-info"><i class="fa fa-download"></i> Generate PDF</button>
                                    <button class="btn btn-warning"><i class="fa fa-bug"></i> Report Bug</button>
                                </div>--%>
                            </div>
                            <!-- /.box -->

                          

                           

                        </section>
                        <!-- right col (We are only adding the ID to make the widgets sortable)-->
                        <section class="col-xs-12">
                            <!-- Map box -->
                            <div class="box box-primary">
                                <div class="box-header">
                                    
                                    <h3 class="box-title">
                                        Reported Errors
                                    </h3>
                                </div>
                                 <asp:UpdatePanel ID="upPnlSearch" runat="server">
                        <ContentTemplate>
                                <div class="box-body no-padding">

                                     <div class="col-xs-4">
                         <div class="box box-warning">

                            
                            <div class="box-header">
                                <h3 class="box-title">Search Citation</h3>
                            </div>
                             <div class="box-body">
                                 
                                 <div class="form-group">
                                    <label>
                                        Citation: *
                                        
                                    </label>
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="txtrepotedErroSearchYear" runat="server" class="form-control" ToolTip="Year"  Width="60" placeholder="Year" ></asp:TextBox></td>
                                            <td><asp:DropDownList ID="ddlrepotedErroSearchJournal" runat="server" class="form-control" Width="90"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="txtrepotedErroSearchPageNo" runat="server" class="form-control" ToolTip="Number" Width="60" placeholder="No."></asp:TextBox></td>
                                        </tr>
                                    </table>
                                            
                                </div>
                                  
                                  <asp:Button ID="btnRepotedErroSearch" runat="server" CssClass="btn btn-primary" Text="Search" Width="150" CausesValidation="false" OnClick="btnRepotedErroSearch_Click"  />
                                 
                                
               <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upPnlSearch">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                 
                                 </div>
                             </div>
                        </div>
                            
                                  
                                    <asp:GridView ID="gvPendingReportError" runat="server" class="table table-bordered table-hover"
                                        Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageIndex="7" OnRowEditing="gvPendingReportError_RowEditing" OnPageIndexChanging="gvPendingReportError_PageIndexChanging">
                                        <Columns>
                                             <asp:TemplateField HeaderText="Reported On">
                            <ItemTemplate>
                                <asp:Label ID="lblFCreatedOn" runat="server" Text='<%# Eval("FCreatedOn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("ItemType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Error Reported">
                            <ItemTemplate>
                                <asp:Label ID="lblComment" runat="server" Text='<%# Eval("UserComment") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User">
                            <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserFullName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                          
                                            
                                         
                                            <asp:TemplateField HeaderText="View">
                            <ItemTemplate>
                                <a href='<%# Eval("CustomLink") %>' target="_blank">View</a>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlWorkflow" runat="server">
                                    <asp:ListItem Value="1" Text="Pending"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Approve"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Rejected"></asp:ListItem>
                                </asp:DropDownList>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Admin Comments">
                            <ItemTemplate>
                               <asp:TextBox ID="txtAdminComments" runat="server" TextMode="MultiLine"></asp:TextBox>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Update Status">
                            <ItemTemplate>
                               <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" CssClass="btn btn-primary" Width="100" CommandName="Edit"/>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                                             
                                        </Columns>
                                        <RowStyle CssClass="table table-striped" />
                                    </asp:GridView>
                                     </ContentTemplate>
                    </asp:UpdatePanel>
                                </div><!-- /.box-body-->
                                <%--<div class="box-footer">
                                    <button class="btn btn-info"><i class="fa fa-download"></i> Generate PDF</button>
                                    <button class="btn btn-warning"><i class="fa fa-bug"></i> Report Bug</button>
                                </div>--%>
                            </div>
                            <!-- /.box -->

                          

                           

                        </section><!-- right col -->
                    </div><!-- /.row (main row) -->

                </section><!-- /.content -->
</asp:Content>

