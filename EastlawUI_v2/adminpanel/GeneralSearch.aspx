<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralSearch.aspx.cs" Inherits="EastlawUI_v2.adminpanel.GeneralSearch" 
    MasterPageFile="~/adminpanel/Site1.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <style>
        .highlight {text-decoration: none;color:black;background:yellow;}
        .highlightBlue {text-decoration: none;color:black;background:rgba(32, 185, 58, 0.31);}
    </style>
     <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Cases
                        <small>Search</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Cases</li>
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
                                    <h3 class="box-title">Search Parameter</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                   
                                        <div class="form-group">
                                            <label>Free Text*
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtFreeText" ErrorMessage="Required" ForeColor="Red" 
                                        ></asp:RequiredFieldValidator>
                                                <asp:Label ID="lblID" runat="server" Visible="false" Text="0"></asp:Label>
                                     </label>
                                            <asp:TextBox ID="txtFreeText" runat="server" class="form-control"> </asp:TextBox>
                                           </div>

                <asp:Button id="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search"   Width="150" OnClick="btnSearch_Click" />

                                   
                                    
                                  
                                     
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
                    <h3 class="box-title">Search Result <h4>Total Records: <asp:Label ID="lblCount" runat="server"></asp:Label></h4></h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->

                <div id="divNoResult" runat="server" style="display:none" >
                    <div style="width:80%;float:left">
                        <h3 style="color:#D11515">No documents satisfy your query.</h3>
                    <p>You may want to <span style="color:#D11515">Edit</span> your Search By 
                    </p>
                    <p>inserting <span style="color:red"><b>“ “</b></span> to search the exact phrase within a document; <span style="color:red"><b>OR</b></span> <br />
                        inserting  <span style="color:red"><b>AND</b></span> between your terms to find them anywhere in the same document; <span style="color:red"><b>OR</b></span><br />
                        by inserting  <span style="color:red"><b>OR</b></span> between your terms to find combination of two keywords existing together or separately.
                    </p>
                   
                    </div>
                    </div>
                 <div id="divResult" runat="server">

                
                <asp:GridView ID="gvLst" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="Id"
                     GridLines="None" AllowPaging="true" AllowCustomPaging="true" PageSize="20" OnPageIndexChanging="gvLst_PageIndexChanging" OnRowCommand="gvLst_RowCommand">
                    <%--<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />--%>
                    <pagersettings mode="NumericFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"  
            position="TopAndBottom" />
                    <pagerstyle cssclass="gridview" >

</pagerstyle>
                    <Columns>
                        <asp:TemplateField ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>

                           
                        <div class="rows_ct" style="padding-left:10px;padding-right:15px;padding-bottom:25px;">
                	<%--<div class="lft">Date:<strong><%# Eval("JDate") %></strong> </div>--%>
                    <div class="rgt">
                        
                        <strong><a href='<%# Eval("Link") %>'><span style="text-transform:capitalize"> <%# Eval("Title") %></span></a></strong><br />
                        <%# Eval("OtherContent") %>
                    
                             
                    </div>
                           <span style="float:right"> 
                               <strong><a href='<%# Eval("Link") %>'>View Full ...</a></strong><br /></span>
                            
                </div>
                                <hr />
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:HiddenField ID="hdResultType" runat="server" Value='<%# Eval("ResultType") %>' />
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Final Review">
                            <ItemTemplate>
                                <ItemTemplate>
                                 <asp:Button ID="btnUpdateStatus" runat="server" Text="Final Review" CssClass="btn btn-primary" Width="100" CommandName="FinalReview" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
                </asp:GridView>
                
                </div>
            </div>
            <!-- /.box -->
        </div>
       
                </section>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


