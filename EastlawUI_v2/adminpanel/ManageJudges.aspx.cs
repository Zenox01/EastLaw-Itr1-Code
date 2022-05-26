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
    public partial class ManageJudges : System.Web.UI.Page
    {
        EastLawBL.Judges objJ = new EastLawBL.Judges();
        EastLawBL.Cases objcase = new EastLawBL.Cases();
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
                GetCourtMaster();
                GetJudges(0);
            }
        }
        void GetCourtMaster()
        {
            try
            {
                DataTable dt = new DataTable();
                 dt = objcase.GetActiveCourtMasters();

                 ddlCourtMaster.DataSource = dt;
                 ddlCourtMaster.DataTextField = "CourtName";
                 ddlCourtMaster.DataValueField = "ID";
                 ddlCourtMaster.DataBind();
                ddlCourtMaster.Items.Insert(0, new ListItem("Select", "0"));

                ddlCourtSearch.DataSource = dt;
                ddlCourtSearch.DataTextField = "CourtName";
                ddlCourtSearch.DataValueField = "ID";
                ddlCourtSearch.DataBind();
                ddlCourtSearch.Items.Insert(0, new ListItem("All", "0"));
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "GetJudges", e.Message);
            }
        }
        void GetJudges(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJ.GetJudgeNew(ID);
                DataTable dtCount= objJ.GetJudgeCount();

                if (dtCount != null)
                {
                    lblCount.Text = dtCount.Rows.Count.ToString();
                }
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
                   // dt.AcceptChanges();
                    gv.DataSource = dt;
                    gv.DataBind();
                }
                else
                {
                    lblID.Text = dt.Rows[0]["ID"].ToString();
                    txtName.Text = dt.Rows[0]["JudgeName"].ToString();
                    ddlCourtMaster.SelectedValue=dt.Rows[0]["CurrentCourtMasterID"].ToString();
                    txtStartDate.Text = dt.Rows[0]["ServiceStart"].ToString();
                    txtEndDate.Text = dt.Rows[0]["ServieEnd"].ToString();
                    //Editor1.Content = dt.Rows[0]["Details"].ToString();
                    imgFl.ImageUrl = "/store/judge/" + dt.Rows[0]["profileimg"].ToString();
                    if (dt.Rows[0]["Approve"].ToString() == "1")
                        chkApprove.Checked = true;
                    else
                        chkApprove.Checked = false;
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
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "GetJudges", e.Message);
            }
        }
        void JudgeSearch()
        {
            try
            {
                string cri = "Where ID is not null";

                

                if (ddlCourtSearch.SelectedValue != "0")
                    cri = cri + " AND CurrentCourtMasterID='" + ddlCourtSearch.SelectedValue + "'";


                if (!string.IsNullOrEmpty(txtJudgeNameSearch.Text.Trim()))
                    cri = cri + " AND  judgename like '%" + txtJudgeNameSearch.Text.Trim()+"%' ";

               
                DataTable dt = new DataTable();
                dt = objJ.GetJudgeNewSearch(cri);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        
                        gv.DataSource = dt;
                        gv.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {

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
                    string destDir = Server.MapPath("../store/judge/");

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

                objJ.JudgeName = txtName.Text.Trim();
                objJ.CurrentCourtMasterID = int.Parse(ddlCourtMaster.SelectedValue);
                objJ.ServiceStart = txtStartDate.Text.Trim();
                objJ.ServieEnd = txtEndDate.Text.Trim();
                objJ.ProfileImg = ImageName;
                //objJ.Details = Editor1.Content;
                if (chkApprove.Checked == true)
                    objJ.Approve = 1;
                else
                    objJ.Approve = 0;
                if (chkActive.Checked == true)
                    objJ.Active = 1;
                else
                    objJ.Active = 0;
                objJ.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objJ.InsertJudgeNew();
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    txtName.Text = "";
                    chkActive.Checked = false;
                    chkApprove.Checked = false;
                   // Editor1.Content = "";
                    ddlCourtMaster.SelectedIndex = 0;
                    lblID.Text = "0";
                    //chkActive.Checked = false;
                    GetJudges(0);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "SaveRecord", e.Message);
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
                    string destDir = Server.MapPath("../store/judge/");

                    string FileName = Path.GetFileName(fuploadimage.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fuploadimage.SaveAs(destPath);

                    ImageName = fuploadimage.FileName;
                    ImageType = "Local";


                }
                else
                {
                    ImageName = lblfuploadWord.Text;

                }
                objJ.JudgeName = txtName.Text.Trim();
                objJ.CurrentCourtMasterID = int.Parse(ddlCourtMaster.SelectedValue);
                objJ.ServiceStart = txtStartDate.Text.Trim();
                objJ.ServieEnd = txtEndDate.Text.Trim();
                objJ.ProfileImg = ImageName;
                //objJ.Details = Editor1.Content;
                if (chkApprove.Checked == true)
                    objJ.Approve = 1;
                else
                    objJ.Approve = 0;
                if (chkActive.Checked == true)
                    objJ.Active = 1;
                else
                    objJ.Active = 0;
                objJ.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objJ.EditJudgeNew(ID);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    txtName.Text = "";
                   // Editor1.Content = "";
                    lblID.Text = "0";
                    chkActive.Checked = false;
                    chkApprove.Checked = false;
                    // Editor1.Content = "";
                    ddlCourtMaster.SelectedIndex = 0;
                    //chkActive.Checked = false;
                    GetJudges(0);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "EditRecord", e.Message);
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                if (ddlCourtSearch.SelectedValue != "0")
                    JudgeSearch();
                else
                    GetJudges(0);
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "gv_PageIndexChanging", ex.Message);
            }
        }

        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //GridViewRow row = gv.Rows[e.RowIndex];
                //HiddenField hd = default(HiddenField);

                //if ((row != null))
                //{
                //    hd = (HiddenField)row.FindControl("hdID");
                //    int ID = Convert.ToInt32(hd.Value);

                //    int chk = objJ.DeleteJudge(ID, int.Parse(Session["UserID"].ToString()));
                //    if (chk > 0)
                //    {
                //        divSuccess.Style["Display"] = "";
                //        divError.Style["Display"] = "none";

                //    }
                //    else
                //    {
                //        divSuccess.Style["Display"] = "none";
                //        divError.Style["Display"] = "";
                //    }
                //}
                //gv.EditIndex = -1;
                //GetJudges(0,0,20);

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "gv_RowDeleting", ex.Message);
            }
        }

        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gv.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    GetJudges(ID);


                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "gv_RowEditing", ex.Message);
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                ImageButton imgBtn = default(ImageButton);
                string script = null;
                script = "";

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    imgBtn = (ImageButton)e.Row.Controls[0].FindControl("ibtnDelete");
                    script = "javascript:return(confirm_delete())";
                    imgBtn.Attributes.Add("onclick", script);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "gv_RowDataBound", ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            Editor1.Content = "";
            //chkActive.Checked = false;
            lblID.Text = "0";
            chkActive.Checked = false;
            chkApprove.Checked = false;
            // Editor1.Content = "";
            ddlCourtMaster.SelectedIndex = 0;
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
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "btnSave_Click", ex.Message);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            JudgeSearch();
        }

        protected void btnAll_Click1(object sender, EventArgs e)
        {
            GetJudges(0);
        }
    }
}