<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewsletter.aspx.cs" Inherits="EastlawUI_v2.adminpanel.newsletter.AddNewsletter" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Newsletter
                        <small>Add/Edit Newsletter</small>
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
                                <h3 class="box-title">Newsletter Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                 <div class="form-group">
                                    <label>
                                        Newsletter Type: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="ddlNewsletterType" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlNewsletterType" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlNewsletterType_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="General" Text="General"></asp:ListItem>
                                        <asp:ListItem Value="Elements" Text="Elements"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Newsletter Template: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="ddlNewsletterTemplate" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlNewsletterTemplate" runat="server" class="form-control">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="Template 01" Text="Template 01"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                
                                <div class="form-group">
                                    <label>
                                       Newsletter Subject: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="txtNewsletterTitle" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtNewsletterTitle" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                       Banner: *
                                                 <asp:RequiredFieldValidator ID="rfvFupload" runat="server"
                                                     ControlToValidate="fupload" ErrorMessage="Required" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>

                                    </label>
                                    
                                    <asp:FileUpload ID="fupload" runat="server" class="form-control"/>
                                    <asp:Label ID="lblCoverUpload" runat="server" Visible="false"></asp:Label>
                                    <asp:Image ID="imgCover" runat="server"  Visible="false"/>
                                </div>

                                <div class="form-group" id="divTemplateSelection" runat="server" style="display:none">
                                    <label>
                                       Template: *
                                                 <asp:RequiredFieldValidator ID="rfvddlTemplate" runat="server"
                                                     ControlToValidate="ddlTemplate" ErrorMessage="Required" ForeColor="Red" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>

                                    </label>
                                    
                                      <asp:DropDownList ID="ddlTemplate" runat="server" class="form-control">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="Template 01" Text="Template 01"></asp:ListItem>
                                        
                                    </asp:DropDownList>
                                </div>
                             
                                 <div class="form-group" id="divNewsletterContent" runat="server" style="display:none">
                                    <label>
                                       Newsletter Content: 
                                                
                                    </label>
                                    
                                     
                                    <telerik:RadEditor runat="server" ID="editorContent"  Width="100%" Height="450" >
    <ImageManager ViewPaths="/adminpanel/newsletter/mediafiles/imgs" UploadPaths="/adminpanel/newsletter/mediafiles/imgs" MaxUploadFileSize="1000000"/>
    <DocumentManager ViewPaths="/adminpanel/newsletter/mediafiles/docs" UploadPaths="/adminpanel/newsletter/mediafiles/docs" MaxUploadFileSize="10000000" />
    <MediaManager ViewPaths="/adminpanel/newsletter/mediafiles/videos" UploadPaths="/adminpanel/newsletter/mediafiles/videos" MaxUploadFileSize="10000000"/>
