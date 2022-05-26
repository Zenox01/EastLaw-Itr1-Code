using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace EastlawUI_v2.adminpanel
{
    public partial class JudgeMergePanel : System.Web.UI.Page
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
                GetOldTaggesJudges();
                GetCourtMaster();
               
            }
        }
        void GetCourtMaster()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetActiveCourtMasters();

                ddlCourt.DataSource = dt;
                ddlCourt.DataTextField = "CourtName";
                ddlCourt.DataValueField = "ID";
                ddlCourt.DataBind();

                ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
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
                dt = objJ.GetJudgeNewByCourtMaster(CourtMasterID);

                ddlNewJudge.DataSource = dt;
                ddlNewJudge.DataTextField = "JudgeName";
                ddlNewJudge.DataValueField = "ID";
                ddlNewJudge.DataBind();

                ddlNewJudge.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "GetJudges", e.Message);
            }
        }
        void GetOldTaggesJudges()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJ.GetMergeJudgesOld();

                ddlOldTaggesJudges.DataSource = dt;
                ddlOldTaggesJudges.DataTextField = "JudgeName";
                ddlOldTaggesJudges.DataValueField = "JudgesVal";
                ddlOldTaggesJudges.DataBind();

                ddlOldTaggesJudges.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception e)
            {
                
            }
        }
        void GetTaggesCasesOldByJudges(string JudgeName,int CourtMaster)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJ.GetCasesMergeJudgesOldByJudgeName(JudgeName,CourtMaster);

                gv.DataSource = dt;
                gv.DataBind();

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "GetJudges", e.Message);
            }
        }

        protected void ddlOldTaggesJudges_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetTaggesCasesOldByJudges(ddlOldTaggesJudges.SelectedValue,int.Parse(ddlCourt.SelectedValue));
            }
            catch { }

        }
        void GetSelectedDataAndMove(int NewJudgeID)
        {
            try
            {
              
                HiddenField hdTaggedIDOld = default(HiddenField);
                Label lblCaseID = default(Label);
                CheckBox chkSel = default(CheckBox);
               

                foreach (GridViewRow row in gv.Rows)
                {
                    if ((row != null))
                    {
                        string str = "";
                        hdTaggedIDOld = (HiddenField)row.FindControl("hdTaggedIDOld");
                        lblCaseID = (Label)row.FindControl("lblCaseID");
                        chkSel = (CheckBox)row.FindControl("chkSel");
                        int chk = 0;
                        if (chkSel.Checked == true)
                        {
                            chk = objJ.TaggeCaseinJudgesNew(NewJudgeID, int.Parse(lblCaseID.Text), int.Parse(Session["UserID"].ToString()), int.Parse(hdTaggedIDOld.Value.ToString()), ref str);
                        }



                    }
                }
                divSuccess.Style["Display"] = "";
                divError.Style["Display"] = "none";
                GetOldTaggesJudges();
                gv.DataSource = null;
                gv.DataBind();

            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //txtName.Text = "";
            //Editor1.Content = "";
            ////chkActive.Checked = false;
            //lblID.Text = "0";
            //chkActive.Checked = false;
            //chkApprove.Checked = false;
            //// Editor1.Content = "";
            //ddlCourtMaster.SelectedIndex = 0;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                GetSelectedDataAndMove(int.Parse(ddlNewJudge.SelectedValue));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "btnSave_Click", ex.Message);
            }
        }

        protected void ddlCourt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetJudgesByCourtMaster(int.Parse(ddlCourt.SelectedValue));
            }
            catch { }
        }
    }
}