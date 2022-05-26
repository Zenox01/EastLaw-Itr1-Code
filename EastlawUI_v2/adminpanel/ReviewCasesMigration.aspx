<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewCasesMigration.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ReviewCasesMigration" 
    MasterPageFile="~/adminpanel/Site1.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <head>
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"
type = "text/javascript"></script> 
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
type = "text/javascript"></script> 
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
rel = "Stylesheet" type="text/css" /> 
<script type="text/javascript">
  <%--  $(document).ready(function () {
        $("#<%=txtJudgeName.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("/Service.asmx/GetJudgesTitle") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            minLength: 1
        });

    });

    $(document).ready(function () {
        $("#<%=txtAdvA.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("/Service.asmx/GetAdvocateTitle") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            minLength: 1
        });

    });
    $(document).ready(function () {
        $("#<%=txtAdvR.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("/Service.asmx/GetAdvocateTitle") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            minLength: 1
        });

    });--%>
</script> 
    </head>
    
     <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Migration Utility
                        <small>Cases Migration Review</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Cases Migration</li>
                    </ol>
                </section>
            <table>
                <tr>
                    <td><asp:Label ID="lblTitle" runat="server" Text="Citation Type:"></asp:Label></td>
                    <td>
      <asp:DropDownList ID="ddlCitationType" runat="server" class="chosen-select" AutoPostBack="True" Width="500" OnSelectedIndexChanged="ddlCitationType_SelectedIndexChanged">
          <asp:ListItem Value="1" Text="New Citation" Selected="True"></asp:ListItem>
          <asp:ListItem Value="2" Text="Alternate Citation"></asp:ListItem>
      </asp:DropDownList></td>
                </tr>
            </table>
            
            <section class="content">
                <div class="row">
                   
                    <!-- left column -->
                    <!--/.col (left) -->
                    <!-- right column -->
                    <div id="divNewCitation" runat="server">
                    <div class="col-xs-8" >
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Citation Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="form-group">
                                    <label>
                                        Journal: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                     ControlToValidate="ddlJournal" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlJournal" runat="server" class="chosen-select"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Year: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                     ControlToValidate="ddlYear" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlYear" runat="server" class="chosen-select"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Citation : *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtCitation" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtCitation" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Citation No : *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                     ControlToValidate="txtCitationNo" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtCitationNo" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                      Court: 
                                                
                                    </label>
                                        <%--<telerik:RadComboBox RenderMode="Lightweight" ID="ddlRadCourts" runat="server" Height="200" Width="315" 
                        DropDownWidth="315" EmptyMessage="" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" > </telerik:RadComboBox>--%>
                                     <%--<asp:TextBox ID="txtCourt" runat="server" class="form-control"> </asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlCourts" runat="server" class="chosen-select"></asp:DropDownList>
                                     <%--<telerik:RadComboBox RenderMode="Lightweight" ID="ddlCourts" runat="server" Height="200" Width="315" 
                        DropDownWidth="315" EmptyMessage="" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" > </telerik:RadComboBox>--%>
                                </div>
                                 <div class="form-group">
                                    <label>
                                      Court City Name: 
                                                
                                    </label>
                                     <asp:TextBox ID="txtCourtCityName" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>
                                       Judgment Date: 
                                                
                                    </label>
                                    <asp:TextBox ID="txtJDate" runat="server" class="form-control"> </asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>
                                        Auto Generated Citation/Local Referecne: 
                                    </label>

                                   
                                    <asp:TextBox ID="txtAutoGeneratedCitation" runat="server" class="form-control"> </asp:TextBox>
                                    
                                </div>
                                <%--<div class="form-group">
                                    <label>
                                        Judge: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                     ControlToValidate="ddlJudge" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>
                                    </label>
                                    
                                    <table>
                                        <tr>
                                            <td>
                                                <h3>Judge #1:</h3>
                                            </td>
                                            <td><asp:DropDownList ID="ddlJudgeCourtMaster1" runat="server" class="form-control" Width="190"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="txtJudgeName1" runat="server" class="form-control" ToolTip="Number" Width="360" placeholder="Judge Name"></asp:TextBox></td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h3>Judge #1:</h3>
                                            </td>
                                            <td><asp:DropDownList ID="DropDownList1" runat="server" class="form-control" Width="190"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="TextBox2" runat="server" class="form-control" ToolTip="Number" Width="360" placeholder="Judge Name"></asp:TextBox></td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h3>Judge #1:</h3>
                                            </td>
                                            <td><asp:DropDownList ID="DropDownList2" runat="server" class="form-control" Width="190"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="TextBox4" runat="server" class="form-control" ToolTip="Number" Width="360" placeholder="Judge Name"></asp:TextBox></td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h3>Judge #1:</h3>
                                            </td>
                                            <td><asp:DropDownList ID="DropDownList3" runat="server" class="form-control" Width="190"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="TextBox5" runat="server" class="form-control" ToolTip="Number" Width="360" placeholder="Judge Name"></asp:TextBox></td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </div>--%>

                                <div class="form-group">
                                    <label>
                                        Judge: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlJudge" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlJudge" runat="server" class="chosen-select" Visible="false"></asp:DropDownList>
                                    <asp:TextBox ID="txtJudgeName" runat="server" ass="form-control" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lblJudgeID" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblJudge" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Appeal : 
                                                
                                    </label>
                                    <asp:TextBox ID="txtAppeal" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                
                                <div class="form-group">
                                    <label>
                                        Appeallant: *
                                                

                                    </label>
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="txtAppeallant1" runat="server" class="form-control" ToolTip="Year"  Width="200" placeholder="Appeallant 01" ></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtAppeallant2" runat="server" class="form-control" ToolTip="Year"  Width="200" placeholder="Appeallant 02" ></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtAppeallant3" runat="server" class="form-control" ToolTip="Year"  Width="200" placeholder="Appeallant 03" ></asp:TextBox></td>
                                            
                                        </tr>
                                    </table>     
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Appeallant Type: 
                                                
                                    </label>
                                    <asp:TextBox ID="txtAppeallantType" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                
                                 <div class="form-group">
                                    <label>
                                        Respondent: *
                                    </label>
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="txtRespondent1" runat="server" class="form-control" ToolTip="Year"  Width="200" placeholder="Respondent 01" ></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtRespondent2" runat="server" class="form-control" ToolTip="Year"  Width="200" placeholder="Respondent 02" ></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtRespondent3" runat="server" class="form-control" ToolTip="Year"  Width="200" placeholder="Respondent 03" ></asp:TextBox></td>
                                            
                                        </tr>
                                    </table>     
                                </div>
                                
                                <div class="form-group">
                                    <label>
                                        Advocate Appeallant: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="ddlAdvA" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>
                                                
                                    </label>
                                   <asp:DropDownList ID="ddlAdvA" runat="server" class="chosen-select" Visible="false"></asp:DropDownList>
                                    <asp:TextBox ID="txtAdvA" runat="server" ass="form-control" Visible="false"></asp:TextBox>
                                     <asp:Label ID="lblAdvAID" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblaAdvA" runat="server"></asp:Label>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Advocate Responded: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="ddlAdvR" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>
                                                
                                    </label>
                                    <asp:DropDownList ID="ddlAdvR" runat="server" class="chosen-select" Visible="false"></asp:DropDownList>
                                     <asp:TextBox ID="txtAdvR" runat="server" ass="form-control" Visible="false"></asp:TextBox>
                                      <asp:Label ID="lblAdvRID" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblAdvR" runat="server"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <label>
                                       Hear Date: 
                                                
                                    </label>
                                    <asp:TextBox ID="txtHearDate" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                       Head Notes:         
                                    </label>
                                    <cc1:editor ID="editorHeadNotes" runat="server"  Height="500px" Width="100%" />
                                </div>
                                <div class="form-group" >

                                <label>
                                 Manual Case Summary?
                                    
                                    </label>
                                 <asp:CheckBox ID="chkManualCaseSummary" runat="server"  AutoPostBack="True" class="form-control" OnCheckedChanged="chkManualCaseSummary_CheckedChanged" /> 
                                 
                                    
                              
                            </div>
                                <div class="form-group" style="display:none" id="divCaseSummary" runat="server">

                                <label>
                                        Case Summary:*
                                    
                                    </label>
                                  
                                 <cc1:editor ID="editortxtSummary" runat="server"  Height="400px" Width="100%" />
                                    
                              
                            </div>
                                <div class="form-group">
                                    <label>
                                      Judgment: 
                                    </label>
                                     
                                     
                                   <%-- <cc1:editor ID="editorJudgment" runat="server"  Height="300px" Width="100%" />--%>

                                    <%--<telerik:RadWindow ID="RadWindow1" runat="server" Width="805" Height="500" VisibleOnPageLoad="true">
