<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PracticeAreaSearchGeneral.aspx.cs" Inherits="EastlawUI_v2.PracticeAreaSearchGeneral" 
    MasterPageFile="~/MemberMaster.Master"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
    <style>
        .btn_2 {
            height:28px !important;
            padding: 2px 12px;
            float:right;
        }
        .inner .form-control {
        width:40%;}
    </style>
    <div class="container">
<div class="row breadcrum">
    <ul class="bc">
                     <%if (Session["MemberID"] != null)
          { 
          %>
        <li><a href="/member/member-dashboard" class="first"> Home</a></li>
        <% } else { %>
        <li><a href="/"> Home</a></li>
        <%} %>
                    <li><a href="/practice-area"> Practice Area</a></li>
                    <li class="current"><asp:Label ID="lblCurCrumb" runat="server"></asp:Label></li>
                </ul>

  </div>
</div>
    <div class="container">
    <div class="row">
    <div class="heading_style">
            
            	<h3><asp:Label ID="lblPA" runat="server" Text="Banking & Finance"></asp:Label></h3>
            
            </div>
            
            <div class="clearfix"></div>
              <asp:UpdatePanel ID="upPnlTop" runat="server">
              <ContentTemplate>
   		<div class="col-lg-6 col-md-6">
        
        	<div class="box">
            <div>
                <asp:Label ID="lblPAID" runat="server" Text="" Visible="false"></asp:Label>
                        <%--<input class="form-control text_field3" placeholder="Search from Practice Area*" name="srch-term" id="srch-term" type="text">--%>
                <asp:TextBox ID="txtSearch" runat="server" class="form-control text_field3" placeholder="Search from Practice Area*"  ></asp:TextBox>

                        <div class="input-group-btn">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"  class="btn btn-default" style="height: 34px;background:#eee;"  />
                            <%--<button class="btn btn-default" style="height: 34px;background:#eee;" type="submit"><i class="fa fa-search"></i></button>--%>
                        </div>
                    </div>
        
        	</div>
            
            <div class="clearfix"></div>
            <div class="box">
            
        		
                <div class="heading_style">
            
            	<h3>Quick Search</h3>
            
            </div>
            
            <div class="row">
            
            	<div class="inner" style="width:100%;">
            	
                	<%--<input type="text" class="form-control" placeholder="Section :" style="margin-left:6px;">     --%>
                    <asp:TextBox ID="txtSection" runat="server" class="form-control" placeholder="Section :" style="margin-left:6px;" ToolTip="Section"></asp:TextBox>          
                    <asp:DropDownList ID="ddlTaggedStatutes" runat="server" class="form-control" style="margin:0 6px;"></asp:DropDownList>
                    <div class="input-group-btn">
                        <asp:Button ID="btnQuickFind" runat="server" Text="Search" class="btn btn-default btn_2" OnClick="btnQuickFind_Click"  />
                        </div>
                   
            	</div>
                
                <%--<button class="btn btn-default btn_2" type="submit"><i class="fa fa-search"></i></button>--%>
                
            
            
            </div>
        
        
        	</div>
            
            <div class="clearfix"></div>
            
            <div class="box" style="display:none">
            
        		
                <div class="heading_style">
            
            	<h3>Department Search</h3>
            
            </div>
            
            <div class="row">
            
            	<div class="inner" style="width:92%;">
            	
                <asp:DropDownList ID="ddlDeptTypeGroups" runat="server" class="form-control" style="margin-left:6px;"></asp:DropDownList> 	               
                    <asp:TextBox ID="txtTypesNo" runat="server" ToolTip="No" class="form-control" placeholder="Enter No :" style="margin:0 6px;"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:Button ID="btnTypeSearch" runat="server" Text="Search" class="btn btn-default btn_2" OnClick="btnTypeSearch_Click"    />
                        </div>
                   
                
            	</div>
                
                
                
            
            
            </div>
        
        
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
        <div class="col-lg-2 col-md-2"></div>
        
        
        <div class="col-lg-4 col-md-4">
        
        
        <div id="myCarousel" class="carousel slide">        
                <div class="carousel-inner">  
                    
                    <%
      try
      {
          System.Data.DataTable dt = new System.Data.DataTable();
          EastLawBL.News objn = new EastLawBL.News();
          dt = objn.GetActiveNewsByPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()));
          for (int a = 0; a < dt.Rows.Count; a++)
          {
              if (!string.IsNullOrEmpty(dt.Rows[a]["imgfile"].ToString()))
              {
                  string imgfilename = "";
                  if (dt.Rows[a]["ImageType"].ToString() == "Local")
                      imgfilename = "/store/news/imgs/" + dt.Rows[a]["imgfile"].ToString();
                  else if (dt.Rows[a]["ImageType"].ToString() == "URL")
                      imgfilename="";
                      //imgfilename = dt.Rows[a]["imgfile"].ToString();
                  else
                      imgfilename = dt.Rows[a]["imgfile"].ToString();
                  
                  if (a == 0)
                  {
                      Response.Write("<div class='item active'> "
                  + "<a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'><img class='thumbnail' src='" + imgfilename + "' alt='" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString()) + "'></a>"
                  + "<div class='caption'>"
                    + "<a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'><h4>" + dt.Rows[a]["Title"].ToString() + " </h4></a> <br>Date: " + dt.Rows[a]["NDate"].ToString() + "<br><i>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Source"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["PracticeAreaSubCatName"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["StatutesCategories"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Author"].ToString()) + "</i>"
                     // + "<p>" + EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["FullContent"].ToString(),10) + " ... </p>"
                  + "</div>"

                  + "<div class='channel_logo'>"

                      + "<img src='img/channel1.png' /></div></div>");
                  }
                  else
                  {
                      Response.Write("<div class='item'> "
                  + "<a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'><img class='thumbnail' src='" + imgfilename + "' alt='" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString()) + "'></a>"
                  + "<div class='caption'>"
                    + "<a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'><h4>" + dt.Rows[a]["Title"].ToString() + " </h4></a> <br>Date: " + dt.Rows[a]["NDate"].ToString() + "<br><i>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Source"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["PracticeAreaSubCatName"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["StatutesCategories"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Author"].ToString()) + "</i>"
                     // + "<p>" + EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["FullContent"].ToString(), 10) + " ... </p>"
                  + "</div>"

                  + "<div class='channel_logo'>"

                      + "<img src='img/channel1.png' /></div></div>"); 
                  }
            
              }
              else
              {
                  if (a == 0)
                  {
                      Response.Write("<div class='item active'> "
                            + "<a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'><img class='thumbnail' src='/images/no_image-128.png' alt='" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString()) + "'></a>"
                            + "<div class='caption'>"
                              + "<a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'><h4>" + dt.Rows[a]["Title"].ToString() + " </h4></a> <br>Date: " + dt.Rows[a]["NDate"].ToString() + "<br><i>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Source"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["PracticeAreaSubCatName"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["StatutesCategories"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Author"].ToString()) + "</i>"
                             //   + "<p>" + EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["FullContent"].ToString(), 10) + " ... </p>"
                            + "</div>"

                            + "<div class='channel_logo'>"

                                + "<img src='img/channel1.png' /></div></div>");
                  }
                  else
                  {
                      Response.Write("<div class='item'> "
                  + "<a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'><img class='thumbnail' src='" + dt.Rows[a]["imgfile"].ToString() + "' alt='" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString()) + "'></a>"
                  + "<div class='caption'>"
                    + "<a href='" + dt.Rows[a]["SourceLink"].ToString() + "' target='_blank'><h4>" + dt.Rows[a]["Title"].ToString() + " </h4></a> <br>Date: " + dt.Rows[a]["NDate"].ToString() + "<br><i>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Source"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["PracticeAreaSubCatName"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["StatutesCategories"].ToString()) + "," + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Author"].ToString()) + "</i>"
//                      + "<p>" + EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["FullContent"].ToString(), 10) + " ... </p>"
                  + "</div>"

                  + "<div class='channel_logo'>"

                      + "<img src='img/channel1.png' /></div></div>");
                  }
            
                  
                  
              }
              if (a == 5)
                  break;
          }
      }
      catch { }
       %>          
                                                                                                 
                </div>
                 <!-- Indicators -->
                  <ol class="carousel-indicators">
                         
                    <%
                        try
                        {
                            System.Data.DataTable dtCount = new System.Data.DataTable();
                            EastLawBL.News objn1 = new EastLawBL.News();
                            dtCount = objn1.GetActiveNewsByPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()));
                            for (int a = 0; a < dtCount.Rows.Count; a++)
                            {
                                if (a == 0)
                                {
                                    Response.Write("<li data-target='#myCarousel' data-slide-to='"+a.ToString()+"' class='active'></li>");
                                }
                                else
                                {
                                    Response.Write("<li data-target='#myCarousel' data-slide-to='"+a.ToString()+"' class='active'></li>");
                                }
                                if (a == 5)
                                    break;
                            }

                        }

                        catch { }
       %>          
               <%--     <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                    <li data-target="#myCarousel" data-slide-to="1"></li>
                    <li data-target="#myCarousel" data-slide-to="2"></li>--%>
                  </ol>                                                                 
            </div>
        
        
        </div>
    	
    </div>  
    </div>
    <div class="clearfix"></div>
    <br />
    <br />
    <div class="container">
        
        	<div class="row">
        
        
                <%
        
            if (Request.QueryString["param"] != null)
            {
                 System.Data.DataTable dtRelDept = new System.Data.DataTable();
                 string cri = "";
            EastLawBL.Departments objdept = new EastLawBL.Departments();
            int PracticeAreaID = int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString());
            if (PracticeAreaID == 23)
            {
                cri = "Where SearchCol in (83)";
                dtRelDept = objdept.GetDepartmentFilesByDepartmentsParentForPracticeAreaDynamic(cri);
            }
            else if (PracticeAreaID == 24)
            {
                cri = "Where SearchCol in (39)";
                dtRelDept = objdept.GetDepartmentFilesByDepartmentsParentForPracticeAreaDynamic(cri);
            }
            else if (PracticeAreaID == 36)
            {
                cri = "Where SearchCol in (23,69,16,19,33,28)";
                dtRelDept = objdept.GetDepartmentFilesByDepartmentsParentForPracticeAreaDynamic(cri);
            }
            else if (PracticeAreaID == 38)
            {
                cri = "Where SearchCol in (46)";
                dtRelDept = objdept.GetDepartmentFilesByDepartmentsParentForPracticeAreaDynamic(cri);
            }     
            if (dtRelDept.Rows.Count > 0)
            {
    
                %>
        		<div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Department Updates</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1">
    <%
        
            //if (Request.QueryString["param"] != null)
            //{
            //     System.Data.DataTable dtRelDept = new System.Data.DataTable();
            //     string cri = "";
            //EastLawBL.Departments objdept = new EastLawBL.Departments();
            //int PracticeAreaID = int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString());
            //if (PracticeAreaID == 23)
            //{
            //    cri = "Where SearchCol in (83)";
            //    dtRelDept = objdept.GetDepartmentFilesByDepartmentsParentForPracticeAreaDynamic(cri);
            //}
            //else if (PracticeAreaID == 24)
            //{
            //    cri = "Where SearchCol in (39)";
            //    dtRelDept = objdept.GetDepartmentFilesByDepartmentsParentForPracticeAreaDynamic(cri);
            //}
            //else if (PracticeAreaID == 36)
            //{
            //    cri = "Where SearchCol in (23,69,16,19,33,28)";
            //    dtRelDept = objdept.GetDepartmentFilesByDepartmentsParentForPracticeAreaDynamic(cri);
            //}
            //else if (PracticeAreaID == 38)
            //{
            //    cri = "Where SearchCol in (46)";
            //    dtRelDept = objdept.GetDepartmentFilesByDepartmentsParentForPracticeAreaDynamic(cri);
            //}     
            //if (dtRelDept.Rows.Count > 0)
            //{
                for (int a = 0; a < dtRelDept.Rows.Count; a++)
                {
                    dtRelDept.Rows[a]["DType"] = EastlawUI_v2.CommonClass.MakeFirstCap(dtRelDept.Rows[a]["DType"].ToString());
                    dtRelDept.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dtRelDept.Rows[a]["Title"].ToString());
                    if (a == 10)
                        break;
                }
                dtRelDept.AcceptChanges();
                for (int b = 0; b < dtRelDept.Rows.Count; b++)
                {

                    Response.Write("<li class='news-item'><table cellpadding='4' width=310px>"
                    + "<tr><td><b>" + EastlawUI_v2.CommonClass.GetWords(dtRelDept.Rows[b]["Title"].ToString(), 5) + " ... - " + dtRelDept.Rows[b]["ParentDept"].ToString() + " - " + dtRelDept.Rows[b]["DeptName"].ToString() + "</b><br /><b>Date:</b> " + dtRelDept.Rows[b]["DDate"].ToString() + "<br />"
                    + "<b>Department:</b> " + dtRelDept.Rows[b]["ParentDeptName"].ToString() + "<br />"
                    + "<br><span style='float:right'><a href='/departments/" + clsUtilities.RemoveSpecialCharacter(dtRelDept.Rows[b]["DeptName"].ToString()).ToLower() + "/" + clsUtilities.RemoveSpecialCharacter(dtRelDept.Rows[b]["WordFile"].ToString()).ToLower() + "." + EncryptDecryptHelper.Encrypt(dtRelDept.Rows[b]["ID"].ToString()) + "'>Read more...</a></span></td></tr></table></li>");
                    if (b == 10)
                        break;
                }
                 
            //}
            //} 
            %>

