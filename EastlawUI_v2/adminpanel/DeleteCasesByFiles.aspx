<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteCasesByFiles.aspx.cs" Inherits="EastlawUI_v2.adminpanel.DeleteCasesByFiles"
    MasterPageFile="~/adminpanel/Site1.Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content2" runat="server" contentplaceholderid="headCnt">
    
   </asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
   

     <asp:Image ID="Image1" runat="server" ImageUrl="ImageHandler.ashx" /> 
    <%--<asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>

            <section class="content-header">
                <h1>Migration Utility
                       
                    <small>Cases</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li class="active">Cases Remove</li>
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
                                <h3 class="box-title">Cases Removed By File</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="form-group">
                                    <label>
                                        Journal: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                     ControlToValidate="ddlJournal" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlJournal" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlJournal_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Year: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="ddlYear" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlYear" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" ></asp:DropDownList>
                                </div>
                                
                                 <div class="form-group">
                                    <label>
                                       File Names: *
                                        
                                    </label>
                                     <asp:DropDownList ID="ddlFileName" runat="server" class="form-control" >
                                         
                                     </asp:DropDownList>
                                     
                                </div>
                                </div>
                            
                            <asp:Button ID="btnPublish" runat="server" CssClass="btn btn-warning" Text="Publish Cases" Width="220px" OnClick="btnPublish_Click"  />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnUnPublish" runat="server" CssClass="btn btn-default" Text="Un-Publish Cases" Width="220px" OnClick="btnUnPublish_Click"  />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" Text="Remove Cases From Selected File" Width="220px" OnClick="btnDelete_Click" />
                                
                               




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
                                <!-- /.box-body -->
                            </div>
                            <!-- /.box -->
                        </div>
                        <!--/.col (right) -->

                    </div>
                    <%--<div class="col-xs-12">
                            <!-- general form elements disabled -->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">File Details (No. of record  <asp:Label ID="lblNoOfRecords" runat="server" Text="0"></asp:Label>)</h3>
                                    
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                  
                                      



                                  
                                </div><!-- /.box-body -->

                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="true" 
                    Width="100%" AllowPaging="true" PageSize="8" class="table table-bordered table-hover" OnPageIndexChanging="gv_PageIndexChanging">

                                    </asp:GridView>      </div><!-- /.box -->
                        </div>--%><!--/.col (right) -->
                </div>
            </section>

       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

