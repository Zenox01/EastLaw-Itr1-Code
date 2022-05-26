<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatutesListAutoFilter.aspx.cs" Inherits="EastlawUI_v2.StatutesListAutoFilter" 
    MasterPageFile="~/MemberMaster.Master"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
    <link href="/style/css/department.css" rel="stylesheet" type="text/css" />
    <head>
    
        
           <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"
type = "text/javascript"></script> 
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
type = "text/javascript"></script> 
<link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
rel = "Stylesheet" type="text/css" /> 
    <style>.rrClipRegion { border: 0px !important; } </style>  
               
             
        
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtTitle.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("/Service.asmx/GetStatutesTitle") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //response($.map(data.d, function (item)
                        //{
                        //    return {
                        //        label: item.split('-')[0],
                        //        val: item.split('-')[1]
                        //    }
                        //}))

                        response($.map(data.d, function (item) {
                            return {
                                //label: __highlight(item, request.term),
                                //label: item,// __highlight(item, request.term),
                                //value: item

                                //label: __highlight(item, request.term),
                                label: item,
                                value: item
                            };
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            minLength: 1
        }).data("autocomplete")._renderItem = function (ul, item) {
            // only change here was to replace .text() with .html()
            return $("<li></li>")
                  .data("item.autocomplete", item)
                  .append($("<a></a>").html(item.label))
                  .appendTo(ul);
        };

    });


    function __highlight(s, t) {
        var matcher = new RegExp("(" + $.ui.autocomplete.escapeRegex(t) + ")", "ig");
        return s.replace(matcher, " <strong style='color:#eb1b33'>$1</strong> ");
    }

</script>


 
     <style>
         .ui-autocomplete {
    max-height: 200px;
    overflow-y: auto;
    /* prevent horizontal scrollbar */
    overflow-x: hidden;
    border:1px solid #222;
    position:absolute;
    width:500px;
  }

          .alphabar {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 13px;
	
	
	background-repeat: repeat-x;
	float: left;
	width: 23px;
	margin-right: 1px;
	margin-left: 2px;
	padding-top: 3px;
	padding-right: 3px;
	padding-bottom: 3px;
	padding-left: 3px;
	border: thin solid #CCC;
}
         .alphabarLeg {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 13px;
	
	
	background-repeat: repeat-x;
	float: left;
	
	margin-right: 1px;
	margin-left: 2px;
	padding-top: 3px;
	padding-right: 3px;
	padding-bottom: 3px;
	padding-left: 3px;
	border: thin solid #CCC;
}
.alpha-outer {
	float: left;
	width: 100%;
	padding-top: 10px;
	padding-bottom: 5px;
}
     </style>
    </head>
 <%--   <asp:UpdatePanel ID="upPnlTop" runat="server">
              <ContentTemplate>--%>
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
                    <li class="current" id="lblCurCrumb" runat="server"></li>
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
        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="true" aria-controls="collapseOne">
          <i class="fa fa-gavel" aria-hidden="true" style="display:inline-block;"></i>
          <p class="sr_head">Categories</p>
        </a>
      </h4>
        </div>
        <div id="collapseTwo" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
          <div class="panel-body panel-body2">
          <div class="scrollbar" id="style-2" style="height:173px;">
      <div class="force-overflow">
        <asp:CheckBoxList ID="chkCat" runat="server" OnSelectedIndexChanged="chkCat_SelectedIndexChanged" AutoPostBack="true" ></asp:CheckBoxList>
           
          </div></div>
          </div>
        </div>
      </div>
      <div class="panel">
        <div class="panel-heading" role="tab" id="headingTwo">
          <h4 class="panel-title">
        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
          <i class="fa fa-gavel" aria-hidden="true" style="display:inline-block;"></i>
          <p class="sr_head">Filter</p>
        </a>
      </h4>
        </div>
        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo">
          <div class="panel-body panel-body2">
          <div class="scrollbar" id="style-2" style="height:173px;">
      <div class="force-overflow">
        <asp:CheckBoxList ID="chkFilter" runat="server" OnSelectedIndexChanged="chkFilter_SelectedIndexChanged" AutoPostBack="true" ></asp:CheckBoxList>
           
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
                                     <%-- <telerik:RadAutoCompleteBox RenderMode="Lightweight" runat="server" ID="radTitle" CssClass="form-control"   EmptyMessage="Please type here"
                 InputType="Token" HighlightFirstMatch="true" Width="200" DropDownWidth="300px">
            </telerik:RadAutoCompleteBox>--%>
                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" style="margin-left: 6px;" placeholder="Title" ValidationGroup="Title"></asp:TextBox>
                    	         <cc1:AutoCompleteExtender ServiceMethod="SearchStatuteTitle"
    MinimumPrefixLength="2"
    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
    TargetControlID="txtTitle"
    ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false">
</cc1:AutoCompleteExtender>
                    
                    <%--<input type="text" class="form-control" style="margin:0 9px;" placeholder="Year:">--%>
                     <asp:TextBox ID="txtYear" runat="server" ToolTip="Year" class="form-control" style="margin:0 9px;" placeholder="Year:"></asp:TextBox>
                    
                
                        <asp:DropDownList ID="ddlStatutesCat" runat="server" class="form-control">
                                           
                                        </asp:DropDownList>
                    	
                    	
                  
            	</div>
                
                <%--<button class="btn btn-default btn_2" type="submit"><i class="fa fa-search"></i></button>--%>
                            <asp:Button ID="btnSearchTitle" runat="server" Text="Search" class="btn btn-default btn_2" OnClick="btnSearchTitle_Click"    ValidationGroup="Title"/>
                
            </div>
           <%-- <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Required" ForeColor="Red" Enabled="false" ValidationGroup="Title" Display="Dynamic"></asp:RequiredFieldValidator><br />
          --%>              
                    </div>

       
        
            <%--<div class="alpha-outer"  style="padding:0;margin-bottom:20px;">
            
            	<ul>
                      <%
                          Response.Write("<li ><a href='?alp=all' style='font-size:12px;margin:0 2px'>ALL</a></li>");
                for (int i = 0; i < 26; i++)
                {
                    Response.Write("<li><a href='?alp=" + Convert.ToChar(i + 65) + "' style='font-size:12px;margin:0 2px'>" + Convert.ToChar(i + 65) + "</a></li>");
                    
                    
                }
                
                 %>
                	
                </ul>
            
            </div> --%>       
                 
    </div>
          <%
             if (Session["MemberID"] != null)
             {
                 Response.Write("<a href='?alp=all'> <div class='alphabarLeg'>ALL</div></a>");
                 for (int i = 0; i < 26; i++)
                 {
                     Response.Write("<a href='?alp=" + Convert.ToChar(i + 65) + "'> <div class='alphabarLeg'>" + Convert.ToChar(i + 65) + "</div></a>");


                 }
             }
                
                 %>
        
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
                           <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <%# Eval("PracticeArea") %>
                                </ItemTemplate>
                                 </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title" ItemStyle-Width="500">
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

  
    
    
    </div>
    <!-------------- right Side End --------------->
    
    
     
    	
    </div>  
    </div>
                 <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlTop">
                    <ProgressTemplate>
                     
                           <div class="modal1">
        <div class="center1">
           <img alt="" src="/style/img/ajax_loader_big.gif" />
        </div>
    </div>
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                  </ContentTemplate>
        </asp:UpdatePanel>--%>
</asp:Content>


