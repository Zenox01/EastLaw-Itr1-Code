<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateCaseExcel.aspx.cs" Inherits="EastlawUI_v2.adminpanel.GenerateCaseExcel" 
    MasterPageFile="~/adminpanel/Site1.Master"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content2" runat="server" contentplaceholderid="headCnt">
    
   </asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
   

    <%-- <asp:Image ID="Image1" runat="server" ImageUrl="ImageHandler.ashx" /> --%>
    <%--<asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>

            <section class="content-header">
                <h1>Cases
                       
                    <small>Cases Excel</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li class="active">Cases Excel</li>
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
                                <h3 class="box-title">Generate Excel</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                 
                                 <div class="form-group">
                                    <label>
                                        Court: 
                                       
                                    </label>
                                     <%--<asp:DropDownList ID="ddlCourt" runat="server" class="form-control"></asp:DropDownList>--%>
                                     <telerik:RadComboBox RenderMode="Lightweight" ID="ddlRadCourts" runat="server" Height="200" Width="315" 
                        DropDownWidth="315" EmptyMessage="" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" > </telerik:RadComboBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Journal: 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlJournal" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlJournal" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Year: 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                     ControlToValidate="ddlYear" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlYear" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                               <%-- <div class="form-group">
                                    <label>
                                        <asp:Label ID="lblYearVolumn" runat="server" Text="Year"></asp:Label>: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="ddlYear" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlYear" runat="server" class="form-control"></asp:DropDownList>
                                </div>--%>

                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Choose Columns"></asp:Label>: *
                                       
                                    </label>
                                     <asp:CheckBoxList ID="cblColumns" runat="server" CellPadding="20" CellSpacing="10" RepeatColumns="4" >
                                         <asp:ListItem Value="E.JournalName" Text="Journal Name"></asp:ListItem>
                                         <asp:ListItem Value="A.Citation" Text="Citation"></asp:ListItem>
                                         <asp:ListItem Value="A.CitationRef" Text="Citation Ref"></asp:ListItem>
                                         <asp:ListItem Value="B.JudgeName" Text="Judge Name"></asp:ListItem>
                                         <asp:ListItem Value="A.Appeal" Text="Appeal No"></asp:ListItem>
                                         <asp:ListItem Value="A.Appeallant" Text="Appeallant"></asp:ListItem>
                                         <asp:ListItem Value="A.Respondent" Text="Respondent"></asp:ListItem>
                                         <asp:ListItem Value="A.JDate" Text="JDate"></asp:ListItem>
                                         <asp:ListItem Value="C.AdvocateName" Text="Adv Appeallant"></asp:ListItem>
                                         <asp:ListItem Value="D.AdvocateName" Text="Adv Respondent"></asp:ListItem>
                                         <asp:ListItem Value="A.HearDate" Text="HearDate"></asp:ListItem>
                                         <asp:ListItem Value="A.Result" Text="Result"></asp:ListItem>
                                         <asp:ListItem Value="A.Court" Text="Court"></asp:ListItem>
                                         <asp:ListItem Value="A.Court_Area" Text="Court Area"></asp:ListItem>
                                         <asp:ListItem Value="dbo.ReturnCaseTaggedStatute_withoutstyle(A.ID)" Text="Tagges Statutes"></asp:ListItem>
                                         
                                    </asp:CheckBoxList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Filename: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtFileName" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                     
                                    <asp:TextBox ID="txtFileName" runat="server"  class="form-control"></asp:TextBox>
                                </div>
                              
                              

                                

                                <asp:Button ID="btnGenerateExcel" runat="server" CssClass="btn btn-primary" Text="Generate Excel File" Width="200px" OnClick="btnGenerateExcel_Click"  />
                                




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
                    <!--/.col (right) -->
                </div>
            </section>

       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

