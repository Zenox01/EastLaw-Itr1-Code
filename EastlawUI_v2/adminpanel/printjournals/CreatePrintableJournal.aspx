<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePrintableJournal.aspx.cs" Inherits="EastlawUI_v2.adminpanel.printjournals.CreatePrintableJournal"
    MasterPageFile="~/adminpanel/Site1.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
   
     <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
            
      <section class="content-header">
                    <h1>
                        Print Journal
                        <small>Add/Edit Print Journal </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Glosssory</li>
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
                                <h3 class="box-title">Journal Detail</h3>
                            </div>
                            
     
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                                
                                <div class="form-group">
                                    <label>
                                        Ref No:
                                               
                                    </label>
                                    <asp:TextBox ID="txtRefno" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Title :
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtTitle" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Password:
                                                
                                    </label>
                                    <asp:TextBox ID="txtPwd" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                               
                                <div class="form-group">
                                    <label>
                                        Citation: *
                                                

                                    </label>
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="txtCitationYear" runat="server" class="form-control" ToolTip="Year"  Width="60" ></asp:TextBox></td>
                                            <td><asp:DropDownList ID="ddlJournals" runat="server" class="form-control" Width="90"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="txtCitationNumber" runat="server" class="form-control" ToolTip="Number" Width="60"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                    
                                    <br />
                                    <asp:Button ID="btnAddCitation" runat="server" Text="Add"  CssClass="btn btn-primary" Width="150" OnClick="btnAddCitation_Click"/>
                                    
                                </div>

                                
                                <div class="form-group">
                                    <label>
                                        Active:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
                                </div>


                                  <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updPnl">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="../media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>

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
                    
                    <div class="col-md-6">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Citations</h3>
                            </div>
                            
     
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                               <asp:GridView ID="gvCitations" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" >
                                        <Columns>
                                        <asp:TemplateField HeaderText="Citation">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                                <asp:HiddenField ID="hdItemID" runat="server" Value='<%# Eval("ItemID") %>' />
                                
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Citation") %>'></asp:Label>
                                                <hr />
                            </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20" />
                            </itemtemplate>
                                        </asp:TemplateField>
                                            </Columns>
                                    </asp:GridView>





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
