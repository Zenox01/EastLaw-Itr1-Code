using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using ClosedXML.Excel;

namespace EastlawUI_v2.adminpanel
{
    public partial class GenerateCaseExcel : System.Web.UI.Page
    {
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        EastLawBL.Cases objcases = new EastLawBL.Cases();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("default.aspx");
                }
                if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                    Response.Redirect("NotAuthorize.aspx");
                GetJournals();
                LoadYears();
                GetCourts();
            }
        }
        void GetJournals()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJo.GetActiveJournals();

                ddlJournal.DataValueField = "ID";
                ddlJournal.DataTextField = "JournalName";
                ddlJournal.DataSource = dt;
                ddlJournal.DataBind();
                ddlJournal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("UtilityCases.aspx", "GetJournals", e.Message);
            }
        }
        void LoadYears()
        {
            // Clear items:    
            ddlYear.Items.Clear();
            // Add default item to the list
            ddlYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            // Start loop
            for (int i = 0; i < 69; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
            }

            for (int i = 1; i < 201; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlYear.Items.Add(i.ToString());

            }
        }
        void GetCourts()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcases.GetCasesCourtsGroup();

                ddlRadCourts.DataValueField = "court";
                ddlRadCourts.DataTextField = "court";
                ddlRadCourts.DataSource = dt;
                ddlRadCourts.DataBind();

                //ddlCourt.DataValueField = "court";
                //ddlCourt.DataTextField = "court";
                //ddlCourt.DataSource = dt;
                //ddlCourt.DataBind();
                //ddlCourt.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));



                //ddlSearchCourt.DataValueField = "court";
                //ddlSearchCourt.DataTextField = "court";
                //ddlSearchCourt.DataSource = dt;
                //ddlSearchCourt.DataBind();
                //ddlSearchCourt.Items.Insert(0, new ListItem("Select", "0"));


            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "GetAdvocates", e.Message);
            }
        }

        protected void ExportToExcel(DataTable  dtSource,string FileName)
        {

            DataTable dt = dtSource;

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename="+FileName+".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        DataTable GetResults()
        {
            try
            {
                string cri = "Where A.Citation is not null";
                string columns = "A.ID,";


                if (ddlRadCourts.SelectedValue != "")
                    cri = cri + " AND A.Court='" + ddlRadCourts.SelectedValue + "'";


                if (ddlJournal.SelectedValue != "0")
                    cri = cri + " AND A.JournalID='" + ddlJournal.SelectedValue + "'";

                if (ddlYear.SelectedValue != "0")
                    cri = cri + " AND A.Year='" + ddlYear.SelectedValue + "'";

                for (int a = 0; a < cblColumns.Items.Count; a++)
                {
                    if (cblColumns.Items[a].Selected == true)
                    {
                        columns = columns + cblColumns.Items[a].Value +",";
                    }
                }
                if (!string.IsNullOrEmpty(columns))
                    columns = columns.Remove(columns.Length - 1, 1);

                DataTable dt = new DataTable();
                dt = objcases.GetCaseForDynamicExcel(cri,columns);
                
                        return dt;
                
            }
            catch {
                return null;
            }
        }

        public void ExportExcel_XML(DataTable dt, String FileName)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    //Response.End();
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); //This would bypass the Application_EndRequest
                }
            }
        }
        protected void btnGenerateExcel_Click(object sender, EventArgs e)
        {
            ExportExcel_XML(GetResults(), txtFileName.Text);
        }
   

    }
}