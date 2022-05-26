<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagePracticeCategories.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManagePracticeCategories" 
    MasterPageFile="~/adminpanel/Site1.Master"%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Masters
                        <small>Practice Area Category Details</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Practice Area</li>
                    </ol>
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
                                    <h3 class="box-title">Practice Area Category Details</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                  
                                        <div class="form-group">
                                            <label>Practice Area Category Name: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtCatName" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                <asp:Label ID="lblID" runat="server" Visible="false" Text="0"></asp:Label>
                                     </label>
                                            <asp:TextBox ID="txtCatName" runat="server" class="form-control"> </asp:TextBox>
                                           </div>

                                        <div class="form-group">
                                            <label>Description: 
                                       </label>
                                            <asp:TextBox ID="txtDes" runat="server" class="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                                        </div>
                                            
                                              <div class="form-group">
                                            <label>Active:
                                            </label>
                                                  <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
                                            </div>

                                           

                                         
                                     <asp:Button ID="btnCancel" runat="server" Text="Cancel"   CssClass="btn btn-primary" Width="150" OnClick="btnCancel_Click"/>
                &nbsp;&nbsp;
                <asp:Button id="btnSave" runat="server" CssClass="btn btn-primary" Text="Save"   Width="150" OnClick="btnSave_Click"/>

                                   
                                    
                                  
                                     
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
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->
                        </div><!--/.col (right) -->
                         
                    </div>   
                        <div class="col-xs-12">

            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Practice Area Categories List</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->

                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" PageSize="20" class="table table-bordered table-hover"
                     onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound" onrowdeleting="gv_RowDeleting" 
                        onrowediting="gv_RowEditing">

                    <Columns>
                        <asp:TemplateField HeaderText="Practice Area Category Name">
                            <ItemTemplate>
                                <asp:Label ID="lblPracticeAreaCatName" runat="server" Text='<%# Eval("PracticeAreaCatName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No. Of Sub Cat Practice Area ">
                            <ItemTemplate>
                                <asp:Label ID="lblSubCatNo" runat="server" Text='<%# Eval("NoOfSubCat") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("strActive") %>'></asp:Label>
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
            </div>
            <!-- /.box -->
        </div>
       
                </section>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

