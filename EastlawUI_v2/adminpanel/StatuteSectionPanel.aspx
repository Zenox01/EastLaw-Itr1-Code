<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatuteSectionPanel.aspx.cs" Inherits="EastlawUI_v2.adminpanel.StatuteSectionPanel"
    MasterPageFile="~/adminpanel/Site1.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cntplc">
    <head>
        <script type="text/javascript">  
            function onClientContextMenuItemClicking(sender, args) {
                var menuItem = args.get_menuItem();
                var treeNode = args.get_node();
                var nodeAttributes = treeNode.get_attributes();
                menuItem.get_menu().hide();

                switch (menuItem.get_value()) {
                    case "Edit":
                    // Can't post, proprietary code
                    case "NewStep":
                        break;
                    case "NewSubStep":
                        break;
                    case "Delete":
                        var result = confirm("Are you sure you want to delete the item: " + treeNode.get_text());
                        args.set_cancel(!result);
                        break;
                    case "ChangeType":
                        var result2 = confirm("Changing the step type will completely discard the step's current values, are you sure you want to proceed?");
                        args.set_cancel(!result2);
                }
            }
        </script>
        <script>  
            Telerik.Web.UI.RadMenu._getViewPortSize = function () {
                var viewPortSize = $telerik.getViewPortSize();

                // The document scroll is not included in the viewport size
                // calculation under FF/quirks and Edge.      
                var quirksMode = document.compatMode != "CSS1Compat";
                if (($telerik.isFirefox && quirksMode) || Telerik.Web.Browser.edge) {
                    viewPortSize.height += document.body.scrollTop;
                }
                else if (Telerik.Web.Browser.chrome) {
                    viewPortSize.height += Math.max(document.body.scrollTop, document.scrollingElement.scrollTop);
                }

                return viewPortSize;
            };
        </script>
    </head>
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>

            <section class="content-header">
                <h1>Statutes Section
                       
                    <small>Statutes Section Details</small>
                </h1>

            </section>

            <section class="content">
                <div class="row">
                    <!-- left column -->
                    <div class="col-xs-3">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Statutes
                                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                                </h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                            </div>
                            <!-- /.box-body -->
                            <div class="alert alert-danger alert-dismissable" id="divError1" runat="server" style="display: none">
                                <button type="button" class="close" data-dismiss="alert">
                                    ×</button>
                                <strong>Transaction failed ... </strong>
                            </div>
                            <div class="alert alert-info alert-dismissable" id="divSuccess1" runat="server" style="display: none">
                                <button type="button" class="close" data-dismiss="alert">
                                    ×</button>
                                <strong>Transaction success !</strong>
                            </div>
                            <div class="form-group">
                                <label>
                                    Statute: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                     ControlToValidate="ddlStatute" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                </label>
                                <asp:DropDownList ID="ddlStatute" runat="server" class="chosen-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStatute_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updPnl">
                                    <ProgressTemplate>


                                        <img alt="" src="media/img/loader.gif" />




                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <asp:CheckBox ID="chkSOATagEnable" runat="server" Text="Enable for Case Tagging" AutoPostBack="True" OnCheckedChanged="chkSOATagEnable_CheckedChanged" />
                            <asp:CheckBox ID="chkMerging" runat="server" Text="Merging" />
                            <telerik:RadTreeView RenderMode="Lightweight" runat="server" ID="tvDept"
                                OnNodeClick="tvDept_NodeClick" OnContextMenuItemClick="tvDept_ContextMenuItemClick" AllowNodeEditing="true"
                                OnNodeEdit="tvDept_NodeEdit" CausesValidation="false" EnableDragAndDrop="True" OnNodeDrop="tvDept_HandleDrop"
                                OnClientContextMenuItemClicking="onClientContextMenuItemClicking">
                                <ContextMenus>
                                    <telerik:RadTreeViewContextMenu runat="server" ID="Caction" ClickToOpen="True" EnableScreenBoundaryDetection="false">
                                        <Items>
                                            <telerik:RadMenuItem Text="Add New Index" Value="NewIndex">
                                            </telerik:RadMenuItem>
                                            <%-- <telerik:RadMenuItem Text="Edit / View" Value="Edit">
                    </telerik:RadMenuItem>--%>
                                            <telerik:RadMenuItem Text="Delete Index" Value="Delete">
                                            </telerik:RadMenuItem>

                                        </Items>
                                    </telerik:RadTreeViewContextMenu>
                                </ContextMenus>

                            </telerik:RadTreeView>

                        </div>
                        <!-- /.box -->
                    </div>
                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-8">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Index Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">

                                <div class="form-group">
                                    <label>
                                        Element Data: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtElementData" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblID" runat="server" Visible="false" Text="0"></asp:Label>
                                    </label>
                                    <asp:TextBox ID="txtElementData" runat="server" class="form-control"> </asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>
                                        Sort Order: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="txtSortOrder" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtSortOrder" runat="server" class="form-control" Text="0"> </asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers"
                                        TargetControlID="txtSortOrder">
                                    </asp:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Index Content: 
                                                
                                    </label>
                                    <cc1:Editor ID="editorIndexContent" runat="server" Height="500px" Width="100%" />


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
                    <!--/.col (right) -->

                    <div class="col-md-4">
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Add Citation</h3>
                            </div>
                            <div class="box-body">

                                <div class="form-group">
                                    <label>
                                        Citation: *
                                        
                                    </label>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCitationHyperlinkingYear" runat="server" class="form-control" ToolTip="Year" Width="60" placeholder="Year"></asp:TextBox></td>
                                            <td>
                                                <asp:DropDownList ID="CitationHyperlinkingJournal" runat="server" class="form-control" Width="90"></asp:DropDownList></td>
                                            <td>
                                                <asp:TextBox ID="txtCitationHyperlinkingPageNo" runat="server" class="form-control" ToolTip="Number" Width="60" placeholder="No."></asp:TextBox></td>
                                        </tr>
                                    </table>

                                </div>

                                <asp:Button ID="btnCitationSearch" runat="server" CssClass="btn btn-primary" Text="Search" Width="150" CausesValidation="false" OnClick="btnCitationSearch_Click" />

                                &nbsp;&nbsp;
                                
                                <asp:GridView ID="gvCitationSearch" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None">
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
                                                <asp:CheckBox ID="chksel" runat="server" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                            <br />

                            <asp:Button ID="btnSOACaseSave" runat="server" CssClass="btn btn-primary" Text="Save" Width="150" OnClick="btnSOACaseSave_Click" />

                        </div>


                    </div>

                    <div class="col-md-4">
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Merge/Moving Panel</h3>
                            </div>
                            <div class="box-body">

                                <div class="form-group">
                                    <label>
                                        Source: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlISourceIndex" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:DropDownList ID="ddlISourceIndex" runat="server" class="chosen-select">
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group">
                                    <label>
                                        Destination: *
                                        
                                    </label>
                                    <asp:DropDownList ID="ddlIDestinationIndex" runat="server" class="chosen-select">
                                    </asp:DropDownList>


                                </div>
                                <div class="form-group">
                                    <label>
                                        Merge/Move:
                                           
                                    </label>
                                    <asp:RadioButton ID="radioMerge" runat="server" class="form-control" Text="Merge" GroupName="A" />
                                    <asp:RadioButton ID="radioMove" runat="server" class="form-control" Text="Move" GroupName="A" />
                                </div>




                            </div>
                            <br />
                            <br />

                            <asp:Button ID="btnMergeMove" runat="server" CssClass="btn btn-primary" Text="Save" Width="150" OnClick="btnMergeMove_Click" OnClientClick="return confirm('Do you want to Merge or Move this record?');" />

                        </div>


                    </div>
                    <asp:UpdatePanel ID="udpnlSearchTagg" runat="server">
                        <ContentTemplate>
                            <div class="col-md-4">
                                <div class="box box-warning">
                                    <div class="box-header">
                                        <h3 class="box-title">Search Tagged Citation</h3>
                                    </div>
                                    <div class="box-body">

                                        <div class="form-group">
                                            <label>
                                                Citation: *
                                        
                                            </label>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtTaggedCitationSearchYear" runat="server" class="form-control" ToolTip="Year" Width="60" placeholder="Year"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTaggedCitationSearchJournal" runat="server" class="form-control" Width="90"></asp:DropDownList></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTaggedCitationSearchNo" runat="server" class="form-control" ToolTip="Number" Width="60" placeholder="No."></asp:TextBox></td>
                                                </tr>
                                            </table>

                                        </div>

                                        <asp:Button ID="btnTaggedCitationSearch" runat="server" CssClass="btn btn-primary" Text="Search" Width="150" CausesValidation="false" OnClick="btnTaggedCitationSearch_Click" />
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="udpnlSearchTagg">
                                            <ProgressTemplate>


                                                <img alt="" src="media/img/loader.gif" />




                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        &nbsp;&nbsp;
                                 
                                
                                    </div>
                                    <br />
                                    <br />



                                </div>


                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="col-md-9" style="float: right">

                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Tagged Citations</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                            </div>
                            <!-- /.box-body -->

                            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
                                Width="100%" AllowPaging="True" PageSize="20" class="table table-bordered table-hover"
                                OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting"
                                OnRowEditing="gv_RowEditing">

                                <Columns>
                                    <asp:TemplateField HeaderText="Statute Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Element Data">
                                        <ItemTemplate>
                                            <asp:Label ID="lblElementData" runat="server" Text='<%# Eval("ElementData") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Citation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCitation" runat="server" Text='<%# Eval("Citation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("strActive") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                                ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" />
                                            <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete" Visible="true"
                                                ImageUrl="~/adminpanel/media/img/Delete.png" Height="16" Width="16" CausesValidation="false" />
                                            <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                </Columns>
                            </asp:GridView>
                        </div>
                        <!-- /.box -->
                    </div>
                </div>





            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

