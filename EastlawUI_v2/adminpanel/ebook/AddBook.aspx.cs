using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace EastlawUI_v2.adminpanel.ebook
{
    public partial class AddBook : System.Web.UI.Page
    {
        EastLawBL.EBook objeb = new EastLawBL.EBook();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                        Response.Redirect("NotAuthorize.aspx");
                    GetActiveEBookCategories();
                    if (Request.QueryString["param"] != null)
                        GetEBook(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        void GetActiveEBookCategories()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objeb.GetActiveEBookCategories();
                ddlCategory.DataValueField = "ID";
                ddlCategory.DataTextField = "EBookCat";
                ddlCategory.DataSource = dt;
                ddlCategory.DataBind();

                ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                
            }
        }

        void GetEBook(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objeb.GetEBook(ID);
                if (ID == 0)
                {
                    //dt.Columns.Add("strActive");
                    //for (int a = 0; a < dt.Rows.Count; a++)
                    //{
                    //    if (dt.Rows[a]["Active"].ToString() == "1")
                    //        dt.Rows[a]["strActive"] = "Yes";
                    //    else
                    //        dt.Rows[a]["strActive"] = "No";
                    //}
                    //dt.AcceptChanges();
                    //gv.DataSource = dt;
                    //gv.DataBind();
                }
                else
                {
                    ddlCategory.SelectedValue = dt.Rows[0]["EBookCatID"].ToString();
                    txtTitle.Text = dt.Rows[0]["Title"].ToString();
                    if(!string.IsNullOrEmpty(dt.Rows[0]["CoverPhoto"].ToString()))
                    {
                        lblCoverUpload.Text = dt.Rows[0]["CoverPhoto"].ToString();
                        imgCover.ImageUrl="/store/ebook/cover/"+dt.Rows[0]["CoverPhoto"].ToString();
                        rfvFupload.Enabled=false;

                    }
                    
                    txtShortInfo.Text = dt.Rows[0]["ShortInfo"].ToString();
                    txtAuthor.Text = dt.Rows[0]["Author"].ToString();
                    txtPublishedOn.Text = dt.Rows[0]["PublishedOn"].ToString();
                    txtNoOfPages.Text = dt.Rows[0]["NoOfPages"].ToString();
                    txtSubscriptionPrice.Text = dt.Rows[0]["SubscriptionPrice"].ToString();
                    editorOverview.Content= dt.Rows[0]["Overview"].ToString();
                    
                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";
                }
            }
            catch (Exception e)
            {
                
            }
        }
        void SaveRecord()
        {
            try
            {
                string coverimg = "";
                if (fupload.HasFile)
                {
                    string destDir = Server.MapPath("/store/ebook/cover/");

                    string FileName = Path.GetFileName(fupload.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fupload.SaveAs(destPath);

                    coverimg = fupload.FileName;
                  


                }
                objeb.DType = ddlType.SelectedValue;
                objeb.EBookCatID = int.Parse(ddlCategory.SelectedValue);
                objeb.Title = txtTitle.Text.Trim();
                objeb.CoverPhoto = coverimg;
                objeb.ShortInfo = txtShortInfo.Text.Trim();
                objeb.Author = txtAuthor.Text.Trim();
                objeb.PublishedOn = txtPublishedOn.Text.Trim();
                objeb.NoOfPages =int.Parse(txtNoOfPages.Text.Trim());
                objeb.Overview = editorOverview.Content;
                objeb.SubscriptionPrice = double.Parse(txtSubscriptionPrice.Text.Trim());
                if (chkActive.Checked == true)
                    objeb.Active = 1;
                else
                    objeb.Active = 0;
                objeb.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objeb.InsertEBook();
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
            
            ddlCategory.SelectedIndex = 0;
            txtTitle.Text = "";
            //fupload.FileName = "";
            txtShortInfo.Text = "";
            txtAuthor.Text = "";
            txtPublishedOn.Text = "";
            txtNoOfPages.Text = "";
            txtSubscriptionPrice.Text = "";
            editorOverview.Content = "";
            chkActive.Checked = false;

        }
        void EditRecord(int ID)
        {
            try
            {
                string coverimg = "";
                if (fupload.HasFile)
                {
                    string destDir = Server.MapPath("/store/ebook/cover/");

                    string FileName = Path.GetFileName(fupload.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fupload.SaveAs(destPath);

                    coverimg = fupload.FileName;



                }
                else
                {
                    coverimg = lblCoverUpload.Text;
                }
                objeb.EBookCatID = int.Parse(ddlCategory.SelectedValue);
                objeb.Title = txtTitle.Text.Trim();
                objeb.CoverPhoto = coverimg;
                objeb.ShortInfo = txtShortInfo.Text.Trim();
                objeb.Author = txtAuthor.Text.Trim();
                objeb.PublishedOn = txtPublishedOn.Text.Trim();
                objeb.NoOfPages = int.Parse(txtNoOfPages.Text.Trim());
                objeb.Overview = editorOverview.Content;
                objeb.SubscriptionPrice = double.Parse(txtSubscriptionPrice.Text.Trim());
                if (chkActive.Checked == true)
                    objeb.Active = 1;
                else
                    objeb.Active = 0;
                objeb.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objeb.EditEBook(ID);
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
    }
}