<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatutesList.aspx.cs" Inherits="EastlawUI_v2.StatutesList"
    MasterPageFile="~/MemberMaster.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
    <asp:UpdatePanel ID="upPnlTop" runat="server">
              <ContentTemplate>
     <link href="/style/css/accordian.css" rel="stylesheet" type="text/css" />
    <div class="container">
<div class="row breadcrum">
     <ul class="bc">
                    <%if (Session["MemberID"] != null)
          { 
          %>
        <li  class="first"><a href="/member/member-dashboard"> Home</a></li>
        <% } else { %>
        <li><a href="/"> Home</a></li>
        <%} %>
        <li><a href="/statutes/legislations" class="first"> Legislation</a></li>
                    <li class="current"><asp:Label ID="lblCurCrumb" runat="server" Text="Legislation Search Result"></asp:Label></li>
                </ul>

<%--<ul class="bc">
    <li><a href="" class="first">Home</a></li>
 
    <li><a href="#" class="current">Legislation</a></li>
   
</ul>--%>
  </div>
</div>

    <div class="container">
    <div class="row">
    
    <!------------ Search Tabs Section ------------->
    
    	
   
    
    <!-------------- Left Side --------------->
    <div class="col-lg-3 col-md-3">
    
  
  <div class="panel-group wrap" id="accordion" role="tablist" aria-multiselectable="true">
      <div class="panel">
        <div class="panel-heading" role="tab" id="headingOne">
          <h4 class="panel-title">
        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
          <i class="fa fa-gavel" aria-hidden="true" style="display:inline-block;"></i>
          <p class="sr_head">Categories</p>
        </a>
      </h4>
        </div>
        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
          <div class="panel-body panel-body2">
          <div class="scrollbar" id="style-2" style="height:173px;">
      <div class="force-overflow">
        <asp:CheckBoxList ID="chkCat" runat="server" OnSelectedIndexChanged="chkCat_SelectedIndexChanged" AutoPostBack="true" ></asp:CheckBoxList>
           <%-- <ul>
                <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
                 <li>
                    <input type="checkbox" class="checkbox2" style="margin-right:5px;">
                    <p class="checbox-p">[ 230 1 T R 5981 ] (1)</p>
                </li>
          </ul>--%>
          </div></div>
          </div>
        </div>
      </div>
      <!-- end of panel -->

    
      <!-- end of panel -->

      </div>
      </div>

      
    <!-------------- Left Side End --------------->
    
    
    
    <!-------------- right Side --------------->
    <div class="col-lg-9 col-md-9 margin_bot_20">
    
    <div class="row">
    <div class="style0">
    	<div class="pull-left topper" style="width:100%;">
                        <div class="box" style="width:100%;">
            
            	<div class="inner" style="background:#eee;">
            	
                	
                    
                    
                               <%-- <telerik:radautocompletebox RenderMode="Lightweight"  runat="server" ID="radAutoCompleteTitle"  EmptyMessage="Please Select Court"  AllowCustomEntry="true"
                InputType="Text" Width="350"  >
                        <TokensSettings AllowTokenEditing="true" />
            </telerik:radautocompletebox>--%>
                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" style="margin-left: 6px;" placeholder="Title" ValidationGroup="Title"></asp:TextBox>
                    	
                    
                    <%--<input type="text" class="form-control" style="margin:0 9px;" placeholder="Year:">--%>
                     <asp:TextBox ID="txtYear" runat="server" ToolTip="Year" class="form-control" style="margin:0 9px;" placeholder="Year:"></asp:TextBox>
                    
                
                        <asp:DropDownList ID="ddlStatutesCat" runat="server" class="form-control">
                                           
                                        </asp:DropDownList>
                    	
                    	
                  
            	</div>
                
                <%--<button class="btn btn-default btn_2" type="submit"><i class="fa fa-search"></i></button>--%>
                            <asp:Button ID="btnSearchTitle" runat="server" Text="Search" class="btn btn-default btn_2" OnClick="btnSearchTitle_Click"    ValidationGroup="Title"/>
                
            </div>
            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Required" ForeColor="Red" Enabled="false" ValidationGroup="Title" Display="Dynamic"></asp:RequiredFieldValidator><br />
                        
                    </div>
                    
                 
    </div>
    </div>
    
    <div class="clearfix"></div>
    
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
                        <asp:TemplateField ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                          <asp:TemplateField HeaderText="Subject" ControlStyle-Width="200">
                            <ItemTemplate>
                                <%# Eval("PracticeArea") %>
                                </ItemTemplate>
                                 </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title" ControlStyle-Width="600">
                            <ItemTemplate>
                               <a href='<%# "/statutes/" +  clsUtilities.RemoveSpecialChars(Eval("Title").ToString()).Replace(" ","-")+"."+ EncryptDecryptHelper.Encrypt(Eval("ID").ToString()) %>'> <%# EastlawUI_v2.CommonClass.MakeFirstCap((string)Eval("Title")) %></a></strong>
                                <br />        
                                <span style="font-size:8pt">
 <%# Eval("ChildStatutestxt") %></span>
                           
                          <%--  <div class="rows_ct" style="padding-left:10px;padding-bottom:25px;">
                
                    <div class="rgt">
                      
                    <strong><a href='<%# "/Statutes/" +  clsUtilities.RemoveSpecialChars(Eval("Title").ToString()).Replace(" ","-")+"."+ EncryptDecryptHelper.Encrypt(Eval("ID").ToString()) %>'> <%# EastLawUI.CommonClass.MakeFirstCap((string)Eval("Title")) %></a></strong><br />
                 
                        <%# Eval("FormatedDateAct") %>
                        <br />
                        <span style="font-size:8pt">
 <%# Eval("ChildStatutestxt") %></span>
                    </div>
                           <span style="float:right"> 
                               <strong><a href='<%# "/Statutes/" +  clsUtilities.RemoveSpecialChars(Eval("Title").ToString()).Replace(" ","-")+"."+ EncryptDecryptHelper.Encrypt(Eval("ID").ToString()) %>'>View Full ...</a></strong><br /></span>
                            
                </div>
                                <hr />--%>
                                 <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <%# Eval("Date") %>
                                </ItemTemplate>
                                 </asp:TemplateField>
                         <asp:TemplateField HeaderText="Topic">
                            <ItemTemplate>
                                <%# Eval("CatName") %>
                                </ItemTemplate>
                                 </asp:TemplateField>
                    </Columns>
                </asp:GridView>

        <%--<table class="table table-hover table-striped">
        
        	<tr>
            	<th>Title</th>
                <th>Act Number</th>
                <th>Subject</th>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
            <tr>
            	<td><a href="#">ACADEMY OF SCIENTIFIC AND INNOVATIVE RESEARCH ACT, 2017</a></td>
                <td>13 of 2017</td>
                <td>Finance</td>
            </tr>
        
        </table>--%>
  
    
    
    </div>
    <!-------------- right Side End --------------->
    
    
     
    	
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


