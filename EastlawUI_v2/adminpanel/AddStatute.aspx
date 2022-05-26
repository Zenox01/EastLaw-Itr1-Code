<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStatute.aspx.cs" Inherits="EastlawUI_v2.adminpanel.AddStatute" 
    MasterPageFile="~/adminpanel/Site1.Master"%>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
<%--    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>
             
      <section class="content-header">
                    <h1>
                        Statutes
                        <small>Statutes Details</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Statutes</li>
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
                                <h3 class="box-title">Statutes Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                 <div class="form-group">
                                    <label>
                                        Type: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="ddlType" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:DropDownList ID="ddlType" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        <asp:ListItem Value="N/A" Text="N/A" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="PRIMARY" Text="PRIMARY"></asp:ListItem>
                                        <asp:ListItem Value="SECONDARY" Text="SECONDARY"></asp:ListItem>
                                        <asp:ListItem Value="Amendment Act" Text="Amendment Act"></asp:ListItem>
                                        <asp:ListItem Value="Version" Text="Version"></asp:ListItem>
                                        <asp:ListItem Value="Bills" Text="Bills"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group" runat="server" id="divPrimaryStatutes" style="display:none">
                                    <label>
                                        Primary Statutes: *
                                                 <asp:RequiredFieldValidator ID="rfvPS" runat="server"
                                                     ControlToValidate="ddlPrimaryStatutes" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:DropDownList ID="ddlPrimaryStatutes" runat="server" class="form-control">
                                       <asp:ListItem Value="0" Text="N/A" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                       Main Categories: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="ddlCategories" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:DropDownList ID="ddlCategories" runat="server" class="chosen-select">
                                       
                                    </asp:DropDownList>
                                </div>
                              

                                <div class="form-group">
                                    <label>
                                       Other  Categories: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                     ControlToValidate="ddlOtherCategories" ErrorMessage="Required" ForeColor="Red" InitialValue="0" ValidationGroup="OtherCat"></asp:RequiredFieldValidator>
                                    </label>
                                    
                                    <asp:DropDownList ID="ddlOtherCategories" runat="server" class="chosen-select" ValidationGroup="OtherCat"></asp:DropDownList>
                                    <br /><br />
                                    <asp:Button ID="btnOtherCategories" runat="server" Text="Add"  CssClass="btn btn-primary" ValidationGroup="OtherCat" Width="150" OnClick="btnOtherCategories_Click" />
                                    
                                </div>
                                <div class="form-group">
                                    <label>
                                       Title: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtTitle" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                 <div class="form-group" style="display:none">
                                    <label>
                                       Title Variation 1:

                                    </label>
                                    <asp:TextBox ID="txtTitaleVariation1" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group" style="display:none">
                                    <label>
                                       Title Variation 2:

                                    </label>
                                    <asp:TextBox ID="txtTitaleVariation2" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group" style="display:none">
                                    <label>
                                       Title Variation 3:

                                    </label>
                                    <asp:TextBox ID="txtTitaleVariation3" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>
                                       Date: 

                                    </label>
                                    <asp:TextBox ID="txtDate" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>
                                       Act: 

                                    </label>
                                    <asp:TextBox ID="txtAct" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Group: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlGroup" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlGroup" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Group Sub Group: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                     ControlToValidate="ddlSubGroup" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlSubGroup" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                
                                <div class="form-group">
                                    <label>
                                        Word File:
                                         
                                    </label>
                                    
                                   <asp:FileUpload ID="fuploadWord" runat="server" class="form-control" />
                                    <asp:Label ID="lblfuploadWord" runat="server" Visible="false"></asp:Label>
                                    <asp:HyperLink ID="hypLnkWord" runat="server" Target="_blank"></asp:HyperLink>
                                    
                                </div>
                                <div class="form-group">
                                    <label>
                                        PDF File: *
                                         
                                    </label>
                                    
                                   <asp:FileUpload ID="fuploadPDF" runat="server" class="form-control" />
                                    <asp:Label ID="lblfuploadPDF" runat="server" Visible="false"></asp:Label>
                                    <asp:HyperLink ID="hypLnkPDF" runat="server" Target="_blank"></asp:HyperLink>
                                    
                                </div>
                                <div class="form-group">
                                    <label>
                                        Practice Area: *
                                         
                                    </label>
                                    
                                  <asp:CheckBoxList ID="chkLstPA" runat="server" RepeatColumns="2"></asp:CheckBoxList>
                                    
                                </div>
                                  <div class="form-group">
                                    <label>
                                       Last Document Update Date:  (DD/MM/YYYY)

                                    </label>
                                    <asp:TextBox ID="txtLastDoumentUpdateDate" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Active:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
                                </div>




                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" Width="150"  />
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
                                <h3 class="box-title">Other Categories</h3>
                            </div>
                            
     
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                               <asp:GridView ID="gvOtherCat" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" >
                                        <Columns>
                                        <asp:TemplateField HeaderText="Category Name">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("OtherCategoryID") %>' />
                                
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("OtherCategoryName") %>'></asp:Label>
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
            
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
