using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace EastlawUI_v2.adminpanel
{
    public partial class AddEditCourts : System.Web.UI.Page
    {
        
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        EastLawBL.Common objcom = new EastLawBL.Common();
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
                GetHeiryMstr();
                GetAppealMstr();
                GetCountries();
                if (Request.QueryString["param"] != null)
                    GetCourt(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
               
            }

        }
        void GetCourt(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetCourtMasters(ID);
                if (dt.Rows.Count > 0)
                {
                    lblID.Text = dt.Rows[0]["ID"].ToString();
                    ddlCountry.SelectedValue = dt.Rows[0]["Jrd_Cunty"].ToString();
                    
                    txtCourtName.Text = dt.Rows[0]["CourtName"].ToString();
                    txtSort.Text = dt.Rows[0]["SortOrder"].ToString();
                    txtJuriAreaName.Text = dt.Rows[0]["Jrd_Area"].ToString();
                    txtJuriDistriName.Text = dt.Rows[0]["Jrd_Distrct"].ToString();
                    lblfuploadWord.Text= dt.Rows[0]["FileNm"].ToString();
                    txtShortContent.Text = dt.Rows[0]["ShortDes"].ToString();
                    imgFl.ImageUrl = "/store/court/" + dt.Rows[0]["FileNm"].ToString();
                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";
                    GetMasterLinking(int.Parse(lblID.Text), "heir");
                    GetMasterLinking(int.Parse(lblID.Text), "apel");



                }

            }
            catch (Exception e)
            {

            }
        }
        void GetMasterLinking(int CrtID,string reftran)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetCourtMasterslinking(CrtID, reftran);
                if (reftran == "heir")
                {
                    for (int a = 0; a < chkhierarchy.Items.Count; a++)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (chkhierarchy.Items[a].Value == dt.Rows[i]["refcode"].ToString())

                                chkhierarchy.Items[a].Selected = true;

                        }
                    }
                }
               else if (reftran == "apel")
                {
                    for (int a = 0; a < chkCrtAppealType.Items.Count; a++)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (chkCrtAppealType.Items[a].Value == dt.Rows[i]["refcode"].ToString())

                                chkCrtAppealType.Items[a].Selected = true;

                        }
                    }
                }

            }

            catch { }
        }
        
        void GetHeiryMstr()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetActiveCourtMastersHeirchy();
                chkhierarchy.DataValueField = "ID";
                chkhierarchy.DataTextField = "Crt";
                chkhierarchy.DataSource = dt;
                chkhierarchy.DataBind();
                
            }
            catch (Exception ex)
            {

            }
        }
        void GetAppealMstr()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetActiveCourtAppealMasters();
                chkCrtAppealType.DataValueField = "ID";
                chkCrtAppealType.DataTextField = "Crt_appl_mstr";
                chkCrtAppealType.DataSource = dt;
                chkCrtAppealType.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
        void GetCountries()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcom.GetCountries();
                ddlCountry.DataValueField = "Code";
                ddlCountry.DataTextField = "Name";
                ddlCountry.DataSource = dt;
                ddlCountry.DataBind();

                ddlCountry.Items.Insert(0, new ListItem("Select", "0"));

                ddlCountry.SelectedValue = "PAK";
                
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "GetCountries", ex.Message);
            }
        }
        void SaveRecord()
        {
            try
            {
                string ImageType = "";
                string ImageName = "";
                if (fuploadimage.HasFile)
                {
                    string destDir = Server.MapPath("../store/court/");

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

                objcase.CourtName = txtCourtName.Text.Trim();
                objcase.SortOrder = int.Parse(txtSort.Text.Trim());
                objcase.FileNm = ImageName;
                objcase.ShortDes = txtShortContent.Text;
                objcase.Jrd_Cunty = ddlCountry.SelectedValue;
                objcase.Jrd_Area = txtJuriAreaName.Text.Trim();
                objcase.Jrd_Distrct = txtJuriDistriName.Text.Trim();

                if (chkActive.Checked == true)
                    objcase.Active = 1;
                else
                    objcase.Active = 0;
                objcase.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objcase.AddCourtMaster();
                
                if (chk > 0)
                {
                    AddCourtMasterLinking(chk,"heir");
                    AddCourtMasterLinking(chk, "apel");
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
        void EditRecord(int ID)
        {
            try
            {
                string ImageType = "";
                string ImageName = "";
                if (fuploadimage.HasFile)
                {
                    string destDir = Server.MapPath("../store/court/");

                    string FileName = Path.GetFileName(fuploadimage.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fuploadimage.SaveAs(destPath);

                    ImageName = fuploadimage.FileName;
                    ImageType = "Local";


                }
                else
                {
                    ImageName = lblfuploadWord.Text ;

                }

                objcase.CourtName = txtCourtName.Text.Trim();
                objcase.SortOrder = int.Parse(txtSort.Text.Trim());
                objcase.FileNm = ImageName;
                objcase.ShortDes = txtShortContent.Text;
                objcase.Jrd_Cunty = ddlCountry.SelectedValue;
                objcase.Jrd_Area = txtJuriAreaName.Text.Trim();
                objcase.Jrd_Distrct = txtJuriDistriName.Text.Trim();

                if (chkActive.Checked == true)
                    objcase.Active = 1;
                else
                    objcase.Active = 0;
                objcase.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objcase.EditCourtMaster(ID);
                if (chk > 0)
                {
                    objcase.DeleteCourtMasterLinking(ID, "heir", int.Parse(Session["UserID"].ToString()));
                    AddCourtMasterLinking(ID, "heir");
                    objcase.DeleteCourtMasterLinking(ID, "apel", int.Parse(Session["UserID"].ToString()));
                    AddCourtMasterLinking(ID, "apel");
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

          
            ddlCountry.SelectedIndex = 0;
            txtCourtName.Text = "";
            txtSort.Text = "";
            txtShortContent.Text = "";
            txtJuriAreaName.Text = "";
            txtJuriDistriName.Text = "";
           
            
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
                if (Request.QueryString["param"] == null)
                    SaveRecord();
                else
                    EditRecord(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch (Exception ex)
            {

            }
        }

        
       
        void AddCourtMasterLinking(int CrtID,string trntyp)
        {
            try
            {
                if (trntyp == "heir")
                {
                    for (int a = 0; a < chkhierarchy.Items.Count; a++)
                    {
                        if (chkhierarchy.Items[a].Selected == true)
                            objcase.AddCourtMasterLinking(CrtID,trntyp,int.Parse(chkhierarchy.Items[a].Value), int.Parse(Session["UserID"].ToString()));
                    }
                }
                else if (trntyp == "apel")
                {
                    for (int a = 0; a < chkCrtAppealType.Items.Count; a++)
                    {
                        if (chkCrtAppealType.Items[a].Selected == true)
                            objcase.AddCourtMasterLinking(CrtID, trntyp, int.Parse(chkCrtAppealType.Items[a].Value), int.Parse(Session["UserID"].ToString()));
                    }
                }
            }
            catch { }
        }
       
    }
}