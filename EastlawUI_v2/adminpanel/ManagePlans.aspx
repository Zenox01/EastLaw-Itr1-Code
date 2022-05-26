<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagePlans.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManagePlans"
    MasterPageFile="~/adminpanel/Site1.Master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                       Masters
                        <small>Plans Details</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Plans</li>
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
                                    <h3 class="box-title">Plans Details</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                   <div class="form-group">
                                    <label>
                                        Plan Type: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                     ControlToValidate="ddlPlanType" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>
                                     <asp:DropDownList ID="ddlPlanType" runat="server" class="form-control" >
                                         <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                         <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
                                         <asp:ListItem Value="Corporate" Text="Corporate"></asp:ListItem>
                                     </asp:DropDownList>
                                </div>
                                        <div class="form-group">
                                            <label>Plan Name: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtPlanName" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                <asp:Label ID="lblID" runat="server" Visible="false" Text="0"></asp:Label>
                                     </label>
                                            <asp:TextBox ID="txtPlanName" runat="server" class="form-control"> </asp:TextBox>
                                           </div>
                                    <div class="form-group">
                                            <label>No. of days: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="txtNoOfDays" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                
                                     </label>
                                            <asp:TextBox ID="txtNoOfDays" runat="server" class="form-control"> </asp:TextBox>
                                           
                                          <asp:FilteredTextBoxExtender ID="txtFiltered01_FilteredTextBoxExtender" runat="server" Enabled="True" 
                                              FilterType="Numbers" TargetControlID="txtNoOfDays"></asp:FilteredTextBoxExtender>
                                           </div>
                                    <div class="form-group">
                                            <label>No. of Users: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                        ControlToValidate="txtNoOfUsers" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                
                                     </label>
                                            <asp:TextBox ID="txtNoOfUsers" runat="server" class="form-control"> </asp:TextBox>
                                           
                                          <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" 
                                              FilterType="Numbers" TargetControlID="txtNoOfUsers"></asp:FilteredTextBoxExtender>
                                           </div>
                                    <div class="form-group">
                                            <label>Price: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="txtPrice" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                
                                     </label>
                                            <asp:TextBox ID="txtPrice" runat="server" class="form-control"> </asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txtPrice"></asp:FilteredTextBoxExtender>
                                           </div>
                                    <div class="form-group">
                                            <label>Tax: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                        ControlToValidate="txtTax" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                
                                     </label>
                                            <asp:TextBox ID="txtTax" runat="server" class="form-control"> </asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txtTax"></asp:FilteredTextBoxExtender>
                                           </div>
                                     <div class="form-group">
                                            <label>No. of logins per day: * <i>Zero '0' means unlimited</i>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="txtNoOfLoginPerday" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                
                                     </label>
                                            <asp:TextBox ID="txtNoOfLoginPerday" runat="server" class="form-control"> </asp:TextBox>
                                           
                                          <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" 
                                              FilterType="Numbers" TargetControlID="txtNoOfDays"></asp:FilteredTextBoxExtender>
                                           </div>
                                       <div class="form-group">
                                            <label>No. of case view per day: * <i>Zero '0' means unlimited</i>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="txtNoOfCasesPerDay" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                
                                     </label>
                                            <asp:TextBox ID="txtNoOfCasesPerDay" runat="server" class="form-control"> </asp:TextBox>
                                           
                                          <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" 
                                              FilterType="Numbers" TargetControlID="txtNoOfCasesPerDay"></asp:FilteredTextBoxExtender>
                                           </div>
                                     <div class="form-group">
                                            <label>No. of statutes view per day: * <i>Zero '0' means unlimited</i>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="txtnoofstatutesview_perday" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                
                                     </label>
                                            <asp:TextBox ID="txtnoofstatutesview_perday" runat="server" class="form-control"> </asp:TextBox>
                                           
                                          <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True" 
                                              FilterType="Numbers" TargetControlID="txtnoofstatutesview_perday"></asp:FilteredTextBoxExtender>
                                           </div>

                                       
                                            
                                              <div class="form-group">
                                            <label>Active:
                                            </label>
                                                  <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
                                            </div>
                                     <div class="form-group">
                                            <label>Show on Frontend:
                                            </label>
                                                  <asp:CheckBox ID="chkShowFront" runat="server" class="form-control" />
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
                    <h3 class="box-title">Plans List</h3>
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
                        <asp:TemplateField HeaderText="Plan Name">
                            <ItemTemplate>
                                <asp:Label ID="lblPlanName" runat="server" Text='<%# Eval("PlanName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No. of Days">
                            <ItemTemplate>
                                <asp:Label ID="lblNoofDays" runat="server" Text='<%# Eval("NoofDays") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="No. of Users">
                            <ItemTemplate>
                                <asp:Label ID="lblNoOfUsers" runat="server" Text='<%# Eval("NoOfUsers") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Tax">
                            <ItemTemplate>
                                <asp:Label ID="lblTax" runat="server" Text='<%# Eval("Tax") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No. of login/day">
                            <ItemTemplate>
                                <asp:Label ID="lblnooflogin_perday" runat="server" Text='<%# Eval("nooflogin_perday") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No. of Cases View/day">
                            <ItemTemplate>
                                <asp:Label ID="lblnoofcasesview_perday" runat="server" Text='<%# Eval("noofcasesview_perday") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No. of statutes view/day">
                            <ItemTemplate>
                                <asp:Label ID="lblnoofstatutesview_perday" runat="server" Text='<%# Eval("noofstatutesview_perday") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("strActive") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Show Front">
                            <ItemTemplate>
                                <asp:Label ID="lblShowFront" runat="server" Text='<%# Eval("strShowFront") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                    ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" />
                                <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete" Visible="false"
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
    </asp:UpdatePanel>
</asp:Content>

