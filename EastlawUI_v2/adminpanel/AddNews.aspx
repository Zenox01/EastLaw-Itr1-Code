<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNews.aspx.cs" Inherits="EastlawUI_v2.adminpanel.AddNews" 
    MasterPageFile="~/adminpanel/Site1.Master"%>



<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
     <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        News
                        <small>News Details</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">News Details </li>
                    </ol>
                </section>
      
            <section class="content">
                <div class="row">
                    <!-- left column -->
                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-xs-8">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">News Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="form-group">
                                    <label>
                                        Type : *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlType" ErrorMessage="Required" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:DropDownList ID="ddlType" runat="server" class="form-control">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="News" Text="News"></asp:ListItem>
                                        <asp:ListItem Value="Update" Text="Update"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Title :

                                    </label>
                                    <asp:TextBox ID="txtURL" runat="server" class="form-control"> </asp:TextBox>
                                    <asp:Button ID="btnFetchURL" runat="server" OnClick="btnFetchURL_Click" Text="Fetch URL" Width="200" CausesValidation="false" />
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Practice Area:

                                    </label>
                                    <asp:DropDownList ID="ddlPA" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                
                                <div class="form-group">
                                    <label>
                                        Title : *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtTitle" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control"> </asp:TextBox>
                                    <asp:Label ID="lblID" runat="server" Visible="false" Text="0"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Court:
                                    </label>
                                     <asp:DropDownList ID="ddlCourtMaster" runat="server" class="chosen-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCourtMaster_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Judge:
                                    </label>
                                     <asp:DropDownList ID="ddlNewJudge" runat="server" class="chosen-select" AutoPostBack="false">
                                         <asp:ListItem Value="0" Text="Not Applicable"></asp:ListItem>
                                     </asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                       Statute Categories: *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                     ControlToValidate="ddlStatuteCategories" ErrorMessage="Required" ForeColor="Red" InitialValue="0" ValidationGroup="OtherCat"></asp:RequiredFieldValidator>
                                    </label>
                                    
                                    <asp:DropDownList ID="ddlStatuteCategories" runat="server" class="chosen-select" ValidationGroup="OtherCat"></asp:DropDownList>
                                    <br /><br />
                                    <asp:Button ID="btnOtherCategories" runat="server" Text="Add"  CssClass="btn btn-primary" ValidationGroup="OtherCat" Width="150" OnClick="btnOtherCategories_Click" />
                                    
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Keyword / Hastags :

                                    </label>
                                    <asp:TextBox ID="txtKeywords" runat="server" class="form-control"> </asp:TextBox>
                                    
                                </div>
                                <div class="form-group">
                                    <label>
                                        Date :
                                    </label>
                                    <asp:TextBox ID="txtDate" runat="server" class="form-control"> </asp:TextBox>
                                    
                                </div>
                                 <div class="form-group">
                                    <label>
                                       Source :
                                    </label>
                                    <asp:TextBox ID="txtSource" runat="server" class="form-control"> </asp:TextBox>
                                    
                                </div>
                                 <div class="form-group">
                                    <label>
                                       Source Link :
                                    </label>
                                    <asp:TextBox ID="txtSourceLink" runat="server" class="form-control"> </asp:TextBox>
                                    
                                </div>
                                <div class="form-group">
                                    <label>
                                       Author :
                                    </label>
                                    <asp:TextBox ID="txtAuthor" runat="server" class="form-control"> </asp:TextBox>
                                    
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Manual Image Upload:
                                         
                                    </label>
                                    
                                   <asp:FileUpload ID="fuploadimage" runat="server" class="form-control" />
                                    <asp:Label ID="lblfuploadWord" runat="server" Visible="false"></asp:Label>
                                    
                                    
                                </div>
                                <div class="form-group">
                                    <label>
                                       Images :
                                    </label>
                                  <asp:GridView ID="gvImgs" runat="server" AutoGenerateColumns="false">
                                      <Columns>
                                          <asp:TemplateField>
                                              <ItemTemplate>
                                                   <img src='<%# Eval("ImgSrc") %>' width="200" height="200"/>
                                                  <asp:HiddenField ID="hdImgSrc" runat="server" Value='<%# Eval("ImgSrc") %>' />
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                          <asp:TemplateField ControlStyle-Width="400">
                                              <ItemTemplate>
                                                  <asp:RadioButton ID="radioChoose" runat="server" AutoPostBack="True" OnCheckedChanged="radioChoose_CheckedChanged" />
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                      </Columns>
                                  </asp:GridView>
                                    
                                </div>
                                <div class="form-group">
                                    <label>
                                       Short Content :
                                    </label>
                                    <asp:TextBox ID="txtShortContent" runat="server" class="form-control" TextMode="MultiLine"> </asp:TextBox>
                                    
                                </div>
                               
                              
                                <div class="form-group">
                                    <label>
                                       Full Content:         
                                    </label>
                                    <%--<cc1:editor ID="editorContent" runat="server"  Height="300px" Width="100%" />--%>

                                    <telerik:RadEditor runat="server" ID="editorContent"  Width="100%" Height="450" >
    <ImageManager ViewPaths="/store/news/imgs" UploadPaths="/store/news/imgs" MaxUploadFileSize="1000000"/>
    <DocumentManager ViewPaths="/store/news/docs" UploadPaths="/store/news/docs" MaxUploadFileSize="10000000" />
    <MediaManager ViewPaths="/store/news/videos" UploadPaths="/store/news/videos" MaxUploadFileSize="10000000"/>
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
                    <div class="col-md-4">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Statute Categories</h3>
                            </div>
                            
     
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                               <asp:GridView ID="gvOtherCat" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" >
                                        <Columns>
                                        <asp:TemplateField HeaderText="Category Name">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("OtherCategoryID") %>' />
                                
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("OtherCategoryName") %>'></asp:Label>
                                                <hr />
                            </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/adminpanel/media/img/Delete.png" Width="20" Height="20" />
                            </itemtemplate>
                                        </asp:TemplateField>
                                            </Columns>
                                    </asp:GridView>





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



