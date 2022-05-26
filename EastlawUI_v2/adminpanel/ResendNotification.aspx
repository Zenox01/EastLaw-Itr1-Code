<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResendNotification.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ResendNotification" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $("[id*=chkHeader]").live("click", function () {
        var chkHeader = $(this);
        var grid = $(this).closest("table");
        $("input[type=checkbox]", grid).each(function () {
            if (chkHeader.is(":checked")) {
                $(this).attr("checked", "checked");
                $("td", $(this).closest("tr")).addClass("selected");
            } else {
                $(this).removeAttr("checked");
                $("td", $(this).closest("tr")).removeClass("selected");
            }
        });
    });
    $("[id*=chkRow]").live("click", function () {
        var grid = $(this).closest("table");
        var chkHeader = $("[id*=chkHeader]", grid);
        if (!$(this).is(":checked")) {
            $("td", $(this).closest("tr")).removeClass("selected");
            chkHeader.removeAttr("checked");
        } else {
            $("td", $(this).closest("tr")).addClass("selected");
            if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                chkHeader.attr("checked", "checked");
            }
        }
    });
</script>

<%--    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>
             
      <section class="content-header">
                    <h1>
                        Users
                        <small>Re-Send Notification</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Notification</li>
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
                                <h3 class="box-title">Users</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                 <div class="form-group">
                                    <label>
                                        Status: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="ddlStatus" ErrorMessage="Required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </label>

                                    <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                           <asp:ListItem Text="Select" Value="0"  Selected="True"></asp:ListItem>
                                        <%--  <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>--%>
                            <asp:ListItem Text="Expired" Value="Expired"></asp:ListItem>
                                        <asp:ListItem Text="Pending - Activation" Value="Pending - Activation"></asp:ListItem>
                                        <%--<asp:ListItem Text="General Block" Value="General Block"></asp:ListItem>
                                        <asp:ListItem Text="Breach of privacy Block" Value="Breach of privacy Block"></asp:ListItem>
                                        <asp:ListItem Text="Non-Payment Block" Value="Non-Payment Block"></asp:ListItem>--%>
                                              </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        From Date: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtFromDate" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtFromDate" runat="server" class="form-control"> </asp:TextBox>
                                     <asp1:CalendarExtender ID="clnStart" runat="server" TargetControlID="txtFromDate"
                                Format="MM/dd/yyyy">
                            </asp1:CalendarExtender>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        To Date: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="txtToDate" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtToDate" runat="server" class="form-control"> </asp:TextBox>
                                      <asp1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                 Format="MM/dd/yyyy">
                            </asp1:CalendarExtender>
                                </div>

               
            <asp:Button ID="btnView" runat="server" CssClass="btn btn-primary" Text="View Records" Width="150" OnClick="btnView_Click"  />





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
                        <!--/.col (right) -->
                         
                    </div>   
                      <div class="col-md-12">
                    <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Users List (<asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>)</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                  
               <asp:Button ID="btnSendAll" runat="server" CssClass="btn btn-primary" Text="Send Notifications To All" Width="200" OnClick="btnSendAll_Click"  />  &nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Button ID="btnSend" runat="server" CssClass="btn btn-primary" Text="Send Notifications" Width="150" OnClick="btnSend_Click"  />
                                    





                                </div><!-- /.box-body -->
                                 <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
                                <div class="alert alert-danger alert-dismissable" id="div1" runat="server" style="display: none">
                                    <button type="button" class="close" data-dismiss="alert">
                                        ×</button>
                                    <strong>Transaction failed ... </strong>
                                </div>
                                <div class="alert alert-info alert-dismissable" id="div2" runat="server" style="display: none">
                                    <button type="button" class="close" data-dismiss="alert">
                                        ×</button>
                                    <strong>Transaction success !</strong>
                                </div>
                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"  
                    Width="100%" AllowPaging="false" PageSize="20" class="table table-bordered table-hover"
                    
                       OnRowCommand="gv_RowCommand1" AllowSorting="true" OnSorting="gv_Sorting">

                    <Columns>
                        <asp:TemplateField  SortExpression="CustNo">
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSortCustNo" runat="server" Text="User #" CommandName="Sort" CommandArgument="CustNo" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustNo" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField SortExpression="FullName">
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSort" runat="server" Text="Full Name" CommandName="Sort" CommandArgument="FullName" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField  SortExpression="EmailID">
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSortEmail" runat="server" Text="Email ID" CommandName="Sort" CommandArgument="EmailID" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("EmailID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="PlanName" >
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSortPlan" runat="server" Text="Plan Name" CommandName="Sort" CommandArgument="PlanName" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPlanName" runat="server" Text='<%# Eval("PlanName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                      
                        <asp:TemplateField SortExpression="PhoneNo">
                              <HeaderTemplate>
        <asp:LinkButton ID="lnkSortPhoneNo" runat="server" Text="Phone No" CommandName="Sort" CommandArgument="PhoneNo" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPhoneNo" runat="server" Text='<%# Eval("PhoneNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField SortExpression="Status">
                            <HeaderTemplate>
        <asp:LinkButton ID="lnkSortStatus" runat="server" Text="Status" CommandName="Sort" CommandArgument="Status" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField  SortExpression="FormatedExpire" >
                              <HeaderTemplate>
        <asp:LinkButton ID="lnkSortPlanExpire" runat="server" Text="Plan Expire" CommandName="Sort" CommandArgument="FormatedExpire" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPlanExpire" runat="server" Text='<%# Eval("FormatedExpire") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField SortExpression="ExpireIn" >
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSortNoOfDays" runat="server" Text="No. Of Days" CommandName="Sort" CommandArgument="ExpireIn" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Eval("ExpireIn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="strActive">
                            <HeaderTemplate>
        <asp:LinkButton ID="lnkSortActive" runat="server" Text="Active" CommandName="Sort" CommandArgument="strActive" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblActive" runat="server" Text='<%# Eval("strActive") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField SortExpression="CreatedOn">
                            <HeaderTemplate>
        <asp:LinkButton ID="lnkSortCreatedOn" runat="server" Text="Reg Date" CommandName="Sort" CommandArgument="CreatedOn" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Eval("CreatedOn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FirstLogin">
                            <HeaderTemplate>
        <asp:LinkButton ID="lnkSortFirstLogin" runat="server" Text="First Login" CommandName="Sort" CommandArgument="FirstLogin" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFirstLogin" runat="server" Text='<%# Eval("FirstLogin") %>'></asp:Label>
                                  <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField>
        <%--    <HeaderTemplate>
                <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="True" OnCheckedChanged="chkHeader_CheckedChanged" />
            </HeaderTemplate>--%>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
                        

                    </Columns>
                </asp:GridView> 
             <%--</ContentTemplate>
    </asp:UpdatePanel>--%>



                            </div>
                          </div>
                        <!-- /.box -->
                   
                    <!--/.col (right) -->

                
                

            </section>
            
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
