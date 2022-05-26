using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

namespace EastlawUI_v2.adminpanel
{
    public partial class ManageGlossaryList : System.Web.UI.Page
    {
        EastLawBL.Keywords objkey = new EastLawBL.Keywords();
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
                BindTelerikTree(0);
                //GetGlossory(0);

            }
        }
        private void BindTelerikTree(int ID)
        {
            try
            {
                DataTable dtlevel1 = objkey.GetActiveGlossoryByParent(0);
                if (dtlevel1 != null)
                {
                    for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                    {
                        RadTreeNode level1 = new RadTreeNode(dtlevel1.Rows[l1]["GlossoryName"].ToString(), dtlevel1.Rows[l1]["ID"].ToString());
                        tvDept.Nodes.Add(level1);


                        DataTable dtlevel2 = objkey.GetActiveGlossoryByParent(int.Parse(dtlevel1.Rows[l1]["ID"].ToString()));
                        if (dtlevel2 != null)
                        {
                            for (int l2 = 0; l2 < dtlevel2.Rows.Count; l2++)
                            {
                                RadTreeNode level2 = new RadTreeNode(dtlevel2.Rows[l2]["GlossoryName"].ToString(), dtlevel2.Rows[l2]["ID"].ToString());
                                level1.Nodes.Add(level2);

                                DataTable dtlevel3 = objkey.GetActiveGlossoryByParent(int.Parse(dtlevel2.Rows[l2]["ID"].ToString()));
                                if (dtlevel3 != null)
                                {
                                    for (int l3 = 0; l3 < dtlevel3.Rows.Count; l3++)
                                    {
                                        RadTreeNode level3 = new RadTreeNode(dtlevel3.Rows[l3]["GlossoryName"].ToString(), dtlevel3.Rows[l3]["ID"].ToString());
                                        level2.Nodes.Add(level3);
                                    }
                                }
                            }
                        }

                    }
                }
                tvDept.ExpandAllNodes();
            }
            catch (Exception Ex) { throw Ex; }
        }
        void GetGlossory(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objkey.GetGlossory(ID);
                if (ID == 0)
                {
                    dt.Columns.Add("strActive");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["Active"].ToString() == "1")
                            dt.Rows[a]["strActive"] = "Yes";
                        else
                            dt.Rows[a]["strActive"] = "No";
                    }
                    dt.AcceptChanges();
                    lblCount.Text = dt.Rows.Count.ToString();
                    gv.DataSource = dt;
                    gv.DataBind();
                }

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageGlossaryList.aspx", "GetGlossory", e.Message);
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetGlossory(0);
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageGlossaryList.aspx", "gv_PageIndexChanging", ex.Message);
            }
        }

        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gv.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    int chk = objkey.DeleteGlossory(ID, int.Parse(Session["UserID"].ToString()));
                    if (chk > 0)
                    {
                        divSuccess.Style["Display"] = "";
                        divError.Style["Display"] = "none";

                    }
                    else
                    {
                        divSuccess.Style["Display"] = "none";
                        divError.Style["Display"] = "";
                    }
                }
                gv.EditIndex = -1;
                GetGlossory(0);

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageGlossaryList.aspx", "gv_RowDeleting", ex.Message);
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

                    Response.Redirect("AddGlossary.aspx?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));



                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageGlossaryList.aspx", "gv_RowEditing", ex.Message);
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
                ExceptionHandling.SendErrorReport("ManageGlossaryList.aspx", "gv_RowDataBound", ex.Message);
            }
        }
        protected void tvDept_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            switch (e.MenuItem.Value)
            {
                case "Edit":
                    Response.Redirect("AddGlossary.aspx?param="+EncryptDecryptHelper.Encrypt( e.Node.Value.ToString()));

                    break;
                case "Add New":
                    if (e.Node.Level != 0)
                    {
                        Response.Redirect("AddGlossary.aspx?parent=" + EncryptDecryptHelper.Encrypt(e.Node.Value.ToString()));
                        // int chk = objdept.DeleteDepartment(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
                        BindTelerikTree(0);
                        // e.Node.Remove();

                    }
                    else
                    {
                        Response.Redirect("AddGlossary.aspx");
                    }
                    
                    break;
                case "Delete":
                        int chk = objkey.DeleteGlossory(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
                        BindTelerikTree(0);
                        e.Node.Remove();

                    //if (e.Node.Level == 0)
                    //{
                    //    int chk = objkey.DeleteGlossory(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
                    //    BindTelerikTree(0);
                    //    e.Node.Remove();

                    //}
                    //if (e.Node.Level == 1)
                    //{
                    //    int chk = objkey.DeleteGlossory(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
                    //    BindTelerikTree(0);
                    //    e.Node.Remove();

                    //}
                    break;
            }
        }
    }
}