</ul>
</div>
</div>
</div>
<div class="panel-footer">
    <%
                Response.Write("<a href='/departments/practice-area-departments-updates/?param=" + Request.QueryString["param"].ToString() + "&trans=" + Request.QueryString["trans"].ToString() + "'>View More</a>");
         %>
    

</div>
</div>
        
        </div>
        <%} }%>
        
        
        
        
        <div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Legislation</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
        <asp:Timer ID="TimerLegislation" runat="server" OnTick="TimerTickLegislation" Interval="1"></asp:Timer>
            <asp:Image ID="imgLoaderLegislation" runat="server" ImageUrl="/page-loader.gif" />

                   <telerik:RadRotator RenderMode="Lightweight" ID="RadRotatorLegislation" runat="server" Width="330" Height="300" CssClass="horizontalRotator"  ScrollDirection="Up"
                ScrollDuration="500" FrameDuration="2000" ItemHeight="100" ItemWidth="300" RotatorType="AutomaticAdvance">
                <ItemTemplate>
                  
                 <p>
                   <b>  <a href='<%# Eval("Link") %>'><%# Eval("Title") %></a></b><br />
                                                                          
                 </p>
                    <hr></hr>
                </ItemTemplate>
            </telerik:RadRotator>
             </ContentTemplate>
