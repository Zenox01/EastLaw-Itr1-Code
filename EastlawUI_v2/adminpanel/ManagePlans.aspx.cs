using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class ManagePlans : System.Web.UI.Page
    {
        EastLawBL.Plans objPlan = new EastLawBL.Plans();
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
                GetPlans(0);
            }
        }
        void GetPlans(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPlan.GetPlans(ID);
                if (ID == 0)
                {
                    dt.Columns.Add("strActive");
                    dt.Columns.Add("strShowFront");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["Active"].ToString() == "1")
                            dt.Rows[a]["strActive"] = "Yes";
                        else
                            dt.Rows[a]["strActive"] = "No";

                        if (dt.Rows[a]["ShowOnFront"].ToString() == "1")
                            dt.Rows[a]["strShowFront"] = "Yes";
                        else
                            dt.Rows[a]["strShowFront"] = "No";
                    }
                    dt.AcceptChanges();
                    gv.DataSource = dt;
                    gv.DataBind();
                }
                else
                {
                    lblID.Text = dt.Rows[0]["ID"].ToString();
                    ddlPlanType.SelectedValue = dt.Rows[0]["PlanType"].ToString();
                    txtPlanName.Text = dt.Rows[0]["PlanName"].ToString();
                    txtNoOfDays.Text = dt.Rows[0]["NoofDays"].ToString();
                    txtPrice.Text = dt.Rows[0]["Price"].ToString();
                    txtNoOfUsers.Text = dt.Rows[0]["NoOfUsers"].ToString();
                    txtNoOfLoginPerday.Text = dt.Rows[0]["nooflogin_perday"].ToString();
                    txtNoOfCasesPerDay.Text = dt.Rows[0]["noofcasesview_perday"].ToString();
                    txtnoofstatutesview_perday.Text = dt.Rows[0]["noofstatutesview_perday"].ToString();
                    txtTax.Text = dt.Rows[0]["Tax"].ToString();
                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;

                    if (dt.Rows[0]["ShowOnFront"].ToString() == "1")
                        chkShowFront.Checked = true;
                    else
                        chkShowFront.Checked = false;
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePlans.aspx", "GetPlans", e.Message);
            }
        }
        void SaveRecord()
        {
            try
            {
                objPlan.PlanType = ddlPlanType.SelectedValue;
                objPlan.PlanName = txtPlanName.Text.Trim();
                objPlan.NoofDays = int.Parse(txtNoOfDays.Text);
                objPlan.Price = int.Parse(txtPrice.Text);
                objPlan.NoOfUsers = int.Parse(txtNoOfUsers.Text);
                objPlan.Tax = float.Parse(txtTax.Text);
                objPlan.nooflogin_perday = int.Parse(txtNoOfLoginPerday.Text);
                objPlan.noofcasesview_perday = int.Parse(txtNoOfCasesPerDay.Text);
                objPlan.noofstatutesview_perday = int.Parse(txtnoofstatutesview_perday.Text);
                if (chkActive.Checked == true)
                    objPlan.Active = 1;
                else
                    objPlan.Active = 0;
                if (chkShowFront.Checked == true)
                    objPlan.ShowOnFront = 1;
                else
                    objPlan.ShowOnFront = 0;
                objPlan.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objPlan.InsertPlan();
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();
                    GetPlans(0);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePlans.aspx", "SaveRecord", e.Message);
            }
        }
        void ClearFields()
        {
            ddlPlanType.SelectedIndex = 0;
            txtPlanName.Text = "";
            txtNoOfDays.Text = "";
            txtPrice.Text = "";
            lblID.Text = "0";
            chkActive.Checked = false;
            chkShowFront.Checked = false;
            txtNoOfUsers.Text = "0";
            txtNoOfLoginPerday.Text = "";
            txtNoOfCasesPerDay.Text = "";
            txtnoofstatutesview_perday.Text = "";
            txtTax.Text = "0";
 
        }
        void EditRecord(int ID)
        {
            try
            {
                objPlan.PlanType = ddlPlanType.SelectedValue;
                objPlan.PlanName = txtPlanName.Text.Trim();
                objPlan.NoofDays = int.Parse(txtNoOfDays.Text);
                objPlan.Price = int.Parse(txtPrice.Text);
                objPlan.nooflogin_perday = int.Parse(txtNoOfLoginPerday.Text);
                objPlan.NoOfUsers = int.Parse(txtNoOfUsers.Text);
                objPlan.noofcasesview_perday = int.Parse(txtNoOfCasesPerDay.Text);
                objPlan.noofstatutesview_perday = int.Parse(txtnoofstatutesview_perday.Text);
                objPlan.Tax = float.Parse(txtTax.Text);
                if (chkActive.Checked == true)
                    objPlan.Active = 1;
                else
                    objPlan.Active = 0;
                if (chkShowFront.Checked == true)
                    objPlan.ShowOnFront = 1;
                else
                    objPlan.ShowOnFront = 0;
                objPlan.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objPlan.EditPlan(ID);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();
                    GetPlans(0);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePlans.aspx", "EditRecord", e.Message);
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetPlans(0);
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManagePlans.aspx", "gv_PageIndexChanging", ex.Message);
            }
        }

        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //try
            //{
            //    GridViewRow row = gv.Rows[e.RowIndex];
            //    HiddenField hd = default(HiddenField);

            //    if ((row != null))
            //    {
            //        hd = (HiddenField)row.FindControl("hdID");
            //        int ID = Convert.ToInt32(hd.Value);

            //        int chk = objPA.DeletePracticeAreaCat(ID, int.Parse(Session["UserID"].ToString()));
            //        if (chk > 0)
            //        {
            //            divSuccess.Style["Display"] = "";
            //            divError.Style["Display"] = "none";

            //        }
            //        else
            //        {
            //            divSuccess.Style["Display"] = "none";
            //            divError.Style["Display"] = "";
            //        }
            //    }
            //    gv.EditIndex = -1;
            //    GetPracticeCategories(0);

            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandling.SendErrorReport("ManagePlans.aspx", "gv_RowDeleting", ex.Message);
            //}
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

                    GetPlans(ID);


                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManagePlans.aspx", "gv_RowEditing", ex.Message);
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    ImageButton imgBtn = default(ImageButton);
            //    string script = null;
            //    script = "";

            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        imgBtn = (ImageButton)e.Row.Controls[0].FindControl("ibtnDelete");
            //        script = "javascript:return(confirm_delete())";
            //        imgBtn.Attributes.Add("onclick", script);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandling.SendErrorReport("ManagePlans.aspx", "gv_RowDataBound", ex.Message);
            //}
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
                ExceptionHandling.SendErrorReport("ManagePlans.aspx", "btnSave_Click", ex.Message);
            }
        }
    }
}