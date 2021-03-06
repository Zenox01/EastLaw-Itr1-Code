<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCourtsMaster.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManageCourtsMaster" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <%--    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>
             
      <section class="content-header">
                    <h1>
                        Courts
                        <small>Courts Master List</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Manage Courts Master</li>
                    </ol>
                </section>
      
    <section class="content">
                    <div class="row">
                        <!-- left column -->
                        <!--/.col (left) -->
                        <!-- right column -->
                        <%--<div class="col-md-6">--%>
                      
                        
                        <div class="col-xs-12">
                            <!-- general form elements disabled -->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Courts Master List</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                  
                                      



                                  
                                </div><!-- /.box-body -->
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
                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="Id" OnRowCancelingEdit="gv_RowCancelingEdit" 
                                    OnRowDataBound="gv_RowDataBound" OnRowEditing="gv_RowEditing" 
                                    OnRowUpdating="gv_RowUpdating" OnRowCommand="gv_RowCommand" 
                                    ShowFooter="True" OnRowDeleting="gv_RowDeleting"
                                     class="table table-bordered table-hover"
                                    Width="100%"> 
        <Columns> 
             
            <asp:TemplateField HeaderText="Court Name" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEditCourtName" runat="server" Text='<%# Bind("CourtName") %>'></asp:TextBox> 
                     
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:TextBox ID="txtNewCourtName" runat="server" ></asp:TextBox> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblCourtName" runat="server" Text='<%# Bind("CourtName") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Sort Order" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEditSortOrder" runat="server" Text='<%# Bind("SortOrder") %>'></asp:TextBox> 
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:TextBox ID="txtNewSortOrder" runat="server" Text="0" ></asp:TextBox> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblSortOrder" runat="server" Text='<%# Bind("SortOrder") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 

             <asp:TemplateField HeaderText="Country"> 
               
                <ItemTemplate> 
                    <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Country") %>'></asp:Label> 
                </ItemTemplate> 
               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Area"> 
               
                <ItemTemplate> 
                    <asp:Label ID="lblJrd_Area" runat="server" Text='<%# Eval("Jrd_Area") %>'></asp:Label> 
                </ItemTemplate> 
               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="District"> 
               
                <ItemTemplate> 
                    <asp:Label ID="District" runat="server" Text='<%# Eval("Jrd_Distrct") %>'></asp:Label> 
                </ItemTemplate> 
               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hierarchy"> 
               
                <ItemTemplate> 
                    <asp:Label ID="lblhierarchy" runat="server" Text='<%# Eval("CourtHeir") %>'></asp:Label> 
                </ItemTemplate> 
               
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Appeal Types"> 
               
                <ItemTemplate> 
                    <asp:Label ID="lblAppealTypes" runat="server" Text='<%# Eval("CourtAppealTypes") %>'></asp:Label> 
                </ItemTemplate> 
               
            </asp:TemplateField>
             
            <asp:TemplateField HeaderText="Active"> 
                <EditItemTemplate> 
                    <asp:CheckBox ID="chkActive" runat="server" />
                </EditItemTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblActive" runat="server" Text='<%# Eval("strActive") %>'></asp:Label> 
                </ItemTemplate> 
                <FooterTemplate> 
                    <asp:CheckBox ID="chkNewActive" runat="server" />
                </FooterTemplate> 
            </asp:TemplateField> 

               <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                    ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" />
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                           <%--     <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete" 
                                    ImageUrl="~/adminpanel/media/img/Delete.png" Height="16" Width="16" CausesValidation="false" />--%>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
            
          <%--  <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Left"> 
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
            </asp:TemplateField> --%>

            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" Visible="false" /> 
        </Columns> 
        </asp:GridView> 

    </div><!-- /.box -->
                        </div><!--/.col (right) -->
                         
                    </div>   
                </section>
            
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>