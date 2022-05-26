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
using System.Globalization;

namespace EastlawUI_v2.adminpanel
{
    public partial class UtilityCases : System.Web.UI.Page
    {
        private System.Object syncLock = new System.Object();
        EastLawBL.Cases objCases = new EastLawBL.Cases();
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        private SQLiteConnection sqlite;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("default.aspx");
                }
                if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                    Response.Redirect("NotAuthorize.aspx");
                GetJournals();
                GetCitationVariation();
                LoadYears();
            }
            //GetDataTable("7242-CLC-2013-0406.db", "select * from images");
            //DataTable dtdataimages = GetCasesImages("7242-CLC-2013-0406.db", "select * from images");
            //if (dtdataimages.Rows.Count > 0)
            //{
            //    for (int a = 0; a < dtdataimages.Rows.Count; a++)
            //    {
            //        objCases.GUID = dtdataimages.Rows[a]["guid"].ToString();
            //        objCases.ImageData = GetBytes(dtdataimages.Rows[a]["ImgData"].ToString());
            //        int chk = objCases.InsertCaseImagesMigrate();
            //    }
            //}
            //string val = "{4F42D19F-8ED2-4022-98D4-1D85DE257044}";

            //DataTable dtdataimages = objCases.GetCasesImagesByGUID(val);
            
            //if (dtdataimages.Rows.Count > 0)
            //{
            //    for (int a = 0; a < dtdataimages.Rows.Count; a++)
            //    {
            //        //objCases.GUID = dtdataimages.Rows[a]["guid"].ToString();
            //        //objCases.ImageData = GetBytes(dtdataimages.Rows[a]["ImgData"].ToString());
            //        //int chk = objCases.InsertCaseImagesMigrate();
            //    }
            //}

            
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
                    ddlJournal.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("UtilityCases.aspx", "GetJournals", e.Message);
            }
        }
        void GetCitationVariation()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objCases.GetCitationVariation(0);

                ddlVaraition.DataValueField = "ID";
                ddlVaraition.DataTextField = "Vari";
                ddlVaraition.DataSource = dt;
                ddlVaraition.DataBind();
                ddlVaraition.Items.Insert(0, new ListItem("Select", "0"));

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
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            // Start loop
            for (int i = 0; i < 69; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
            }
        }
        void LoadVolumns()
        {
            // Clear items:    
            ddlYear.Items.Clear();
            // Add default item to the list
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            // Start loop
            for (int i = 1; i < 201; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlYear.Items.Add(i.ToString());
                
            }
        }
        //static Image GetImage()
        //{
        //    Image image = null;
        //    string val = "{4F42D19F-8ED2-4022-98D4-1D85DE257044}";
        //    SqlConnection sqlcon = new SqlConnection(DBHelper.GetConnectionString());
        //    SqlCommand cm = sqlcon.CreateCommand();
        //    sqlcon.Open();
        //    cm.CommandText = "select data from tbl_CasesImages Where guid='" + val + "'";
        //    byte[] img = (byte[])cm.ExecuteScalar();
        //    sqlcon.Close();

        //    if (img != null)
        //    {
        //        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //        {
        //            ms.Write(img, 0, img.Length);
        //            ms.Position = 0L;

        //            image = new Bitmap(ms);
        //            image.Save(dialog.FileName, ImageFormat.JPEG);


        //        }
        //    }
          

        //    return image;
        //}
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
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
                string destDir = Server.MapPath("../adminpanel/dbfilescases/");

                string FileName = Path.GetFileName(fuploader.PostedFile.FileName);
                string destPath = Path.Combine(destDir, Session["tempID"].ToString() + "-" + FileName);
                fuploader.SaveAs(destPath);

                Img = Session["tempID"].ToString() + "-" + fuploader.FileName;

                lblFileNameMsg.Text = "File uploaded ";
                lblFileName.Text = Img;
                lblFileNameMsg.Visible = true;
                lblFileName.Visible = true;

                btnUpload.Visible = false;
                btnLoad.Visible = true;
                btnProcess.Visible = false;
                btnKeywordTagging.Visible = false;

            }
            else
            {
                lblFileNameMsg.Text = "";
                lblFileName.Text = "";
                lblFileNameMsg.Visible = false;
                lblFileName.Visible = false;

                btnUpload.Visible = true;
                btnLoad.Visible = false;
                btnProcess.Visible = false;
                btnKeywordTagging.Visible = false;
 
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFile();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ExtractDataFromUploadedFile(lblFileName.Text.ToString(), "Select * from data");
                gv.DataSource = dt;
                gv.DataBind();

                btnUpload.Visible = false;
                btnLoad.Visible = false;
                btnProcess.Visible = true;
                btnKeywordTagging.Visible = false;

            }
            catch(Exception ex)
            { }
           
        }
        DataTable ExtractDataFromUploadedFile(string FileName,string Query)
        {
            string dbString = "Data Source=" + Server.MapPath("dbfilescases/" + FileName + "");
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
        public byte[] getByteArray(DataRow row, int offset)
        {
            object blob = row[offset];
            if (blob == null) return null;
            byte[] arData = (byte[])blob;
            return arData;
        }
        static byte[] GetBytes(SQLiteDataReader reader)
        {
            const int CHUNK_SIZE = 2 * 1024;
            byte[] buffer = new byte[CHUNK_SIZE];
            long bytesRead;
            long fieldOffset = 0;
            using (MemoryStream stream = new MemoryStream())
            {
                while ((bytesRead = reader.GetBytes(1, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
            }
        }
       
        public DataTable GetCasesImages(string FileName,string sql)
        {
             
            lock (syncLock)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("GUID");
                    dt.Columns.Add("ImgData");

                    string dbString = "Data Source=" + Server.MapPath("dbfilescases/" + FileName + "");
                    using (var c = new SQLiteConnection(dbString))
                    {
                        c.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                        {
                            using (SQLiteDataReader rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    byte[] buffer = GetBytes(rdr);
                                    DataRow dr = dt.NewRow();
                                    //dr["ImgData"] = ;
                                    dr["ImgData"] = System.Text.Encoding.ASCII.GetString(buffer);
                                    dr["GUID"] = rdr.GetString(0);
                                    dt.Rows.Add(dr);
                                }
                                dt.AcceptChanges();
                                
                               // dt.Load(rdr);
                                return dt;


                            }

                            
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            ExtractDataFromUploadedFile(lblFileName.Text.ToString(),"Select * from data");
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
                dtdata = ExtractDataFromUploadedFile(lblFileName.Text.ToString(),"Select * from data");
                if (dtdata.Rows.Count > 0)
                {
                    for (int a = 0; a < dtdata.Rows.Count; a++)
                    {
                        objCases.FileName = lblFileName.Text.ToString();
                        objCases.Status = 2;
                        objCases.JournalID = int.Parse(ddlJournal.SelectedValue);
                        objCases.Year = int.Parse(ddlYear.SelectedValue);
                        objCases.Citation = dtdata.Rows[a]["Citation"].ToString().Trim();
                        objCases.CitationRef = "";

                        if (string.IsNullOrEmpty(dtdata.Rows[a]["Judge"].ToString()))
                            objCases.JudgeStr = "N/A";
                        else
                            objCases.JudgeStr = dtdata.Rows[a]["Judge"].ToString().Replace("Before", "").Trim();

                        objCases.Appeal = dtdata.Rows[a]["Appeal"].ToString().Trim();
                        //objCases.Appeallant = dtdata.Rows[a]["Appeallant"].ToString().Replace("---Respondent", "").Replace("---Appellants", "").Replace("---Petitioner", "").Replace("---Appellant","").Replace("--- Petitioner","").Trim();
                        objCases.Appeallant = CommonClass.Format_Appeallant_Field(dtdata.Rows[a]["Appeallant"].ToString()).Trim();
                        objCases.AppeallantType = CommonClass.LastWord(dtdata.Rows[a]["Appeallant"].ToString().Trim());
                        //objCases.Respondent = dtdata.Rows[a]["Respondent"].ToString().Replace("--Respondents","").Trim();
                        objCases.Respondent = CommonClass.Format_Respondent_Field(dtdata.Rows[a]["Respondent"].ToString()).Trim();
                        //char[] MyChar = {'decided on ','heard on '};
                        objCases.JDate = dtdata.Rows[a]["JDate"].ToString().Replace("decided on ", "").Replace("heard on ", "").Trim();
                        if (string.IsNullOrEmpty(dtdata.Rows[a]["AdvA"].ToString()))
                            objCases.AdvocateASTR = "N/A";
                        else
                        {
                            //objCases.AdvocateASTR = dtdata.Rows[a]["AdvA"].ToString().Trim().Substring(0, input.IndexOf("for"));
                            objCases.AdvocateASTR = dtdata.Rows[a]["AdvA"].ToString().Replace("for Appellant", "").Replace("for Petitioner", "").Replace("for Respondent", "").Trim();
                        }
                        if (string.IsNullOrEmpty(dtdata.Rows[a]["AdvR"].ToString()))
                            objCases.AdvocateRSTR = "N/A";
                        else
                        {
                            //objCases.AdvocateRSTR = dtdata.Rows[a]["AdvR"].ToString().Trim().Substring(0, input.IndexOf("for"));
                            objCases.AdvocateRSTR = dtdata.Rows[a]["AdvR"].ToString().Replace("for Appellant", "").Replace("for Petitioner", "").Replace("for Respondent", "").Trim();
                        }
                        objCases.HearDate = dtdata.Rows[a]["HDate"].ToString().Trim();
                        objCases.HeadNotes = dtdata.Rows[a]["HeadNote"].ToString().Trim();
                        //                        objCases.Judgment = System.Text.RegularExpressions.Regex.Replace(dtdata.Rows[a]["Judgment"].ToString(), @"\s+", " ").Trim();
                        //objCases.Judgment = dtdata.Rows[a]["Judgment"].ToString().Replace(System.Environment.NewLine, "<br><br>");
                        if (chkFormat.Checked == true)
                            objCases.Judgment = System.Text.RegularExpressions.Regex.Replace(dtdata.Rows[a]["Judgment"].ToString(), @"\r\n?|\n", "<br/>").Trim();
                        else
                            objCases.Judgment = System.Text.RegularExpressions.Regex.Replace(dtdata.Rows[a]["Judgment"].ToString(), @"\s+", " ").Trim();
                        objCases.JudgmentType = CommonClass.FirstWords(System.Text.RegularExpressions.Regex.Replace(dtdata.Rows[a]["Judgment"].ToString(), @"\s+", " "), 1).Trim();
                        objCases.Result = dtdata.Rows[a]["Result"].ToString().Trim();
                        objCases.Court = CommonClass.MakeFirstCap(dtdata.Rows[a]["Court"].ToString().Trim());
                        objCases.CreatedBy = int.Parse(Session["UserID"].ToString());
                        objCases.CaseSummary = CommonClass.GetCaseSummary(System.Text.RegularExpressions.Regex.Replace(dtdata.Rows[a]["Judgment"].ToString(), @"\r\n?|\n", "<br/>").Trim());
                        objCases.DateFormated = ValidateDate(dtdata.Rows[a]["JDate"].ToString().Replace("decided on ", "").Replace("heard on ", "").Trim());
                        objCases.PageNo = dtdata.Rows[a]["Citation"].ToString().Trim().Split(' ').Last();

                        if (chkPriorityTagging.Checked == true)
                            objCases.PriorityTagging = 1;
                        else
                            objCases.PriorityTagging = 0;
                        int chk = objCases.InsertCaseMigrate();
                        if(chk > 0)
                        {
                            if (ddlVaraition.SelectedValue != "0")
                            {
                                int chk1 = objCases.InsertCitationVariationTagging(int.Parse(ddlVaraition.SelectedValue), chk);
                            }
                        }
                        ////if (a == 9)
                        ////{
                        //    divSuccess.Style["Display"] = "";
                        //    divError.Style["Display"] = "none";
                        //    gv.DataSource = null;
                        //    gv.DataBind();
                        //    break;
                        //}

                    }
                    Session.Remove("tempID");
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                   // lblFileName.Text = "";
                    gv.DataSource = null;
                    gv.DataBind();

                    btnUpload.Visible = false;
                    btnLoad.Visible = false;
                    btnProcess.Visible = false;
                    btnKeywordTagging.Visible = true;
                   
                }


                DataTable dtdataimages = new DataTable();
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
        int ValidateDate(string Date)
        {
            DateTime dateValue;
            if (DateTime.TryParseExact(Date, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                return 1;
            else
                return 0;
        }
        protected void btnKeywordTagging_Click(object sender, EventArgs e)
        {
            try
            {
                int chk = objCases.KeywordTaggingByFileName(lblFileName.Text.ToString());
                if(chk >0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";

                    lblFileName.Text = "";
                    lblFileNameMsg.Text = "";
                    btnUpload.Visible = true;
                    btnLoad.Visible = false;
                    btnProcess.Visible = false;
                    btnKeywordTagging.Visible = false;
                }
            }
            catch(Exception ex)
            { }
        }

        protected void ddlYearVolumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYearVolumn.SelectedValue == "Year")
            {
                lblYearVolumn.Text = "Year";
                LoadYears();
            }
            else if (ddlYearVolumn.SelectedValue == "Volume")
            {
                lblYearVolumn.Text = "Volume";
                LoadVolumns();
            }
        }

       
        
    }
}