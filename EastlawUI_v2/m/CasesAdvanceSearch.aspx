<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CasesAdvanceSearch.aspx.cs" Inherits="EastlawUI_v2.m.CasesAdvanceSearch"
    MasterPageFile="~/m/MemberMaster.Master" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scrp" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="updpnl" runat="server">
            <ContentTemplate>

    <div id="divWithoutlogin" runat="server" style="display:none">
        <div class="bannde-slide">
            <img src="/m/images/slide2.jpg" alt="banner" /></div>


        <div class="contentPage">
            <h1>
                <div class="container">
                    <div class="margin">
                        Case Law
                    </div>
                </div>
                
                <div class="container">
                    <div class="margin">
                        <%
                        try
                        {
                            System.Data.DataTable dt = new System.Data.DataTable();
                            EastLawBL.Pages objpages = new EastLawBL.Pages();

                            dt = objpages.GetPages(10);
                            if (dt.Rows.Count > 0)
                            {

                                Response.Write("<p>" + dt.Rows[0]["FullContent"].ToString() + "</p>");
                            }
                        }
                        catch { } 
                    %>
                    </div>
                </div>
                <h1></h1>
                <h1></h1>
                <h1></h1>
                <h1></h1>
                <h1></h1>
                <h1></h1>
                <h1></h1>
                <h1></h1>
                <h1></h1>
                <h1></h1>
                <h1></h1>
            </h1>

        </div>
    </div>


    <div id="divWithLogin" runat="server">
        <div class="bannde-slide">
            <img src="/m/images/CurrentAwareness2.jpg" alt="Current Awareness" /></div>

        <div class="contentPage">
            
                <div class="container">
                    <h2 class="custom"> Advance Case Law Search</h2>
                    <div class="margin">
                       
                    </div>

                </div>
               
                <div class="resources grayBg">
                    <div class="container">
                        <div class="margin">
                            <div class="adSearch">
                                <div class="row">
                                    <div class="txt">
                                        Search Any word(s)</div>
                                    <div class="inputRow">
                                        <div class="lft">
                                            <asp:TextBox ID="txtFreeText" runat="server" placeholder="Enter Any word(s)" ValidationGroup="A" Width="300px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFreetxt" runat="server" ForeColor="Red" ErrorMessage="Please enter keyword" 
                                                ControlToValidate="txtFreeText" ValidationGroup="A" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="rgt">
                                            <asp:Button ID="btnSearchFreetext" runat="server" CssClass="input2" OnClick="btnSearchFreetext_Click" Text="Search" ValidationGroup="A" Visible="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="adSearch">
                                <div class="row">
                                    <div class="txt">
                                        Search Exact Phrase</div>
                                    <div class="inputRow">
                                        <div class="lft">
                                            <asp:TextBox ID="txtSearchexactphrase" runat="server" placeholder="Enter Search exact phrase" ValidationGroup="B" Width="300px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="Please enter keyword" 
                                                ControlToValidate="txtSearchexactphrase" ValidationGroup="B" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="rgt">
                                            <asp:Button ID="btnSearchexactphrase" runat="server" CssClass="input2"  Text="Search" ValidationGroup="B" Visible="true" OnClick="btnSearchexactphrase_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="adSearch">
                                <div class="row">
                                    <div class="txt">
                                        Multiple Phrases</div>
                                    <div class="inputRow">
                                        <div class="lft">
                                            <div class="one">
                                                <asp:TextBox ID="txtExactPhraseMore" runat="server" placeholder="Add First phrase" Width="175px" ValidationGroup="ExactPhraseMore"> </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ErrorMessage="Please enter keyword" 
                            ControlToValidate="txtExactPhraseMore" ValidationGroup="ExactPhraseMore" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="two">
                                                AND </div>
                                            <div class="three">
                                                <asp:TextBox ID="txtExactPhraseMore2" runat="server" placeholder="Add Second phrase" Width="175px" ValidationGroup="ExactPhraseMore" ></asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red" ErrorMessage="Please enter keyword" 
                            ControlToValidate="txtExactPhraseMore2" ValidationGroup="ExactPhraseMore" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                         <div class="rgt">
                                            <asp:Button ID="btnExactPhraseMore" runat="server" CssClass="input2" Text="Search" ValidationGroup="ExactPhraseMore" Visible="true" OnClick="btnExactPhraseMore_Click" />
                                        </div>
                                    </div>
                                    
                                </div>
                            </div>
                               
                            </div>
                            <div class="adSearch">
                                <div class="row">
                                    <div class="txt">
                                        Subject/Keyword</div>
                                    <div class="inputRow">
                                        <div class="lft">
                                          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radtxtKeywords"
                                                 ErrorMessage="Enter Search Text" ForeColor="Red" ValidationGroup="Subject"></asp:RequiredFieldValidator>--%>
                                    <%--        <asp:TextBox ID="txtKeyword" runat="server" placeholder="" TextMode="MultiLine" ValidationGroup="B" Width="400px"></asp:TextBox>
                                            <asp:HiddenField ID="hfCustomerId" runat="server" />
                                            <span>Use mouse for selection of multiple keywords</span>--%>
                                              <telerik:radautocompletebox RenderMode="Lightweight"  runat="server" ID="radtxtKeywords" CssClass="form-control text_field"  EmptyMessage="Select multiple keywords"  AllowCustomEntry="true"
                InputType="Token" Width="350" >
                            <TokensSettings AllowTokenEditing="true" />
            </telerik:radautocompletebox>
                                        </div>
                                         <div class="rgt">
                                            <asp:Button ID="btnMultipleKeywords" runat="server" CssClass="input2" Text="Search"  Visible="true" OnClick="btnMultipleKeywords_Click" />
                                             <asp:Label ID="lblCaseAdvanceMsg" runat="server" Text="" Visible="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        <%--    <div class="adSearch">
                                <div class="row">
                                    <div class="txtSmall">
                                        Case Name</div>
                                    <div class="inputRow">
                                        <div class="lft">
                                            <div class="one">
                                                <asp:TextBox ID="txtAppeallant" runat="server" placeholder="Appeallant" Width="175px"> </asp:TextBox>
                                            </div>
                                            <div class="two">
                                                V</div>
                                            <div class="three">
                                                <asp:TextBox ID="txtRespondent" runat="server" placeholder="Respondent" Width="175px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="adSearch">
                                <div class="row">
                                    <div class="txt">
                                        Court</div>
                                    <div class="inputRow">
                                     <%--   <asp:CheckBoxList ID="chkLstCourt" runat="server" class="checkBox">
                                        </asp:CheckBoxList>--%>
                                        <div class="lft">
                                        <asp:DropDownList ID="ddlPartyCourt" runat="server"  ></asp:DropDownList> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPartyCourt" ErrorMessage="Select Court" 
                                            ForeColor="Red" ValidationGroup="Party" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                    </div>
                                </div>
                            </div>
                           <div class="adSearch">
                                <div class="row">
                                    <div class="txt">
                                       Party Name</div>
                                    <div class="inputRow">
                                        <div class="lft">
                                            <asp:TextBox ID="txtPartyNames" runat="server" placeholder="Enter Any word(s)" ValidationGroup="A" Width="300px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red" ErrorMessage="Please enter party name" 
                                                ControlToValidate="txtFreeText" ValidationGroup="Party" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                      
                                    </div>
                                </div>
                            </div>
                            <div class="adSearch">
                                <div class="row">
                                    <div class="inputRow">
                                        <div class="rgt2">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="input2" OnClick="btnSearch_Click" Text="Search" ValidationGroup="Party" />
                                            &nbsp;
                                            <asp:Label ID="lblPartySearch" runat="server" Text="" Visible="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            


        </div>
    </div>
                
            </ContentTemplate>
        </asp:UpdatePanel>
        </form>
</asp:Content>

