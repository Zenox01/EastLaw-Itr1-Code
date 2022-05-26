<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PracticeArea.aspx.cs" Inherits="EastlawUI_v2.m.PracticeArea"
    MasterPageFile="~/m/MemberMaster.Master" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div class="contentPage">
	<h1>
    <div class="container">
        <div class="margin">
            Practice Area
        </div>
        
    </div>
    </h1>

	<div class="clear"></div>
    <div class="folder">
    	<div class="container">
        	<div class="margin">
            
            	<div class="ptrArea">
                 <%
                            EastLawBL.PracticeAreas objpa = new EastLawBL.PracticeAreas();
                            System.Data.DataTable dtPA = new System.Data.DataTable();
                            dtPA = objpa.GetActivePracticeAreaSubCategoriesByCategory(3);
                            for(int a=0;a<dtPA.Rows.Count;a++)
                            {
                                Response.Write("<div class='blocks'>"
                        + "<a href='/m/Practice-Area/" + dtPA.Rows[a]["PracticeAreaSubCatName"].ToString().Replace(" ", "-") + "?param=" + EncryptDecryptHelper.Encrypt(dtPA.Rows[a]["ID"].ToString()) + "&trans=" + dtPA.Rows[a]["PracticeAreaSubCatName"].ToString().Replace(" ", "-") + "'><div class='colss " + dtPA.Rows[a]["IconTag"].ToString() + "'>"
                            + "<div class='smText'>&nbsp;</div>"
                            + "<div class='pic'>&nbsp;</div>"
                            + "<div class='heading'>"+ dtPA.Rows[a]["PracticeAreaSubCatName"].ToString() + "</div>"
                            + "<div class='clear'></div>"
                        + "</div></a></div>");
                                //Response.Write("<a href='/Practice-Area/" + dtPA.Rows[a]["PracticeAreaSubCatName"].ToString().Replace(" ", "-") + "?param=" + EncryptDecryptHelper.Encrypt(dtPA.Rows[a]["ID"].ToString()) + "&trans=" + dtPA.Rows[a]["PracticeAreaSubCatName"].ToString().Replace(" ", "-") + "'><li class='boxH'> <img src='/images/tax-icon-25.png' height='100' width='140'><br>" + dtPA.Rows[a]["PracticeAreaSubCatName"].ToString() + "</li></a>");
                            }
                     %>
                   
                    
                </div>
                
            </div>
        </div>
    </div>	


    
</div>
</asp:Content>

