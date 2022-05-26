<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageDepartmentFile.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManageDepartmentFile" 
    MasterPageFile="~/adminpanel/Site1.Master"%>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
     <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Departments
                        <small>Departments File</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Departments File </li>
                    </ol>
                </section>
      
            <section class="content">
                <div class="row">
                    <!-- left column -->
                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-xs-12">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Departments File Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                                
                                <div class="form-group">
                                    <label>
                                        Title : *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtTitle" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control"> </asp:TextBox>
                                    <asp:TextBox ID="txtDeptID" runat="server" class="form-control" Visible="false" Text="0"> </asp:TextBox>
                                    <asp:Label ID="lblID" runat="server" Visible="false" Text="0"></asp:Label>
                                </div>
                                   
                                <div class="form-group">
                                    <label>
                                        Year : 
                                    </label>
                                    <asp:TextBox ID="txtYear" runat="server" class="form-control"> </asp:TextBox>
                                </div
                                <div class="form-group">
                                    <label>
                                        No : 
                                    </label>
                                    <asp:TextBox ID="txtNo" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Date : 
                                    </label>
                                    <asp:TextBox ID="txtDate" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Type : 
                                    </label>
                                    <asp:TextBox ID="txtType" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                              
                                <div class="form-group">
                                    <label>
                                       Index Content:         
                                    </label>
                                    <%--<cc1:editor ID="editorContent" runat="server"  Height="300px" Width="100%" />--%>

                                    <telerik:RadEditor runat="server" ID="editorContent"  Width="100%" Height="450" >
    <ImageManager ViewPaths="/store/departmentsfiles/imgs" UploadPaths="/store/departmentsfiles/imgs" MaxUploadFileSize="1000000"/>
    <DocumentManager ViewPaths="/store/departmentsfiles/docs" UploadPaths="/store/departmentsfiles/docs" MaxUploadFileSize="10000000" />
    <MediaManager ViewPaths="/store/departmentsfiles/videos" UploadPaths="/store/departmentsfiles/videos" MaxUploadFileSize="10000000"/>
</telerik:RadEditor>
                                </div>
                                
                                
                                
                                
                                 
                                

                                <div class="form-group">
                                    <label>
                                        Active:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Date Formated 
                                         
                                    </label>
                                    
                                    <asp:CheckBox ID="chkDateFormated" runat="server" Checked="true" />
                                </div>
                                <asp:Label ID="lblDeptFileAdd" runat="server"></asp:Label>


                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" Width="150" OnClick="btnCancel_Click" />
                                &nbsp;&nbsp;
               <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" Width="150" OnClientClick = " return confirm('Are you sure you want to delete this record. ?');" OnClick="btnDelete_Click"/>
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



