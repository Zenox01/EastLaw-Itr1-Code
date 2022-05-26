<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseAdvanceSearch.aspx.cs" Inherits="EastlawUI_v2.CaseAdvanceSearch" 
    MasterPageFile="~/MemberMaster.Master"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
       <asp:UpdatePanel ID="upPnlTop" runat="server">
            <ContentTemplate>
    <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    
    <li><a href="#" class="current">Case Law</a></li>
</ul>
  </div>
</div>
    <div class="container">
    <div class="row margin_bot_30">
    
   		<div class="col-lg-8 col-md-8">
        
        	
            <div class="heading_style">
            
            	<h3>Search with keywords</h3>
            
            </div>
        
        <div class="box">
        
        
        <div>
                        <asp:RequiredFieldValidator ID="rfvFreetxt" runat="server" ForeColor="Red" ErrorMessage="Please enter keyword" ControlToValidate="txtFreeText" ValidationGroup="A"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtFreeText" runat="server" class="form-control text_field" placeholder=" Search Any word(s)" ValidationGroup="A"></asp:TextBox>
                        <div class="input-group-btn">
                            <%--<button class="btn btn-default btn_height2" type="submit"><i class="fa fa-search"></i></button>--%>
                            <asp:Button ID="btnSearchFreetext" runat="server" Text="Search" CssClass="btn btn-default btn_height2" ValidationGroup="A" OnClick="btnSearchFreetext_Click"  Visible="true"   />
                        </div>
                    </div>
        
        </div>

                <div class="box">
        
        
        <div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="Please enter keyword" 
                            ControlToValidate="txtExactPhrase" ValidationGroup="ExactPhrase"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtExactPhrase" runat="server" class="form-control text_field" placeholder="Search exact phrase" ValidationGroup="ExactPhrase" ></asp:TextBox>
                        <div class="input-group-btn">
                            <%--<button class="btn btn-default btn_height2" type="submit"><i class="fa fa-search"></i></button>--%>
                            <asp:Button ID="btnExactPhrase" runat="server" Text="Search" CssClass="btn btn-default btn_height2" ValidationGroup="ExactPhrase" OnClick="btnExactPhrase_Click"  Visible="true"   />
                        </div>
                    </div>
        
        </div>
               <div class="box">
            
            	<div class="inner">
            	 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="Please enter keyword 1" 
                            ControlToValidate="txtExactPhraseMore" ValidationGroup="ExactPhraseMore" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtExactPhraseMore" runat="server" class="form-control text_field" placeholder="Add First phrase" ValidationGroup="ExactPhraseMore" Width="200"></asp:TextBox> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ErrorMessage="Please enter keyword 2" 
                            ControlToValidate="txtExactPhraseMore" ValidationGroup="ExactPhraseMore" Display="Dynamic"></asp:RequiredFieldValidator>
                      <asp:TextBox ID="txtExactPhraseMore2" runat="server" class="form-control text_field" placeholder="Add Second phrase" ValidationGroup="ExactPhraseMore" Width="200"></asp:TextBox>
                
            	</div>
          <asp:Button ID="btnExactPhraseMore" runat="server" Text="Search" CssClass="btn btn-default btn_height2" ValidationGroup="ExactPhraseMore" OnClick="btnExactPhraseMore_Click"  Visible="true"   />
         
                
            </div>
              
        
         <div class="box">
          <telerik:radautocompletebox RenderMode="Lightweight"  runat="server" ID="radtxtKeywords" CssClass="form-control text_field"  EmptyMessage="Select multiple keywords"  AllowCustomEntry="true"
                InputType="Token" Width="650" >
                            <TokensSettings AllowTokenEditing="true" />
            </telerik:radautocompletebox>
        	<div class="input-group-btn">
                            <asp:Button ID="btnMultiKeywords" runat="server" Text="Search" CssClass="btn btn-default btn_height2"  OnClick="btnMultiKeywords_Click"  Visible="true"   />
                        </div>
           <%-- <button class="submitBtn" type="submit">Submit</button>
             <asp:HiddenField ID="hfCustomerId" runat="server" />
                    	<asp:Button ID="btnKeywordSearch" runat="server" Text="Search" CssClass="submitBtn"  ValidationGroup="B" OnClick="btnKeywordSearch_Click" Visible="false"  />
           
        
        
        --%>
        </div>
               <div class="box" style="display:none">
          <telerik:radautocompletebox RenderMode="Lightweight"  runat="server" ID="radAutoCompleteCourt"  EmptyMessage="Please Select Court"  AllowCustomEntry="true"
                InputType="Token" Width="350" >
                            <TokensSettings AllowTokenEditing="true" />
            </telerik:radautocompletebox>
        	
           <%-- <button class="submitBtn" type="submit">Submit</button>
             <asp:HiddenField ID="hfCustomerId" runat="server" />
                    	<asp:Button ID="btnKeywordSearch" runat="server" Text="Search" CssClass="submitBtn"  ValidationGroup="B" OnClick="btnKeywordSearch_Click" Visible="false"  />
           
        
        
        --%>
        </div>
        
        
        
        <%--<div class="box">
             <div style="height:200px;overflow:scroll;background-color:rgba(211, 211, 211, 0.18);padding-left:5px;">
          <asp:CheckBoxList ID="chkLstCourt" runat="server" >

                       </asp:CheckBoxList>
                 </div>
        	
        </div>--%>
        
         <div class="box">
        
        	<%--<input type="text" placeholder="Type" class="form-control" />--%>
        <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn_style" OnClick="btnSearch_Click" Visible="false"/>
             <asp:Label ID="lblCaseAdvanceMsg" runat="server" Visible="true" Text=""></asp:Label>
        	<%--<button class="btn_style">
            
            Search&nbsp;&nbsp;&nbsp;
            <i class="fa fa-search"></i>
            </button>--%>
        </div>
        <div class="heading_style">
            
            	<h3>Search By Party Name</h3>
            
            </div>
                <div class="box">
          <%--<telerik:radautocompletebox RenderMode="Lightweight"  runat="server" ID="radCourtParty"  EmptyMessage="Please Select Court"  AllowCustomEntry="true"
                InputType="Token" Width="350" >
                            <TokensSettings AllowTokenEditing="true" />
            </telerik:radautocompletebox>--%>
                    <asp:DropDownList ID="ddlPartyCourt" runat="server" CssClass="form-control"></asp:DropDownList>
        	
           <%-- <button class="submitBtn" type="submit">Submit</button>
             <asp:HiddenField ID="hfCustomerId" runat="server" />
                    	<asp:Button ID="btnKeywordSearch" runat="server" Text="Search" CssClass="submitBtn"  ValidationGroup="B" OnClick="btnKeywordSearch_Click" Visible="false"  />
           
        
        
        --%>
        </div>
               <div class="box">
        <%--<input type="text" placeholder="Party Name" class="form-control" />--%>
            <asp:TextBox ID="txtPartyNames" runat="server" placeholder="Party Name" class="form-control"  ></asp:TextBox>
        
        </div>
      
        
       <div class="box">
        
        	<%--<input type="text" placeholder="Type" class="form-control" />--%>
        <asp:Button ID="btnPartySearch" runat="server" Text="Search" class="btn_style" OnClick="btnPartySearch_Click" />
             <asp:Label ID="lblPartySearch" runat="server" Visible="true" Text=""></asp:Label>
        	<%--<button class="btn_style">
            
            Search&nbsp;&nbsp;&nbsp;
            <i class="fa fa-search"></i>
            </button>--%>
        </div>
        
       <%-- <div class="box">
        
        	<select class="form-control" >
            	<option>Select Judge Type</option>
            </select>
        
        	<button class="btn_style">
            
            Search&nbsp;&nbsp;&nbsp;
            <i class="fa fa-search"></i>
            </button>
        
        </div>--%>
        
        
        
        
        </div>
        
        	<div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Supreme Court of Pakistan </b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1" style="height:auto !important; font-size:14px;">
    <%
                                                            System.Data.DataTable dtLatest = new System.Data.DataTable();
                                                            EastLawBL.Cases objc = new EastLawBL.Cases();
                                                            dtLatest = objc.GetCasesByCourtFront("Supreme Court");
                                                            if (dtLatest != null)
                                                            {
                                                                for (int a = 0; a < dtLatest.Rows.Count; a++)
                                                                {
                                                                    Response.Write("<li class='news-item'>"
                                                                       + "<table cellpadding='4' width=310px><tr>"
                                                                        + "<td><b>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 2) + "... VS " + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 2) + "...</b><br />"
                                                                        + "<b>Date:</b> " + dtLatest.Rows[a]["FormatedJdate"].ToString() + "<br /><b>Court:</b> " + dtLatest.Rows[a]["Court"].ToString() 
                                                                        //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                                                                        + " <br><span style='float:right'> <a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "'>Read more...</a></span></td></tr></table></li>");
                                                                    if (a == 15)
                                                                        break;
                                                                } 
                                                            }
                                                            
                                                        %>


</ul>
</div>
</div>
</div>
<div class="panel-footer"><a href="/cases/supreme-court-of-pakistan">View More</a></div>
</div>
        
        </div>
        
        <div class="col-lg-4 col-md-4" style="display:none">
        
        
        <div class="panel panel-default style">
  <div class="panel-heading panel-heading2">Find Case Law <i class="fa fa-search" aria-hidden="true" style="float:right;font-size: 19px;"></i></div>
  <div class="panel-body my_panel">

   <div class="heading_style">
            	<h3>Search with keywords</h3>
            </div>
      <b>Free Text Search </b><br />
<p>Use any keyword to search through our database.</p>
      <b>Multiple Keyword Search</b><br />
<p>Use this feature to find case law of any court with multiple keywords. Enter choice of keyword or select from our multiple keywords to find combination of search in any selected court.</p>
      <p>Type a word or part of a word, or use wild cards that you feel represents your search query most accurately. </p>

        <div class="heading_style">
            	<h3>Search by Party Name</h3>
            </div>
     
<p>Are you looking to find case law by Party Name?</p>
      <p>Select a court and enter Party Name (either Appellant or Respondent). It will bring you all results from related party names. </p>

       <div class="heading_style">
            	<h3>Search Case Law by Section (Coming Soon)</h3>
            </div>
     
<p>Enter Section, select a statute, find all the related case laws linked to your need.</p>
      

  	
    
    
  
  </div>
</div>
        
        
        </div>
    	
    </div>  
    </div>
    <div class="container">
        
        	<div class="row">
        
        
        	
        
        
        
        
        
        <div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>High Court</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1" style="height:auto !important; font-size:14px;">
<%
                                                           // System.Data.DataTable dtHigh = new System.Data.DataTable();
                                                           // EastLawBL.Cases objc = new EastLawBL.Cases();
                                                            dtLatest = objc.GetCasesByCourtFront("high court");
                                                            if (dtLatest != null)
                                                            {
                                                                for (int a = 0; a < dtLatest.Rows.Count; a++)
                                                                {
                                                                    Response.Write("<li class='news-item'>"
                                                                       + "<table cellpadding='4' width=310px><tr>"
                                                                        + "<td><b>" + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 2) + "... VS " + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 2) + "...</b><br />"
                                                                        + "<b>Date:</b> " + dtLatest.Rows[a]["FormatedJdate"].ToString() + "<br /><b>Court:</b> " + dtLatest.Rows[a]["Court"].ToString() 
                                                                        //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                                                                        + " <br><span style='float:right'> <a href='/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString()) + "'>Read more...</a></span></td></tr></table></li>");
                                                                    if (a == 15)
                                                                        break;
                                                                } 
                                                            }
                                                            
                                                        %>
</ul>
</div>
</div>
</div>
<div class="panel-footer"><a href="/cases/high-court">View More</a></div>
</div>
        
        </div>
        
        
        
        
        
        
        <%--<div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Circular</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1" style="height:auto !important; font-size:14px;">
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
        
        </div>--%>
        		
        	
            </div>
        
        </div>
                  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlTop">
                    <ProgressTemplate>
                     
                           <div class="modal1">
        <div class="center1">
           <img alt="" src="/style/img/ajax_loader_big.gif" />
        </div>
    </div>
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                </ContentTemplate>
           </asp:UpdatePanel>
</asp:Content>


