using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SQLite;
using System.Data;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
namespace EastlawUI_v2.adminpanel
{
    public partial class StatutesUtility : System.Web.UI.Page
    {
        EastLawBL.Statutes objs = new EastLawBL.Statutes();
        private SQLiteConnection sqlite;
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
                LoadYears();
            }
        }
        void LoadYears()
        {
            // Clear items:    
            ddlYear.Items.Clear();
            // Add default item to the list
            ddlYear.Items.Insert(0, new ListItem("Not Applicable", "0"));
            // Start loop
            for (int i = 0; i < 69; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
            }
        }
        void SessionNumber()
        {
            int num;
            Random random = new Random();
            num = random.Next(0, 10000);
            Session["tempID"] = num;
        }
        void UploadFile()
        {
            string Img = "";
            if (Session["tempID"] == null)
            {
                SessionNumber();
            }
            if (fuploader.HasFile)
            {
                string destDir = Server.MapPath("../adminpanel/dbfilesstatutes/");

                string FileName = Path.GetFileName(fuploader.PostedFile.FileName);
                string destPath = Path.Combine(destDir, Session["tempID"].ToString() + "-" + FileName);
                fuploader.SaveAs(destPath);

                Img = Session["tempID"].ToString() + "-" + fuploader.FileName;

                lblFileNameMsg.Text = "File uploaded ";
                lblFileName.Text = Img;
                lblFileNameMsg.Visible = true;
                lblFileName.Visible = true;

            }
            else
            {
                lblFileNameMsg.Text = "";
                lblFileName.Text = "";
                lblFileNameMsg.Visible = false;
                lblFileName.Visible = false;

            }
        }
        DataTable ExtractDataFromUploadedFile(string FileName, string Query)
        {
            string dbString = "Data Source=" + Server.MapPath("dbfilesstatutes/" + FileName + "");
            sqlite = new SQLiteConnection(dbString);

            DataTable dt = new DataTable();
            //dt = selectQuery("Select * from data");
            dt = selectQuery(Query);

            lblNoOfRecords.Text = dt.Rows.Count.ToString();
            return dt;
        }
        public DataTable selectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
            sqlite.Close();
            return dt;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFile();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = ExtractDataFromUploadedFile(lblFileName.Text.ToString(), "Select * from data2");
            gv.DataSource = dt;
            gv.DataBind();

        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            ExtractDataFromUploadedFile(lblFileName.Text.ToString(), "Select * from data2");
        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            InsertMigratedData();

        }
        void InsertMigratedData()
        {
            try
            {
                DataTable dtdata = new DataTable();
                dtdata = ExtractDataFromUploadedFile(lblFileName.Text.ToString(), "Select * from data2");
                if (dtdata.Rows.Count > 0)
                {
                    var regex = new Regex("dated", RegexOptions.IgnoreCase);
                    for (int a = 0; a < dtdata.Rows.Count; a++)
                    {
                      //  Regex.Replace(input, fWord, "****", RegexOptions.IgnoreCase);

                        //objs.Title = Regex.Replace(dtdata.Rows[a]["title"].ToString(), @"\s+", " ").Trim();
                        //objs.Date = Regex.Replace(regex.Replace(dtdata.Rows[a]["date"].ToString(), ""), @"\s+", " ").Trim();
                        //objs.Cntnt = Regex.Replace(dtdata.Rows[a]["text"].ToString(), @"\s+", " ").Trim();
                        ////objs.Status = "Migrated";
                        //objs.Active = 0;
                       
                        //objs.CreatedBy = int.Parse(Session["UserID"].ToString());
                        //int chk = objs.InsertStatutes();



                        objs.CatName = dtdata.Rows[a]["Category"].ToString();
                        objs.Title = Regex.Replace(dtdata.Rows[a]["header"].ToString(), @"\s+", " ").Trim();
                        objs.CreatedBy = 0;
                        objs.Active = 1;
                        objs.WorkflowID = 1;
                        objs.GroupID = 1;
                        objs.SubGroupID = 1;
                        objs.Cntnt = System.Text.RegularExpressions.Regex.Replace(dtdata.Rows[a]["texts"].ToString(), @"\r\n?|\n", "<br/>").Trim();// Regex.Replace(dtdata.Rows[a]["texts"].ToString(), @"\s+", " ").Trim();
                        objs.Primary_Secondary = "SECONDARY";
                        objs.StatutesContentType = "Content";
                        objs.Act = "";
                        objs.Date = Regex.Replace(regex.Replace(dtdata.Rows[a]["date"].ToString(), ""), @"\s+", " ").Trim();
                        if (ddlYear.SelectedValue != "0")
                            objs.SYear = int.Parse(ddlYear.SelectedValue);
                        if (ddlType.SelectedValue != "0")
                            objs.SType = ddlType.SelectedValue;

                        int chk = objs.InsertStatutesUtility();

                    }
                }


                //DataTable dtdataimages = new DataTable();
                //dtdataimages = GetCasesImages(lblFileName.Text.ToString(), "Select * from images");
                //if (dtdataimages.Rows.Count > 0)
                //{
                //    for (int a = 0; a < dtdataimages.Rows.Count; a++)
                //    {
                //        objCases.GUID = dtdataimages.Rows[a]["guid"].ToString();
                //        objCases.ImageData = GetBytes(dtdataimages.Rows[a]["ImgData"].ToString());
                //        int chk = objCases.InsertCaseImagesMigrate();
                //    }
                //}
            }
            catch (Exception ex)
            {

            }


        }
    }
}