<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UtilityCases.aspx.cs" Inherits="EastlawUI_v2.adminpanel.UtilityCases"
    MasterPageFile="~/adminpanel/Site1.Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content2" runat="server" contentplaceholderid="headCnt">
    
   </asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
   

    <%-- <asp:Image ID="Image1" runat="server" ImageUrl="ImageHandler.ashx" /> --%>
    <%--<asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>

            <section class="content-header">
                <h1>Migration Utility
                       
                    <small>Cases Migration</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li class="active">Cases Migration</li>
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
                                <h3 class="box-title">Migration Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                 <div class="form-group">
                                    <label>
                                        Year/Volume: *
                                        
                                    </label>
                                     <asp:DropDownList ID="ddlYearVolumn" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlYearVolumn_SelectedIndexChanged">
                                         <asp:ListItem Value="Year" Text="Year"></asp:ListItem>
                                         <asp:ListItem Value="Volume" Text="Volume"></asp:ListItem>
                                     </asp:DropDownList>
                                     
                                </div>
                                <div class="form-group">
                                    <label>
                                        Journal: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlJournal" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlJournal" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="lblYearVolumn" runat="server" Text="Year"></asp:Label>: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="ddlYear" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlYear" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                         Citation Variation: 
                                        
                                    </label>
                                     <asp:DropDownList ID="ddlVaraition" runat="server" class="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label>
                                        File Name:*&nbsp;
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="fuploader" ErrorMessage="Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                    </label>
                                    
                              
                                    <asp:FileUpload ID="fuploader" runat="server" class="form-control"  />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only DB file is allowed!" ValidationExpression ="^.+(.db|.DB)$" ControlToValidate="fuploader" ForeColor="Red"> </asp:RegularExpressionValidator>
                                    <asp:Label ID="lblFileNameMsg" runat="server" Text="" Visible="false" ForeColor="Green" Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblFileName" runat="server" Text="" Visible="false" ForeColor="Green"></asp:Label>

                                </div>
                                 <div class="form-group">
                                    <label>
                                        Format New Line in Judgment:&nbsp;
                                    </label>
                                    <asp:CheckBox ID="chkFormat" runat="server" />

                                </div>
                                <div class="form-group">
                                    <label>
                                        Priority Tagging:&nbsp;
                                    </label>
                                    <asp:CheckBox ID="chkPriorityTagging" runat="server" />

                                </div>

                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Upload File" Width="100px" OnClick="btnUpload_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnLoad" runat="server" CssClass="btn btn-success" Text="Extract Date From File" Width="170px" OnClick="btnLoad_Click" Visible="false" CausesValidation="false"/>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnProcess" runat="server" CssClass="btn btn-info" Text="Start Migrate Process" Width="180px" OnClick="btnProcess_Click" Visible="false" CausesValidation="false" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnKeywordTagging" runat="server" CssClass="btn bg-maroon" Text="Keywords Tagging" Width="180px" Visible="false" OnClick="btnKeywordTagging_Click"  CausesValidation="false" />





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
                    <div class="col-xs-12">
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
                        </div><!--/.col (right) -->
                </div>
            </section>

       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

