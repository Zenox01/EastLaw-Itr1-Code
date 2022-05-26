<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageEBooks.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ebook.ManageEBooks"
    MasterPageFile="~/adminpanel/Site1.Master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                       E-Books
                        <small>E-Books List</small>
                    </h1>
                    
                </section>
      
    <section class="content">
                    <div class="row">
                         
                    </div>   
                        <div class="col-xs-12">
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
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">E-Books/Statutes List</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->

                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"  
                    Width="100%" AllowPaging="True" PageSize="20" class="table table-bordered table-hover"
                     onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound" onrowdeleting="gv_RowDeleting" OnRowCommand="gv_RowCommand"
                        onrowediting="gv_RowEditing">

                    <Columns>
                         <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("DType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EBook/Statutes Category">
                            <ItemTemplate>
                                <asp:Label ID="lblEBookCat" runat="server" Text='<%# Eval("EBookCat") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Author">
                            <ItemTemplate>
                                <asp:Label ID="lblAuthor" runat="server" Text='<%# Eval("Author") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Published On">
                            <ItemTemplate>
                                <asp:Label ID="lblPublishedOn" runat="server" Text='<%# Eval("PublishedOn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="No. Of Pages">
                            <ItemTemplate>
                                <asp:Label ID="lblNoOfPages" runat="server" Text='<%# Eval("NoOfPages") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SubscriptionPrice">
                            <ItemTemplate>
                                <asp:Label ID="lblSubscriptionPrice" runat="server" Text='<%# Eval("SubscriptionPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                      
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("strActive") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Manage Index">
                            <ItemTemplate>
                                
                                <asp:LinkButton ID="lnkManage" runat="server" CommandName="ManageIndex" Text="Manage" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                    ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" />
                                <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete" Visible="true"
                                    ImageUrl="~/adminpanel/media/img/Delete.png" Height="16" Width="16" CausesValidation="false" OnClientClick='return confirm("Are you sure you want to delete this user?");'/>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>



                    </Columns>
                </asp:GridView>
            </div>
            <!-- /.box -->
        </div>
       
                </section>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

