<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MySubscription.aspx.cs" Inherits="EastlawUI_v2.MySubscription" 
    MasterPageFile="~/Withoutlogin.Master"%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlc">
       <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    <li><a href="#" class="current">My Subscription</a></li>
   
</ul>
  </div>
</div>
      <div class="container">
    <div class="row">
     <%--<img src="/images/final.jpg" width="100%" />  --%>
    
    
   		<div class="col-lg-12 col-md-12">
        
        	<div class="row">
                 <h5><asp:Label id="lblUserState" runat="server" ForeColor="Blue"></asp:Label></h5>

<div class="srchLft" id="divPlans" runat="server" >
                    <div id="divPendingOrders" runat="server">
                        <h4>Pending Orders</h4>
                        <asp:GridView ID="gvPendingOrders" runat="server" Width="100%" AutoGenerateColumns="false" OnRowEditing="gvPendingOrders_RowEditing"  CssClass="grid" CellPadding="20" >
                            <HeaderStyle BackColor="#D11515" ForeColor="White"  VerticalAlign="Middle" />
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Order #" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Eval("OrdNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plan" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlanName" runat="server" Text='<%# Eval("PlanName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="No. Of Days" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        
                                        <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Eval("NoofDays") %>'></asp:Label>Days
                                            
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Price" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# "Rs. "+ Eval("Price") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Status"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="" Visible="false"  >
                                    <ItemTemplate>
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSelect" runat="server" Text="Pay With Easypaisa" Width="250" class="btn btn-danger btn_style" CommandName="Edit"  />
                                         <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                        </ItemTemplate>
                                     </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                    <br />
                    </div>
                    
                    <h4>Individual</h4>
                   <asp:GridView ID="gvPlans" runat="server" Width="100%" AutoGenerateColumns="false" OnRowEditing="gvPlans_RowEditing"  CssClass="grid"  CellPadding="20" >
                            <HeaderStyle BackColor="#D11515" ForeColor="White" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Plan"   ItemStyle-Width="500">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlanName" runat="server" Text='<%# Eval("PlanName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                         <HeaderTemplate>
         <div style="text-align:center;">Validity (No. of days)</div>
     </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Eval("NoofDays") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                      <HeaderTemplate>
         <div style="text-align:center;">Price</div>
     </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-decoration:line-through">
                                            <%--<%# "Rs. "+( (Convert.ToDouble(Eval("Price"))/100 * 20) + (Convert.ToDouble(Eval("Price"))))  %>--%>
                                            <%--<%# "Rs. "+ Eval("OldPrice","{0:#,##}") %>--%>
                                        </div>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# "Rs. "+ Eval("Price","{0:#,##}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText=""  >
                                    <ItemTemplate>
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSelect" runat="server" Text="Choose Plan" class="btn btn-danger btn_style" CommandName="Edit"  />
                                         <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                        </ItemTemplate>
                                     </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    <br />
                    <h4>Corporate</h4>
                    <asp:GridView ID="gvCorporatePlans" runat="server" Width="100%" AutoGenerateColumns="false" OnRowEditing="gvCorporatePlans_RowEditing"  CssClass="grid"  CellPadding="20" >
                            <HeaderStyle BackColor="#D11515" ForeColor="White"  VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Plan" ItemStyle-Width="500">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlanName" runat="server" Text='<%# Eval("PlanName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                         <HeaderTemplate>
         <div style="text-align:center;">Validity (No. of days)</div>
     </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Eval("NoofDays") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                      <HeaderTemplate>
         <div style="text-align:center;">Price</div>
     </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# "Rs. "+ Eval("Price","{0:#,##}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText=""  >
                                    <ItemTemplate>
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSelect" runat="server" Text="Choose Plan" class="btn btn-danger btn_style" CommandName="Edit"  />
                                         <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                        </ItemTemplate>
                                     </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    
    
                	<div class="row1">
                        
                       
                        
                    </div>
                    
                  
               
                    
                </div>

            	
            
            </div>
        
        </div>
        
        
        
        
    	
    </div>  
    </div>
    <div class="clearfix"></div>
    </asp:Content>