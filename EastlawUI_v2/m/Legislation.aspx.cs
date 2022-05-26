using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.m
{
    public partial class Legislation : System.Web.UI.Page
    {
        EastLawBL.Statutes objstate = new EastLawBL.Statutes();
        EastLawBL.Departments objdpt = new EastLawBL.Departments();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Validate();
                // LoadYears();
                GetStatutesCat();

            }
        }
        void GetStatutesCat()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objstate.GetActiveStatutesCategories();

                ddlStatutesCat.DataValueField = "ID";
                ddlStatutesCat.DataTextField = "CatName";
                ddlStatutesCat.DataSource = dt;
                ddlStatutesCat.DataBind();
                ddlStatutesCat.Items.Insert(0, new ListItem("All", "0"));

            }
            catch (Exception e)
            {
            }
        }
        void LoadYears()
        {
            // Clear items:    
            ddlSROYear.Items.Clear();
            ddlCircularYear.Items.Clear();
            // Add default item to the list
            ddlSROYear.Items.Insert(0, new ListItem("Select Year", "0"));
            ddlCircularYear.Items.Insert(0, new ListItem("Select Year", "0"));
            // Start loop
            for (int i = 0; i < 69; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlSROYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
                ddlCircularYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
            }
        }
        void Validate()
        {
            if (Session["MemberID"] != null)
            {
                divWithLogin.Style["Display"] = "";
                divWithoutlogin.Style["Display"] = "none";
                LoadYears();
            }
            else
            {
                divWithLogin.Style["Display"] = "none";
                divWithoutlogin.Style["Display"] = "";
            }
            //divWithLogin.Style["Display"] = "";
            //divWithoutlogin.Style["Display"] = "none";
        }

        protected void btnSearchTitle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTitle.Text.Trim()))
                {
                    string cri = "Where A.IsDeleted=0";

                    if (!string.IsNullOrEmpty(txtTitle.Text.Trim()))
                        cri = cri + " AND CONTAINS(A.Title,'\"" + txtTitle.Text + "\"')";

                    //if (chkPrimTitle.Checked == true && chkSecTitle.Checked == true)
                    //    cri = cri + " AND (A.Pri_Sec='PRIMARY' OR A.Pri_Sec='SECONDARY')";

                    //if (chkPrimTitle.Checked == true)
                    //    cri = cri + " AND A.Pri_Sec='PRIMARY'";

                    //if (chkSecTitle.Checked == true)
                    //    cri = cri + " AND A.Pri_Sec='SECONDARY'";



                    DataTable dt = new DataTable();
                    dt = objstate.GetStatutesSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Session["StatutesSearch"] = dt;
                            Response.Redirect("/m/Statutes/Search-Result");
                        }
                    }
                }

            }
            catch { }
        }

        protected void btnSearchFreetext_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFreeText.Text.Trim()))
                {
                    string cri1 = "Where A.IsDeleted=0";
                    string cri2 = "Where A.IsDeleted=0";

                    if (!string.IsNullOrEmpty(txtFreeText.Text.Trim()))
                    {
                        cri1 = cri1 + " AND CONTAINS(A.Title,'\"" + txtFreeText.Text + "\"')  or  CONTAINS(A.Cntnt,'\"" + txtFreeText.Text + "\"') or CONTAINS(F.IndexTitle,'\"" + txtFreeText.Text + "\"') or CONTAINS(F.IndexContent,'\"" + txtFreeText.Text + "\"')";
                        cri2 = cri2 + " AND CONTAINS(A.Title,'\"" + txtFreeText.Text + "\"')  or  CONTAINS(A.Cntnt,'\"" + txtFreeText.Text + "\"') ";
                    }

                    if (chkPrimFreeText.Checked == true)
                    {
                        cri1 = cri1 + " AND A.Pri_Sec='PRIMARY'";
                        cri2 = cri2 + " AND A.Pri_Sec='PRIMARY'";
                    }

                    if (chkSecFreeText.Checked == true)
                    {
                        cri1 = cri1 + " AND A.Pri_Sec='SECONDARY'";
                        cri2 = cri2 + " AND A.Pri_Sec='SECONDARY'";
                    }


                    DataTable dt = new DataTable();
                    dt = objstate.GetStatutesSearchFreeText(cri1, cri2);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Session["StatutesSearch"] = dt;
                            Response.Redirect("/m/Statutes/Search-Result");
                        }
                    }
                }

            }
            catch { }
        }

        protected void btnSearchWithinIndex_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchWithinIndex.Text.Trim()))
                {
                    string cri = "Where A.IsDeleted=0";

                    if (!string.IsNullOrEmpty(txtSearchWithinIndex.Text.Trim()))
                        cri = cri + " AND CONTAINS(F.IndexTitle,'\"" + txtSearchWithinIndex.Text + "\"') OR CONTAINS(F.IndexContent,'\"" + txtSearchWithinIndex.Text + "\"')";

                    DataTable dt = new DataTable();
                    dt = objstate.GetStatutesSearchWithinIndex(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Session["StatutesSearch"] = dt;
                            Response.Redirect("/m/Statutes/Search-Result");
                        }
                    }
                }

            }
            catch { }
        }

        protected void btnSRO_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string cri = "Where A.IsDeleted=0";

            //    if (!string.IsNullOrEmpty(txtSRONo.Text.Trim()))
            //        cri = cri + " AND CONTAINS(A.Title,'\"" + txtSRONo.Text + "\"') AND A.SType='SRO'";

            //    if (ddlSROYear.SelectedValue != "0")
            //        cri = cri + " AND A.SYear='" + ddlSROYear.SelectedValue + "'";


            //    DataTable dt = new DataTable();
            //    dt = objstate.GetStatutesSearch(cri);
            //    if (dt != null)
            //    {
            //        if (dt.Rows.Count > 0)
            //        {
            //            Session["StatutesSearch"] = dt;
            //            Response.Redirect("/Statutes/Search-Result");
            //        }
            //    }

            //}
            //catch { }
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (!string.IsNullOrEmpty(txtSRONo.Text.Trim()))
                {

                    cri = cri + " AND A.DType='S.R.O.'";
                    cri = cri + " AND  CONTAINS(A.No,'\"" + txtSRONo.Text + "\"')";
                    if (ddlSROYear.SelectedValue != "0")
                        cri = cri + " AND  A.Year='" + ddlSROYear.SelectedValue + "'";

                    DataTable dt = new DataTable();
                    dt = objdpt.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Session["DeptSearch"] = dt;
                            Session["DeptSearchKeyWord"] = "SRO From Legislation";
                            Response.Redirect("/m/Departments/Search-Result");
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCircular_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string cri = "Where A.IsDeleted=0";

            //    if (!string.IsNullOrEmpty(txtTitle.Text.Trim()))
            //        cri = cri + " AND CONTAINS(A.Title,'\"" + txtTitle.Text + "\"')  and AND A.SType='Circular'";

            //    if (ddlCircularYear.SelectedValue != "0")
            //        cri = cri + " AND A.SYear='" + ddlCircularYear.SelectedValue + "'";


            //    DataTable dt = new DataTable();
            //    dt = objstate.GetStatutesSearch(cri);
            //    if (dt != null)
            //    {
            //        if (dt.Rows.Count > 0)
            //        {
            //            Session["StatutesSearch"] = dt;
            //            Response.Redirect("/Statutes/Search-Result");
            //        }
            //    }

            //}
            //catch { }
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (!string.IsNullOrEmpty(txtCircularNo.Text.Trim()))
                {

                    cri = cri + " AND A.DType='CIRCULAR NO.'";
                    cri = cri + " AND  CONTAINS(A.No,'\"" + txtCircularNo.Text + "\"')";
                    if (ddlCircularYear.SelectedValue != "0")
                        cri = cri + " AND  A.Year='" + ddlCircularYear.SelectedValue + "'";

                    DataTable dt = new DataTable();
                    dt = objdpt.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Session["DeptSearch"] = dt;
                            Session["DeptSearchKeyWord"] = "Circular From Legislation";
                            Response.Redirect("/m/Departments/Search-Result");
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSearchTitle_Click1(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTitle.Text.Trim()) || !string.IsNullOrEmpty(txtYear.Text.Trim()) || ddlStatutesCat.SelectedValue != "0")
                {
                    string cri = "Where A.IsDeleted=0 and A.Active=1";

                    if (!string.IsNullOrEmpty(txtTitle.Text.Trim()))
                        cri = cri + " AND CONTAINS(A.Title,'\"" + txtTitle.Text + "\"')";

                    if (!string.IsNullOrEmpty(txtYear.Text.Trim()))
                        cri = cri + " AND CONTAINS(A.Title,'\"" + txtYear.Text + "\"')";


                    //if (!string.IsNullOrEmpty(txtYear.Text.Trim()))
                    //    cri = cri + " AND  A.Year='" + txtYear.Text.Trim() + "'";

                    if (ddlStatutesCat.SelectedValue != "0")
                        cri = cri + " AND  A.CatID=" + ddlStatutesCat.SelectedValue + "";

                    //if (chkPrimTitle.Checked == true && chkSecTitle.Checked == true)
                    //    cri = cri + " AND (A.Pri_Sec='PRIMARY' OR A.Pri_Sec='SECONDARY')";

                    //if (chkPrimTitle.Checked == true)
                    //    cri = cri + " AND A.Pri_Sec='PRIMARY'";

                    //if (chkSecTitle.Checked == true)
                    //    cri = cri + " AND A.Pri_Sec='SECONDARY'";



                    DataTable dt = new DataTable();
                    dt = objstate.GetStatutesSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Session["StatutesSearch"] = dt;
                            Response.Redirect("/m/Statutes/Search-Result");
                        }
                    }

                }

            }
            catch { }
        }
    }
}