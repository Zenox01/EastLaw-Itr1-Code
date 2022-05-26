<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CasesReport.aspx.cs" Inherits="EastlawUI_v2.adminpanel.CasesReport"
    MasterPageFile="~/adminpanel/Site1.Master" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
<%--    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>
             
      <section class="content-header">
                    <h1>
                        Cases
                        <small>Reports Details</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Cases Reports</li>
                    </ol>
                </section>
      
            <section class="content">
                <div class="row">
                    <!-- left column -->
                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-6">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Cases Reports</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                 <div class="form-group">
                                    <label>
                                        Report Type: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="ddlReportType" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlReportType" runat="server" class="form-control">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="Pending Reported Error" Text="Pending Reported Error"></asp:ListItem>
                                        <asp:ListItem Value="Fully Tagged Cases (Without Final Review)" Text="Fully Tagged Casesr (Without Final Review)"></asp:ListItem>
                                        <asp:ListItem Value="Fully Tagged Cases (With Final Review)" Text="Fully Tagged Casesr (With Final Review)"></asp:ListItem>
                                        
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group" style="display:none">
                                    <label>
                                        From Date: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtFromDate" ErrorMessage="Required" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtFromDate" runat="server" class="form-control"> </asp:TextBox>
                                     <asp1:CalendarExtender ID="clnStart" runat="server" TargetControlID="txtFromDate"
                                Format="MM/dd/yyyy">
                            </asp1:CalendarExtender>
                                </div>
                                 <div class="form-group" style="display:none">
                                    <label>
                                        To Date: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="txtToDate" ErrorMessage="Required" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtToDate" runat="server" class="form-control"> </asp:TextBox>
                                      <asp1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                 Format="MM/dd/yyyy">
                            </asp1:CalendarExtender>
                                </div>

               
            <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-primary" Text="Generate Report" Width="150" OnClick="btnGenerate_Click" />





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
                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                    <!--/.col (right) -->

                </div>
                

            </section>
            
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
