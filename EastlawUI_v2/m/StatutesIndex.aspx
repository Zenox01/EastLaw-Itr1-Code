<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatutesIndex.aspx.cs" Inherits="EastlawUI_v2.m.StatutesIndex" 
    MasterPageFile="~/m/MemberMaster.Master"%>


<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div class="contentPage">
        <%try
          {
              if (Session["MemberID"] != null)
              {
                  
                   %>
	<h1>
    <div class="container">
        <div class="margin">
            <%
                        System.Data.DataTable dt = new System.Data.DataTable();
                        EastLawBL.Statutes objstate = new EastLawBL.Statutes();

                        //dt = objstate.GetStatutesIndexByStatutesID(int.Parse(HttpContext.Current.Items["statutesid"].ToString()));
                        dt = objstate.GetStatutes(int.Parse(HttpContext.Current.Items["statutesid"].ToString()));
                        if (dt != null)
                        {
                            //if (dt.Rows.Count > 0)
                            //{
                            //    if (dt.Rows[0]["StatutesContentType"].ToString() == "IndexContent")
                            //    {
                            //        if (dt.Rows.Count > 1)
                            //        {

                            //            Response.Write("<h3><a href='/m/Statutes/" + clsUtilities.RemoveSpecialChars(dt.Rows[0]["Title"].ToString()).Replace(" ", "-") + "/Full-View" + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["ID"].ToString()) + "?cont=Full-View'>CLICK HERE TO VIEW FULL STATUTE</a></h3>");
                            //        }
                            //    }
                            //}
                        }
                         %>
            
        </div>
    </div>
    </h1>

	<div class="container">
    	<div class="margin">
        	<p>
                  <ul>
                <asp:LinkButton ID="lnkPDF" runat="server" Text="Download PDF" OnClick="lnkPDF_Click" Visible="false"></asp:LinkButton><br /><br />
                <asp:LinkButton ID="lnkWord" runat="server" Text="Download Word" OnClick="lnkWord_Click" Visible="false"></asp:LinkButton>
                   
                <%
                    //if (string.IsNullOrEmpty(HttpContext.Current.Items["statutespdffilename"].ToString()))
                    //{
                    //    lnkPDF.Visible = false;
                    //}
                    //   // Response.Write("<li><a href='/store/statutesdocs/pdf/" + HttpContext.Current.Items["statutespdffilename"].ToString() + "' target='_blank'>PDF</a></li>");
                    //if (string.IsNullOrEmpty(HttpContext.Current.Items["statuteswordfilename"].ToString()))
                    //{
                    //    lnkWord.Visible = false;
                    //}
                    //    Response.Write("<li><a href='/store/statutesdocs/word/" + HttpContext.Current.Items["statuteswordfilename"].ToString() + "' target='_blank'>Word</a></li>"); 
                        %>
            	
               
            </ul>
            	<%
                 try
                     {
                         Response.Write("<center><strong> " + dt.Rows[0]["Title"].ToString() + "</strong></center><br>");
                         if (!string.IsNullOrEmpty(dt.Rows[0]["PDFFileName"].ToString()))
                         {
                             //Response.Write("<object data='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "' type='application/pdf' width='100%' height='700'><p>Alternative text - include a link <a href='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "'>to the PDF!</a></p></object>");
                             Response.Write("<iframe src='//eastlaw.pk/annotations_1.2.5/aspnet/simple_document.aspx?doc=" + dt.Rows[0]["PDFFileName"].ToString() + "' height='1100px' width='100%'></iframe>");
                         }
                         //if (dt.Rows.Count > 0)
                         //{
                         //    Response.Write("<span style='float:right'><a href='/m/Statutes/Search-Result'>Go to Search Page</a></span>");
                         //    Response.Write("<center><strong> " + dt.Rows[0]["Title"].ToString() + "</strong></center><br>");
                         //    if (dt.Rows[0]["StatutesContentType"].ToString() == "IndexContent")
                         //    {
                         //        if (dt.Rows.Count > 1)
                         //        {
                         //           // Response.Write("<h3><a href='/Statutes/" + clsUtilities.RemoveSpecialChars(dt.Rows[0]["Title"].ToString()).Replace(" ", "-") + "/Full-View" + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["ID"].ToString()) + "?cont=Full-View'>View Full</a></h3>");
                         //            Response.Write("<center><table style='width:90%' border='0'>");
                         //            for (int a = 0; a < dt.Rows.Count; a++)
                         //            {
                                         
                         //                if (a == 0)
                         //                {
                         //                    // Response.Write(dt.Rows[a]["IndexTitle"].ToString() + "<br>");
                         //                    //Response.Write("<tr><td style='text-align:center'>" + dt.Rows[a]["IndexTitle"].ToString() + "</td>");
                         //                    Response.Write("<td style='text-align:left'><a href='/m/Statutes/" + clsUtilities.RemoveSpecialChars(dt.Rows[a]["Title"].ToString()).Replace(" ", "-") + "/" + clsUtilities.RemoveSpecialChars(dt.Rows[a]["IndexTitle"].ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString()) + "'> " + dt.Rows[a]["IndexTitle"].ToString() + "</a></td></tr>");
                         //                }
                         //                else
                         //                {
                         //                   // Response.Write("<tr><td style='text-align:center'>" + a + "</td>");
                         //                    Response.Write("<td style='text-align:left'><a href='/m/Statutes/" + clsUtilities.RemoveSpecialChars(dt.Rows[a]["Title"].ToString()).Replace(" ", "-") + "/" + clsUtilities.RemoveSpecialChars(dt.Rows[a]["IndexTitle"].ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString()) + "'> " + dt.Rows[a]["IndexTitle"].ToString() + "</a></td></tr>");
                         //                }

                         //            }

                         //            Response.Write("</table></center>");
                         //        }
                         //        else
                         //        {
                         //            System.Data.DataTable dtIndexDetails = new System.Data.DataTable();
                         //            dtIndexDetails = objstate.GetStatutesIndex(int.Parse(dt.Rows[0]["ID"].ToString()));
                         //            if (dt.Rows.Count > 0)
                         //            {
                         //                //Response.Write("<span style='float:right'><a href='/Statutes/" + clsUtilities.RemoveSpecialCharacter(dt.Rows[0]["Title"].ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["StatutesID"].ToString()) + "'>Go to Index Page</a></span>");
                         //                //Response.Write("<br><strong>INDEX: " + dt.Rows[0]["IndexTitle"].ToString() + "</strong><br>");

                         //                Response.Write(FormatContent(dtIndexDetails.Rows[0]["IndexContent"].ToString()));
                         //                // Response.Write("</p>");

                         //            }
                                      
                         //        }
                         //    }
                         //    else if(dt.Rows.Count > 0)
                         //    {
                         //        if (dt.Rows.Count > 1)
                         //        {
                         //            Response.Write("<center><table style='width:90%' border='0'>");
                         //            for (int a = 0; a < dt.Rows.Count; a++)
                         //            {
                         //                if (a == 0)
                         //                {
                         //                    // Response.Write(dt.Rows[a]["IndexTitle"].ToString() + "<br>");
                         //                    //Response.Write("<tr><td style='text-align:center'>" + dt.Rows[a]["IndexTitle"].ToString() + "</td>");
                         //                        Response.Write("<td style='text-align:left'><a href='/m/Statutes/" + clsUtilities.RemoveSpecialChars(dt.Rows[a]["Title"].ToString()).Replace(" ", "-") + "/" + clsUtilities.RemoveSpecialChars(dt.Rows[a]["IndexTitle"].ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString()) + "'> " + dt.Rows[a]["IndexTitle"].ToString() + "</a></td></tr>");
                         //                }
                         //                else
                         //                {
                         //                   // Response.Write("<tr><td style='text-align:center'>" + a + "</td>");
                         //                    Response.Write("<td style='text-align:left'><a href='/m/Statutes/" + clsUtilities.RemoveSpecialChars(dt.Rows[a]["Title"].ToString()).Replace(" ", "-") + "/" + clsUtilities.RemoveSpecialChars(dt.Rows[a]["IndexTitle"].ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString()) + "'> " + dt.Rows[a]["IndexTitle"].ToString() + "</a></td></tr>");
                         //                }

                         //            }

                         //            Response.Write("</table></center>");
                         //        }
                         //        else
                         //        {
                         //            System.Data.DataTable dtIndexDetails = new System.Data.DataTable();
                         //            dtIndexDetails = objstate.GetStatutesIndex(int.Parse(dt.Rows[0]["ID"].ToString()));
                         //            if (dt.Rows.Count > 0)
                         //            {
                         //                //Response.Write("<span style='float:right'><a href='/Statutes/" + clsUtilities.RemoveSpecialCharacter(dt.Rows[0]["Title"].ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["StatutesID"].ToString()) + "'>Go to Index Page</a></span>");
                         //                //Response.Write("<br><strong>INDEX: " + dt.Rows[0]["IndexTitle"].ToString() + "</strong><br>");

                         //                Response.Write(FormatContent(dtIndexDetails.Rows[0]["IndexContent"].ToString()));
                         //                // Response.Write("</p>");

                         //            }
                                      
                         //        }
                         //    }
                             
                         //    else
                         //    {
                         //        //Response.Write("<embed width='100%' height='700' src='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "#toolbar=1&navpanes=1&scrollbar=1'></embed>");
                         //       // Response.Write("<object data='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "' type='application/pdf' width='100%' height='100%'><p>Alternative text - include a link <a href='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + ">to the PDF!</a></p></object>");
                                 
                         //        if (!string.IsNullOrEmpty(dt.Rows[0]["PDFFileName"].ToString()))
                         //        {
                         //            //  Response.Write("<embed width='100%' height='700' src='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "#toolbar=1&navpanes=1&scrollbar=1'></embed>");
                         //            Response.Write("<object data='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "' type='application/pdf' width='100%' height='700'><p>Alternative text - include a link <a href='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "'>to the PDF!</a></p></object>");
                         //        }
                         //        else if (!string.IsNullOrEmpty(dt.Rows[0]["WordFileName"].ToString()))
                         //        {
                         //            //Response.Write("<object data='/store/statutesdocs/word/" + dt.Rows[0]["WordFileName"].ToString() + "' type='application/pdf' width='100%' height='700'><p>Alternative text - include a link <a href='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "'>to the PDF!</a></p></object>");
                         //            Response.Write("<object data='/store/statutesdocs/word/" + dt.Rows[0]["WordFileName"].ToString() + "'>You do not have Word installed on your machine</object>");
                         //        }
                         //    }
                         //   // Response.Write("</center>");
                         //}
                        // else if(dt.Rows.Count> 0 )
                         //else
                         //{
                             
                         //      dt = objstate.GetStatutes(int.Parse(HttpContext.Current.Items["statutesid"].ToString()));
                         //      if (dt.Rows.Count > 0)
                         //      {
                         //          Response.Write("<span style='float:right'><a href='/m/Statutes/Search-Result'>Go to Search Page</a></span>");
                         //          Response.Write("<center><strong> " + dt.Rows[0]["Title"].ToString() + "</strong><br>");
                         //          if (dt.Rows[0]["StatutesContentType"].ToString() == "File")
                         //          {
                         //              if (!string.IsNullOrEmpty(dt.Rows[0]["PDFFileName"].ToString()))
                         //              {
                         //                  //  Response.Write("<embed width='100%' height='700' src='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "#toolbar=1&navpanes=1&scrollbar=1'></embed>");
                         //                  Response.Write("<object data='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "' type='application/pdf' width='100%' height='700'><p>Alternative text - include a link <a href='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "'>to the PDF!</a></p></object>");
                         //                  return;
                         //              }
                         //              else if (!string.IsNullOrEmpty(dt.Rows[0]["WordFileName"].ToString()))
                         //              {
                         //                  //Response.Write("<object data='/store/statutesdocs/word/" + dt.Rows[0]["WordFileName"].ToString() + "' type='application/pdf' width='100%' height='700'><p>Alternative text - include a link <a href='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "'>to the PDF!</a></p></object>");
                         //                  Response.Write("<object data='/store/statutesdocs/word/" + dt.Rows[0]["WordFileName"].ToString() + "'>You do not have Word installed on your machine</object>");
                         //              }
                                       
                         //          }
                                  
                                   
                         //          Response.Write("</center>");
                         //          if (dt.Rows[0]["StatutesContentType"].ToString() == "Content")
                         //          {
                         //              Response.Write(FormatContent(dt.Rows[0]["Cntnt"].ToString()));
                         //          }
                         //          if (!string.IsNullOrEmpty(dt.Rows[0]["PDFFileName"].ToString()))
                         //          {
                         //              Response.Write("<object data='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "' type='application/pdf' width='100%' height='700'><p>Alternative text - include a link <a href='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "'>to the PDF!</a></p></object>");
                         //          }
                         //      } 
                         //}
                     }
                     catch { } 
                  }
                     %>
            </p>
        </div>
    </div>
        <%
          }
          catch { } %>
</div>
</asp:Content>


