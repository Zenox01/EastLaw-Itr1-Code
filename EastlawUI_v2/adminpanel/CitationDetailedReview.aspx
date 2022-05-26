<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CitationDetailedReview.aspx.cs" Inherits="EastlawUI_v2.adminpanel.CitationDetailedReview"
    MasterPageFile="~/adminpanel/Site1.Master" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cntplc">

    <asp:UpdatePanel ID="updPnl" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <section class="content-header">
                <h1>Cases
                       
                    <small>Detailed Review </small>
                </h1>

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
                                <h3 class="box-title">Citation Detail</h3>
                            </div>


                            <!-- /.box-header -->
                            <div class="box-body">

                                <div class="form-group">
                                    <label>
                                        Citation: 

                                    </label>

                                    <asp:Label ID="lblCitation" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Journal-Year: 

                                    </label>

                                    <asp:Label ID="lblJournalYear" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Judgment Date:

                                    </label>

                                    <asp:Label ID="lblJDate" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Publish:

                                    </label>

                                    <asp:Label ID="lblPublish" runat="server"></asp:Label>
                                </div>

                            </div>


                            <%--                   <a class="btn btn-block btn-info" data-toggle="modal" data-target="#Headnotes-modal" style="width:150px"> View Headnotes</a>
                            <a class="btn btn-block btn-info" data-toggle="modal" data-target="#Judgment-modal" style="width:150px"> View Judgment</a>
                            <a class="btn btn-block btn-info" data-toggle="modal" data-target="#Summary-modal" style="width:150px"> View Summary</a>--%>
                            <asp:HyperLink ID="hplLnkEditCitation" runat="server" Text="Edit Citaion" Target="_blank"></asp:HyperLink>
                            &nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="hypLnkViewCaseDetails" runat="server" Text="View Full Detail" Target="_blank" CssClass="btn btn-primary"></asp:HyperLink>

                            <asp:Button ID="btnEditCitation" runat="server" Text="Edit Citation" CssClass="btn btn-primary" Visible="false" Width="150" OnClick="btnEditCitation_Click" />

                            <div class="modal fade" id="Headnotes-modal" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                            <h4 class="modal-title">Headnotes</h4>
                                        </div>
                                        <form action="#" method="post">
                                            <div class="modal-body">

                                                <%-- <span class="input-group-addon">Link(s) to Cases Citations </span><br />--%>
                                                <%--<p class="help-block"></p>--%>
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div id="divHeadnotes" runat="server"></div>
                                                    </div>
                                                </div>



                                            </div>
                                            <div class="modal-footer clearfix">

                                                <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>Discard</button>


                                            </div>
                                        </form>
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>
                            <div class="modal fade" id="Judgment-modal" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                            <h4 class="modal-title">Judgment</h4>
                                        </div>
                                        <form action="#" method="post">
                                            <div class="modal-body">

                                                <%--<span class="input-group-addon">Link(s) to Cases Citations </span><br />--%>
                                                <%--  <p class="help-block"></p>--%>
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div id="divJudgment" runat="server"></div>
                                                    </div>
                                                </div>



                                            </div>
                                            <div class="modal-footer clearfix">

                                                <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>Discard</button>


                                            </div>
                                        </form>
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>
                            <div class="modal fade" id="Summary-modal" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                            <h4 class="modal-title">Case Summary</h4>
                                        </div>
                                        <form action="#" method="post">
                                            <div class="modal-body">

                                                <%--<span class="input-group-addon">Link(s) to Cases Citations </span><br />
                               <p class="help-block"></p>--%>
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div id="divSummary" runat="server"></div>

                                                    </div>
                                                </div>



                                            </div>

                                            <div class="modal-footer clearfix">

                                                <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>Discard</button>


                                            </div>
                                        </form>
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>


                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                    <div class="col-md-6" style="display: block">
                        <!-- general form elements disabled -->
                        <asp:UpdatePanel ID="updPnlSummary" runat="server">
                            <ContentTemplate>
                                <div class="box box-warning">
                                    <div class="box-header">
                                        <h3 class="box-title">Case Summary</h3>
                                    </div>


                                    <!-- /.box-header -->


                                    <div class="box-body">

                                        <label>
                                            Case Summary:*
                                    
                                        </label>


                                        <cc1:Editor ID="editortxtSummary" runat="server" Height="500px" Width="100%" />


                                    </div>
                                    <div class="box-body">



                                        <asp:Button ID="btnSaveEdit0" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSaveEdit0_Click" Text="Update Summary" Width="150" />
                                        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updPnlSummary">
                                            <ProgressTemplate>


                                                <img alt="" src="media/img/loader.gif" />




                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <div class="alert alert-danger alert-dismissable" id="divErrorSummary" runat="server" style="display: none">
                                            <button type="button" class="close" data-dismiss="alert">
                                                ×</button>
                                            <strong>Transaction failed ... </strong>
                                        </div>
                                        <div class="alert alert-info alert-dismissable" id="divSuccessSummary" runat="server" style="display: none">
                                            <button type="button" class="close" data-dismiss="alert">
                                                ×</button>
                                            <strong>Transaction success !</strong>
                                        </div>
                                    </div>

                                    <!-- /.box-body -->
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- /.box -->
                    </div>
                    <div class="col-md-6">
                        <asp:UpdatePanel ID="updPnlStatutes" runat="server">
                            <ContentTemplate>

                                <!-- general form elements disabled -->
                                <div class="box box-warning">
                                    <div class="box-header">
                                        <h3 class="box-title">Tagged Statutes</h3>
                                    </div>


                                    <!-- /.box-header -->
                                    <div class="box-body">

                                        <asp:GridView ID="gvTaggedStatutes" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowDeleting="gvTaggedStatutes_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Statutes Title">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type" Visible="false">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlType" class="form-control">
                                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Section" Value="Section"></asp:ListItem>
                                                            <asp:ListItem Text="Rule" Value="Rule"></asp:ListItem>
                                                            <asp:ListItem Text="Order" Value="Order"></asp:ListItem>
                                                            <asp:ListItem Text="Article" Value="Article"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Value" Visible="false">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAttriVal" runat="server" Text='<%# Eval("AttriLink") %>' class="form-control"></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20"
                                                            OnClientClick=" return confirm('Do you want to delete ?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <span style="float: right">
                                            <asp:Button ID="btnUpdateStatues" runat="server" Text="Update Stattute" CssClass="btn btn-primary" Width="150" OnClick="btnUpdateStatues_Click" Visible="false" />
                                            <div class="alert alert-danger alert-dismissable" id="divStatutesFailed" runat="server" style="display: none">
                                                <button type="button" class="close" data-dismiss="alert">
                                                    ×</button>
                                                <strong>Transaction failed ... </strong>
                                            </div>
                                            <div class="alert alert-info alert-dismissable" id="divStatueSuccess" runat="server" style="display: none">
                                                <button type="button" class="close" data-dismiss="alert">
                                                    ×</button>
                                                <strong>Transaction success !</strong>
                                            </div>
                                        </span>



                                    </div>
                                    <h3>Add New</h3>
                                    <div class="box-body">
                                        <label>
                                            Select Statutes:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="ddlStat" ErrorMessage="Required" ForeColor="Red" Enabled="true" InitialValue="0" ValidationGroup="Statutes"> </asp:RequiredFieldValidator>
                                        </label>


                                        <%--                                     <telerik:RadComboBox RenderMode="Lightweight" ID="ddlStatutes" runat="server" Height="200" Width="315" 
                                        DropDownWidth="315" EmptyMessage="" HighlightTemplatedItems="true"
                                        EnableLoadOnDemand="true" Filter="StartsWith"></telerik:RadComboBox>--%>

                                        <asp:DropDownList ID="ddlStat" runat="server" class="chosen-select"></asp:DropDownList>

                                    </div>
                                    <div class="box-body" style="display: none">
                                        <label>
                                            Enter Statutes Title:
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="ddlStat" ErrorMessage="Required" ForeColor="Red"  Enabled="true" InitialValue="0" ValidationGroup="Statutes"> </asp:RequiredFieldValidator>--%>
                                        </label>


                                        <asp:TextBox ID="txtStatTitle" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="box-body">
                                        <%-- <label>
                                        Number/Text:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="txtSAR" ErrorMessage="Required" ForeColor="Red"  Enabled="true" ValidationGroup="SAR"> </asp:RequiredFieldValidator>
                                    </label>
                                  <asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="enter section/article/rule text *" ValidationGroup="Statutes"></asp:TextBox><br />--%>
                                        <asp:Button ID="btnAddStatutes" runat="server" Text="Add Stattute" CssClass="btn btn-primary" Width="150" ValidationGroup="Statutes" OnClick="btnAddStatutes_Click" />

                                    </div>
                                    <!-- /.box-body -->
                                </div>

                                <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updPnlStatutes">
                                    <ProgressTemplate>


                                        <img alt="" src="media/img/loader.gif" />




                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- /.box -->
                    </div>
                    <div class="col-md-6">
                        <asp:UpdatePanel ID="updPnlStatutesSOA" runat="server">
                            <ContentTemplate>

                                <!-- general form elements disabled -->
                                <div class="box box-warning">
                                    <div class="box-header">
                                        <h3 class="box-title">Tagged SOA</h3>
                                    </div>


                                    <!-- /.box-header -->
                                    <div class="box-body">

                                        <asp:GridView ID="gvTaggedSOA" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowDeleting="gvTaggedSOA_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Statutes Title">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SOA">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblElementData" runat="server" Text='<%# Eval("ElementData") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20"
                                                            OnClientClick=" return confirm('Do you want to delete ?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <span style="float: right">
                                            <div class="alert alert-danger alert-dismissable" id="divErrorSOA" runat="server" style="display: none">
                                                <button type="button" class="close" data-dismiss="alert">
                                                    ×</button>
                                                <strong>Transaction failed ... </strong>
                                            </div>
                                            <div class="alert alert-info alert-dismissable" id="divSuccessSOA" runat="server" style="display: none">
                                                <button type="button" class="close" data-dismiss="alert">
                                                    ×</button>
                                                <strong>Transaction success !</strong>
                                            </div>
                                        </span>



                                    </div>
                                    <h3>Add New</h3>
                                    <div class="box-body">
                                        <label>
                                            Select Statutes:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="ddlSOAStat" ErrorMessage="Required" ForeColor="Red" Enabled="true" InitialValue="0" ValidationGroup="StatutesSOA"> </asp:RequiredFieldValidator>
                                        </label>


                                        <%--                                     <telerik:RadComboBox RenderMode="Lightweight" ID="ddlStatutes" runat="server" Height="200" Width="315" 
                                        DropDownWidth="315" EmptyMessage="" HighlightTemplatedItems="true"
                                        EnableLoadOnDemand="true" Filter="StartsWith"></telerik:RadComboBox>--%>

                                        <asp:DropDownList ID="ddlSOAStat" runat="server" class="chosen-select" AutoPostBack="True" OnSelectedIndexChanged="ddlSOAStat_SelectedIndexChanged"></asp:DropDownList>

                                    </div>
                                    <div class="box-body">
                                        <label>
                                            Select SOA:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="ddlSOA" ErrorMessage="Required" ForeColor="Red" Enabled="true" InitialValue="0" ValidationGroup="StatutesSOA"> </asp:RequiredFieldValidator>
                                        </label>


                                        <%--                                     <telerik:RadComboBox RenderMode="Lightweight" ID="ddlStatutes" runat="server" Height="200" Width="315" 
                                        DropDownWidth="315" EmptyMessage="" HighlightTemplatedItems="true"
                                        EnableLoadOnDemand="true" Filter="StartsWith"></telerik:RadComboBox>--%>

                                        <asp:DropDownList ID="ddlSOA" runat="server" class="chosen-select"></asp:DropDownList>

                                    </div>

                                    <div class="box-body">
                                        <%-- <label>
                                        Number/Text:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="txtSAR" ErrorMessage="Required" ForeColor="Red"  Enabled="true" ValidationGroup="SAR"> </asp:RequiredFieldValidator>
                                    </label>
                                  <asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="enter section/article/rule text *" ValidationGroup="Statutes"></asp:TextBox><br />--%>
                                        <asp:Button ID="btnAddSOA" runat="server" Text="Add SOA" CssClass="btn btn-primary" Width="150" ValidationGroup="StatutesSOA" OnClick="btnAddSOA_Click" />

                                    </div>
                                    <h3>Add New Section</h3>
                                    <div class="box-body">
                                        <label>
                                            Select Statutes:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="ddlStatutesNewSOAIndex" ErrorMessage="Required" ForeColor="Red" Enabled="true" InitialValue="0" ValidationGroup="NewStatutesSOAIndex"> </asp:RequiredFieldValidator>
                                        </label>
                                        <asp:DropDownList ID="ddlStatutesNewSOAIndex" runat="server" class="chosen-select"></asp:DropDownList>
                                    </div>
                                    <div class="box-body">
                                        <label>
                                            Select Statutes Section Type:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                        ControlToValidate="ddlStatutesNewSOAIndexType" ErrorMessage="Required" ForeColor="Red"
                                        Enabled="true" InitialValue="0" ValidationGroup="NewStatutesSOAIndex"> </asp:RequiredFieldValidator>
                                        </label>
                                        <asp:DropDownList ID="ddlStatutesNewSOAIndexType" runat="server" class="chosen-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStatutesNewSOAIndexType_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Parent" Value="Parent"></asp:ListItem>
                                            <asp:ListItem Text="Child" Value="Child"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="box-body" id="divddlStatutesNewSOAIndexParent" runat="server" style="display: none">
                                        <label>
                                            Select SOA Parent:*
                                    <asp:RequiredFieldValidator ID="rfvddlStatutesNewSOAIndexParent" runat="server"
                                        ControlToValidate="ddlStatutesNewSOAIndexParent" ErrorMessage="Required" ForeColor="Red"
                                        Enabled="true" InitialValue="0" ValidationGroup="NewStatutesSOAIndex"> </asp:RequiredFieldValidator>
                                        </label>


                                        <asp:DropDownList ID="ddlStatutesNewSOAIndexParent" runat="server" class="chosen-select"></asp:DropDownList>

                                    </div>
                                    <div class="box-body">
                                        <label>
                                            Select SOA Parent:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                        ControlToValidate="txtStatutesNewSOAIndexSection" ErrorMessage="Required" ForeColor="Red"
                                        Enabled="true" ValidationGroup="NewStatutesSOAIndex"> </asp:RequiredFieldValidator>
                                        </label>


                                        <asp:TextBox ID="txtStatutesNewSOAIndexSection" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="box-body">

                                        <asp:Button ID="btnStatutesNewSOAIndexSection" runat="server" Text="Add New SOA" CssClass="btn btn-primary" Width="150" ValidationGroup="NewStatutesSOAIndex" OnClick="btnStatutesNewSOAIndexSection_Click" />

                                    </div>
                                </div>

                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPnlStatutesSOA">
                                    <ProgressTemplate>


                                        <img alt="" src="media/img/loader.gif" />




                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- /.box -->
                    </div>
                    <div class="col-md-6" style="display: none">
                        <asp:UpdatePanel ID="updPnlSAR" runat="server">
                            <ContentTemplate>
                                <!-- general form elements disabled -->
                                <div class="box box-warning">
                                    <div class="box-header">
                                        <h3 class="box-title">Tagged Section/Articles/Rule</h3>
                                    </div>


                                    <!-- /.box-header -->
                                    <div class="box-body">

                                        <asp:GridView ID="gvSAR" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowDeleting="gvSAR_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Searcl Level">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                                        <asp:Label ID="lblSearchLevel" runat="server" Text='<%# Eval("SearchLevel") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Find">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtLinkText" runat="server" Text='<%# Eval("LinkText") %>' class="form-control"></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Find Source">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>

                                                        <asp:Label ID="LinkSource" runat="server" Text='<%# Eval("LinkSource") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20"
                                                            OnClientClick=" return confirm('Do you want to delete ?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>





                                    </div>
                                    <h3>Add New</h3>
                                    <div class="box-body">
                                        <label>
                                            Select Type:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="ddlSAR" ErrorMessage="Required" ForeColor="Red" Enabled="true" InitialValue="0" ValidationGroup="SAR"> </asp:RequiredFieldValidator>
                                        </label>

                                        <asp:DropDownList ID="ddlSAR" runat="server" ValidationGroup="SAR" class="form-control">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Section" Value="Section"></asp:ListItem>
                                            <asp:ListItem Text="Rule" Value="Rule"></asp:ListItem>
                                            <asp:ListItem Text="Order" Value="Order"></asp:ListItem>
                                            <asp:ListItem Text="Article" Value="Article"></asp:ListItem>
                                        </asp:DropDownList>


                                    </div>
                                    <div class="box-body">
                                        <label>
                                            Number/Text:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtSAR" ErrorMessage="Required" ForeColor="Red" Enabled="true" ValidationGroup="SAR"> </asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtSAR" runat="server" class="form-control" placeholder="enter section/article/rule text *" ValidationGroup="SAR"></asp:TextBox><br />
                                        <asp:Button ID="btnAddSAR" runat="server" Text="Add S/A/R" CssClass="btn btn-primary" Width="150" ValidationGroup="SAR" OnClick="btnAddSAR_Click" />

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updPnlSAR">
                                    <ProgressTemplate>
                                        <img src="../images/ajax-loader.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- /.box -->
                    </div>
                    <div class="col-md-6">
                        <asp:UpdatePanel ID="updPnlCitation" runat="server">
                            <ContentTemplate>
                                <div class="box box-warning">
                                    <div class="box-header">
                                        <h3 class="box-title">Tagged Citation Within</h3>
                                    </div>


                                    <!-- /.box-header -->
                                    <div class="box-body">

                                        <asp:GridView ID="gvInsideCitation" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowDeleting="gvInsideCitation_RowDeleting" OnRowDataBound="gvInsideCitation_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="LinkSource">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLinkSource" runat="server" Text='<%# Eval("LinkSource") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Citation">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                                        <asp:HiddenField ID="hdLinkedCaseID" runat="server" Value='<%# Eval("LinkedCaseID") %>' />
                                                        <asp:TextBox ID="txtCitation" runat="server" Text='<%# Eval("Citation") %>' class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20"
                                                            OnClientClick=" return confirm('Do you want to delete ?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <h3>Add New</h3>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <label>
                                                Citation: *
                                            </label>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtCitationYear" runat="server" class="form-control" ToolTip="Year" Width="60" placeholder="Year"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlJournalSearch" runat="server" class="form-control" Width="90"></asp:DropDownList></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCitationNumber" runat="server" class="form-control" ToolTip="Number" Width="60" placeholder="No."></asp:TextBox></td>
                                                </tr>
                                            </table>

                                        </div>
                                        <%--<label>
                                        Citation:*
                                    <asp:RequiredFieldValidator ID="rfvCitation" runat="server"
                                                     ControlToValidate="txtAddCitation" ErrorMessage="Required" ForeColor="Red"  Enabled="true" ValidationGroup="Citation"> </asp:RequiredFieldValidator>
                                    </label>
                                  <asp:TextBox ID="txtAddCitation" runat="server" class="form-control" placeholder="enter citation *" ValidationGroup="Citation"></asp:TextBox><br />--%>
                                        <%--<asp:Button ID="btnAddCitation" runat="server" Text="Add Citation" CssClass="btn btn-primary" Width="150" ValidationGroup="Citation" OnClick="btnAddCitation_Click"  />--%>
                                        <asp:Button ID="btnCitationHyperlinkingSearch" runat="server" CssClass="btn btn-primary" Text="Search" Width="150" CausesValidation="false" OnClick="btnCitationHyperlinkingSearch_Click" />
                                        &nbsp;&nbsp;
                                
                                        <asp:GridView ID="gvCitationHyperlinkingSearch" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowEditing="gvCitationHyperlinkingSearch_RowEditing">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Citation">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />

                                                        <asp:Label ID="lblCitation" runat="server" Text='<%# Eval("Citation") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Journal">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJournalName" runat="server" Text='<%# Eval("JournalName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case ID">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select" Visible="true">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>

                                                        <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Edit" CausesValidation="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updPnlCitation">
                                    <ProgressTemplate>
                                        <img src="media/img/ajax-loader.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- /.box -->
                    </div>
                    <div class="col-md-6">
                        <asp:UpdatePanel ID="upPnalAlternateCitation" runat="server">
                            <ContentTemplate>
                                <div class="box box-warning">
                                    <div class="box-header">
                                        <h3 class="box-title">Alternate Citations</h3>
                                    </div>


                                    <!-- /.box-header -->
                                    <div class="box-body">

                                        <asp:GridView ID="gvAlternateCitations" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowDeleting="gvAlternateCitations_RowDeleting">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Alternate Citation">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />

                                                        <asp:Label ID="lblCitation" runat="server" Text='<%# Eval("Citation") %>' class="form-control"></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20"
                                                            OnClientClick=" return confirm('Do you want to delete ?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>





                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <asp:UpdateProgress ID="UpdateProgress7" runat="server" AssociatedUpdatePanelID="upPnalAlternateCitation">
                                    <ProgressTemplate>
                                        <img src="../images/ajax-loader.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- /.box -->
                    </div>
                    <div class="col-md-6">
                        <asp:UpdatePanel ID="updPnlJudges" runat="server">
                            <ContentTemplate>
                                <div class="box box-warning">
                                    <div class="box-header">
                                        <h3 class="box-title">Old Judges Strength</h3>
                                    </div>


                                    <!-- /.box-header -->
                                    <div class="box-body">

                                        <asp:GridView ID="gvJudges" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowDeleting="gvJudges_RowDeleting">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Judge Name">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />

                                                        <asp:TextBox ID="txtJudgeName" runat="server" Text='<%# Eval("JudgeName") %>' class="form-control"></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Author">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAuthor" runat="server" Checked='<%# Convert.ToBoolean(Eval("Authortxt")) %>' />
                                                        <%--<asp:TextBox ID="txtJudgeName" runat="server" Text='<%# Eval("JudgeName") %>'  class="form-control"></asp:TextBox>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20"
                                                            OnClientClick=" return confirm('Do you want to delete ?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>





                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updPnlJudges">
                                    <ProgressTemplate>
                                        <img src="../images/ajax-loader.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- /.box -->
                    </div>
                    <div class="col-md-6">
                        <asp:UpdatePanel ID="updPnlJudgesNew" runat="server">
                            <ContentTemplate>
                                <div class="box box-warning">
                                    <div class="box-header">
                                        <h3 class="box-title">New Judges</h3>
                                    </div>


                                    <!-- /.box-header -->
                                    <div class="box-body">

                                        <asp:GridView ID="gvNewJudgesList" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowDeleting="gvNewJudgesList_RowDeleting">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Judge Name">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />

                                                        <asp:Label ID="lblJudgeName" runat="server" Text='<%# Eval("JudgeName") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Author">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblAuthor" runat="server" Text='<%# Eval("Author") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20"
                                                            OnClientClick=" return confirm('Do you want to delete ?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                    <h3>Add New Judge</h3>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <label>
                                                Select Court Master: *
                                            </label>
                                            <asp:DropDownList ID="ddlCourtMasterJudge" runat="server" class="chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCourtMasterJudge_SelectedIndexChanged"></asp:DropDownList>


                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Select Judge: *
                                            </label>
                                            <asp:DropDownList ID="ddlCourtJudgeNew" runat="server" class="chosen-select"></asp:DropDownList>

                                        </div>

                                        <asp:Button ID="btnAddNewJudge" runat="server" CssClass="btn btn-primary" Text="Add New Judge" Width="150" CausesValidation="false" OnClick="btnAddNewJudge_Click" />


                                    </div>



                                </div>
                                <!-- /.box-body -->
                                </div>
                                <asp:UpdateProgress ID="UpdateProgress8" runat="server" AssociatedUpdatePanelID="updPnlJudgesNew">
                                    <ProgressTemplate>
                                        <img src="../images/ajax-loader.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- /.box -->
                    </div>
                    <div class="col-md-6">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Action</h3>
                            </div>


                            <!-- /.box-header -->
                            <div class="box-body">

                                <div class="form-group">
                                    <label>
                                        Publish ?: 

                                    </label>


                                    <asp:CheckBox ID="chkPublish" runat="server" />
                                </div>
                                <div class="form-group">
                                    <label>
                                        Ready For Print/Export ?: 

                                    </label>


                                    <asp:CheckBox ID="chkPrintReady" runat="server" />
                                </div>





                            </div>

                            <asp:UpdateProgress ID="upProcessReg" runat="server" AssociatedUpdatePanelID="updPnl">
                                <ProgressTemplate>
                                    <img src="../images/ajax-loader.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:Button ID="btnFinalReview" runat="server" Text="Review Complete - Save" CssClass="btn btn-primary" Width="250" OnClick="btnFinalReview_Click" />

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



                </div>



            </section>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


