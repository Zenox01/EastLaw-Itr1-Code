<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCourts.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManageCourts" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Cases
                        <small>Courts Group</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Courts Group</li>
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
                                    <h3 class="box-title">Courts Group List</h3>
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
                                <asp:GridView ID="grdContact" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="court" OnRowCancelingEdit="grdContact_RowCancelingEdit" 
                                    OnRowDataBound="grdContact_RowDataBound" OnRowEditing="grdContact_RowEditing" 
                                    OnRowUpdating="grdContact_RowUpdating" OnRowCommand="grdContact_RowCommand" 
                                    ShowFooter="True" OnRowDeleting="grdContact_RowDeleting"
                                     class="table table-bordered table-hover"
                                    Width="100%"> 
        <Columns> 
            
            <asp:TemplateField HeaderText="Court Name" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEdit" runat="server" Text='<%# Bind("court") %>'></asp:TextBox> 
                     <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("court") %>' />
                    <asp:Label ID="lblOldName" runat="server" Value='<%# Eval("court") %>'  ></asp:Label>
                </EditItemTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("court") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="No. of Cases" HeaderStyle-HorizontalAlign="Left"> 
              
                <ItemTemplate> 
                    <asp:Label ID="lblCount" runat="server" Text='<%# Bind("NoOfRecords") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Area Name" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEditArea" runat="server" ></asp:TextBox> 
                     
                </EditItemTemplate> 
               
            </asp:TemplateField> 
            <%-- <asp:TemplateField HeaderText="Court Master" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:RequiredFieldValidator ID="rfvCourtMaster" runat="server" ControlToValidate="ddlEditCourtMaster" InitialValue="0" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                     <asp:DropDownList ID="ddlEditCourtMaster" runat="server" DataTextField="CourtName" DataValueField="ID"> </asp:DropDownList> 
                </EditItemTemplate> 
              
                <ItemTemplate> 
                    <asp:Label ID="lblCourtMaster" runat="server" Text='<%# Bind("MasterCourtName") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> --%>
            
            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton> 
                    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton> 
                </EditItemTemplate> 
                
                <ItemTemplate> 
                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton> 
                </ItemTemplate> 
            </asp:TemplateField> 

           
        </Columns> 
        </asp:GridView> 

    </div><!-- /.box -->
                        </div><!--/.col (right) -->
                         
                    </div>   
                </section>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>