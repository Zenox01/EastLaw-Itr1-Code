<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsletterList.aspx.cs" Inherits="EastlawUI_v2.adminpanel.newsletter.NewsletterList"
    MasterPageFile="~/adminpanel/Site1.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cntplc">
    <section class="content-header">
        <h1>Newsletter
                        <small>Newsletter List</small>
        </h1>

    </section>

    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Newsletters List</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body table-responsive">


                        <asp:GridView ID="gvLst" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover" Width="100%" OnRowEditing="gvLst_RowEditing" OnRowDeleting="gvLst_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="Newsletter Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNewsletterType" runat="server" Text='<%# Eval("NewsletterType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Newsletter Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNewsletterTitle" runat="server" Text='<%# Eval("NewsletterTitle") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Template Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTemplateName" runat="server" Text='<%# Eval("TemplateName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("strActive") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>

                                        <a href='<%# "viewnewsletter.aspx?id=" +  Eval("ID") %>' target="_blank">View</a>
                                        <a href='<%# "/adminpanel/newsletter/htmlfiles/"+ Eval("ID") +".html"%>' target="_blank" download>Download</a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-danger btn-flat" CommandName="Edit" />

                                        <asp:Button ID="btnSendTest" runat="server" Text="Send Test Email" CssClass="btn btn-danger btn-flat" CommandName="Delete" />                                        

                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <!-- /.box-body -->
                </div>
            </div>
        </div>
        <!-- /.row -->
    </section>

</asp:Content>