</asp:UpdatePanel>

    <%
        //<ul class="demo1">
        //if (Request.QueryString["param"] != null)
        //{
        //    System.Data.DataTable dtStat = new System.Data.DataTable();
        //    EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
        //    dtStat = objPA.GetTaggesStatuesWithPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()));
        //    {
        //        if (dtStat.Rows.Count > 0)
        //        {

        //            for (int a = 0; a < dtStat.Rows.Count; a++)
        //            {
        //                Response.Write("<li class='news-item'><table cellpadding='4' width=310px>"
        //         + "<tr><td><b>" + EastlawUI_v2.CommonClass.MakeFirstCap(dtStat.Rows[a]["Title"].ToString()) + "<br />"
        //         + " <br><span style='float:right'><a href='/statutes/" + EastlawUI_v2.CommonClass.RemoveSomeCharacters(dtStat.Rows[a]["Title"].ToString()) + "." + EncryptDecryptHelper.Encrypt(dtStat.Rows[a]["ID"].ToString()) + "'>Read more...</a></span></td></tr></table></li>");

        //            }
        //        }

        //    }
        //} 
        //</ul>
            %>


</div>
</div>
</div>
<div class="panel-footer"> 
      <%
          Response.Write("<a href='/statutes/practice-area-legislations/?param=" + Request.QueryString["param"].ToString() + "&trans=" + Request.QueryString["trans"].ToString() + "'>View More</a>");
         %>

