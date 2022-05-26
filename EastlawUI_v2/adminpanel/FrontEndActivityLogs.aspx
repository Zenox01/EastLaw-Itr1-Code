<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrontEndActivityLogs.aspx.cs" Inherits="EastlawUI_v2.adminpanel.FrontEndActivityLogs" 
    MasterPageFile="~/adminpanel/Site1.Master"%>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Logs
                        <small>Activity List</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Logs</li>
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
                                    <h3 class="box-title">Activity List</h3>
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
                                Log Type: <asp:DropDownList ID="ddlLogType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLogType_SelectedIndexChanged" Width="300">

                                          </asp:DropDownList>
                                <br />
                                <br />
                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" PageSize="20" class="table table-bordered table-hover"
                     onpageindexchanging="gv_PageIndexChanging"  >

                    <Columns>
                         <asp:TemplateField HeaderText="Access Source">
                            <ItemTemplate>
                                <asp:Label ID="lblAccessSource" runat="server" Text='<%# Eval("AccessSource") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label ID="lblActivityType" runat="server" Text='<%# Eval("ActivityType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Label ID="lblAction" runat="server" Text='<%# Eval("Action") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Text">
                            <ItemTemplate>
                                <asp:Label ID="lbltxt" runat="server" Text='<%# Eval("txt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="URL">
                            <ItemTemplate>
                                <a href='<%# Eval("URL") %>' target="_blank"><%# Eval("URL") %></a>
<%--<asp:Label ID="lblURL" runat="server" Text='<%# Eval("URL") %>'></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IP">
                            <ItemTemplate>
                                <asp:Label ID="lblIPAdd" runat="server" Text='<%# Eval("IPAdd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Member">
                            <ItemTemplate>
                                <asp:Label ID="lblMember" runat="server" Text='<%# Eval("Member") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Country">
                            <ItemTemplate>
                                <asp:Label ID="lblcountryName" runat="server" Text='<%# Eval("countryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Region">
                            <ItemTemplate>
                                <asp:Label ID="lblregionName" runat="server" Text='<%# Eval("regionName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City">
                            <ItemTemplate>
                                <asp:Label ID="lblcityName" runat="server" Text='<%# Eval("cityName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Client Browser">
                            <ItemTemplate>
                                <asp:Label ID="lblClientBrowser" runat="server" Text='<%# Eval("ClientBrowser") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Platform">
                            <ItemTemplate>
                                <asp:Label ID="lblPlatform" runat="server" Text='<%# Eval("Platform") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        

                        <asp:BoundField HeaderText="Date & Time" DataField="CreatedOn" />
                    </Columns>
                </asp:GridView>     </div><!-- /.box -->
                        </div><!--/.col (right) -->
                         
                    </div>   
                </section>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



