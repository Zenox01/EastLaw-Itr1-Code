<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebScrapStatutes.aspx.cs" Inherits="EastlawUI_v2.adminpanel.WebScrapStatutes"
    MasterPageFile="~/adminpanel/Site1.Master"
     %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
   

     
    <%--<asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>

            <section class="content-header">
                <h1>Web Statutes
                       
                    <small>Statutes</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li class="active">Statutes Web</li>
                </ol>
            </section>

            <section class="content">
                <div class="row">
                    <!-- left column -->
                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-6">
                        <!-- general form elements disabled -->
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
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Pakistan Code (Web)</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                                <div class="form-group">
                                    <label>
                                       Alphabetical Order:
                                        
                                    </label>
                                    <asp:DropDownList ID="ddlAlpha" runat="server" class="form-control">
                                        
                                    </asp:DropDownList>
                                     
                                </div>
                                <div class="form-group">
                                    <label>
                                        Pakistan Code:
                                        
                                    </label>
                                     <asp:Button ID="btnPakistanCode" runat="server" CssClass="btn btn-primary" Text="Scrap from Pakistan Code" Width="250px" OnClick="btnPakistanCode_Click" />
                                </div>

                                </div>

                            <div class="box-body">
                                
                                <div class="form-group">
                                    <label>
                                       Alphabetical Order (Subordinates):
                                        
                                    </label>
                                    <asp:DropDownList ID="ddlSubordinateStatutesAlpha" runat="server" class="form-control">
                                        
                                    </asp:DropDownList>
                                     
                                </div>
                                <div class="form-group">
                                    <label>
                                        Pakistan Code:
                                        
                                    </label>
                                     <asp:Button ID="btnPakistanCodeSubordinateStatutes" runat="server" CssClass="btn btn-primary" Text="Scrap from Pakistan Code (Subordinate)" Width="250px" OnClick="btnPakistanCodeSubordinateStatutes_Click"  />
                                </div>

                                </div>
                                

                                
                                <!-- /.box-body -->
                            </div>
                    <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Punjab Code (Web)</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                                <div class="form-group">
                                    <label>
                                       Alphabetical Order:
                                        
                                    </label>
                                    <asp:DropDownList ID="ddlPunjabCodeAlpha" runat="server" class="form-control">
                                        
                                    </asp:DropDownList>
                                     
                                </div>
                                <div class="form-group">
                                    <label>
                                        
                                        
                                    </label>
                                     <asp:Button ID="btnPunjabCodeAlpha" runat="server" CssClass="btn btn-primary" Text="Scrap from Punjab Code" Width="250px" OnClick="btnPunjabCodeAlpha_Click" />
                                    
                                </div>

                                </div>

                            
                                

                                
                                <!-- /.box-body -->
                            </div>
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Sindh Laws (Web)</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                               <%-- <div class="form-group">
                                    <label>
                                       Alphabetical Order:
                                        
                                    </label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                                        
                                    </asp:DropDownList>
                                     
                                </div>--%>
                                <div class="form-group">
                                    <label>
                                        
                                        
                                    </label>
                                     <asp:Button ID="btnSindlawAct" runat="server" CssClass="btn btn-primary" Text="Scrap from Sindh Laws Acts" Width="250px" OnClick="btnSindlawAct_Click"  />
                                     &nbsp;&nbsp;<asp:Button ID="btnSindhLawOrdinance" runat="server" CssClass="btn btn-info" Text="Scrap from Sindh Laws Ordinance" Width="250px" OnClick="btnSindhLawOrdinance_Click"  />
                                </div>

                                </div>

                            
                                

                                
                                <!-- /.box-body -->
                            </div>
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Khyber Pakhtunkhwa (Web)</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                               <%-- <div class="form-group">
                                    <label>
                                       Alphabetical Order:
                                        
                                    </label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                                        
                                    </asp:DropDownList>
                                     
                                </div>--%>
                                <div class="form-group">
                                    <label>
                                        
                                        
                                    </label>
                                     <asp:Button ID="btnKPKUpload" runat="server" CssClass="btn btn-primary" Text="Scrap from KPK" Width="250px" OnClick="btnKPKUpload_Click"   />
                                     
                                </div>

                                </div>

                            
                                

                                
                                <!-- /.box-body -->
                            </div>
                            <!-- /.box -->
                        </div>
                        <!--/.col (right) -->

                    </div>
                    <!--/.col (right) -->
                </div>
            </section>

       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

