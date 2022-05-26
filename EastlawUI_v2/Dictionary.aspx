<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dictionary.aspx.cs" Inherits="EastlawUI_v2.Dictionary" 
    MasterPageFile="~/MemberMaster.Master"%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
    <link href="/style/css/department.css" rel="stylesheet" type="text/css" />
    <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    
    <li><a href="#" class="current">Dictionary</a></li>
</ul>
  </div>
</div>
        <div class="container">
    <div class="row">
    
    <div class="heading_style" style="padding:0;margin-bottom:20px;">
            
            	<ul>
                      <%
                          Response.Write("<li><a href='?alp=all'>ALL</a></li>");
                for (int i = 0; i < 26; i++)
                {
                    Response.Write("<li><a href='?alp=" + Convert.ToChar(i + 65) + "'>" + Convert.ToChar(i + 65) + "</a></li>");
                    
                    
                }
                
                 %>
                	
                </ul>
            
            </div>
    
   		<div class="col-lg-8 col-md-8">
        
        	<div class="row">
                <div class="box">
            <div>
            <asp:TextBox ID="txtSearch" runat="server"  CssClass="form-control text_field" Width="300px" AutoPostBack="false" OnTextChanged="txtSearch_TextChanged" placeholder="Search" ></asp:TextBox>
                        <div class="input-group-btn">
                            
                            <asp:Button ID="btnSearch" runat="server" Text="Search"   CssClass="btn btn-default btn_height2" OnClick="btnSearch_Click"  />
                        </div>
                    </div>
                    </div>
        
                <h3 style="color:#AC0101"><asp:Label ID="lblWord" runat="server"></asp:Label></h3>
                <div id="divResult" runat="server"></div>
                <asp:Label ID="lblResult" runat="server"></asp:Label>
                <asp:GridView ID="gvLst" runat="server" AutoGenerateColumns="false" CssClass="table table-filter" Width="100%" GridLines="None" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvLst_PageIndexChanging">
                    <pagersettings mode="NumericFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"  
            position="TopAndBottom" />
                    <pagerstyle cssclass="gridview" >

</pagerstyle>
                    <Columns>
                     
                        <asp:TemplateField>
                            <ItemTemplate>
                
                <div class="row border-bottom">
                	
                    <h3 class="margin-top-0"><a href="#"><%# Eval("Word") %></a></h3>
                    <p><%# Eval("Meaning") %></p>
                    
                </div>

                             
                                </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            	
            
            </div>
        
        </div>
        
        
        
        <div class="col-lg-4 col-md-4" style="display:none">
        
        
        <div class="panel panel-default style" >
  <div class="panel-heading panel-heading2">Popular Articles<i class="fa fa-snowflake-o" aria-hidden="true" style="float:right;font-size: 19px;"></i></div>
  <div class="panel-body my_panel">
  
  	<ul>
    
    	<li><a href="#">How Do You Prove a Defamation of Character Claim?</a></li>
        <li><a href="#">Best Way to Run a Free Arrest Warrant Check</a></li>
        <li><a href="#">How to Order a Criminal Background Check on Yourself</a></li>
        <li><a href="#">How Do I Get a Copy of My Criminal Record?</a></li>
        <li><a href="#">How to Calculate a Settlement in a Workman's Comp Injury</a></li>
        
    </ul>
  
  </div>
</div>


		
        
        <div class="panel panel-default style" >
  <div class="panel-heading panel-heading2">Related<i class="fa fa-snowflake-o" aria-hidden="true" style="float:right;font-size: 19px;"></i></div>
  <div class="panel-body my_panel">
  
  	<ul>
    
    	<li><a href="#">Anti-Theft Device</a></li>
        <li><a href="#">Anti-Lock Brake System (ABS)</a></li>
        <li><a href="#">Agreed Value</a></li>
        <li><a href="#">Bodily Injury Liability Coverage</a></li>
        <li><a href="#">Blue Book</a></li>
        <li><a href="#">Benchmark Rate</a></li>
        
    </ul>
  
  </div>
</div>
        
        
        
        
        </div>
    	
    </div>  
    </div>
    <div class="clearfix"></div>
    </asp:Content>