</div>
</div>
        
        </div>
        
        
        
        
        
        
        <div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Latest Judgements</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
    <asp:UpdatePanel ID="upPnlLatestCase" runat="server">
    <ContentTemplate>
        
        <asp:Timer ID="Timer1" runat="server" OnTick="TimerTick" Interval="1"></asp:Timer>
            <asp:Image ID="imgLoader" runat="server" ImageUrl="/page-loader.gif" />

                   <telerik:RadRotator RenderMode="Lightweight" ID="RadRotatorCases" runat="server" Width="330" Height="300" CssClass="horizontalRotator"  ScrollDirection="Up"
                ScrollDuration="500" FrameDuration="2000" ItemHeight="130" ItemWidth="300" RotatorType="AutomaticAdvance">
                <ItemTemplate>
                  
                 <p>
                   <b>  <a href='<%# Eval("Link") %>'><%# Eval("Title") %></a></b><br />
                                                                    <b>Date:</b> <%# Eval("JDate") %><br />
                                                                    <b>Court:</b> <%# Eval("Court") %><br />
                                                                          
                 </p>
                    <hr></hr>
                </ItemTemplate>
            </telerik:RadRotator>
             </ContentTemplate>
</asp:UpdatePanel>

    <%
        //<ul class="demo1">
        //if (Request.QueryString["param"] != null)
        //{
        //    try
        //    {
        //        System.Data.DataTable dtCases = new System.Data.DataTable();
        //        EastLawBL.PracticeAreas objPA1 = new EastLawBL.PracticeAreas();

        //        dtCases = objPA1.GetTaggesCasesWithPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()));
        //        {
        //            if (dtCases.Rows.Count > 0)
        //            {
        //                dtCases.Columns.Add("Title");
        //                dtCases.Columns.Add("Link");
        //                for (int a = 0; a < dtCases.Rows.Count; a++)
        //                {
        //                    dtCases.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtCases.Rows[a]["Appeallant"].ToString(), 3)) + "... VS " + EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtCases.Rows[a]["Respondent"].ToString(), 3));
        //                    dtCases.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtCases.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtCases.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtCases.Rows[a]["ID"].ToString());
        //                }
        //                dtCases.AcceptChanges();
        //                if (dtCases.Rows.Count > 0)
        //                {


        //                    for (int a = 0; a < dtCases.Rows.Count; a++)
        //                    {
        //                        Response.Write("<li class='news-item'><table cellpadding='4' width=310px>"
        //                 + "<tr><td><b>" + dtCases.Rows[a]["Title"].ToString() + " <br></b><b>Court: </b>" + dtCases.Rows[a]["Court"].ToString() + " <br><b>Date: </b> " + dtCases.Rows[a]["JDate"].ToString() + "<br />"
        //                 + "<br><span style='float:right'><a href='" + dtCases.Rows[a]["Link"].ToString() + "'>Read more...</a></a></td></tr></table></li>");

        //                    }

        //                }
        //            }

        //        }
        //    }
        //    catch { }
        //} 
        //</ul>
            %>


</div>
</div>
</div>
<div class="panel-footer"> 
      <%
          Response.Write("<a href='/cases/practice-area-latest-judgments/?param=" + Request.QueryString["param"].ToString() + "&trans=" + Request.QueryString["trans"].ToString() + "'>View More</a>");
         %>
</div>
</div>
        
        </div>
        		
        	
            </div>
        
        </div>

</asp:Content>

