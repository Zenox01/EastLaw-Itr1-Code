<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ebookdetails.aspx.cs" Inherits="EastlawUI_v2.ebookdetails"
    MasterPageFile="~/MemberMaster.Master" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
         <link href="/style/css/ebooks.css" rel="stylesheet" type="text/css" />
	<link href="/style/css/ebooks-tabs.css" rel="stylesheet" type="text/css" />
    <link href="/style/css/accordian.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>

    <!----------- Main Nav End ------------->
    <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="" class="first">Home</a></li>
 
    <li><a href="#" class="current">E-books</a></li>
   
</ul>
  </div>
</div>
    
    <!-------------- Content --------------->
    
    <div class="container">
    <div class="row">
    
    <!------------ Search Tabs Section ------------->
    
    	
 
    
    <!-------------- Left Side --------------->
    <div class="col-lg-3 col-md-3">
    
    
    	
    
    	
  <div class="clearfix"></div>
  
  
  
  
  <div class="panel-group wrap" id="accordion" role="tablist" aria-multiselectable="true">
      <div class="panel">
        <div class="panel-heading" role="tab" id="headingOne">
          <h4 class="panel-title">
        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
          
          <p class="sr_head">Book Index</p>
        </a>
              <h4></h4>
              <h4></h4>
              <h4></h4>
              <h4></h4>
      </h4>
        </div>
        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
          <div class="panel-body panel-body2">
          <div class="scrollbar" id="style-2" style="height:173px;">
           <div class="force-overflow">
        <telerik:RadTreeView RenderMode="Lightweight" runat="server" ID="tvDept" 
                                      OnNodeClick="tvDept_NodeClick" OnContextMenuItemClick="tvDept_ContextMenuItemClick" >
                                           <ContextMenus>
            <%--<telerik:RadTreeViewContextMenu runat="server" ID="Caction" ClickToOpen="True">
                <Items>
                    <telerik:RadMenuItem Text="Add New Folder" Value="NewFolder">
                    </telerik:RadMenuItem>
                     <%--<telerik:RadMenuItem Text="Edit / View" Value="Edit">
                    </telerik:RadMenuItem>
                   <telerik:RadMenuItem Text="Delete Folder" Value="Delete">
                    </telerik:RadMenuItem>
               
                </Items>
            </telerik:RadTreeViewContextMenu>--%>
        </ContextMenus>
                                     
        </telerik:RadTreeView>
         
          </div>
          </div>
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
			
			<%--<div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ebooksDetail">
			
				<img src="https://eastlaw.pk/store/ebook/cover/Tax-2.JPG">
			
			</div>--%>
			
			<div class="col-lg-9 col-md-9 col-sm-12 col-xs-12 ebooksStyle">
			
				<h5><asp:Label ID="lblBookTitle" runat="server"></asp:Label></h5>
                <div class="pull-left topper">
                    <%--<input class="form-control text_field4" placeholder="Search Within..." name="srch-term" id="srch-term" type="text">--%>
                    <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="false" class="form-control text_field4"  placeholder="Search Tex..."></asp:TextBox>
                    <div class="input-group-btn">
                        <%--    <button class="btn btn-default btn_height" type="submit"><i class="fa fa-search"></i></button>--%>
                        <asp:Button ID="btnSearch" runat="server" class="btn btn-default btn_height"  Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>

				
				<%--<ul>
					<li><span>Author</span>: MUHAMMAD ARSHAD</li>
					<li><span>Downloads</span> : 786</li>
					<li><span>Pages</span> : 1476</li>
				</ul>
			
				<button class="btn btn-success">Download</button>--%>
				
			</div>
		
		</div>
		
		<div class="row" style="margin-top: 10px;">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPnl">
                <ProgressTemplate>
                    <div class="modal1">
                        <div class="center1">
                            <img alt="" src="/style/img/ajax_loader_big_search.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div id="divSearchResults" runat="server" >
                
                
                
             <asp:GridView ID="gvLst" runat="server" AllowCustomPaging="true" AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-filter" GridLines="None" 
                 onrowediting="gv_RowEditing" PageSize="20" Width="100%">
                        <%--<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />--%>
                        <pagersettings firstpagetext="First" lastpagetext="Last" mode="NumericFirstLast" nextpagetext="Next" position="TopAndBottom" previouspagetext="Prev" />
                        <pagerstyle cssclass="gridview" />
                        <Columns>
                  
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="media">
                                        <div class="media-body">
                                            <%--<a href='<%# Eval("Link") %>'>--%>

                                            <h4 class="title"><%# Eval("ParentIndex") %> - <%# Eval("IndexTitle") %></h4>
                                            <%--</a>--%>
                                           
                                            <%# Eval("Desc") %>
                                    </div>
                                        <span class="media-meta pull-right"><asp:LinkButton id="lnkView" runat="server" CommandName="Edit" Text="View Full"></asp:LinkButton>
                                            </span>
                                    <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            <div id="divView" runat="server">
                <asp:LinkButton ID="lnkBack" runat="server" OnClick="lnkBack_Click" visible="false"><< Back to results</asp:LinkButton>
            
            <div id="divContent" runat="server" style="border: 0px solid gray;border-radius: 5px;border-left:1px solid gray;padding-left:5px"></div>
		</div>
		<%--<iframe src='//eastlaw.pk/ebookflow/aspnet/simple_document.aspx?doc=KNOW%20YOUR%20TAX%20SERIES.pdf ' height='1100px' width='100%'></iframe>");--%>
		</div>
		
		
    
    
    </div>
    <!-------------- right Side End --------------->
    
    
     
    	
    </div>  
    </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>