<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageStatutesIndex.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManageStatutesIndex" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
<%--     <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>
             
      <section class="content-header">
                    <h1>
                        Statutes
                        <small>Statutes Index</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Manage Statutes Index </li>
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
                                <h3 class="box-title">Index Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                 <div class="form-group">
                                    <label>
                                        Section/Article/Rule : *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlSelectSec" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>

                                    </label>
                                     <asp:DropDownList ID="ddlSelectSec" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlSelectSec_SelectedIndexChanged">
                                         <asp:ListItem Text="None" Value="0"></asp:ListItem>
                                         <asp:ListItem Text="Existing" Value="Existing"></asp:ListItem>
                                         <asp:ListItem Text="New" Value="New"></asp:ListItem>
                                     </asp:DropDownList>
                                    
                                </div>
                                <div class="form-group" id="divExistingSec" runat="server" style="display:none">
                                    <label>
                                        Select Section/Article/Rule : *
                                                 <asp:RequiredFieldValidator ID="rfvExistingSec" runat="server"
                                                     ControlToValidate="ddlSection" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>
                                     <asp:DropDownList ID="ddlSection" runat="server" class="form-control">
                                     </asp:DropDownList>
                                    
                                </div>
                                <div class="form-group" id="divNewSec" runat="server" style="display:none">
                                    <label>
                                        Enter New Section/Article/Rule : * <asp:RequiredFieldValidator ID="rfvType" runat="server"
                                                     ControlToValidate="txtTypeNew" ErrorMessage="Type Required" ForeColor="Red"  Enabled="false"> </asp:RequiredFieldValidator>
                                       <%-- <asp:RequiredFieldValidator ID="rfvNewNo" runat="server"
                                                     ControlToValidate="txtNo" ErrorMessage="Type Required" ForeColor="Red" Enabled="false"> </asp:RequiredFieldValidator>
                                --%>              

                                    </label>
                                    <table>
                                        <tr>
                                            <td>
                                               

                                               <%-- <asp:DropDownList ID="ddlType" runat="server" class="form-control" >
                                         <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                         <asp:ListItem Text="Section" Value="Section"></asp:ListItem>
                                         <asp:ListItem Text="Article" Value="Article"></asp:ListItem>
                                                    <asp:ListItem Text="Rule" Value="Rule"></asp:ListItem>
                                     </asp:DropDownList>--%>
                                                <asp:TextBox ID="txtTypeNew" runat="server" class="form-control" Width="100"> </asp:TextBox>
                                            </td>
                                            <%--<td><asp:TextBox ID="txtNo" runat="server" class="form-control" Width="100"> </asp:TextBox></td>--%>
                                        </tr>
                                    </table>
                                      
                                    
                                    
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Select Chapter/Schdules : *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="ddlChapter_Schdules" ErrorMessage="Required" ForeColor="Red" Enabled="false" InitialValue="N/A"></asp:RequiredFieldValidator>
                                    </label>
                                     <asp:DropDownList ID="ddlChapter_Schdules" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlChapter_Schdules_SelectedIndexChanged">
                                         <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem>
                                         <asp:ListItem Text="Chapter" Value="Chapter"></asp:ListItem>
                                         <asp:ListItem Text="Schedule" Value="Schedule"></asp:ListItem>
                                         <asp:ListItem Text="Division" Value="Division"></asp:ListItem>
                                         <asp:ListItem Text="Part" Value="Part"></asp:ListItem>
                                     </asp:DropDownList>
                                    
                                </div>
                                 <div class="form-group" id="divChapter_Scdhules" runat="server" style="display:none">
                                    <label>
                                        Select <asp:Label ID="lblChapSch" runat="server"></asp:Label> : *
                                                

                                    </label>
                                     <asp:DropDownList ID="ddlChapSchd" runat="server" class="form-control">
                                     </asp:DropDownList>
                                    
                                </div>
                                <div class="form-group" id="divPart_Schedule" runat="server" style="display:none">
                                    <label>
                                        Enter New Chapter/Part/Schdule : 
                                      
                                    </label>
                                    <asp:TextBox ID="txtPart_Schedule" runat="server" class="form-control" Width="100"> </asp:TextBox>
                                    
                                </div>
                                <div class="form-group">
                                    <label>
                                        Index Title : *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtIndexTitle" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtIndexTitle" runat="server" class="form-control"> </asp:TextBox>
                                    <asp:Label ID="lblID" runat="server" Visible="false" Text="0"></asp:Label>
                                </div>

                              
                                <div class="form-group">
                                    <label>
                                       Index Content:         
                                    </label>
                                    <%--<cc1:editor ID="editorContent" runat="server"  Height="300px" Width="100%" />--%>

                                    <telerik:RadEditor runat="server" ID="editorContent"  Width="100%" Height="450px" >
    <ImageManager ViewPaths="/store/statutes/imgs" UploadPaths="/store/statutes/imgs" MaxUploadFileSize="1000000" AllowMultipleSelection="true" DeletePaths="/"/> 
    <DocumentManager ViewPaths="/store/statutes/docs" UploadPaths="/store/statutes/docs" MaxUploadFileSize="10000000" />
    <MediaManager ViewPaths="/store/statutes/videos" UploadPaths="/store/statutes/videos" MaxUploadFileSize="10000000"/>
