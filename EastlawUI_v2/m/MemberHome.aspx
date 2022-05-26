<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberHome.aspx.cs" Inherits="EastlawUI_v2.m.MemberHome" 
    MasterPageFile="~/m/MemberMaster.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <style>
.nav>li>a{
padding:10px 16px;
}
#exTab1 .nav{
background:#ddd;
box-shadow:0 0 10px #000;
height:auto !important;
}
        .nav-pills > li > a {
            padding: 10px 7px !important;
            color: #000;
            border-radius: 0;
        }
.nav-pills>li.active>a, .nav-pills>li.active>a:focus, .nav-pills>li.active>a:hover
{
background:#fff;
color:black;
}
        .nav-pills > li {
        padding:0 !important;}
.nav-pills>li.active>a, .nav-pills>li.active>a:focus, .nav-pills>li.active>a
{
    background:#fff !important;
color:#cc1b05 !important;
}
.adSearch .row .inputRow .lft .srch1
{
width:100%;
}
.adSearch .row .inputRow .lft
{
width:75%;
}
.adSearch .row .inputRow .rgt
{
padding-top:0px;
}
.adSearch .row .inputRow .rgt input
{
padding:8px 10px;
border-radius: 0px 4px 4px 0;
font-size:13px;

}
.adSearch .row .inputRow .lft input
{
border-radius: 4px 0 0 4px;
border-right: 0;
    width: 100%;
}
.tab-pane{
    margin: 0;
    margin-bottom: 5px;
    margin-top: 3px;
}
        .adSearch .row .inputRow .lft .srch1 {
        width:100%;
        padding:0;
        }
.find_legislation .rgt .input
{border-radius:4px !important;
padding:0 0 !important;}
.find_legislation .lft
{
padding-top:5px;
}
.searchBtn3
{
text-align:center !important;
}
#exTab1 .tab-content
{
padding:5px 0;
}
.search_citation .lft11 .cols11 input
{
padding:5px 10px;
border-radius:4px;
}
.search_citation .lft11 .cols11 select
{
padding:5px 6px;
border-radius:4px;
}
.search_citation .rgt11
{
margin:auto;
float:none;
}
    


    </style>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scp" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="upPnlTop" runat="server">
              <ContentTemplate>
    <div class="contentPage">
        <br />
    <div class="container">

	<div id="exTab1" >	
