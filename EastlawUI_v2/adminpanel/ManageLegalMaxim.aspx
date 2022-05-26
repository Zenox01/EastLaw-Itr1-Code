<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageLegalMaxim.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManageLegalMaxim" 
    MasterPageFile="~/adminpanel/Site1.Master"%>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Legal Maxim
                        <small>Legal Maxim </small>
                    </h1>
                  <%--  <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Dictionary</li>
                    </ol>--%>
                </section>
      
    <section class="content">
                    <div class="row">
                        <!-- left column -->
                        <!--/.col (left) -->
                        <!-- right column -->
                        <%--<div class="col-md-6">--%>
                        <%--<span style="float:right;padding-right:15px">
                            <asp:Button ID="btnKeywordTagging" runat="server" CssClass="btn bg-maroon" Text="Keywords Tagging" Width="180px" OnClick="btnKeywordTagging_Click"  CausesValidation="false" />
                            <br />
                            <br />
                        </span>--%>
                        
                        <div class="col-xs-12">
                            <!-- general form elements disabled -->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Legal Maxim List</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                  
                                      



                                  
                                </div><!-- /.box-body -->
                            
                                <asp:GridView ID="grdContact" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="Id" OnRowCancelingEdit="grdContact_RowCancelingEdit" 
                                    OnRowDataBound="grdContact_RowDataBound" OnRowEditing="grdContact_RowEditing" 
                                    OnRowUpdating="grdContact_RowUpdating" OnRowCommand="grdContact_RowCommand" 
                                    ShowFooter="True" OnRowDeleting="grdContact_RowDeleting"
                                     class="table table-bordered table-hover"
                                    Width="100%"> 
        <Columns> 
            
            <asp:TemplateField HeaderText="Word" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEditWord" runat="server" Text='<%# Bind("Word") %>'></asp:TextBox> 
                     <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:TextBox ID="txtNewWord" runat="server" ></asp:TextBox> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblWord" runat="server" Text='<%# Bind("Word") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Meaning" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEditMeaning" runat="server" Text='<%# Bind("Meaning") %>'></asp:TextBox> 
                    
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:TextBox ID="txtNewMeaning" runat="server" TextMode="MultiLine" ></asp:TextBox> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblMeaning" runat="server" Text='<%# Bind("Meaning") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
             
            <asp:TemplateField HeaderText="Active"> 
                <EditItemTemplate> 
                    <asp:CheckBox ID="chkActive" runat="server" />
                </EditItemTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblActive" runat="server" Text='<%# Eval("Active") %>'></asp:Label> 
                </ItemTemplate> 
                <FooterTemplate> 
                    <asp:CheckBox ID="chkNewActive" runat="server" />
                </FooterTemplate> 
            </asp:TemplateField> 
            
            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton> 
                    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton> 
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:LinkButton ID="lnkAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Insert"></asp:LinkButton> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton> 
                </ItemTemplate> 
            </asp:TemplateField> 

            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" /> 
        </Columns> 
        </asp:GridView> 
 <asp:UpdateProgress ID="upProcessReg" runat="server" AssociatedUpdatePanelID="updPnl">
                                            <ProgressTemplate>
                                                <img src="../images/ajax-loader.gif"  />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
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
    </div><!-- /.box -->
                        </div><!--/.col (right) -->
                         
                    </div>   
                </section>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