<ContentTemplate>--%>
<telerik:RadEditor runat="server" ID="editorJudgment"  Width="100%" Height="750" >
    <ImageManager ViewPaths="/store/cases/imgs" UploadPaths="/store/cases/imgs" MaxUploadFileSize="1000000" />
    <DocumentManager ViewPaths="/store/cases/docs" UploadPaths="/store/cases/docs" MaxUploadFileSize="10000000" />
    <MediaManager ViewPaths="/store/cases/videos" UploadPaths="/store/cases/videos" MaxUploadFileSize="10000000"/>
</telerik:RadEditor>
                                   <%-- <cc1:editor ID="editorJudgment" runat="server"  Height="600px" Width="100%" />--%>
<%--</ContentTemplate>
</telerik:RadWindow>--%>

<%--<script type="text/javascript">
    var oldCommand = Telerik.Web.UI.RadEditor.prototype.toggleEnhancedEdit;

    Telerik.Web.UI.RadEditor.prototype.toggleEnhancedEdit = function (newValue) {
        if ($telerik.isSafari && typeof (newValue) != "undefined" && false == this.disableContentAreaStylesheet(newValue)) {
            window.setTimeout(Function.createDelegate(this, function () {
                this.disableContentAreaStylesheet(newValue);
            }), 200);
        } else {
            oldCommand.call(this, newValue);

        }
    };
