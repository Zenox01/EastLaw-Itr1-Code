<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopArea.ascx.cs" Inherits="EastlawUI_v2.m.usercontrols.TopArea" %>
 <div id="page">
    <div class="header top">
    <div class="container">
        <div class="margin">
            <div class="lft"><a href="#menu" class="nevs"><span></span></a></div>
            <div class="rgt">
            	<asp:HyperLink ID="hypLnkWelcome" runat="server" NavigateUrl="/m/Member/Member-Dashboard"></asp:HyperLink> | 
                <a href="/m/?bG9nb3V0=do">Sign Out</a>
                
            </div>
        </div>
    </div>
    </div>
    
    <nav id="menu" style="color:white" >
    	
        <ul>
        <li class="mrg">
        	<a href="#"><span class="userPic">
                <img src="/m/images/User-blue-icon.png" alt="user" />

        	            </span>
            	<strong class="pad1">
                    <asp:Label ID="lblUserName" runat="server"></asp:Label> </strong><br />
                <%--<strong class="font12 pad2">name of the company</strong>--%>
            </a>
        </li>
            <li><a href="/m/Member/Member-Dashboard">Home</a></li>
            <li><a href="/m/Cases/Advance-Search">Cases</a></li>
            <li><a href="/m/Legislation">Legislation</a></li>
            <li><a href="/m/Practice-Area">Practice Area</a></li>
            <li><a href="/m/Departments/All-Departments">Department</a></li>
           
            <li><a href="#">E-Newslive</a></li>
            <li><a href="/m/?bG9nb3V0=do">Sign out</a></li>
             <br />
            <br />
            <br />
            <br />
            <div>
                <li style="display:inline;font-size:12px;padding-right:20px;padding-left:20px"><a href="#">Recent Search</a></li>|
                <li style="display:inline;font-size:12px;padding-right:20px;padding-left:20px"><a href="/m/Member/My-Folders">My Folders</a></li>|
                <li style="display:inline;font-size:12px;padding-right:20px;padding-left:20px"><a href="#">Dictionary</a></li>|
                <li style="display:inline;font-size:12px;padding-right:20px;padding-left:20px"><a href="/m/en/Site-Feedback">Site Feedback</a></li>|
                <li style="display:inline;font-size:12px;padding-right:20px;padding-left:20px"><a href="#">Account Settings</a></li>|
                <li style="display:inline;font-size:12px;padding-right:20px;padding-left:20px"><a href="/m/en/Help">Help</a></li>
                
            </div>
        </ul>
        
 

    </nav>
</div>
 <div class="clear"></div>  
    
<div class="logo">
    <div class="container"><img src="/m/images/eastlaw_logo_m.png" alt="logo" /></div>
</div>
   
<div class="clear"></div>

 <div class="nav" >
	<div class="container">
    	<div class="margin">
    	<ul>
        	<li class="class2"><a href="/m/Member/Member-Dashboard"><img src="/m/images/icon/home.png" alt="home" title="home" /></a></li>
            <li class="class1"><a href="/m/Cases/Advance-Search"><img src="/m/images/icon/law.png" alt="Law" title="advance case search" /></a></li>
            <li class="class1"><a href="/m/Legislation"><img src="/m/images/icon/scal.png" alt="Scale" title="find legislation"/></a></li>
            <li class="class1"><a href="/m/Practice-Area"><img src="/m/images/icon/doc.png" alt="Doc Details" title="practice area search" /></a></li>
            <li class="class3"><a href="/m/en/Contact-us"><img src="/m/images/icon/call.png" alt="Call" title="contact us"/></a></li>
        </ul>
        </div>
    </div>
</div>
