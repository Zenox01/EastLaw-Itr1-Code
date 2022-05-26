<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCMSPages.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManageCMSPages" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
     <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                       Masters
                        <small>CMS</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">CMS Pages</li>
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
                                <h3 class="box-title">Pages Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="form-group">
                                    <label>
                                        Pages: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                     ControlToValidate="ddlPages" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlPages" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPages_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                
                                <div class="form-group">
                                    <label>
                                        Page Title : *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtTitle" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control"> </asp:TextBox>
                                </div>

                                
                                <div class="form-group">
                                    <label>
                                        Keywords :     
                                    </label>
                                    <asp:TextBox ID="txtKeywords" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                
                              
                                <div class="form-group">
                                    <label>
                                       Short Content:         
                                    </label>
                                    <%--<cc1:editor ID="editorShortContent" runat="server"  Height="300px" Width="100%" />--%>
                                     <telerik:RadEditor ID="editorShortContent" Runat="server" Width="100%" Height="450">
                                         <ImageManager ViewPaths="/store/cms/imgs" UploadPaths="/store/cms/imgs" MaxUploadFileSize="1000000" />
    <DocumentManager ViewPaths="/store/cms/docs" UploadPaths="/store/cms/docs" MaxUploadFileSize="10000000"/>
    <MediaManager ViewPaths="/store/cms/videos" UploadPaths="/store/cms/videos" MaxUploadFileSize="10000000"/>
                                    </telerik:RadEditor>
                                </div>
                                <div class="form-group">
                                    <label>
                                      Full Content: 
                                    </label>
                                     
                                     
                                    <%--<cc1:editor ID="editorFullContent" runat="server"  Height="300px" Width="100%" />--%>

                                    <telerik:RadEditor ID="editorFullContent" Runat="server" Width="100%" Height="450">
                                         <ImageManager ViewPaths="/store/cms/imgs" UploadPaths="/store/cms/imgs" MaxUploadFileSize="1000000" />
    <DocumentManager ViewPaths="/store/cms/docs" UploadPaths="/store/cms/docs" MaxUploadFileSize="10000000"/>
    <MediaManager ViewPaths="/store/cms/videos" UploadPaths="/store/cms/videos" MaxUploadFileSize="10000000"/>
                                    </telerik:RadEditor>
                                    <br />

                                    <%--<telerik:RadWindow ID="RadWindow1" runat="server" Width="805" Height="500" VisibleOnPageLoad="true">
<ContentTemplate>--%>
<%--<telerik:RadEditor runat="server" ID="editorJudgment"  Width="950" Height="450" >
</telerik:RadEditor>--%>
<%--</ContentTemplate>
</telerik:RadWindow>--%>

                                                
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
                    <!--/.col (right) -->

                </div>
                

            </section>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