</script>--%>
                                                
                                </div>
                                
                                
                                <div class="form-group">
                                    <label>
                                      Judgment Type: 
                                                
                                    </label>
                                     <asp:TextBox ID="txtJudgmentType" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>
                                      Result: 
                                                
                                    </label>
                                     <asp:TextBox ID="txtResult" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                
                               

                                <div class="form-group">
                                    <label>
                                        Priority Tagging:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkPriorityTagging" runat="server" class="form-control" />
                                </div>



                                <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updPnl">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" Width="150" OnClick="btnCancel_Click" />
                                &nbsp;&nbsp;
               
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" Width="150" OnClick="btnSave_Click" />
                                 
                                 &nbsp;&nbsp;
               
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" Text="Delete" Width="150" OnClick="btnDelete_Click"  />

                                
                                 &nbsp;&nbsp;
               
            <asp:Button ID="btnFindHyperlinking" runat="server" CssClass="btn btn-default" Text="Check Hyperlinking" Width="150" OnClick="btnFindHyperlinking_Click" CausesValidation="false"  />




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
                        <div class="col-xs-4">
                         <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Judges Name</h3>
                            </div>
                             <div class="box-body">
                                 <div class="form-group">
                                    <label>
                                        Court:
                                    </label>
                                     <asp:DropDownList ID="ddlCourtMaster" runat="server" class="chosen-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCourtMaster_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Judge 01:
                                    </label>
                                     <asp:DropDownList ID="ddlNewJudge1" runat="server" class="chosen-select"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Judge 02:
                                    </label>
                                     <asp:DropDownList ID="ddlNewJudge2" runat="server" class="chosen-select"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Judge 03:
                                    </label>
                                     <asp:DropDownList ID="ddlNewJudge3" runat="server" class="chosen-select"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Judge 04:
                                    </label>
                                     <asp:DropDownList ID="ddlNewJudge4" runat="server" class="chosen-select"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Judge 05:
                                    </label>
                                     <asp:DropDownList ID="ddlNewJudge5" runat="server" class="chosen-select"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Judge 06:
                                    </label>
                                     <asp:DropDownList ID="ddlNewJudge6" runat="server" class="chosen-select"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Judge 07:
                                    </label>
                                     <asp:DropDownList ID="ddlNewJudge7" runat="server" class="chosen-select"></asp:DropDownList>
                                </div>
                                 
                                   
                                
                                 
                                 </div>
                             </div>
                        </div>
                       <div class="col-xs-4">
                         <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Linked Appeal</h3>
                            </div>
                             <div class="box-body">
                                 <div class="form-group">
                                    <label>
                                        Judgment Date: (DD/MM/YYYY)
                                    </label>
                                     <asp:TextBox ID="txtLinkedAppeadJDate" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Court:
                                    </label>
                                     <asp:DropDownList ID="ddlSearchCourt" runat="server" class="chosen-select"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Citation: (optional)
                                                

                                    </label>
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="txtCitationYear" runat="server" class="form-control" ToolTip="Year"  Width="60" placeholder="Year" ></asp:TextBox></td>
                                            <td><asp:DropDownList ID="ddlJournalSearch" runat="server" class="form-control" Width="90"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="txtCitationNumber" runat="server" class="form-control" ToolTip="Number" Width="60" placeholder="No."></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                                   <div class="form-group">
                                    <label>
                                        <br />
                                        Appeal No:
                                    </label>
                                     <asp:TextBox ID="txtAppealNo" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                  <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" Width="150" CausesValidation="false" OnClick="btnSearch_Click"  />
                                 
                                 &nbsp;&nbsp;
               <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="upPnlSearch">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                 
                                 </div>
                             </div>
                        </div>
                    <div class="col-md-4">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Linked Appeal Citations</h3>
                            </div>
                            
     
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                                <asp:GridView ID="gvCitations" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Citation">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />

                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Citation") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appeal">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>

                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Appeal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>





                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                        <div class="col-md-4">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Citation Linking</h3>
                            </div>
                            
     
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                                <asp:GridView ID="gvCitationLinking" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Citation">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCitation" runat="server" Text='<%# Eval("Citation") %>' Width="230"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Journal">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblJournal" runat="server" Text='<%# Eval("Journal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Linked Case ID">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                
                                                <asp:TextBox ID="txtLinkedCaseID" runat="server" Text='<%# Eval("LinkedCaseID") %>' Width="50"></asp:TextBox>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>






                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                        <div class="col-xs-4">
                         <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Add Hyperlink Citation</h3>
                            </div>
                             <div class="box-body">
                                 
                                 <div class="form-group">
                                    <label>
                                        Citation: *
                                        
                                    </label>
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="txtCitationHyperlinkingYear" runat="server" class="form-control" ToolTip="Year"  Width="60" placeholder="Year" ></asp:TextBox></td>
                                            <td><asp:DropDownList ID="CitationHyperlinkingJournal" runat="server" class="form-control" Width="90"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="txtCitationHyperlinkingPageNo" runat="server" class="form-control" ToolTip="Number" Width="60" placeholder="No."></asp:TextBox></td>
                                        </tr>
                                    </table>
                                            
                                </div>
                                  
                                  <asp:Button ID="btnCitationHyperlinkingSearch" runat="server" CssClass="btn btn-primary" Text="Search" Width="150" CausesValidation="false" OnClick="btnCitationHyperlinkingSearch_Click"  />
                                 
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
               <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upPnlSearch">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                 
                                 </div>
                             </div>
                        </div>
                         <div class="col-xs-4">
                         <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Add Practie Area</h3>
                            </div>
                             <div class="box-body">
                                  <div class="form-group">
                                    <label>
                                        Practice Area Category: *:
                                    </label>
                                     <asp:DropDownList ID="ddlPracticeAreaCat" runat="server" class="chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPracticeAreaCat_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Practice Area Sub Category: *:
                                    </label>
                                     <asp:DropDownList ID="ddlPracticeAreaSubCat" runat="server" class="chosen-select"></asp:DropDownList>
                                </div>
                               
                                  
                                  <asp:Button ID="btnAddPracticeArea" runat="server" CssClass="btn btn-primary" Text="Add Practice Area" Width="150" CausesValidation="false" OnClick="btnAddPracticeArea_Click"   />
                                 
                                 &nbsp;&nbsp;
                                 <asp:GridView ID="gvPracticeAreaLst" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowEditing="gvCitationHyperlinkingSearch_RowEditing">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Pracice Area Cat">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />

                                                <asp:Label ID="lblPACat" runat="server" Text='<%# Eval("PACat") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pracice Area Sub Cat">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPASubCat" runat="server" Text='<%# Eval("PASubCat") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                       
                                    </Columns>
                                </asp:GridView>
               <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="upPnlSearch">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                 
                                 </div>
                             </div>
                        </div>

                        </div>
                    <asp:UpdatePanel ID="upPnlSearch" runat="server">
                        <ContentTemplate>
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upPnlSearch">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                            <div id="divAlternateCitation" runat="server" style="display:none">
                            <div class="col-xs-6">
                         <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Source Citation</h3>
                            </div>
                             <div class="box-body">
                               
                                 <div class="form-group">
                                    <label>
                                        Citation: *
                                                

                                    </label>
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="txtSourceAlternateCaseYear" runat="server" class="form-control" ToolTip="Year"  Width="60" placeholder="Year" ></asp:TextBox></td>
                                            <td><asp:DropDownList ID="ddlSourceAlternateCaseJournal" runat="server" class="form-control" Width="90"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="txtSourceAlternateCasePageNo" runat="server" class="form-control" ToolTip="Number" Width="60" placeholder="No."></asp:TextBox></td>
                                        </tr>
                                    </table>
                                            
                                </div>
                                  
                                  <asp:Button ID="btnSourceAddAlternateCase" runat="server" CssClass="btn btn-primary" Text="Search" Width="150" CausesValidation="false" OnClick="btnSourceAddAlternateCase_Click"  />
                                 
                                 &nbsp;&nbsp;
               
                                 
                                 </div>
                             </div>
                                <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Source Citations</h3>
                            </div>
                            
     
                            <!-- /.box-header -->
                            <div class="box-body">
                                <asp:Label ID="lblSourceCitationID" runat="server"></asp:Label>
                                <asp:Label ID="lblSourceCitation" runat="server"></asp:Label>
                                <asp:GridView ID="gvSourceCitation" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Citation">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />

                                                <asp:Label ID="lblCitation" runat="server" Text='<%# Eval("Citation") %>'></asp:Label>
                                                
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
                                                
                                                <asp:RadioButton ID="radioSel" runat="server" GroupName="AA" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>





                            </div>
                            <!-- /.box-body -->
                        </div>
                        </div>
                           
                            <div class="col-xs-6">
                         <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Alternate Citation</h3>
                            </div>
                             <div class="box-body">
                               
                                 <div class="form-group">
                                    <label>
                                        Citation: *
                                                

                                    </label>
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="txtAlternateCaseYear" runat="server" class="form-control" ToolTip="Year"  Width="60" placeholder="Year" ></asp:TextBox></td>
                                            <td><asp:DropDownList ID="ddlAlternateCaseJournal" runat="server" class="form-control" Width="90"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="txtAlternateCasePageNo" runat="server" class="form-control" ToolTip="Number" Width="60" placeholder="No."></asp:TextBox></td>
                                        </tr>
                                    </table>
                                            
                                </div>
                                  
                                  <asp:Button ID="btnAddAlternateCase" runat="server" CssClass="btn btn-primary" Text="Search" Width="150" CausesValidation="false" OnClick="btnAddAlternateCase_Click"  />
                                 
                                 &nbsp;&nbsp;
               <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlSearch">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                 
                                 
                                 </div>
                             </div>
                                <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Alternate Citations</h3>
                            </div>
                            
     
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                                <asp:GridView ID="gvAlternateCaseLst" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Year">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />

                                                <asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Journal">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdJournalID" runat="server" Value='<%# Eval("JournalID") %>' />

                                                <asp:Label ID="lblJournalName" runat="server" Text='<%# Eval("JournalName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Page No">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                
                                                <asp:Label ID="lblPageNo" runat="server" Text='<%# Eval("PageNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Citation">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                
                                                <asp:TextBox ID="txtCitation" runat="server" Text='<%# Eval("Citation") %>' ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:Button ID="btnSaveAlternateCitations" runat="server" CssClass="btn btn-primary" Text="Save" Width="150" CausesValidation="false" OnClick="btnSaveAlternateCitations_Click"  />
                                <div class="alert alert-danger alert-dismissable" id="divErrorAlternateCitation" runat="server" style="display: none">
                                    <button type="button" class="close" data-dismiss="alert">
                                        ×</button>
                                    <strong>Transaction failed ... </strong>
                                </div>
                                <div class="alert alert-info alert-dismissable" id="divSuccessAlternateCitation" runat="server" style="display: none">
                                    <button type="button" class="close" data-dismiss="alert">
                                        ×</button>
                                    <strong>Transaction success !</strong>
                                </div>


                            </div>
                            <!-- /.box-body -->
                        </div>
                        </div>
                           
                                </div>

                 
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!--/.col (right) -->

                </div>
                

            </section>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


