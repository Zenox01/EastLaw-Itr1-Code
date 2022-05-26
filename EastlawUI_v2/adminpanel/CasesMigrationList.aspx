<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CasesMigrationList.aspx.cs" Inherits="EastlawUI_v2.adminpanel.CasesMigrationList"
    MasterPageFile="~/adminpanel/Site1.Master" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                         Migration Utility
                         <small>Cases Migration List</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Cases Migration</li>
                    </ol>
                </section>
      
    <section class="content">
        <div class="col-xs-12">

            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Search</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->
                <table style="width:100%">
                     <tr>
                        <td style="width:24%">ID: </td>
                        <td style="width:25%"><asp:TextBox ID="txtID" runat="server" Width="180"> </asp:TextBox></td>
                        <td style="width:24%">&nbsp;</td>
                        <td style="width:25%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:24%">Year: </td>
                        <td style="width:25%"><asp:DropDownList ID="ddlYear" runat="server" class="form-control"></asp:DropDownList></td>
                        <td style="width:24%">Journal: </td>
                        <td style="width:25%"><asp:DropDownList ID="ddlJournal" runat="server" class="form-control"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Citations Number: </td>
                        <td> <asp:TextBox ID="txtCNumber" runat="server" Width="80"> </asp:TextBox></td>
                         <td>Court Name: </td>
                        <td> <asp:TextBox ID="txtCourtName" runat="server" Width="180"> </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Court Name: </td>
                        <td> <telerik:RadComboBox RenderMode="Lightweight" ID="ddlRadCourts" runat="server" Height="200" Width="315" 
                        DropDownWidth="315" EmptyMessage="" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" > </telerik:RadComboBox></td>
                         <td>Appeallant  </td>
                        <td><asp:TextBox ID="txtAppeallant" runat="server" Width="180"> </asp:TextBox> </td>
                    </tr>
                     <tr>
                        <td>Judgment Date (DD/MM/YYYY): </td>
                        <td> <asp:TextBox ID="txtJDate" runat="server" Width="80"> </asp:TextBox></td>
                         <td>Respondent  </td>
                        <td><asp:TextBox ID="txtRespondent" runat="server" Width="180"> </asp:TextBox> </td>
                    </tr>
                    <tr>
                        
                        <td> </td>
                        <td style="text-align:right">
                             <asp:UpdateProgress ID="upProcessReg" runat="server" AssociatedUpdatePanelID="updPnl">
                                            <ProgressTemplate>
                                                <img src="../images/ajax-loader.gif"  />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                            <asp:Button ID="btnAll" runat="server" Text="Show All" class="btn btn-block btn-primary"  Width="100" OnClick="btnAll_Click1"  />
                            &nbsp;&nbsp; &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-block btn-primary" OnClick="btnSearch_Click" Width="100" />
                            </td>
                    </tr>
                </table>
                

            </div>
            <!-- /.box -->
        </div>
                    <div class="row">
                        <!-- left column -->
                        <!--/.col (left) -->
                        <!-- right column -->
                        <%--<div class="col-md-6">--%>
                        <div class="col-xs-12">
                            <!-- general form elements disabled -->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Cases Migration List</h3>
                                    
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                  
                                      <asp:Label ID="lblLnk" runat="server" Visible="false" Font-Size="Larger"></asp:Label>



                                  
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
                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                    Width="100%" AllowPaging="True" PageSize="20" class="table table-bordered table-hover"
                     onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound" onrowdeleting="gv_RowDeleting" 
                        onrowediting="gv_RowEditing" OnRowCommand="gv_RowCommand">

                    <Columns>
                         <asp:TemplateField HeaderText="ID">
                         
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Journal">
                            <HeaderStyle Width="50" />
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:Label ID="lblJournal" runat="server" Text='<%# Eval("JournalName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Year">
                            <HeaderStyle Width="50" />
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Citation">
                            <HeaderStyle Width="150" />
                            <ItemStyle Width="150" />
                            <ItemTemplate>
                                <asp:Label ID="lblCitation" runat="server" Text='<%# Eval("Citation") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Internal Citation Ref">
                            <ItemTemplate>
                                <asp:Label ID="lblCitationRef" runat="server" Text='<%# Eval("CitationRef") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Judge">
                            <ItemTemplate>
                                <asp:Label ID="lblJudge" runat="server" Text='<%# Eval("JudgeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Appeal">
                            <ItemTemplate>
                                <asp:Label ID="lblAppeal" runat="server" Text='<%# Eval("Appeal") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Appeallant">
                            <ItemTemplate>
                                <asp:Label ID="lblAppeallant" runat="server" Text='<%# Eval("Appeallant") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%# Eval("Respondent") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Judgment Date">
                            <ItemTemplate>
                                <asp:Label ID="lblJDate" runat="server" Text='<%# Eval("JDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Make Public">
                            <ItemTemplate>
                                <asp:Button ID="btnMakePublicLink" runat="server" Text="Enable Public" CssClass="btn btn-primary" Width="100" CommandName="MakePublic" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                <asp:Button ID="btnDisablePublicLink" runat="server" Text="Disable Public" CssClass="btn box-warning" Width="100" CommandName="DisableMakePublic" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <%--<asp:TemplateField HeaderText="Keywords">
                            <ItemTemplate>
                                <asp:Label ID="lblKeywords" runat="server" Text='<%# Eval("Keywords") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                         <asp:TemplateField HeaderText="Final Review">
                            <ItemTemplate>
                                <ItemTemplate>
                                 <asp:Button ID="btnUpdateStatus" runat="server" Text="Final Review" CssClass="btn btn-primary" Width="100" CommandName="FinalReview" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("strActive") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                       <%-- <asp:TemplateField>
                            <HeaderStyle Width="150" />
                            <ItemStyle Width="150" />
            <ItemTemplate>
                <a class="btn btn-block btn-primary" data-toggle="modal" data-target="#compose-modal<%# Eval("ID") %>"><i class="fa fa-pencil"></i>Quick View</a>
                <div class="modal fade" id="compose-modal<%# Eval("ID") %>" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog" style="width:800px">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><i class="fa fa-user"></i> Citation Details</h4>
                    </div>
                    
                        <div class="modal-body" >
                            
                             <span class="input-group-addon">Keywords</span><br />
                               <p class="help-block"><%# Eval("Keywords") %></p>
                            <span class="input-group-addon">Advocate Appeallant</span><br />
                               <p class="help-block"><%# Eval("AdvA") %></p>

                            <span class="input-group-addon">Advocate Respondent</span><br />
                               <p class="help-block"><%# Eval("AdvR") %></p>

                             <span class="input-group-addon">Appeallant</span><br />
                               <p class="help-block"><%# Eval("Appeallant") %></p>
                            <span class="input-group-addon">Appeallant Type</span><br />
                               <p class="help-block"><%# Eval("AppeallantType") %></p>
                             <span class="input-group-addon">Respondent</span><br />
                               <p class="help-block"><%# Eval("Respondent") %></p>
                               <span class="input-group-addon">Head Notes</span><br />
                               <p class="help-block"><%# Eval("HeadNotes") %></p>
                            <span class="input-group-addon">Judgment</span><br />
                               <p class="help-block"><%# Eval("Judgment") %></p>
                           <div class="form-group">
                                <div class="input-group">
                                 
                                </div>
                            </div>
                           
                           

                        </div>
                        <div class="modal-footer clearfix">

                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i> Discard</button>

                            
                        </div>
                   
                </div><!-- /.modal-content -->
            </div><!-- /.modal-dialog -->
        </div>
            </ItemTemplate>
        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>
                                
                                <asp:LinkButton ID="lnkViewIndex" runat="server" CommandName="View" Text="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                    ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" />
                                <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete" 
                                    ImageUrl="~/adminpanel/media/img/Delete.png" Height="16" Width="16" CausesValidation="false" Visible="false" />
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:HiddenField ID="hdPublicDisplay" runat="server" Value='<%# Eval("PublicDisplay") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>



                    </Columns>
                </asp:GridView>     </div><!-- /.box -->
                        </div><!--/.col (right) -->
                         
                    </div>   
                </section>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



