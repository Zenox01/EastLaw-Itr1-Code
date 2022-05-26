using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace EastlawUI_v2.adminpanel
{
    public partial class AddNews : System.Web.UI.Page
    {
        EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
        EastLawBL.News objn = new EastLawBL.News();
        EastLawBL.Statutes objstat = new EastLawBL.Statutes();
        EastLawBL.Cases objCases = new EastLawBL.Cases();
        EastLawBL.Judges objJudge = new EastLawBL.Judges();
        public string Title { get; set; }
        public string PageUrl { get; set; }
        public string Text { get; set; }
        public string Img { get; set; }
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
                GetPracticeArea();
                GetStatuesCat(0);
                GetCourtMaster();
                if (Request.QueryString["param"] != null)
                    GetNews(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                Session.Remove("OtherCategoryID");
                Session.Remove("OtherCategoryName");
            }

        }
        void GetNews(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objn.GetNews(ID);
                if (dt.Rows.Count > 0)
                {
                    lblID.Text = dt.Rows[0]["ID"].ToString();
                    ddlType.SelectedValue = dt.Rows[0]["DType"].ToString();
                    ddlPA.SelectedValue = dt.Rows[0]["PracticeAreaID"].ToString();
                    txtTitle.Text = dt.Rows[0]["Title"].ToString();
                    txtKeywords.Text = dt.Rows[0]["Keywords"].ToString();
                    txtDate.Text = dt.Rows[0]["NDate"].ToString();
                    txtSource.Text = dt.Rows[0]["Source"].ToString();
                    txtSourceLink.Text = dt.Rows[0]["SourceLink"].ToString();
                    txtAuthor.Text = dt.Rows[0]["Author"].ToString();
                    txtShortContent.Text = dt.Rows[0]["ShortContent"].ToString();
                    editorContent.Content = dt.Rows[0]["FullContent"].ToString();
                    ddlCourtMaster.SelectedValue = dt.Rows[0]["CourtMasterID"].ToString();
                    
                    

                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";
                    GetNewsStatuteCategories(ID);

                    GetJudgesByCourtMaster(int.Parse(dt.Rows[0]["CourtMasterID"].ToString()));
                    ddlNewJudge.SelectedValue = dt.Rows[0]["JudgeID"].ToString();
                }
                
            }
            catch (Exception e)
            {
                
            }
        }
        void GetNewsStatuteCategories(int NewsID)
        {
            try
            {


                DataTable dtLaws = new DataTable();
                dtLaws = objn.GetNewsStatutesCategory(NewsID);
                gvOtherCat.DataSource = dtLaws;
                gvOtherCat.DataBind();
            }
            catch { }
        }
        void GetPracticeArea()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPA.GetActivePracticeAreaCategories();
                ddlPA.DataValueField = "ID";
                ddlPA.DataTextField = "PracticeAreaCatName";
                ddlPA.DataSource = dt;
                ddlPA.DataBind();
                ddlPA.Items.Insert(0, new ListItem("Not Applicable", "0"));
            }
            catch (Exception ex)
            {
                
            }
        }
        void GetCourtMaster()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objCases.GetActiveCourtMasters();

                ddlCourtMaster.DataSource = dt;
                ddlCourtMaster.DataTextField = "CourtName";
                ddlCourtMaster.DataValueField = "ID";
                ddlCourtMaster.DataBind();
                
                ddlCourtMaster.Items.Insert(0, new ListItem("Not Applicable", "0"));
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "GetJudges", e.Message);
            }
        }
        void GetJudgesByCourtMaster(int CourtMasterID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJudge.GetJudgeNewByCourtMaster(CourtMasterID);

                ddlNewJudge.DataSource = dt;
                ddlNewJudge.DataTextField = "JudgeName";
                ddlNewJudge.DataValueField = "ID";
                ddlNewJudge.DataBind();
                ddlNewJudge.Items.Insert(0, new ListItem("Not Applicable", "0"));


              
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "GetJudges", e.Message);
            }
        }
        public void GetStatuesCat(int ID)
        {
            try
            {

                DataTable dt = objstat.GetStatutesCategories(ID);
                if (dt.Rows.Count > 0)
                {
                    ddlStatuteCategories.DataTextField = "CatName";
                    ddlStatuteCategories.DataValueField = "ID";
                    ddlStatuteCategories.DataSource = dt;
                    ddlStatuteCategories.DataBind();
                    ddlStatuteCategories.Items.Insert(0, new ListItem("Select", "0"));

                    
                }


            }
            catch { }

        }

        void SaveRecord()
        {
            try
            {
                string ImageType = "";
                string ImageName = "";
                if (fuploadimage.HasFile)
                {
                    string destDir = Server.MapPath("../store/news/imgs/");

                    string FileName = Path.GetFileName(fuploadimage.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fuploadimage.SaveAs(destPath);

                    ImageName = fuploadimage.FileName;
                    ImageType = "Local";


                }
                else
                {
                    ImageType = "URL";
 
                }
                objn.DType = ddlType.SelectedValue;
                objn.PracticeAreaID = int.Parse(ddlPA.SelectedValue);
                objn.Title = txtTitle.Text;
                objn.Keywords = txtKeywords.Text;
                objn.NDate = txtDate.Text;
                objn.Source = txtSource.Text;
                objn.SourceLink = txtSourceLink.Text;
                objn.Author = txtAuthor.Text;
                objn.ShortContent = txtShortContent.Text;
                objn.FullContent = editorContent.Content;
                objn.ImageType = ImageType;
                objn.CourtMasterID = int.Parse(ddlCourtMaster.SelectedValue);
                objn.JudgeID = int.Parse(ddlNewJudge.SelectedValue);
                if (ImageType == "URL")
                objn.ImgFile = GetGridItemsImgs();
                else
                    objn.ImgFile = ImageName;
              
                if (chkActive.Checked == true)
                    objn.Active = 1;
                else
                    objn.Active = 0;

                objn.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objn.InsertNews();
                if (chk > 0)
                {
                    GetGridItemsOtherCategorysAndSave(chk);
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();
                    Session.Remove("OtherCategoryID");
                    Session.Remove("OtherCategoryName");
              
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                
            }
        }
        void EditRecord(int ID)
        {
            try
            {
                string ImageType = "";
                string ImageName = "";
                if (fuploadimage.HasFile)
                {
                    string destDir = Server.MapPath("../store/news/imgs/");

                    string FileName = Path.GetFileName(fuploadimage.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fuploadimage.SaveAs(destPath);

                    ImageName = fuploadimage.FileName;
                    ImageType = "Local";


                }
                else
                {
                    ImageType = "URL";

                }
                objn.DType = ddlType.SelectedValue;
                objn.PracticeAreaID = int.Parse(ddlPA.SelectedValue);
                objn.Title = txtTitle.Text;
                objn.Keywords = txtKeywords.Text;
                objn.NDate = txtDate.Text;
                objn.Source = txtSource.Text;
                objn.SourceLink = txtSourceLink.Text;
                objn.Author = txtAuthor.Text;
                objn.ShortContent = txtShortContent.Text;
                objn.FullContent = editorContent.Content;
                objn.ImageType = ImageType;
                objn.CourtMasterID = int.Parse(ddlCourtMaster.SelectedValue);
                objn.JudgeID = int.Parse(ddlNewJudge.SelectedValue);
                if (ImageType == "URL")
                    objn.ImgFile = GetGridItemsImgs();
                else
                    objn.ImgFile = ImageName;
              
                if (chkActive.Checked == true)
                    objn.Active = 1;
                else
                    objn.Active = 0;

                objn.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objn.EditNews(ID);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();
                   
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
               
            }
        }
        void ClearFields()
        {
            ddlPA.SelectedIndex = 0;
            txtTitle.Text = "";
            txtKeywords.Text = "";
             txtDate.Text="";
            txtSource.Text="";
            txtSourceLink.Text="";
            txtAuthor.Text = "";
            txtShortContent.Text="";
            editorContent.Content="";
            chkActive.Checked = false;

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            ClearFields();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text == "0")
                    SaveRecord();
                else
                    EditRecord(int.Parse(lblID.Text));
            }
            catch (Exception ex)
            {
               
            }
        }

        protected void btnFetchURL_Click(object sender, EventArgs e)
        {
            try
            {
                var webGet = new HtmlWeb();
                ParseURL objru = new ParseURL();
                var uri = new Uri(txtURL.Text);
                HtmlAgilityPack.HtmlDocument document = webGet.Load(uri.ToString());
                string webPage = objru.ParseHtml(document.DocumentNode.InnerHtml.ToString(), uri);
                if(!String.IsNullOrEmpty(webPage))
                {
                    string imgs="";
                    if (objru.Title != null)
                    txtTitle.Text = objru.Title.ToString();
                    if (objru.PageUrl != null)
                    txtSourceLink.Text = objru.PageUrl.ToString();
                    if (objru.Text != null)
                    editorContent.Content = objru.Text.ToString();
                    if (objru.Img != null)
                        imgs = objru.Img.ToString();
                    GetURLImages(imgs);
                }
            }
            catch { }

        }
        void GetURLImages(string img)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ImgSrc");
                string[] imglst = img.Split('|');
                foreach (string imgsrc in imglst)
                {
                    DataRow dr = dt.NewRow();
                    dr["ImgSrc"] = imgsrc;
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
                gvImgs.DataSource = dt;
                gvImgs.DataBind();

            }
            catch { }
        }

        protected void radioChoose_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectButton = (RadioButton)sender;
            GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
            int a = row.RowIndex;
            foreach (GridViewRow rw in gvImgs.Rows)
            {
                if (selectButton.Checked)
                {
                    if (rw.RowIndex != a)
                    {
                        RadioButton rd = rw.FindControl("radioChoose") as RadioButton;
                        rd.Checked = false;
                    }
                }
            }
        }
        string GetGridItemsImgs()
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdImgSrc = default(HiddenField);
                RadioButton radioBtn = default(RadioButton);

                foreach (GridViewRow row in gvImgs.Rows)
                {
                    if ((row != null))
                    {

                        hdImgSrc = (HiddenField)row.FindControl("hdImgSrc");
                        radioBtn = (RadioButton)row.FindControl("radioChoose");
                        if (radioBtn.Checked == true)
                        {
                            return hdImgSrc.Value.ToString();
                        }
                        


                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void GetGridItemsOtherCategorysAndSave(int NewsID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);

                foreach (GridViewRow row in gvOtherCat.Rows)
                {
                    if ((row != null))
                    {

                        hdID = (HiddenField)row.FindControl("hdID");

                        int chk = objn.AddNewsStatuteCategory(NewsID, int.Parse(hdID.Value), 1);

                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        public void CreateTableValuesOtherCategory()
        {
            try
            {


                string[] OtherCategoryID = Session["OtherCategoryID"].ToString().Split('|');
                string[] OtherCategoryName = Session["OtherCategoryName"].ToString().Split('|');

                int recordnum = OtherCategoryID.Length;

                DataTable dt = new DataTable("tblOtherCat");
                dt.Columns.Add("OtherCategoryID");
                dt.Columns.Add("OtherCategoryName");
                for (int j = 0; j < recordnum - 1; j++)
                {
                    if (!string.IsNullOrEmpty(OtherCategoryID[j].ToString()))
                    {
                        DataRow dr = dt.NewRow();
                        dr["OtherCategoryID"] = OtherCategoryID[j].ToString();
                        dr["OtherCategoryName"] = OtherCategoryName[j].ToString();
                        dt.Rows.Add(dr);
                    }

                }
                ViewState["dt"] = dt;
                DataTable dt1 = (DataTable)ViewState["dt"];
                dt1 = dt.DefaultView.ToTable(true, "OtherCategoryID", "OtherCategoryName");
                gvOtherCat.DataSource = dt1;
                gvOtherCat.DataBind();



                //gvPracticeAreaSubCat.DataSource = dt.DefaultView;
                //gvPracticeAreaSubCat.DataBind();
            }
            catch (Exception ex)
            {
                
            }



        }
        protected void btnOtherCategories_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Request.QueryString["param"] != null))
                {
                    int chk = objn.AddNewsStatuteCategory(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), int.Parse(ddlStatuteCategories.SelectedValue),1);

                    GetNewsStatuteCategories(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                }
                else
                {
                    Session["OtherCategoryID"] += ddlStatuteCategories.SelectedValue + "|";
                    Session["OtherCategoryName"] += ddlStatuteCategories.SelectedItem.Text + "|";
                    CreateTableValuesOtherCategory();
                }
            }
            catch { }
        }
        protected void ddlCourtMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetJudgesByCourtMaster(int.Parse(ddlCourtMaster.SelectedValue));
            }
            catch { }
        }



    }
     #region Fetch URL Data
    public class ParseURL
    {
        public string Title { get; set; }
        public string PageUrl { get; set; }
        public string Text { get; set; }
        public string Img { get; set; }

        public string ParseHtml(string html, Uri uri)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);

            // remove scripts
            foreach (var script in document.DocumentNode.Descendants("script").ToArray())
            {
                script.Remove();
            }

            // remove styles
            foreach (var style in document.DocumentNode.Descendants("style").ToArray())
            {
                style.Remove();
            }

            // remove comments
            foreach (var style in document.DocumentNode.Descendants("#comment").ToArray())
            {
                style.Remove();
            }

            // sometimes </form> is not removed so we have to remove it manually
            string innerText = (document.DocumentNode.InnerText ?? "").Trim().Replace("</form>", "");

            var sb = new StringBuilder();
            var lines = innerText.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                string trimmed = StringUtils.DecodeAndRemoveSpaces(line);
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    sb.AppendLine(trimmed);
                }
            }

            var webPage = new ParseURL { PageUrl = uri.AbsoluteUri };

            var titleNode = document.DocumentNode.Descendants("title").SingleOrDefault();
            if (titleNode != null)
            {
                //webPage.Title = StringUtils.DecodeAndRemoveSpaces(titleNode.InnerText ?? "");\
                Title = StringUtils.DecodeAndRemoveSpaces(titleNode.InnerText ?? "");
            }
            

            string url = "";
            // Now, using LINQ to get all Images
            List<HtmlNode> imageNodes = null;
            imageNodes = (from HtmlNode node in document.DocumentNode.SelectNodes("//img")
                          where node.Name == "img"
                          //&& node.Attributes["class"] != null
                          //&& node.Attributes["class"].Value.StartsWith("img_")
                          select node).ToList();

            foreach (HtmlNode node in imageNodes)
            {
              
                string imgsrc = node.Attributes["src"].Value;
                if (imgsrc.StartsWith("http"))
                    url = url + imgsrc +"|";
                //Console.WriteLine(node.Attributes["src"].Value);
            }
           // webPage.Img = url;
            Img = url;

            //webPage.Text = sb.ToString();
            Text = sb.ToString();

            //return webPage.ToString();
            return"true";
        }
    }
    public class StringUtils
    {
        public static string DecodeAndRemoveSpaces(string text)
        {
            var trimed = HttpUtility.HtmlDecode(text.Trim());
            trimed = trimed.Replace("\t", " ");
            // replace double spaces
            trimed = Regex.Replace(trimed, @"[ ]{2,}", " ");

            return trimed;
        }
    }
     #endregion
}