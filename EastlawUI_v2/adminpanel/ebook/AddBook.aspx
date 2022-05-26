<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ebook.AddBook"
    MasterPageFile="~/adminpanel/Site1.Master" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        E-Book & Statutes 
                        <small>Add/Edit E-Book & Statutes </small>
                    </h1>
                    
                </section>
      
            <section class="content">
                <div class="row">
                    <!-- left column -->
                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-10">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">E-Book & Statutes Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="form-group">
                                    <label>
                                        E-Book/Statute Category: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlType" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlType" runat="server" class="form-control">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="E-Book" Text="E-Book"></asp:ListItem>
                                        <asp:ListItem Value="Statute" Text="Statute"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        E-Book & Statutes Category: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="ddlCategory" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlCategory" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                       Book/ Statutes Title: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtTitle" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                       Cover Photo: *
                                                 <asp:RequiredFieldValidator ID="rfvFupload" runat="server"
                                                     ControlToValidate="fupload" ErrorMessage="Required" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>

                                    </label>
                                    
                                    <asp:FileUpload ID="fupload" runat="server" class="form-control"/>
                                    <asp:Label ID="lblCoverUpload" runat="server" Visible="false"></asp:Label>
                                    <asp:Image ID="imgCover" runat="server"  Visible="false"/>
                                </div>

                                <div class="form-group">
                                    <label>
                                        Short Info: 
                                                
                                    </label>
                                    <asp:TextBox ID="txtShortInfo" runat="server" class="form-control" TextMode="MultiLine" Height="100"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Author:*  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="txtAuthor" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                
                                    </label>
                                    <asp:TextBox ID="txtAuthor" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Published On:* (MM/DD/YYYY)  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                     ControlToValidate="txtPublishedOn" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                
                                    </label>
                                    <asp:TextBox ID="txtPublishedOn" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        No. of Pages:*  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                     ControlToValidate="txtNoOfPages" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                
                                    </label>
                                    <asp:TextBox ID="txtNoOfPages" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Subscription Price:*  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                     ControlToValidate="txtSubscriptionPrice" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                
                                    </label>
                                    <asp:TextBox ID="txtSubscriptionPrice" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>
                                       Overview: 
                                                
                                    </label>
                                    
                                     
                                    <telerik:RadEditor runat="server" ID="editorOverview"  Width="100%" Height="450" >
    <ImageManager ViewPaths="/store/ebook/imgs" UploadPaths="/store/ebook/imgs" MaxUploadFileSize="1000000"/>
    <DocumentManager ViewPaths="/store/ebook/docs" UploadPaths="/store/ebook/docs" MaxUploadFileSize="10000000" />
    <MediaManager ViewPaths="/store/ebook/videos" UploadPaths="/store/ebook/videos" MaxUploadFileSize="10000000"/>
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
                    <!--/.col (right) -->

                </div>
                

            </section>
            
        </ContentTemplate>
         <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
    </asp:UpdatePanel>
</asp:Content>