</telerik:RadEditor>

                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    
                                <%--    <asp:TextBox ID="txtEditor" runat="server" class="ckeditor" TextMode="MultiLine" Height="500" Width="600">

                                    </asp:TextBox>--%>
                                    <%--<textarea class="ckeditor" cols="80" id="editor1" name="editor1" rows="10" runat="server">
             
     
        </textarea>--%>
                                </div>
                                
                                 <div class="form-group">
                                    <label>
                                        Foot Note: 
                                                

                                    </label>
                                    <asp:TextBox ID="txtFootnote" runat="server" class="form-control" TextMode="MultiLine"> </asp:TextBox>
                                    
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
                                 <asp:Button ID="btnSave0" runat="server" CssClass="btn btn-primary" OnClick="btnSave0_Click" Text="test" Width="150" />
                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                    <!--/.col (right) -->

                </div>
                

            </section>
            <div class="col-xs-12">

            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Index</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->
                

                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="false" PageSize="20" class="table table-bordered table-hover"
                     onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound" onrowdeleting="gv_RowDeleting" 
                        onrowediting="gv_RowEditing">

                    <Columns>
                        <asp:TemplateField HeaderText="Index Title">
                            <ItemTemplate>
                                <asp:Label ID="lblIndexTitle" runat="server" Text='<%# Eval("IndexTitle") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section">
                            <ItemTemplate>
                                <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Chapter/Schedule">
                            <ItemTemplate>
                                <asp:Label ID="lblChapter_Schedule" runat="server" Text='<%# Eval("Chapter_Schedule") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Chapter/Schedule Title">
                            <ItemTemplate>
                                <asp:Label ID="lblChapter_SchduleTitle" runat="server" Text='<%# Eval("Chapter_SchduleTitle") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Part/Schedule">
                            <ItemTemplate>
                                <asp:Label ID="lblPart_Schedule" runat="server" Text='<%# Eval("Part_Schedule") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sort Order">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSortOrder" runat="server" Text='<%# Eval("SortOrder") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                         
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                    ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" />
                                <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete"
                                    ImageUrl="~/adminpanel/media/img/Delete.png" Height="16" Width="16" CausesValidation="false" />
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>



                    </Columns>
                </asp:GridView>
                <span style="float:right">
                    <asp:Button ID="btnUpdateSortOrder" runat="server" Text="Update Sort Order" CssClass="btn btn-primary" Width="200" OnClick="btnUpdateSortOrder_Click" CausesValidation="false" />
                    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPnl">
                                            <ProgressTemplate>
                                                <img src="../images/ajax-loader.gif"  />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>--%>
                     <div class="alert alert-danger alert-dismissable" id="divFailed2" runat="server" style="display: none">
                                    <button type="button" class="close" data-dismiss="alert">
                                        ×</button>
                                    <strong>Transaction failed ... </strong>
                                </div>
                                <div class="alert alert-info alert-dismissable" id="divSucuss2" runat="server" style="display: none">
                                    <button type="button" class="close" data-dismiss="alert">
                                        ×</button>
                                    <strong>Transaction success !</strong>
                                </div>
                    </span>
                

            </div>
          </div>
            
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


