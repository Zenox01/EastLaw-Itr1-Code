using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2
{
    public partial class ebook : System.Web.UI.Page
    {
        EastLawBL.EBook objeb = new EastLawBL.EBook();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetActiveEBookCategories();
                GetEBooks();
            }
            catch { }
        }
        void GetActiveEBookCategories()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objeb.GetActiveEBookCategories();
                chkLstCategory.DataValueField = "ID";
                chkLstCategory.DataTextField = "EBookCat";
                chkLstCategory.DataSource = dt;
                chkLstCategory.DataBind();

               
            }
            catch (Exception ex)
            {

            }
        }
        void GetEBooks()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objeb.GetActiveEBook("E-Book");
                
                    dt.Columns.Add("strActive");
                    dt.Columns.Add("Link");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["Active"].ToString() == "1")
                            dt.Rows[a]["strActive"] = "Yes";
                        else
                            dt.Rows[a]["strActive"] = "No";


                        dt.Rows[a]["Link"] = "/ebook/"+ dt.Rows[a]["EBookCat"].ToString()+"/"+clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Title"].ToString(), 10).ToString())).Replace(" ", "-").ToLower()  + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                
                  

                    }
                    dt.AcceptChanges();
                    gvLst.DataSource = dt;
                    gvLst.DataBind();
              

            }
            catch (Exception e)
            {

            }
        }
        protected void gvLst_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {


                ////Button btnShowSummary = default(Button);
                //HiddenField hdSummary = default(HiddenField);
                //System.Web.UI.HtmlControls.HtmlButton btnShowSummary = default(System.Web.UI.HtmlControls.HtmlButton);
                //string script = null;
                //script = "";

                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    btnShowSummary = (System.Web.UI.HtmlControls.HtmlButton)e.Row.Controls[0].FindControl("btnshowsumary");
                //    hdSummary = (HiddenField)e.Row.Controls[0].FindControl("hdSummary");

                //    if (!string.IsNullOrEmpty(hdSummary.Value.ToString()))
                //    {
                //        btnShowSummary.Visible = true;
                //    }
                //    else
                //    {
                //        btnShowSummary.Visible = false;
                //    }
                //}
            }
            catch { }
        }
        protected void gvLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLst.PageIndex = e.NewPageIndex;
                GetEBooks();
            }
            catch (Exception ex)
            {

            }
        }
    }
}