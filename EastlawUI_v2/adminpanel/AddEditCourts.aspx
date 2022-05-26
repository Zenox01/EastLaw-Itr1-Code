<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditCourts.aspx.cs" Inherits="EastlawUI_v2.adminpanel.AddEditCourts" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Courts
                        <small>Court Master Details</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Court Master Details </li>
                    </ol>
                </section>
      
            <section class="content">
                <div class="row">
                    <!-- left column -->
                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-xs-8">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Court Details</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="form-group">
                                    <label>
                                        Court Name : *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="txtCourtName" ErrorMessage="Required" ForeColor="Red" ></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtCourtName" runat="server" class="form-control"> </asp:TextBox>
                                    <asp:Label ID="lblID" runat="server" Visible="false" Text="0"></asp:Label>
                                    
                                </div>
                                <div class="form-group">
                                    <label>
                                        Court hierarchy :

                                    </label>
                                    <asp:CheckBoxList ID="chkhierarchy" runat="server" RepeatColumns="4">
                                       <%-- <asp:ListItem Value="Supreme Courts" Text="Supreme Courts"></asp:ListItem>
                                        <asp:ListItem Value="High Courts" Text="High Courts"></asp:ListItem>
                                        <asp:ListItem Value="Tribunals" Text="Tribunals"></asp:ListItem>
                                        <asp:ListItem Value="Others" Text="Others"></asp:ListItem>--%>
                                    </asp:CheckBoxList>
                                    
                                </div>
                                <div class="form-group">
                                    <label>
                                        Jurisdiction Country: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="ddlCountry" ErrorMessage="Required" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:DropDownList ID="ddlCountry" runat="server" class="form-control">
                                       <%-- <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="News" Text="News"></asp:ListItem>
                                        <asp:ListItem Value="Update" Text="Update"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                
                                 <div class="form-group">
                                    <label>
                                        Jurisdiction Area Name:

                                    </label>
                                    <asp:TextBox ID="txtJuriAreaName" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Jurisdiction District Name:

                                    </label>
                                    <asp:TextBox ID="txtJuriDistriName" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                
                               <div class="form-group">
                                    <label>
                                       Short Content :
                                    </label>
                                    <asp:TextBox ID="txtShortContent" runat="server" class="form-control" TextMode="MultiLine"> </asp:TextBox>
                                    
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Image Upload:
                                         
                                    </label>
                                    
                                   <asp:FileUpload ID="fuploadimage" runat="server" class="form-control" />
                                    <asp:Label ID="lblfuploadWord" runat="server" Visible="false"></asp:Label>
                                    <asp:Image ID="imgFl" runat="server" Height="200" Width="200"/>
                                    
                                </div>
                                
                                
                               
                              
                                 <div class="form-group">
                                    <label>
                                        Sort Order:

                                    </label>
                                    <asp:TextBox ID="txtSort" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                 
                                <div class="form-group">
                                    <label>
                                        Court Appeal Type(s) :

                                    </label>
                                    <asp:CheckBoxList ID="chkCrtAppealType" runat="server" RepeatColumns="4">
                                        
                                    </asp:CheckBoxList>
                                    
                                </div>

                                <div class="form-group">
                                    <label>
                                        Active:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
                                </div>




                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" Width="150" OnClick="btnCancel_Click" />
                                &nbsp;&nbsp;
               
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" Width="150" OnClick="btnSave_Click" />





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
                    </div>
                    
                    <!--/.col (right) -->

                </div>
                

            </section>
           
            
        </ContentTemplate>
          <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
    </asp:UpdatePanel>
</asp:Content>