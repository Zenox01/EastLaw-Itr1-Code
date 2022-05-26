<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageJudges.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManageJudges" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Masters
                        <small>Judges Details</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Judges</li>
                    </ol>
                </section>
      
    <section class="content">
                    <div class="row">
                        <!-- left column -->
                        <!--/.col (left) -->
                        <!-- right column -->
                        <%--<div class="col-md-6">--%>
                            <div class="col-xs-8">
                            <!-- general form elements disabled -->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Judges Details</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                   <div class="form-group">
                                            <label>Current Court:
                                               
                                               
                                     </label>
                                             <asp:DropDownList ID="ddlCourtMaster" runat="server" class="chosen-select"></asp:DropDownList>
                                           </div>
                                        <div class="form-group">
                                            <label>Judge Name: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtName" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                <asp:Label ID="lblID" runat="server" Visible="false" Text="0"></asp:Label>
                                     </label>
                                            <asp:TextBox ID="txtName" runat="server" class="form-control"> </asp:TextBox>
                                           </div>
                                     <div class="form-group">
                                    <label>
                                        Image Upload:
                                         
                                    </label>
                                    
                                   <asp:FileUpload ID="fuploadimage" runat="server" class="form-control" />
                                    <asp:Label ID="lblfuploadWord" runat="server" Visible="false"></asp:Label>
                                    <asp:Image ID="imgFl" runat="server" Height="200" Width="200"/>
                                    
                                </div>
                                   
                                    <div class="form-group">
                                    <label>
                                        Service Start Date:

                                    </label>
                                    <asp:TextBox ID="txtStartDate" runat="server" class="form-control"> </asp:TextBox>
                                     <asp1:CalendarExtender ID="clnStart" runat="server" TargetControlID="txtStartDate"
                                Format="MM/dd/yyyy">
                            </asp1:CalendarExtender>
                                </div>
                                    <div class="form-group">
                                    <label>
                                        Service End Date: 
                                               

                                    </label>
                                    <asp:TextBox ID="txtEndDate" runat="server" class="form-control"> </asp:TextBox>
                                      <asp1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDate"
                                 Format="MM/dd/yyyy">
                            </asp1:CalendarExtender>
                                </div>
                                    <div class="form-group">
                                    <label>
                                        Approve:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkApprove" runat="server" class="form-control" />
                                </div>
                                    <div class="form-group">
                                    <label>
                                        Active:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
                                </div>

                                        <div class="form-group" style="display:none">
                                            <label>Details: 
                                            
                                       </label>
                                            
                                            <cc1:editor ID="Editor1" runat="server"  Height="300px" Width="100%" />
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
                    <h3 class="box-title">Search</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->
                <table style="width:100%">
                     <tr>
                        <td style="width:24%">Court: </td>
                        <td style="width:25%">
                            <asp:DropDownList ID="ddlCourtSearch" runat="server" class="chosen-select"></asp:DropDownList>
                            </td>
                        <td style="width:24%">Judge Name</td>
                        <td style="width:25%"><asp:TextBox ID="txtJudgeNameSearch" runat="server" Width="200"> </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td> &nbsp;</td>
                        <td> </td>
                        <td style="text-align:right">
                             <asp:UpdateProgress ID="upProcessReg" runat="server" AssociatedUpdatePanelID="updPnl">
                                            <ProgressTemplate>
                                                <img src="../images/ajax-loader.gif"  />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                            <asp:Button ID="btnAll" runat="server" Text="Show All" class="btn btn-block btn-primary"  Width="100" OnClick="btnAll_Click1" CausesValidation="false" />
                            &nbsp;&nbsp; &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-block btn-primary" OnClick="btnSearch_Click" Width="100" CausesValidation="false" />
                            </td>
                    </tr>
                </table>
                

            </div>

            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Judges List (<asp:Label ID="lblCount" runat="server"></asp:Label>) </h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->

                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" PageSize="20" AllowCustomPaging="false" class="table table-bordered table-hover"
                     onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound" onrowdeleting="gv_RowDeleting" 
                        onrowediting="gv_RowEditing">
                      <pagersettings mode="NumericFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"  
            position="TopAndBottom" />
                    <Columns>
                        <asp:TemplateField HeaderText="Judge Name">
                            <ItemTemplate>
                                <asp:Label ID="lblJudgeName" runat="server" Text='<%# Eval("JudgeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Current Court">
                            <ItemTemplate>
                                <asp:Label ID="lblCurrentCourt" runat="server" Text='<%# Eval("CourtMaster") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Service Start">
                            <ItemTemplate>
                                <asp:Label ID="lblServiceStart" runat="server" Text='<%# Eval("ServiceStart") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Servie End">
                            <ItemTemplate>
                                <asp:Label ID="lblServieEnd" runat="server" Text='<%# Eval("ServieEnd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approve">
                            <ItemTemplate>
                                <asp:Label ID="lblApprove" runat="server" Text='<%# Eval("Approve") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label ID="lblActive" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <%--<asp:TemplateField Visible="false">
            <ItemTemplate>
                <a class="btn btn-block btn-primary" data-toggle="modal" data-target="#compose-modal<%# Eval("ID") %>"><i class="fa fa-pencil"></i> View Master Links</a>
                <div class="modal fade" id="compose-modal<%# Eval("ID") %>" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><i class="fa fa-user"></i> Juges Master Links</h4>
                    </div>
                    <form action="#" method="post">
                        <div class="modal-body">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><b>Judge Name:</b><%# Eval("JudgeName") %></span>
                                    
                                </div>
                            </div>
                               <span class="input-group-addon">Link(s) to Cases Citations </span><br />
                               <p class="help-block"><%# Eval("CasesCitationsLink") %></p>
                           <div class="form-group">
                                <div class="input-group">
                                 
                                </div>
                            </div>
                           
                           

                        </div>
                        <div class="modal-footer clearfix">

                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i> Discard</button>

                            
                        </div>
                    </form>
                </div><!-- /.modal-content -->
            </div>
        </div>
            </ItemTemplate>
        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                    ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" />
                                <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete"
                                    ImageUrl="~/adminpanel/media/img/Delete.png" Height="16" Width="16" CausesValidation="false" OnClientClick="return confirm('Are you sure?');" />
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:HiddenField ID="hdCurrentCourtMasterID" runat="server" Value='<%# Eval("CurrentCourtMasterID") %>' />
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