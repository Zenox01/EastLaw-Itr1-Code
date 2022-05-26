<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentsList.aspx.cs" Inherits="EastlawUI_v2.m.DepartmentsList" 
    MasterPageFile="~/m/MemberMaster.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <form runat="server">
    <div class="contentPage">
	<h1>
    <div class="container">
        <div class="margin">
           Departments
        </div>
        
    </div>
    </h1>

	
   <div class="resources grayBg">
       <div class="container">
            <div class="margin">
           <div class="rgt" id="spanMyFolder" runat="server">
                    	<h6>Add to folder</h6>
                        <span>
                        	<asp:DropDownList ID="ddlFolders" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFolders_SelectedIndexChanged"></asp:DropDownList> 
                        </span>

                    </div>
                
            
                <div class="dictionary">
                  <asp:GridView ID="gvLst" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" AllowPaging="true" PageSize="20" 
                    OnPageIndexChanging="gvLst_PageIndexChanging"  RowStyle-CssClass="rows" >
                     
                    <pagersettings mode="NumericFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"  
            position="TopAndBottom" />
                    <pagerstyle cssclass="pegi" >

</pagerstyle>
                     <Columns>
                         <%-- <asp:TemplateField ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                         <%-- <asp:TemplateField HeaderText="Type" ControlStyle-Width="100">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("DType") %>'></asp:Label>    
                                                
                                
                                               
                            </itemtemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Title">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />

                        
                                                <h6><a href='<%# "/m/Departments/"+  clsUtilities.RemoveSpecialCharacter(Eval("DeptName").ToString())+"/"+ clsUtilities.RemoveSpecialCharacter(Eval("Title").ToString())+"."+EncryptDecryptHelper.Encrypt(Eval("ID").ToString()) %>' ><%# Eval("Title")%></a></h6>
                                                <br />
                                                <br />
                                               <p> <%# Eval("ShortDesc") %></p>
                                
                                               
                            </itemtemplate>
                                        </asp:TemplateField>
                       
                        <%--  <asp:TemplateField HeaderText="Date">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DDate") %>'></asp:Label>                
                                
                                               
                            </itemtemplate>
                                        </asp:TemplateField>--%>
                                        
                                       
                                            </Columns>
                </asp:GridView>

                </div>
                
               
             
               
            </div>
            
        </div>
        <div class="clear"></div>
   </div> 	


    
</div>
        </form>
</asp:Content>


