<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddGlossary.aspx.cs" Inherits="EastlawUI_v2.adminpanel.AddGlossary"
    MasterPageFile="~/adminpanel/Site1.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
   
     <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
            
      <section class="content-header">
                    <h1>
                        Master
                        <small>Glosssory Tree </small>
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
                                <h3 class="box-title">Create Glosssory Tree</h3>
                            </div>
                            
     
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                                <div class="form-group">
                                    <label>
                                        Type: 

                                    </label>

                                    <asp:DropDownList ID="ddlType" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        <asp:ListItem Value="L1" Text="Level 1"></asp:ListItem>
                                        <asp:ListItem Value="L2" Text="Level 2"></asp:ListItem>
                                        <asp:ListItem Value="L3" Text="Level 3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                 <div class="form-group" id="divLevel1" runat="server" style="display:none">
                                    <label>
                                        Level 1: *
                                                 <asp:RequiredFieldValidator ID="rfvddlLevel1" runat="server"
                                                     ControlToValidate="ddlLevel1" ErrorMessage="Required" ForeColor="Red" InitialValue="0"
                                                     Enabled="false"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlLevel1" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel1_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group" id="divLevel2" runat="server" style="display:none">
                                    <label>
                                        Level 2: *
                                                 <asp:RequiredFieldValidator ID="rfvddlLeve2" runat="server"
                                                     ControlToValidate="ddlLevel2" ErrorMessage="Required" ForeColor="Red" InitialValue="0"
                                                     Enabled="false"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlLevel2" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Title: *
                                                 <asp:RequiredFieldValidator ID="rfvTitle" runat="server"
                                                     ControlToValidate="txtTitle" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Practice Area:

                                    </label>
                                    <asp:DropDownList ID="ddlPA" runat="server" class="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label>
                                        Keywords: *
                                                

                                    </label>
                                    
                                    <asp:DropDownList ID="ddlKeywords" runat="server" class="chosen-select"></asp:DropDownList>
                                    <br /><br />
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary" Width="150"/>
                                    
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
                                        Statutes / Laws: *
                                                

                                    </label>
                                    
                                    <asp:DropDownList ID="ddlLaws" runat="server" class="chosen-select"></asp:DropDownList>
                                    <br /><br />
                                    <asp:Button ID="btnAddLaws" runat="server" Text="Add"  CssClass="btn btn-primary" Width="150" OnClick="btnAddLaws_Click"/>
                                    
                                </div>

                               
                                

                                <div class="form-group">
                                    <label>
                                        Active:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
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
                    <div class="col-md-6">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Keywords</h3>
                            </div>
                            
     
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                               <asp:GridView ID="gvKeywords" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" >
                                        <Columns>
                                        <asp:TemplateField HeaderText="Keyword">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
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
                                
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
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
                    <div class="col-md-6">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Statutes / Laws</h3>
                            </div>
                            
     
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                               <asp:GridView ID="gvLaws" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" >
                                        <Columns>
                                        <asp:TemplateField HeaderText="Statutes / Laws">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
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

                </div>
                

            </section>
          
        </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>


