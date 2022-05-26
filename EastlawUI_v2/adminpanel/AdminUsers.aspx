<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="EastlawUI_v2.adminpanel.AdminUsers" 
    MasterPageFile="~/adminpanel/Site1.Master"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>
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
                        <section class="col-xs-6">
                        <telerik:RadHtmlChart runat="server" ID="radChartUserLogin"  Width="470px" Height="400px">
                    <PlotArea>
                        <Series>
                            <telerik:BarSeries Name="NoOfLogins" DataFieldY="NoOfLogins">
                                <TooltipsAppearance Color="White" DataFormatString="{0}"></TooltipsAppearance>
                                <LabelsAppearance Visible="false">
                                </LabelsAppearance>
                            </telerik:BarSeries>
                        </Series>
                        <XAxis DataLabelsField="DDate">
                            <MinorGridLines Visible="false"></MinorGridLines>
                            <MajorGridLines Visible="false"></MajorGridLines>
                        </XAxis>
                        <YAxis>
                            <LabelsAppearance DataFormatString="{0}"></LabelsAppearance>
                            <MinorGridLines Visible="false"></MinorGridLines>
                        </YAxis>
                    </PlotArea>
                    <Legend>
                        <Appearance Visible="false"></Appearance>
                    </Legend>
                    <ChartTitle Text="No. of User Logins on this month"></ChartTitle>
                </telerik:RadHtmlChart>
                            </section>
                        <section class="col-xs-6">
                        <telerik:RadHtmlChart runat="server" ID="radChartUserRegistration"  Width="470px" Height="400px">
                    <PlotArea>
                        <Series>
                            <telerik:BarSeries Name="NoOfReg" DataFieldY="NoOfReg">
                                <TooltipsAppearance Color="White" DataFormatString="{0}"></TooltipsAppearance>
                                <LabelsAppearance Visible="false">
                                </LabelsAppearance>
                                <Appearance>
                                    <FillStyle BackgroundColor="Green" />
                                </Appearance>
                            </telerik:BarSeries>
                        </Series>
                        <XAxis DataLabelsField="DDate">
                            <MinorGridLines Visible="false" Color="Green"></MinorGridLines>
                            <MajorGridLines Visible="false"></MajorGridLines>
                        </XAxis>
                        <YAxis>
                            <LabelsAppearance DataFormatString="{0}"></LabelsAppearance>
                            <MinorGridLines Visible="false"></MinorGridLines>
                        </YAxis>
                    </PlotArea>
                    <Legend>
                        <Appearance Visible="false"></Appearance>
                    </Legend>
                    <ChartTitle Text="No. of User Registered on this month"></ChartTitle>
                </telerik:RadHtmlChart>
                            </section>
                       <section class="col-xs-12">
                            <!-- Map box -->
                            <div class="box box-primary">
                                <div class="box-header">
                                    
                                    <h3 class="box-title">
                                        Pending Orders
                                    </h3>
                                </div>
                                <div class="box-body no-padding">
                                     
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
                                    <asp:GridView ID="gvPendingInvoices" runat="server" class="table table-bordered table-hover"
                                        Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageIndex="15" OnRowEditing="gvPendingInvoices_RowEditing" OnPageIndexChanging="gvPendingInvoices_PageIndexChanging" OnRowDeleting="gvPendingInvoices_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Order #">
                            <ItemTemplate>
                                <asp:Label ID="lblOrderNo" runat="server" Text='<%# Eval("OrdNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice #">
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                         
                                            <asp:TemplateField HeaderText="Full Name">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Email ID">
                            <ItemTemplate>
                                <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("EmailID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile No">
                            <ItemTemplate>
                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("PhoneNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Plan">
                            <ItemTemplate>
                                <asp:Label ID="lblPlanName" runat="server" Text='<%# Eval("PlanName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Plan Days">
                            <ItemTemplate>
                                <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Eval("NoOfDays") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Plan Price">
                            <ItemTemplate>
                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Method">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentMethod" runat="server" Text='<%# Eval("PaymentMethod") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:BoundField HeaderText="Date" DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" />
                                             <asp:TemplateField HeaderText="View Invoice">
                            <ItemTemplate>
                                <a href='<%# "../store/users/invoices/INV_"+Eval("ID")+".pdf" %>' target="_blank">View</a>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Reject">
                            <ItemTemplate>
                               <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-primary" Width="100" CommandName="Delete"/>
                                
                                
                            </ItemTemplate>
                        </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Update">
                            <ItemTemplate>
                               <asp:Button ID="btnUpdateStatus" runat="server" Text="Update" CssClass="btn btn-primary" Width="100" CommandName="Edit"/>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:HiddenField ID="hdUserID" runat="server" Value='<%# Eval("UserID") %>' />
                                <asp:HiddenField ID="hdPlanID" runat="server" Value='<%# Eval("PlanID") %>' />
                                
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
                        
                        <!-- right col -->
                    </div><!-- /.row (main row) -->

                </section><!-- /.content -->
</asp:Content>


