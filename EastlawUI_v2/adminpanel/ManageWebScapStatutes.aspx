<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageWebScapStatutes.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ManageWebScapStatutes"
    MasterPageFile="~/adminpanel/Site1.Master" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <head>
        <style>
                .alphabar {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 13px;
	
	
	background-repeat: repeat-x;
	float: left;
	width: 23px;
	margin-right: 1px;
	margin-left: 2px;
	padding-top: 3px;
	padding-right: 3px;
	padding-bottom: 3px;
	padding-left: 3px;
	border: thin solid #CCC;
}
         .alphabarLeg {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 13px;
	
	
	background-repeat: repeat-x;
	float: left;
	
	margin-right: 1px;
	margin-left: 2px;
	padding-top: 3px;
	padding-right: 3px;
	padding-bottom: 3px;
	padding-left: 3px;
	border: thin solid #CCC;
}
.alpha-outer {
	float: left;
	width: 100%;
	padding-top: 10px;
	padding-bottom: 5px;
}

        </style>
    </head>
  
             
      <section class="content-header">
                    <h1>
                         Web Statutes
                         <small>Statutes List</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Statutes</li>
                    </ol>
                </section>
      
    <section class="content">
          <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
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
                        <td style="width:25%"><asp:TextBox ID="txtID" runat="server"  class="form-control"> </asp:TextBox></td>
                        <td style="width:24%">&nbsp; </td>
                        <td style="width:25%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:24%">Category: </td>
                        <td style="width:25%"><asp:DropDownList ID="ddlCat" runat="server" class="form-control"></asp:DropDownList></td>
                        <td style="width:24%">Title: </td>
                        <td style="width:25%"><asp:TextBox ID="txtTitle" runat="server"  class="form-control"> </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:24%">Group: </td>
                        <td style="width:25%"><asp:DropDownList ID="ddlGroup" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList></td>
                        <td style="width:24%">Sub Group: </td>
                        <td style="width:25%"><asp:DropDownList ID="ddlSubGroup" runat="server" class="form-control"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td> </td>
                        <td> </td>
                        <td> </td>
                        <td style="text-align:right">
                             <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPnl">
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
            </ContentTemplate>
              </asp:UpdatePanel>
        <div class="alpha-outer">
         <%
             for (int i = 0; i < 26; i++)
             {
                 Response.Write("<a href='ManageWebScapStatutes.aspx?alp=" + Convert.ToChar(i + 65) + "&sc=ad'> <div class='alphabarLeg'>" + Convert.ToChar(i + 65) + "</div></a>");


             }
                
                 %>
            </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
                    <div class="row">
                        <!-- left column -->
                        <!--/.col (left) -->
                        <!-- right column -->
                        <%--<div class="col-md-6">--%> <asp:UpdateProgress ID="upProcessReg" runat="server" AssociatedUpdatePanelID="updPnl">
                                            <ProgressTemplate>
                                                <img src="../images/ajax-loader.gif"  />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                        <span style="float:right;padding-right:15px">
                            <asp:Button ID="btnStatutesTaggingWithCases" runat="server" CssClass="btn bg-maroon" Text="Statutes Tagging With Cases" Width="250px"   CausesValidation="false" OnClick="btnStatutesTaggingWithCases_Click" />
                            <br />
                            <br />
                        </span>
                        <div class="col-xs-12">
                            <!-- general form elements disabled -->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Statutes List (<asp:Label ID="lblCount" runat="server"></asp:Label>) </h3>
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
                                  <asp:Label ID="lblLnk" runat="server" Visible="false" Font-Size="Larger"></asp:Label>

                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"  
                    Width="100%" AllowPaging="True" PageSize="20" class="table table-bordered table-hover"
                     onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound" onrowdeleting="gv_RowDeleting" 
                        onrowediting="gv_RowEditing" OnRowCommand="gv_RowCommand" OnRowCancelingEdit="gv_RowCancelingEdit"
                                    OnRowUpdating="gv_RowUpdating" >

                    <Columns>
                          <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Left"> 
             
               
                <ItemTemplate> 
                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
                       <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                     <asp:Label ID="lblEditType" runat="server" Text='<%# Bind("Pri_Sec") %>'></asp:Label> 
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:Label ID="lblNewType" runat="server" ></asp:Label> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblType" runat="server" Text='<%# Bind("Pri_Sec") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                     <asp:DropDownList ID="ddlEditCat" runat="server" DataTextField="CatName" DataValueField="ID"> </asp:DropDownList> 
                     
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:DropDownList ID="ddlNewCat" runat="server" DataTextField="CatName" DataValueField="ID"> </asp:DropDownList> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblCat" runat="server" Text='<%# Bind("CatName") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Group" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                     <asp:Label ID="lblEditGroup" runat="server" Text='<%# Bind("Statutes_Category_Group") %>'></asp:Label> 
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:Label ID="lblNewGroup" runat="server" ></asp:Label> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblGroup" runat="server" Text='<%# Bind("Statutes_Category_Group") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Sub Group" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                     <asp:Label ID="lblEditSubGroup" runat="server" Text='<%# Bind("Statutes_Category_SubGroup") %>'></asp:Label> 
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:Label ID="lblNewSubGroup" runat="server" ></asp:Label> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblSubGroup" runat="server" Text='<%# Bind("Statutes_Category_SubGroup") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEditTitle" runat="server" Text='<%# Bind("Title") %>'></asp:TextBox> 
                     
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:TextBox ID="txtNewTitle" runat="server" ></asp:TextBox> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEditDate" runat="server" Text='<%# Bind("Date") %>'></asp:TextBox> 
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:TextBox ID="txtNewDate" runat="server" ></asp:TextBox> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField>
                         <asp:TemplateField HeaderText="Act" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEditAct" runat="server" Text='<%# Bind("Act") %>'></asp:TextBox> 
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:TextBox ID="txtNewAct" runat="server" ></asp:TextBox> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblAct" runat="server" Text='<%# Bind("Act") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title Variation 01" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEditTitleVariation1" runat="server" Text='<%# Bind("TitleVariation1") %>' TextMode="MultiLine"></asp:TextBox> 
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:TextBox ID="txtNewTitleVariation1" runat="server" TextMode="MultiLine"></asp:TextBox> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblTitleVariation1" runat="server" Text='<%# Bind("TitleVariation1") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField>
                          <asp:TemplateField HeaderText="Title Variation 02" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEditTitleVariation2" runat="server" Text='<%# Bind("TitleVariation2") %>' TextMode="MultiLine"></asp:TextBox> 
                    <asp:RegularExpressionValidator ID="rfvtxtEditTitleVari1" runat="server" Text="*" ForeColor="Red"
                        ControlToValidate="txtEditTitleVariation2"></asp:RegularExpressionValidator>
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:TextBox ID="txtNewTitleVariation2" runat="server" TextMode="MultiLine"></asp:TextBox> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblTitleVariation2" runat="server" Text='<%# Bind("TitleVariation2") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField>
                         <asp:TemplateField HeaderText="Title Variation 03" HeaderStyle-HorizontalAlign="Left"> 
                <EditItemTemplate> 
                    <asp:TextBox ID="txtEditTitleVariation3" runat="server" Text='<%# Bind("TitleVariation3") %>' TextMode="MultiLine"></asp:TextBox> 
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:TextBox ID="txtNewTitleVariation3" runat="server" TextMode="MultiLine"></asp:TextBox> 
                </FooterTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblTitleVariation3" runat="server" Text='<%# Bind("TitleVariation3") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField>
             
            <asp:TemplateField HeaderText="Active"> 
                <EditItemTemplate> 
                    <asp:CheckBox ID="chkActive" runat="server" />
                </EditItemTemplate> 
                <ItemTemplate> 
                    <asp:Label ID="lblActive" runat="server" Text='<%# Eval("strActive") %>'></asp:Label> 
                    <asp:HiddenField ID="hdPublicDisplay" runat="server" Value='<%# Eval("PublicDisplay") %>' />
                </ItemTemplate> 
                <FooterTemplate> 
                    <asp:CheckBox ID="chkNewActive" runat="server" />
                </FooterTemplate> 
            </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Public">
                            <ItemTemplate>
                                <asp:Label ID="lblPublic" runat="server" Text='<%# Eval("PublicDisplay") %>'></asp:Label> 
                                 </ItemTemplate>
                        </asp:TemplateField>
             <asp:TemplateField HeaderText="Make Public">
                            <ItemTemplate>
                                <asp:Button ID="btnMakePublicLink" runat="server" Text="Enable Public" CssClass="btn btn-primary" Width="100" CommandName="MakePublic" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                <asp:Button ID="btnDisablePublicLink" runat="server" Text="Disable Public" CssClass="btn box-warning" Width="100" CommandName="DisableMakePublic" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Left" Visible="false"> 
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

                          <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                
                                <%--<asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditStatutes" Text="Edit Record" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>--%>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Text="Edit" ></asp:LinkButton>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
<asp:TemplateField HeaderText="View Index">
                            <ItemTemplate>
                                
                                <asp:LinkButton ID="lnkViewIndex" runat="server" CommandName="ViewIndex" Text="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Manage Index">
                            <ItemTemplate>
                                
                                <asp:LinkButton ID="lnkManage" runat="server" CommandName="ManageIndex" Text="Manage" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
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



