<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddGeneralAreas.aspx.cs" Inherits="EastlawUI_v2.adminpanel.AddGeneralAreas"
    MasterPageFile="~/adminpanel/Site1.Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc12" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
   <%-- <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>
             
      <section class="content-header">
                    <h1>
                        General
                        <small>General Area Details</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">General Area</li>
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
                                <h3 class="box-title">General Area Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                 <div class="form-group">
                                    <label>
                                        Area Type: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="ddlGeneralAreaType" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlGeneralAreaType" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlGeneralAreaType_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Subject: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtSubject" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtSubject" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Date: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="txtDate" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtDate" runat="server" class="form-control"> </asp:TextBox>
                                    <cc12:CalendarExtender runat="server"
    TargetControlID="txtDate"
    CssClass="ClassName"
    Format="MMMM d, yyyy"
     />
                                </div>

                                
                                <div class="form-group" id="divAuthor" style="display:none" runat="server">
                                    <label>
                                        Author: 
                                                
                                    </label>
                                    <asp:TextBox ID="txtAuthor" runat="server" class="form-control"> </asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>
                                        Practice Area Category: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlPracticeAreaCat" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlPracticeAreaCat" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPracticeAreaCat_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Practice Area Sub Category: *
                                                 
                                    </label>

                                    <asp:CheckBoxList ID="chkPracticeAreaSubCat" runat="server" RepeatColumns="3"></asp:CheckBoxList>
                                     <asp:Button ID="btnAddPracticeAreaSubCat" runat="server" CssClass="btn btn-primary" Text="Add Practice Areas" Width="150" CausesValidation="false" OnClick="btnAddPracticeAreaSubCat_Click"  />
                                    <asp:GridView ID="gvPracticeAreaSubCat" runat="server" AutoGenerateColumns="false" >
                                        <Columns>
                                        <asp:TemplateField HeaderText="Practice Area Sub Categories">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" />
                            </itemtemplate>
                                        </asp:TemplateField>
                                            </Columns>
                                    </asp:GridView>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Documents: *
                                                 
                                    </label>

                                   <asp:FileUpload ID="FDocUpload" runat="server" class="form-control" />
                                     <asp:Button ID="btnAddDocUpload" runat="server" CssClass="btn btn-primary" Text="Add Document" Width="150" CausesValidation="false" OnClick="btnAddDocUpload_Click"  />
                                     <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="false">
                                         <Columns>
                                             <asp:TemplateField HeaderText="Document">
                                                 <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                 <ItemTemplate>
                                                     <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                                     <asp:HiddenField ID="hdFileName" runat="server" Value='<%# Eval("DocFileName") %>' />

                                                     <asp:HyperLink ID="hypLnk" runat="server" NavigateUrl='<%# "../store/generalareadocs/" +  Eval("DocFileName") %>' Text='<%# Eval("DocFileName") %>' Target="_blank"></asp:HyperLink>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="">
                                                 <HeaderStyle CssClass="titleCareer" HorizontalAlign="Center" />
                                                 <ItemTemplate>
                                                     <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" />
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         </Columns>
                                     </asp:GridView>
                                </div>
                                
                                 <div class="form-group">
                                    <label>
                                       Short Description:         
                                    </label>
                                    <%--<cc1:editor ID="editorshortDes" runat="server"  Height="300px" Width="100%" />--%>
                                     <telerik:RadEditor runat="server" ID="editorshortDes"  Width="100%" Height="450" >
    <ImageManager ViewPaths="/store/generalareas/imgs" UploadPaths="/store/generalareas/imgs" />
    <DocumentManager ViewPaths="/store/generalareas/docs" UploadPaths="/store/generalareas/docs" />
    <MediaManager ViewPaths="/store/generalareas/videos" UploadPaths="/store/generalareas/videos" />
</telerik:RadEditor>
                                </div>
                                 <div class="form-group">
                                    <label>
                                      Full Description: 
                                    </label>
                                     
                                     <%--<cc1:editor ID="editorDes" runat="server"  Height="300px" Width="100%" />--%>

                                   <telerik:RadEditor runat="server" ID="editorDes"  Width="100%" Height="450" >
    <ImageManager ViewPaths="/store/generalareas/imgs" UploadPaths="/store/generalareas/imgs" />
    <DocumentManager ViewPaths="/store/generalareas/docs" UploadPaths="/store/generalareas/docs" />
    <MediaManager ViewPaths="/store/generalareas/videos" UploadPaths="/store/generalareas/videos" />
</telerik:RadEditor>
          <script type="text/javascript">
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
</script>
                                      
                                </div>
                                <div class="form-group">
                                    <label>
                                        Active:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
                                </div>




                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" Width="150"  />
                                &nbsp;&nbsp;
               
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" Width="150" OnClick="btnSave_Click"  />





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
            
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

