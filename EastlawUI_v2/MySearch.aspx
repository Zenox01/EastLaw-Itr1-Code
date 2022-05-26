<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MySearch.aspx.cs" Inherits="EastlawUI_v2.MySearch" 
    MasterPageFile="~/MemberMaster.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
        <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    <li><a href="#" class="current">My Searches</a></li>
   
</ul>
  </div>
</div>
     <div class="container">
    <div class="row">
    
    
    
   		<div class="col-lg-8 col-md-8">
        
        	<div class="row">

                <asp:GridView ID="gvLst" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" AllowPaging="true" 
                    CssClass="table table-filter" PageSize="20" OnPageIndexChanging="gvLst_PageIndexChanging" >
                    <pagersettings mode="NumericFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"  
            position="TopAndBottom" />
                    <pagerstyle cssclass="gridview" >

</pagerstyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Text">
                            <ItemTemplate>
                                <a href='<%# "/Search/"+Eval("SearchText") %>'>'<%# Eval("SearchText") %>'</a>
                                <%--<asp:Label ID="lblTxt" runat="server" Text='<%# Eval("SearchText") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                         <asp:TemplateField HeaderText="Found Results">
                            <ItemTemplate>
                                <asp:Label ID="lblFoundResults" runat="server" Text='<%# Eval("strFoundResult") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField HeaderText="Date & Time" DataField="CreatedOn" />
                    </Columns>
                </asp:GridView>


            	
            
            </div>
        
        </div>
        
        
        
        
    	
    </div>  
    </div>
    <div class="clearfix"></div>
</asp:Content>