<ul  class="nav nav-pills">
			<li class="active">
        <a  href="#1a" data-toggle="tab">Main Search</a>
			</li>
			<li><a href="#2a" data-toggle="tab">Find Legislation</a>
			</li>
			<li><a href="#3a" data-toggle="tab">Search By Citation</a>
			</li>
		</ul>

			<div class="tab-content clearfix">
			  <div class="tab-pane active" id="1a">
             
    <div class="container">
       <h2 class="custom"> Main Search Area</h2>
       
    </div>
    
   <div class="resources grayBg mrBtm">
       <div class="container">
            <div class="margin">
             
                <div class="adSearch">
                	<div class="row">
                        <div class="inputRow">
                        	<div class="lft">
                            	<div class="srch1">
                                    <asp:TextBox ID="txtSearch" runat="server"  placeholder="Search Eastlaw"></asp:TextBox>
                                    
                                     <asp:Label ID="lblMsg" runat="server" Text="Please enter search words" Visible="false" ForeColor="Red"></asp:Label>

                            	</div>
                            </div>
                            <div class="rgt">
                            
                                <asp:Button ID="btnSearch" runat="server" Text="Search" class="input12" OnClick="btnSearch_Click" />
                           <%-- <span><a href="#">Search Tips</a></span>--%>
                            </div>
                            <div class="checkOption" style="display:none">
                             <asp:CheckBoxList ID="chkLst" runat="server" RepeatColumns="2" CssClass="cls1" >
                                <asp:ListItem Text="Cases" Value="Cases" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Legislation" Value="Legislation" ></asp:ListItem>
                                <asp:ListItem Text="Dictionary" Value="Dictionary" ></asp:ListItem>
                                <asp:ListItem Text="Practice Area" Value="Practice Area" ></asp:ListItem>
                                <asp:ListItem Text="Current Awareness" Value="Current Awareness" ></asp:ListItem>
                            </asp:CheckBoxList>
                            
                            	<%--<div class="cls1">
                                  <input name="" type="checkbox" value="">  Cases
                                </div>
                                <div class="cls2">
                                  <input name="" type="checkbox" value="">  Practice Area
                                </div>
                                <div class="cls3">
                                  <input name="" type="checkbox" value="">  Current Awareness
                                </div>
                                
                                
                                <div class="cls1">
                                  <input name="" type="checkbox" value="">  Legislation
                                </div>
                                <div class="cls2">
                                  <input name="" type="checkbox" value="">  Dictionary
                                </div>
                                <div class="cls3">
                                  <span> <a href="#">Report Missing Legislation</a> &nbsp; &nbsp;  </span>
                                  <span><a href="#">Report Missing Citation</a></span>
                                </div>--%>
                                
                                
                            </div>
                        </div>
                    </div>
                </div>
                
                    
                
               
            </div>
            
        </div>
        <div class="clear"></div>
   </div>
				</div>
				<div class="tab-pane" id="2a">
          
    <div class="container">
        <h2 class="custom"> Find Legislation</h2>
       
        
    </div>
   

   <div class="resources grayBg mrBtm">
       <div class="container">
            <div class="margin">
                
                <div class="adSearch">
                	<div class="row">
                        <div class="find_legislation">
                        	
                            <div class="lft">
                                Title
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                    <%--<input type="text" class="input1" value="Acts, Ordinances, Rules, Regulations" class="srch" />--%>
                                    <asp:TextBox ID="txtStatutesTitle" runat="server" ToolTip="Title"  class="input1" placeholder="Acts, Oridinances, Rules, Regulations" ValidationGroup="Legislation" AutoPostBack="True" OnTextChanged="txtStatutesTitle_TextChanged"   ></asp:TextBox>
                                <asp:HiddenField ID="hfCustomerId" runat="server" />
                                    
                                </div>
                                <div class="checkOption2">
                            
                            	<ul>
                            	<li>
                                  <%--<input name="" type="checkbox" value="">  Primary--%>
                                    <asp:CheckBox ID="chkPri" runat="server"   Text="Primary"  Checked="false" Visible="false"/> 
                                </li>
                                <li>
                                  <%--<input name="" type="checkbox" value="">  Secondary--%>
                                    <asp:CheckBox ID="chksec" runat="server"   Text="Secondary"  Checked="false" Visible="false"/>
                                </li>
                                </ul>
                                
                             
                                
                            </div>
                            </div>
                            
                            
                        </div>
                        
                        <div class="find_legislation">
                        	
                            <div class="lft">
                                Year
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                    <%--<input type="text" class="input1" value="Acts, Ordinances, Rules, Regulations by Year" class="srch" />--%>
                                    <asp:TextBox ID="txtYear" runat="server" ToolTip="Year"  class="input1"  placeholder="Acts, Oridinances, Rules, Regulations By Year"></asp:TextBox>
                                    
                                </div>
                                <div class="searchBtn3"><%--<input name="Search" type="button" value="Search">--%>
                                    <asp:Button ID="btnStatutesSearch" runat="server" Text="Search" class="input3" OnClick="btnStatutesSearch_Click" ValidationGroup="Legislation" />
                                <asp:Label ID="lblLegisLationMsg" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            
                            
                        </div>
                    </div>
                </div>
                
            </div>
            
        </div>
        <div class="clear"></div>
   </div> 	
				</div>
        <div class="tab-pane" id="3a">
           
    <div class="container">
        <h2 class="custom">Search By Citation</h2>
        
        
    </div>
  

   <div class="resources grayBg">
       <div class="container">
            <div class="margin">
                
                <div class="adSearch">
                	<div class="row">
                        <div class="search_citation">
                            <div class="lft11">
                            	<div class="cols11">
                            	<%--  <input type="text" value="Year" />--%>

                                    <asp:TextBox ID="txtCitationYear" runat="server"  ToolTip="Year" placeholder="Year" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCitationYear" runat="server" ErrorMessage="Year" ForeColor="Red" ControlToValidate="txtCitationYear" ValidationGroup="Citation">
                            </asp:RequiredFieldValidator>
                           	    </div>
                                <div class="cols11">
                            	  <%--  <select name="jumpMenu" id="jumpMenu" onChange="MM_jumpMenu('parent',this,0)">
                                      <option>Select</option>
                                      <option>2014</option>
                                      <option>2015</option>
                                      <option>2016</option>
                                    </select>--%>

                                    <asp:DropDownList ID="ddlJournals" runat="server"  onChange="MM_jumpMenu('parent',this,0)"></asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="rvfJournals" runat="server" ErrorMessage="Journal" ForeColor="Red" ControlToValidate="ddlJournals" InitialValue="0" ValidationGroup="Citation">
                            </asp:RequiredFieldValidator>
                           	    </div>
                                <div class="cols11">
                            	 <%--<input type="text" value="Page" />--%>
                                      <asp:TextBox ID="txtCitationNumber" runat="server" class="input1" placeholder="Page" ToolTip="Number" Width="55"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNumber" runat="server" ErrorMessage="No." ForeColor="Red" ControlToValidate="txtCitationNumber" ValidationGroup="Citation">
                            </asp:RequiredFieldValidator>
                           	    </div>
                            </div>
                            
                            <div class="rgt11">
                                <%--<input name="Search" type="button" value="Search">--%>
                                <asp:Button ID="btnCitationSearch" runat="server" Text="Search"  OnClick="btnCitationSearch_Click" ValidationGroup="Citation" />
                            <br />
                            <asp:Label ID="lblCitationMsg" runat="server" ForeColor="Red"></asp:Label>

                            </div>
                            
                        </div>
                    </div>
                </div>
                
                
                
                
                
                
                
                
               
            </div>
            
        </div>
        <div class="clear"></div>
   </div> 
				</div>
        
			</div>
  </div>
        </div>
     
   
  
   
  
   

</div>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlTop">
                    <ProgressTemplate>
                     
                           <div class="modal1">
        <div class="center1">
           <img alt="" src="/m/images/ajax_loader_big.gif" />
        </div>
    </div>
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
              </ContentTemplate>
          </asp:UpdatePanel>
        </form>
</asp:Content>


