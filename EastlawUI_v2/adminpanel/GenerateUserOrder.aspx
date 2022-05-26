<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateUserOrder.aspx.cs" Inherits="EastlawUI_v2.adminpanel.GenerateUserOrder" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <%--    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>
             
      <section class="content-header">
                    <h1>
                        Users
                        <small>Generate Order Details</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Generate Order</li>
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
                                <h3 class="box-title">Generate Order</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="form-group" >
                                    <label>
                                        User Email ID : *
                                         <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                                     ControlToValidate="txtEmailID" ErrorMessage="Required" ForeColor="Red" Enabled="false"  ></asp:RequiredFieldValidator>
                                                
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email ID" ControlToValidate="txtEmailID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ></asp:RegularExpressionValidator>
                                         
                                    </label>
                                    <asp:TextBox ID="txtEmailID" runat="server" class="form-control" ></asp:TextBox>
                                </div>
                                 <div class="form-group" >
                                    <label>
                                        Plan: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlPlan" ErrorMessage="Required" ForeColor="Red" InitialValue="0" ></asp:RequiredFieldValidator>

                                    </label>
                                   <asp:DropDownList ID="ddlPlan" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged"></asp:DropDownList>
                                     Plan No. of days: <asp:Label ID="lblPlanNoOfDays" runat="server" Text="0"></asp:Label>
                                    
                                </div>
                                 <div class="form-group" >
                                    <label>
                                        Amount: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                     ControlToValidate="txtAmt" ErrorMessage="Required" ForeColor="Red" ></asp:RequiredFieldValidator>

                                    </label>
                                   <asp:TextBox ID="txtAmt" runat="server" class="form-control" Text="0"> </asp:TextBox>
                                </div>
                                <div class="form-group" >
                                    <label>
                                        Payment Method: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="ddlPaymentMethod" ErrorMessage="Required" ForeColor="Red" InitialValue="0" ></asp:RequiredFieldValidator>

                                    </label>
                                   <asp:DropDownList ID="ddlPaymentMethod" runat="server" class="form-control">
                                       <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                       <asp:ListItem Text="Bank Transfer" Value="Bank Transfer"></asp:ListItem>
                                       <asp:ListItem Text="Demand Draft / PO / Cross Cheque" Value="Demand Draft / PO / Cross Cheque"></asp:ListItem>
                                       <asp:ListItem Text="EasyPaisa" Value="EasyPaisa"></asp:ListItem>
                                   </asp:DropDownList>
                                    
                                </div>
                                

               
            <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-primary" Text="Generate Order" Width="150" OnClick="btnGenerate_Click" />





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


