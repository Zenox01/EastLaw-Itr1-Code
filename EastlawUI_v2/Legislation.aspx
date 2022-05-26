<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Legislation.aspx.cs" Inherits="EastlawUI_v2.Legislation" 
    MasterPageFile="~/MemberMaster.Master"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
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
     </style>
    </head>
          <asp:UpdatePanel ID="upPnlTop" runat="server">
              <ContentTemplate>

    <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    <li><a href="#" class="current">Legislation</a></li>
   
</ul>
  </div>
</div>

    <div class="container">
    <div class="row">
    
    <!------------ Search Tabs Section ------------->
    
    	
   
    
    <!-------------- Left Side --------------->
    

      
    <!-------------- Left Side End --------------->
    
    
    
    <!-------------- right Side --------------->
    <div class="col-lg-12 col-md-12 margin_bot_20">
    
    <div class="row">
    <div class="style0">
    	<div class="pull-left topper" style="width:100%;">
                        <div class="box" style="width:100%;">
            
            	<div class="inner" style="background:#eee;">
            	
                	
                    
                    <%--<input type="text" class="form-control" style="margin-left: 6px;" placeholder="Title">--%>
                    
                         <%-- <telerik:radautocompletebox RenderMode="Lightweight"  runat="server" ID="radAutoCompleteTitle"  EmptyMessage="Please Select Court"  AllowCustomEntry="true"
                InputType="Text" Width="350"  >
                        <TokensSettings AllowTokenEditing="true" />
            </telerik:radautocompletebox>--%>
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
            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Required" ForeColor="Red" Enabled="false" ValidationGroup="Title" Display="Dynamic"></asp:RequiredFieldValidator><br />
                        
                    </div>
                    
                 
    </div>
    </div>
    
    <div class="clearfix"></div>
    
    
		<div class="panal">
        
        	<div class="row head">
        	<h2>Browse Legislation</h2>
            <i class="fa fa-arrow-down pull-right" aria-hidden="true"></i>
        	</div>
        	
            <div class="row panal_inn">
            
            	<ul>
                	<li><a href="/statutes/federal-legislation">Federal Legislation</a></li>
                    <li><a href="/statutes/provincial-legislation">Provincial Legislation</a></li>
                    <li><a href="/statutes/federal-amendment-acts">Federal Amendment Acts</a></li>
                    <li><a href="/statutes/provincial-amendment-acts">Provincial Amendment Acts</a></li>
                    <li><a href="/statutes/federal-rules-and-regulations">Federal Rules and Regulations</a></li>
                    <li><a href="/statutes/provincial-rules-and-regulations">Provincial Rules and Regulations</a></li>
                    <li><a href="/statutes/bill-by-national-assembly">Bill By National Assembly</a></li>
                    <li><a href="/statutes/bill-by-provincial-assembly">Bill By Provincial Assembly</a></li>
                    <li style="display:none"><a href="/statutes/circulars-s-r-o-schemes-etc">Circulars, S.R.O's Schemes, etc</a></li>

                	
                </ul>
            
            </div>
        
        </div>
          
    
    
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