</telerik:RadEditor>
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
                     <div class="col-md-6">
                        <div class="box box-warning" >
                            <div class="box-header">
                                <h3 class="box-title">Cases </h3>
                            </div>
                            
                            <!-- /.box-header -->
                            <div class="box-body">
                                <asp:UpdatePanel ID="upPnlCitations" runat="server">
        <ContentTemplate>
              <div class="form-group">
                                    <label>
                                        Citation: *
                                                

                                    </label>
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="txtCitationYear" runat="server" class="form-control" ToolTip="Year"  Width="60" placeholder="Year" ></asp:TextBox></td>
                                            <td><asp:DropDownList ID="ddlJournalSearch" runat="server" class="form-control" Width="90"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="txtCitationNumber" runat="server" class="form-control" ToolTip="Number" Width="60" placeholder="No."></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"><asp:TextBox ID="txtCaseShortDes" runat="server" class="form-control" ToolTip=""  TextMode="MultiLine" Width="300" placeholder="Enter Short Text" ></asp:TextBox></td>
                                            </tr>
                                    </table>
                                            
                                </div>
                                   <%--<div class="form-group">
                                    <label>
                                       Citations: 
                                    </label>
                                      
                                       <asp:DropDownList ID="ddlCases" runat="server" class="chosen-select"></asp:DropDownList>
                                       
                                </div>--%>
                                 <div class="form-group">
                                    <asp:Button ID="btnAddCase" runat="server" CssClass="btn btn-primary" Text="Add" Width="150" OnClick="btnAddCase_Click" CausesValidation="false"/> 
                                      <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="upPnlCitations">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="../media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>  
                                </div>
                                <div class="form-group">
                                     <asp:GridView ID="gvCases" runat="server" class="table table-striped" AutoGenerateColumns="false" Width="100%" OnRowDeleting="gvCases_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item-Code">
                                        <ItemTemplate>
                                             <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("CaseID") %>' />
                                           <asp:Label ID="lblCaseTitle" runat="server" Text='<%# Eval("CaseTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Short Text">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtShortText" runat="server"  Text='<%# Eval("ShortText") %>' Width="250" Height="100" CausesValidation="false" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="true">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20"
                                    OnClientClick = " return confirm('Do you want to delete ?');" />
                            </itemtemplate>
                                        </asp:TemplateField>
                                     
                                </Columns>

                            </asp:GridView>
                                </div>
                              </ContentTemplate>
                                    </asp:UpdatePanel>
                               
                            </div>
                            <!-- /.box-body -->
                        </div>
                         <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Statutes </h3>
                            </div>
                            
                            <!-- /.box-header -->
                            <div class="box-body">
                                                            <asp:UpdatePanel ID="upPnlStatutes" runat="server">
        <ContentTemplate>
                                   <div class="form-group">
                                    <label>
                                       Statutes Title: 
                                    </label>
                                      <%-- <telerik:RadComboBox RenderMode="Lightweight" ID="ddlStatutes" runat="server" Height="200" Width="315" 
                                        DropDownWidth="315" EmptyMessage="" HighlightTemplatedItems="true"
                                        EnableLoadOnDemand="true" Filter="StartsWith"></telerik:RadComboBox>--%>
                                       <asp:DropDownList ID="ddlStatutes" runat="server" class="chosen-select"></asp:DropDownList>
                                       
                                </div>
                                 <div class="form-group">
                                    <asp:Button ID="btnAddStatutes" runat="server" CssClass="btn btn-primary" Text="Add" Width="150" OnClick="btnAddStatutes_Click" CausesValidation="false"/>  
                                     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlStatutes">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="../media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>  
                                </div>
                                <div class="form-group">
                                <asp:GridView ID="gvStatutes" runat="server" class="table table-striped" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Statutes Title">
                                        <ItemTemplate>
                                             <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("StatutesID") %>' />
                                           <asp:Label ID="lblStatutesTitle" runat="server" Text='<%# Eval("StatutesTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Short Text">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtShortText" runat="server"  Text='<%# Eval("ShortText") %>' Width="250" Height="100" CausesValidation="false" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                </Columns>

                            </asp:GridView>
                              </div>
                               </ContentTemplate>
                                                                </asp:UpdatePanel>
                            </div>
                            <!-- /.box-body -->
                        </div>
                         <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Departments </h3>
                            </div>
                            
                            <!-- /.box-header -->
                            <div class="box-body">
                                <asp:UpdatePanel ID="upPnlDept" runat="server">
        <ContentTemplate>
                                   <div class="form-group">
                                    <label>
                                       Department Name: 
                                    </label>
                                       <%--<telerik:RadComboBox RenderMode="Lightweight" ID="ddlDept" runat="server" Height="200" Width="315" 
                                        DropDownWidth="315" EmptyMessage="" HighlightTemplatedItems="true"
                                        EnableLoadOnDemand="true" Filter="StartsWith"></telerik:RadComboBox>--%>
                                       <asp:DropDownList ID="ddlDept" runat="server" class="chosen-select"></asp:DropDownList>
                                       
                                </div>
                                 <div class="form-group">
                                    <asp:Button ID="btnAddDept" runat="server" CssClass="btn btn-primary" Text="Add" Width="150" OnClick="btnAddDept_Click" CausesValidation="false"/>
                                     <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upPnlDept">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="../media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>    
                                </div>
                                <div class="form-group">
                                <asp:GridView ID="gvDept" runat="server" class="table table-striped" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item-Code">
                                        <ItemTemplate>
                                             <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("DeptID") %>' />
                                           <asp:Label ID="lblDeptTitle" runat="server" Text='<%# Eval("DeptTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Short Text">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtShortText" runat="server"  Text='<%# Eval("ShortText") %>' Width="250" Height="100" CausesValidation="false" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                </Columns>

                            </asp:GridView>
                              </div>
                              </ContentTemplate>
                                    </asp:UpdatePanel>
                               
                            </div>
                            <!-- /.box-body -->
                        </div>
                         <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">News </h3>
                            </div>
                            
                            <!-- /.box-header -->
                            <div class="box-body">
                                <asp:UpdatePanel ID="updPnlNews" runat="server">
        <ContentTemplate>
                                   <div class="form-group">
                                    <label>
                                       News Description: 
                                    </label>
                                       <asp:TextBox ID="txtNewsDes" runat="server"></asp:TextBox>
                                       
                                </div>
            <div class="form-group">
                                    <label>
                                       News URL/Link: 
                                    </label>
                                       <asp:TextBox ID="txtNewsLink" runat="server"></asp:TextBox>
                                       
                                </div>
                                 <div class="form-group">
                                    <asp:Button ID="btnAddNews" runat="server" CssClass="btn btn-primary" Text="Add" Width="150" OnClick="btnAddNews_Click" CausesValidation="false"/>
                                     <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upPnlDept">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="../media/img/loader.gif" />
      
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>    
                                </div>
                                <div class="form-group">
                                <asp:GridView ID="gvNews" runat="server" class="table table-striped" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item-Code">
                                        <ItemTemplate>
                                             <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                           <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("TItle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Short Text">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtShortText" runat="server"  Text='<%# Eval("ShortText") %>' Width="250" Height="100" CausesValidation="false" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="URL">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtURL" runat="server"  Text='<%# Eval("URL") %>' Width="250" Height="100" CausesValidation="false" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                </Columns>

                            </asp:GridView>
                              </div>
                              </ContentTemplate>
                                    </asp:UpdatePanel>
                               
                            </div>
                            <!-- /.box-body -->
                        </div>
                         
                        <!-- general form elements disabled -->
                        
                        <!-- /.box -->
                   
                   
                       
                    </div>
                    
                    
                    <!--/.col (right) -->

                </div>
                

            </section>
            
        </ContentTemplate>
         <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
    </asp:UpdatePanel>
</asp:Content>
