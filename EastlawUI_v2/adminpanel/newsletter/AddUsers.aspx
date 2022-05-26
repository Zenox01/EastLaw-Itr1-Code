<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUsers.aspx.cs" Inherits="EastlawUI_v2.adminpanel.AddUsers" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Users
                        <small>User Details</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Users</li>
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
                                <h3 class="box-title">User Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="form-group">
                                    <label>
                                        User Type: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="ddlUserType" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>
                                     <asp:DropDownList ID="ddlUserType" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div id="divCompany" runat="server" style="display:none">

                               
                                <div class="form-group" >
                                    <label>
                                        Company Name: *
                                                 <asp:RequiredFieldValidator ID="rfvddlCompany" runat="server"
                                                     ControlToValidate="ddlCompany" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>

                                    </label>
                                   <asp:DropDownList ID="ddlCompany" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                       Company User Abbrivation: *
                                         <asp:RequiredFieldValidator ID="rfvCompanyAbbr" runat="server"
                                                     ControlToValidate="txtUserAbbriviations" ErrorMessage="Required" ForeColor="Red" Enabled="false" ></asp:RequiredFieldValidator>
                                                
                                    </label>
                                     <asp:TextBox ID="txtUserAbbriviations" runat="server" class="form-control" > </asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>
                                       Add-On Complimentory Users: *
                                         <asp:RequiredFieldValidator ID="rfvCompUser" runat="server"
                                                     ControlToValidate="txtCompUsers" ErrorMessage="Required" ForeColor="Red" Enabled="false" ></asp:RequiredFieldValidator>
                                                
                                    </label>
                                     <asp:TextBox ID="txtCompUsers" runat="server" class="form-control" Text="0"> </asp:TextBox>
                                </div>
                                     </div>
                                <div class="form-group">
                                    <label>
                                        Organization Type: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                     ControlToValidate="ddlOrgType" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlOrgType" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Organization Name:

                                    </label>

                                    <asp:TextBox ID="txtOrgName" runat="server" class="form-control" Text="0"> </asp:TextBox>
                                </div>
                                <div class="form-group" >
                                    <label>
                                        Plan: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="ddlPlan" ErrorMessage="Required" ForeColor="Red" InitialValue="0" ></asp:RequiredFieldValidator>

                                    </label>
                                   <asp:DropDownList ID="ddlPlan" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged"></asp:DropDownList>
                                    Plan No. of days: <asp:Label ID="lblPlanNoOfDays" runat="server" Text="0"></asp:Label>
                                </div>
                                      <div class="form-group">
                                    <label>
                                        Country: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlCountry" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlCountry" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        City: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                     ControlToValidate="ddlCity" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlCity" runat="server" class="form-control">
                                        
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Postal Address:

                                    </label>

                                    <asp:TextBox ID="txtPostalAddress" runat="server" class="form-control" Text="0" TextMode="MultiLine" Height="100"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Email ID <i> (This will be login name and should be unique)</i>: *
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="txtEmailID" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email ID" ControlToValidate="txtEmailID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ></asp:RegularExpressionValidator>
                                         <asp:Label ID="lblExist" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                                
                                    </label>
                                    <asp:TextBox ID="txtEmailID" runat="server" class="form-control" AutoPostBack="True" OnTextChanged="txtEmailID_TextChanged"></asp:TextBox>
                                </div>
                                <div id="divPassword" runat="server">

                                
                                 <div class="form-group">
                                    <label>
                                        Password: *
                                         <asp:RequiredFieldValidator ID="rfvtxtPassword" runat="server"
                                                     ControlToValidate="txtPassword" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                
                                    </label>
                                     <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                       Confirm Password: *
                                         <asp:RequiredFieldValidator ID="rfvtxtConfirmPassword" runat="server"
                                                     ControlToValidate="txtConfirmPassword" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                
                                    <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword" 
                                        ControlToValidate="txtConfirmPassword" ErrorMessage="Password not match" ForeColor="Red"></asp:CompareValidator>
                                                
                                    </label>
                                     <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control" TextMode="Password"> </asp:TextBox>
                                </div>
                                    </div>
                                <div class="form-group">
                                    <label>
                                       Full Name: *
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                     ControlToValidate="txtFullName" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                
                                    </label>
                                     <asp:TextBox ID="txtFullName" runat="server" class="form-control" > </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                       Phone Number: *
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                     ControlToValidate="txtPhone" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                
                                    </label>
                                     <asp:TextBox ID="txtPhone" runat="server" class="form-control" > </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Address: 
                                                
                                    </label>
                                    <asp:TextBox ID="txtAdd" runat="server" class="form-control" TextMode="MultiLine" Height="100"> </asp:TextBox>
                                </div>
                          

                                
                                <div class="form-group">
                                    <label>
                                        No. of PC/Systems Allowed : * (0 means unlimited)
                                                
                                    </label>
                                    <asp:TextBox ID="txtNoOfPCAllowed" runat="server" class="form-control"> </asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txtFiltered01_FilteredTextBoxExtender" runat="server" Enabled="True" 
                                              FilterType="Numbers" TargetControlID="txtNoOfPCAllowed"></asp:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Status: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                     ControlToValidate="ddlStatus" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Approved" Value="Approved" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                        <asp:ListItem Text="Pending - Activation" Value="Pending - Activation"></asp:ListItem>
                                        <asp:ListItem Text="General Block" Value="General Block"></asp:ListItem>
                                        <asp:ListItem Text="Breach of privacy Block" Value="Breach of privacy Block"></asp:ListItem>
                                        <asp:ListItem Text="Non-Payment Block" Value="Non-Payment Block"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Need User Verification ?:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkVerify" runat="server" class="form-control" AutoPostBack="True" OnCheckedChanged="chkVerify_CheckedChanged" />
                                </div>
                                <div class="form-group">
                                    <label>
                                        Active:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
                                </div>
                                <div class="form-group">
                                    <label>
                                        Generate Order & Invoice:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkGenerateOrder" runat="server" class="form-control" />
                                </div>





                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" Width="150" OnClick="btnCancel_Click" />
                                &nbsp;&nbsp;
               
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" Width="150" OnClick="btnSave_Click" />





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
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>