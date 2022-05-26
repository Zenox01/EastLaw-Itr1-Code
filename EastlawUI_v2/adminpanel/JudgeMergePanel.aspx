<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JudgeMergePanel.aspx.cs" Inherits="EastlawUI_v2.adminpanel.JudgeMergePanel" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Masters
                        <small>Judges Details</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Judges</li>
                    </ol>
                </section>
      
    <section class="content">
                    <div class="row">
                        <!-- left column -->
                        <!--/.col (left) -->
                        <!-- right column -->
                        <%--<div class="col-md-6">--%>
                            <div class="col-xs-8">
                            <!-- general form elements disabled -->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Judges Details</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                  <h3>New Judges Information</h3>
                               
                                    <div class="form-group">
                                            <label>Court:
                                               
                                               
                                     </label>
                                             <asp:DropDownList ID="ddlCourt" runat="server" class="chosen-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCourt_SelectedIndexChanged"></asp:DropDownList>
                                           </div>
                                    <div class="form-group">
                                            <label>New Judge:
                                               
                                               
                                     </label>
                                             <asp:DropDownList ID="ddlNewJudge" runat="server" class="chosen-select"></asp:DropDownList>
                                           </div>
                                    <div class="form-group">
                                            <label>Old Tagged Judges Court:
                                               
                                               
                                     </label>
                                             <asp:DropDownList ID="ddlOldTaggesJudges" runat="server" class="chosen-select" AutoPostBack="True" OnSelectedIndexChanged="ddlOldTaggesJudges_SelectedIndexChanged"></asp:DropDownList>
                                        
                                           </div>
                                   
                                    

                                            
                                            

                                           

                                         
                                     <asp:Button ID="btnCancel" runat="server" Text="Cancel"   CssClass="btn btn-primary" Width="150" OnClick="btnCancel_Click"/>
                &nbsp;&nbsp;
                <asp:Button id="btnSave" runat="server" CssClass="btn btn-primary" Text="Save"   Width="150" OnClick="btnSave_Click"/>

                                   
                                    
                                   <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updPnl">
                    <ProgressTemplate>
                     
                     
           <img alt="" src="media/img/loader.gif" />
      
                                
                           
                      
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
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->
                        </div><!--/.col (right) -->
                         
                    </div>   
                        <div class="col-xs-12">

            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Tagged Citations List</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->

                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="false" PageSize="20" AllowCustomPaging="false" class="table table-bordered table-hover"
                     
                        >
                      <pagersettings mode="NumericFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"  
            position="TopAndBottom" />
                    <Columns>
                        <asp:TemplateField HeaderText="Judge Name">
                            <ItemTemplate>
                                <asp:Label ID="lblJudgeName" runat="server" Text='<%# Eval("JudgeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Case ID">
                            <ItemTemplate>
                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("CaseID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Citation">
                            <ItemTemplate>
                                <asp:Label ID="lblCitation" runat="server" Text='<%# Eval("Citation") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Judgment Date">
                            <ItemTemplate>
                                <asp:Label ID="lblJDate" runat="server" Text='<%# Eval("JDate") %>'></asp:Label>
                                <asp:HiddenField ID="hdTaggedIDOld" runat="server" Value='<%# Eval("TaggedIDOld") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Court">
                            <ItemTemplate>
                                <asp:Label ID="lblCourt" runat="server" Text='<%# Eval("Court") %>'></asp:Label>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSel" runat="server" Checked="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         
                     <%--   <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                    ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" />
                                <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete"
                                    ImageUrl="~/adminpanel/media/img/Delete.png" Height="16" Width="16" CausesValidation="false" OnClientClick="return confirm('Are you sure?');" />
                               
                            </ItemTemplate>
                        </asp:TemplateField>--%>



                    </Columns>
                </asp:GridView>
                

            </div>
            <!-- /.box -->
        </div>
       
                </section>
                
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
