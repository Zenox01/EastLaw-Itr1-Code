<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ebook.aspx.cs" Inherits="EastlawUI_v2.ebook" 
    MasterPageFile="~/MemberMaster.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
    <link href="/style/css/ebooks.css" rel="stylesheet" type="text/css" />
	<link href="/style/css/ebooks-tabs.css" rel="stylesheet" type="text/css" />
    <link href="/style/css/accordian.css" rel="stylesheet" type="text/css" />
    <style>
        .tab-content{background:none;Height:auto;}

    </style>
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
    
    	
   <div class="heading_style">
            
            	<h3>Seach Result of <i style="color:#c4161c;">"E-books"</i></h3>
            
            </div>
    
    <!-------------- Left Side --------------->
    <div class="col-lg-3 col-md-3">
    
    
    	
    
    	
  <div class="clearfix"></div>
  
  
  
  
  <div class="panel-group wrap" id="accordion" role="tablist" aria-multiselectable="true">
      <div class="panel">
        <div class="panel-heading" role="tab" id="headingOne">
          <h4 class="panel-title">
        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
          
          <p class="sr_head">Categories</p>
        </a>
      </h4>
        </div>
        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
          <div class="panel-body panel-body2">
          <div class="scrollbar" id="style-2" style="height:173px;">
      <div class="force-overflow">
      
           <asp:CheckBoxList ID="chkLstCategory" runat="server">
                                        </asp:CheckBoxList>
          </div></div>
          </div>
        </div>
      </div>
      <!-- end of panel -->


      </div>
      </div>

      
    <!-------------- Left Side End --------------->
    
    
    
    <!-------------- right Side --------------->
    <div class="col-lg-9 col-md-9 margin_bot_20">
    
    
    
    <div class="clearfix"></div>
    
    <div id="exTab1" class="hidden-xs" style="display:none">	
<ul  class="nav nav-pills nav2">
			<li class="active">
        <a  href="#1a" data-toggle="tab">Featured Title</a>
			</li>
			<li><a href="#2a" data-toggle="tab">Best Seller</a>
			</li>
			
		</ul>

			<div class="tab-content clearfix">
			  <div class="tab-pane active" id="1a">
          
				  <div class="row">
        <div class="row ebooks-row">
            <div class="col-md-9">
                <h3>
                    Featured Titles</h3>
            </div>
            <div class="col-md-3">
                <!-- Controls -->
                <div class="controls pull-right hidden-xs">
                    <a class="left fa fa-chevron-left btn btn-success" href="#carousel-example"
                        data-slide="prev"></a><a class="right fa fa-chevron-right btn btn-success" href="#carousel-example"
                            data-slide="next"></a>
                </div>
            </div>
        </div>
        <div id="carousel-example" class="carousel slide hidden-xs" data-ride="carousel">
            <!-- Wrapper for slides -->
            <div class="carousel-inner">
                <div class="item active">
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="/style/img/book-1.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="/style/img/book-2.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="/style/img/book-3.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="img/book-4.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="/style/img/book-1.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="/style/img/book-2.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="img/book-3.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="img/book-4.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
				  
				</div>
				<div class="tab-pane" id="2a">
          
					<div class="row">
        <div class="row ebooks-row">
            <div class="col-md-9">
                <h3>
                    Best Seller</h3>
            </div>
            <div class="col-md-3">
                <!-- Controls -->
                <div class="controls pull-right hidden-xs">
                    <a class="left fa fa-chevron-left btn btn-success" href="#carousel-example2"
                        data-slide="prev"></a><a class="right fa fa-chevron-right btn btn-success" href="#carousel-example2"
                            data-slide="next"></a>
                </div>
            </div>
        </div>
        <div id="carousel-example2" class="carousel slide hidden-xs" data-ride="carousel">
            <!-- Wrapper for slides -->
            <div class="carousel-inner">
                <div class="item active">
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="/style/img/book-1.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="/style/img/book-2.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="img/book-3.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="img/book-4.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="img/book-1.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="img/book-2.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="img/book-3.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-item">
                                <div class="photo">
                                    <img src="img/book-4.jpg" class="img-responsive" alt="a" />
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
					
				</div>
        
			</div>
  </div>

        
  	<!-----  Table ------>
		
		<div class="ebooksListing">
			<ul>

                 <asp:GridView ID="gvLst" runat="server" AllowCustomPaging="true" AllowPaging="true" AutoGenerateColumns="false"
                      CssClass="table table-filter" GridLines="None" OnPageIndexChanging="gvLst_PageIndexChanging" 
                     OnRowDataBound="gvLst_RowDataBound" PageSize="20" Width="100%">
                        
                        <pagersettings firstpagetext="First" lastpagetext="Last" mode="NumericFirstLast" nextpagetext="Next" position="TopAndBottom" previouspagetext="Prev" />
                        <pagerstyle cssclass="gridview" />
                        <Columns>
                              <asp:TemplateField>
                                <ItemTemplate>
                            	<li>
					
                                    <a href="<%# Eval("Link") %>">
					<div class="col-lg-2 col-md-2 col-sm-4 col-xs-12">
						
						<img src="/store/ebook/cover/<%# Eval("CoverPhoto") %>" class="img-responsive" alt="eBooks"/>
						
					</div>
					<div class="col-lg-10 col-md-10 col-sm-8 col-xs-12 description">
					
						<a href="<%# Eval("Link") %>"><h4><%# Eval("Title") %></h4></a>
						<p>
                        <%# Eval("ShortInfo") %></p>
						<%--<button class="btn btn-success">Download</button>--%>
					</div>
                                        </a>
					
				</li>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                            </Columns>
                     </asp:GridView>

			
			
			</ul>
		</div>
    
    
    </div>
    <!-------------- right Side End --------------->
    
    
     
    	
    </div>  
    </div>
    
    <!-------------- Content End --------------->
</asp:Content>


