<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseSearchBySection.aspx.cs" Inherits="EastlawUI_v2.CaseSearchBySection" 
    MasterPageFile="~/MemberMaster.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
   
     <%--    <asp:UpdatePanel ID="upPnlTop" runat="server">
            <ContentTemplate>--%>
     <!----------- Main Nav End ------------->
    
    <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    
    <li><a href="#" class="current">Case Law by Section</a></li>
</ul>
  </div>
</div>
    <!-------------- Content --------------->
    
    <div class="container">
    <div class="row">
    
   		<div class="col-lg-8 col-md-8">
        
        	
            <div class="heading_style">
            
            	<h3>Case Law by Section</h3>
            
            </div>
        
        
        	<div class="box">
            <div>
            	  
                <asp:DropDownList ID="ddlType" runat="server"  AutoPostBack="True" CssClass="form-control js-example-placeholder-single" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                    <asp:ListItem Value="PRIMARY" Text="Acts , Statutes , Ordinances"></asp:ListItem>
                    <asp:ListItem Value="SECONDARY" Text="Rules, Regulations, Orders, Schemes"></asp:ListItem>
                </asp:DropDownList>
            </div>
            </div>
            
            <div class="box">
        
        
        <div style="width:100%;">
               <asp:DropDownList ID="ddlStatutes" runat="server"  CssClass="form-control js-example-placeholder-single" AutoPostBack="True" OnSelectedIndexChanged="ddlStatutes_SelectedIndexChanged">
                   
                </asp:DropDownList>
                        <%--<input class="form-control text_field" placeholder="Search a term to search in title" name="srch-term" id="srch-term" type="text">
                        <div class="input-group-btn">
                            <button class="btn btn-default btn_height2" type="submit"><i class="fa fa-search"></i></button>
                        </div>--%>
                    </div>
               
        
        </div>

               <div class="box">
            <div>
            	  
                <div style="width:100%;">
               <asp:DropDownList ID="ddlSections" runat="server"  CssClass="form-control js-example-placeholder-single" >
                   
                </asp:DropDownList>
                      
                    </div>
               
            </div>
            </div>

               <div class="box">
            <div>
            	  
                <div style="width:100%;">
                    </div>
                  <asp:Button id="btnSearch" runat="server" class="btn btn-style" Text="Search" OnClick="btnSearch_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="true" Text=""></asp:Label>
                </div>
                   </div>
        
        <div class="box" style="display:none">
        
        <asp:ListBox ID="lstSectionsList" runat="server"  class="form-control text_area">

        </asp:ListBox>
        <%--<textarea class="form-control text_area" readonly="readonly"></textarea>--%>
        
        
           
        </div>
        
        	
            
            <div class="clearfix"></div>
           
        
        </div>
        
        
        
        <div class="col-lg-4 col-md-4">
        
        
        <div class="panel panel-default" style="    box-shadow: 0 0 10px #000;">
  <div class="panel-heading panel-heading2">Find Case Law by Section / Rule<i class="fa fa-search" aria-hidden="true" style="float:right;font-size: 19px;"></i></div>
  <div class="panel-body my_panel">
  
  	<p>Use this feature to find case law of any court on a particular Sections/Rule.<br /><br />
    
    Type a word or part of a word, or use wild cards that you feel represent your search query most accurately, in the ‘Search Text’ box, click on the ‘Find’ button, and find the result you’re looking for in the results tree.
    <br /><br />
    
    The results will list all Acts/Rules which have the text of your search query in the title of that Acts/Rule.
    <br /><br />
    
    Scroll down to the relevant Section/Rule and click on "Go" below. The "Database Panel" will display the number of results in each Tab. Read through all the results or narrow your search by selecting one or multiple Jurisdiction.
    <br />
    
    
    </p>
  
  </div>
</div>
        
        
        </div>
    	
    </div>  
    </div>
    <div class="clearfix"></div>
    <!-------------- Content End --------------->
    <div class="container" style="display:none">
        
        	<div class="row">
        
        
        		<div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>National Assembly</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1">
<li class="news-item">
<table cellpadding="4">
<tr>

<td><b>Shahid Parveen VS Muhammad Nawaz</b><br />


<b>Date:</b> July 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>John Cena VS Triple H</b><br />


<b>Date:</b> June 19, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Jeff Hardy VS CM Punk</b><br />


<b>Date:</b> August 11, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Undertaker VS Edge</b><br />


<b>Date:</b> July 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Brock Lesnar VS The Rock</b><br />


<b>Date:</b> Oct 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Ronnie VS Selby</b><br />


<b>Date:</b> July 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Kane VS Nasir Khan Jaan</b><br />


<b>Date:</b> Jan 10, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
</ul>
</div>
</div>
</div>
<div class="panel-footer"> </div>
</div>
        
        </div>
        
        
        
        
        
        <div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Provincial Assemblies</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1">
<li class="news-item">
<table cellpadding="4">
<tr>

<td><b>Shahid Parveen VS Muhammad Nawaz</b><br />


<b>Date:</b> July 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>John Cena VS Triple H</b><br />

<b>Date:</b> June 19, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Jeff Hardy VS CM Punk</b><br />


<b>Date:</b> August 11, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Undertaker VS Edge</b><br />


<b>Date:</b> July 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Brock Lesnar VS The Rock</b><br />


<b>Date:</b> Oct 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Ronnie VS Selby</b><br />


<b>Date:</b> July 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Kane VS Nasir Khan Jaan</b><br />


<b>Date:</b> Jan 10, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
</ul>
</div>
</div>
</div>
<div class="panel-footer"> </div>
</div>
        
        </div>
        
        
        
        
        
        
        <div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Circular</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1">
<li class="news-item">
<table cellpadding="4">
<tr>

<td><b>Shahid Parveen VS Muhammad Nawaz</b><br />


<b>Date:</b> July 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>John Cena VS Triple H</b><br />


<b>Date:</b> June 19, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Jeff Hardy VS CM Punk</b><br />


<b>Date:</b> August 11, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Undertaker VS Edge</b><br />


<b>Date:</b> July 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Brock Lesnar VS The Rock</b><br />


<b>Date:</b> Oct 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Ronnie VS Selby</b><br />


<b>Date:</b> July 26, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
<li class="news-item">
<table cellpadding="4">
<tr>
<td><b>Kane VS Nasir Khan Jaan</b><br />


<b>Date:</b> Jan 10, 2017<br />
Lorem ipsum dolor sit amet, consectetur adipiscing
 <a href="#">Read more...</a></td>
</tr>
</table>
</li>
</ul>
</div>
</div>
</div>
<div class="panel-footer"> </div>
</div>
        
        </div>
        		
        	
            </div>
        
        </div>
                  <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlTop">
                    <ProgressTemplate>
                     
                           <div class="modal1">
        <div class="center1">
           <img alt="" src="/style/img/ajax_loader_big.gif" />
        </div>
    </div>
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                </ContentTemplate>
           </asp:UpdatePanel>--%>
</asp:Content>


