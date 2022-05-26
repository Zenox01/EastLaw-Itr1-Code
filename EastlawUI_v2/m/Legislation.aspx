<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Legislation.aspx.cs" Inherits="EastlawUI_v2.m.Legislation" 
    MasterPageFile="~/m/MemberMaster.Master"%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <form runat="server">
    <div class="bannde-slide"><img src="images/CurrentAwareness2.jpg" alt="Current Awareness" /></div>


<div class="contentPage">
    <div id="divWithoutlogin" runat="server">
        
	<div class="container">
    	<div class="margin">
             <%
                    try
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        EastLawBL.Pages objpages = new EastLawBL.Pages();
                    
                    dt = objpages.GetPages(12);
                    if (dt.Rows.Count > 0)
                    {
                        
                        Response.Write("<p>" + dt.Rows[0]["FullContent"].ToString() + "</p>");
                    } 
                    }
                    catch { } %>
        </div>
    </div>
        </div>
    <div  id="divWithLogin" runat="server">
	
    <div class="container">
        <h2 style="custom">Advance Legislation Search</h2>
        
    </div>
   

	
   <div class="resources grayBg">
       <div class="container">
            <div class="margin">
            
                <div class="abcPegi" style="display:none">
                	<ul>
                    	<li><a href="#" class="active">A</a></li>
                        <li><a href="#">B</a></li>
                        <li><a href="#">C</a></li>
                        <li><a href="#">D</a></li>
                        <li><a href="#">E</a></li>
                        <li><a href="#">F</a></li>
                        <li><a href="#">G</a></li>
                        <li><a href="#">H</a></li>
                        <li><a href="#">I</a></li>
                        <li><a href="#">J</a></li>
                        <li><a href="#">K</a></li>
                        <li><a href="#">L</a></li>
                        <li><a href="#">M</a></li>
                        <li><a href="#">N</a></li>
                        <li><a href="#">O</a></li>
                        <li><a href="#">P</a></li>
                        <li><a href="#">Q</a></li>
                        <li><a href="#">R</a></li>
                        <li><a href="#">S</a></li>
                        <li><a href="#">T</a></li>
                        <li><a href="#">U</a></li>
                        <li><a href="#">V</a></li>
                        <li><a href="#">W</a></li>
                        <li><a href="#">X</a></li>
                        <li><a href="#">Y</a></li>
                        <li><a href="#">Z</a></li>
                    </ul>
                </div>

               
                
                <div class="adSearch">
                	<div class="row">
                    	<div class="txt"><asp:Label ID="Label1" runat="server" Text="Title"></asp:Label></div>
                        <div class="inputRow">
                        	<div class="lft">
                        <asp:TextBox ID="txtTitle" runat="server" placeholder="" Width="300px"  ValidationGroup="Title"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Title" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                          
                        </div>
                    </div>
                </div>
                <div class="adSearch">
                	<div class="row">
                    	<div class="txt"><asp:Label ID="Label3" runat="server" Text="Year"></asp:Label></div>
                        <div class="inputRow">
                        	<div class="lft">
                        <asp:TextBox ID="txtYear" runat="server" placeholder="" Width="300px"  ValidationGroup="Title"></asp:TextBox>
                                </div>
                         
                        </div>
                    </div>
                </div>
                <div class="adSearch">
                	<div class="row">
                    	<div class="txt"><asp:Label ID="Label5" runat="server" Text="Subject"></asp:Label></div>
                        <div class="inputRow">
                        	<div class="lft">
                        
                                <asp:DropDownList ID="ddlStatutesCat" runat="server"  ></asp:DropDownList> 
                                </div>
                         
                        </div>
                    </div>
                </div>
                <div class="adSearch">
                                <div class="row">
                                    <div class="inputRow">
                                        <div class="rgt2">
                                            <asp:Button ID="btnSearchTitle" runat="server" CssClass="input2" Text="Search" ValidationGroup="Party" OnClick="btnSearchTitle_Click1" />
                                            &nbsp;
                                            <asp:Label ID="lblPartySearch" runat="server" Text="" Visible="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                
                <div class="adSearch" style="display:none">
                	<div class="row">
                    	<div class="txt"><asp:Label ID="Label7" runat="server" Text="Free Text" ></asp:Label></div>
                        <div class="inputRow">
                        	<div class="lft">
                        <asp:TextBox ID="txtFreeText" runat="server" placeholder="" Width="300px"  ValidationGroup="FreeText"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtFreeText" runat="server" ControlToValidate="txtFreeText" ErrorMessage="Required" ForeColor="Red" ValidationGroup="FreeText" Display="Dynamic"></asp:RequiredFieldValidator>
                                <div class="psRow">
                            	<div class="lft3"><asp:CheckBox ID="chkPrimFreeText" runat="server" Text="" Checked="true" /></div>
                                <div class="rgt3">Primary</div>
                                <div class="lft3"><asp:CheckBox ID="chkSecFreeText" runat="server" Text="" Checked="true" /></div>
                                <div class="rgt3">Secondary</div>
                            </div>
                        	</div>

                            <div class="rgt"><asp:Button ID="btnSearchFreetext" runat="server" Text="Search"  OnClick="btnSearchFreetext_Click"   ValidationGroup="FreeText" /></div>
                        </div>
                    </div>
                </div>
                
                <div class="adSearch" style="display:none">
                	<div class="row">
                    	<div class="txt"><asp:Label ID="Label2" runat="server" Text="Search within index"></asp:Label></div>
                        <div class="inputRow">
                        	<div class="lft">
                        <asp:TextBox ID="txtSearchWithinIndex" runat="server" placeholder="" Width="300px"  ValidationGroup="SearchWithinIndex"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtSearchWithinIndex" runat="server" ControlToValidate="txtSearchWithinIndex" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SearchWithinIndex" Display="Dynamic"></asp:RequiredFieldValidator>
                          <%--  <div class="psRow">
                            	<div class="lft3"><input name="" type="checkbox" value=""></div>
                                <div class="rgt3">Primary</div>
                                <div class="lft3"><input name="" type="checkbox" value=""></div>
                                <div class="rgt3">Secondary</div>
                            </div>--%>
                            </div>
                            <div class="rgt"><input name="Search" value="Search" type="button"></div>
                        </div>
                    </div>
                </div>
                
                
                
                <div class="adSearch" style="display:none">
                	<div class="row">
                    	<div class="txt"> <asp:Label ID="Label4" runat="server" Text="S.R.O No." ></asp:Label></div>
                        <div class="inputRow">
                        	<div class="lft">
                            	<div class="one1"><asp:TextBox ID="txtSRONo" runat="server" ToolTip="Year" Width="130"  placeholder="Enter S.R.O No."></asp:TextBox></div>
                                <div class="two2">Year:</div>
                                <div class="three3">
                                	<asp:DropDownList ID="ddlSROYear" runat="server"  ></asp:DropDownList> 
                                </div>
                            </div>
                            <div class="rgt"><asp:Button ID="btnSRO" runat="server" Text="Search"  OnClick="btnSRO_Click"  /></div>
                            
                        </div>
                    </div>
                </div>
                
                <div class="adSearch" style="display:none">
                	<div class="row">
                    	<div class="txt"><asp:Label ID="Label6" runat="server" Text="Circular No." ></asp:Label></div>
                        <div class="inputRow">
                        	<div class="lft">
                            	<div class="one1"><asp:TextBox ID="txtCircularNo" runat="server" ToolTip="Year" placeholder="Enter Circular No."></asp:TextBox></div>
                                <div class="two2">Year:</div>
                                <div class="three3">
                                	<asp:DropDownList ID="ddlCircularYear" runat="server" ></asp:DropDownList> 
                                </div>
                            </div>
                            <div class="rgt"><asp:Button ID="btnCircular" runat="server" Text="Search" OnClick="btnCircular_Click" /></div>
                            
                        </div>
                    </div>
                </div>
                
                <div class="adSearch">
                	<div class="row">
                    	<div class="txt">Search Index</div>
                        
                        <div class="searchIndex">
                            
                	<div class="cols1"><a href="/m/statutes/federal-legislation">Federal Legislation</a></div>
                    <div class="cols1"><a href="/m/statutes/provincial-legislation">Provincial Legislation</a></div>
                    <div class="cols1"><a href="/m/statutes/federal-amendment-acts">Federal Amendment Acts</a></div>
                    <div class="cols1"><a href="/m/statutes/provincial-amendment-acts">Provincial Amendment Acts</a></div>
                    <div class="cols1"><a href="/m/statutes/federal-rules-and-regulations">Federal Rules and Regulations</a></div>
                    <div class="cols1"><a href="/m/statutes/provincial-rules-and-regulations">Provincial Rules and Regulations</a></div>
                    <div class="cols1"><a href="/m/statutes/bill-by-national-assembly">Bill By National Assembly</a></div>
                    <div class="cols1"><a href="/m/statutes/bill-by-provincial-assembly">Bill By Provincial Assembly</a></div>

                            
                             <%
                                
                
                                //System.Data.DataTable dtCatYear = new System.Data.DataTable();
                                //EastLawBL.Statutes objs = new EastLawBL.Statutes();
                                //dtCatYear = objs.GetStatutesCatWithYear();
                                //for (int a = 0; a < dtCatYear.Rows.Count; a++)
                                //{

                                //    Response.Write("<div class='cols1'><a href='/Statutes/Search-Result?cat=" + EncryptDecryptHelper.Encrypt(dtCatYear.Rows[a]["CatID"].ToString()) + "'>" + dtCatYear.Rows[a]["CatName"].ToString() + "</a></div>");
                                    

                                //}
                                 %>
                        <%--	<div class="cols1"><a href="#">Administrative Laws</a></div>
                            <div class="cols2"><a href="#">1850</a></div>
                            <div class="cols2"><a href="#">1869</a></div>
                            <div class="cols2"><a href="#">1883</a></div>
                            <div class="cols2"><a href="#">1886</a></div>
                            
                            <div class="cols1"><a href="#">Administrative Laws</a></div>
                            <div class="cols2"><a href="#">1850</a></div>
                            <div class="cols2"><a href="#">1869</a></div>
                            <div class="cols2"><a href="#">1883</a></div>
                            <div class="cols2"><a href="#">1886</a></div>
                            
                            <div class="cols1"><a href="#">Administrative Laws</a></div>
                            <div class="cols2"><a href="#">1850</a></div>
                            <div class="cols2"><a href="#">1869</a></div>
                            <div class="cols2"><a href="#">1883</a></div>
                            <div class="cols2"><a href="#">1886</a></div>
                            
                            <div class="cols1"><a href="#">Administrative Laws</a></div>
                            <div class="cols2"><a href="#">1850</a></div>
                            <div class="cols2"><a href="#">1869</a></div>
                            <div class="cols2"><a href="#">1883</a></div>
                            <div class="cols2"><a href="#">1886</a></div>--%>
                        </div>
                    </div>
                </div>
                
                <!--<div class="adSearch">
                	<div class="row">
                        <div class="inputRow">
                            <div class="rgt2"><input name="Search" value="Search" type="button"></div>
                        </div>
                    </div>
                </div>-->
               
            </div>
            
        </div>
        <div class="clear"></div>
   </div> 	
        </div>


    
</div>
        </form>
</asp:Content>

