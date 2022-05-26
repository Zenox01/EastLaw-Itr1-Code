using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class ManageWebScapStatutes : System.Web.UI.Page
    {
        EastLawBL.Statutes objstat = new EastLawBL.Statutes();
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
                GetStatuesCat(0);
                GetStatuesGroup(0);
                GetStatuesSubGroupByGroup(0);
                GetStatutes(0);
                Session.Remove("StateSearch");
                if (Request.QueryString["sc"] != null)
                {
                    StatutesSearch();
                }
                
            }
        }
        public void GetStatuesCat(int ID)
        {
            try
            {

                DataTable dt = objstat.GetStatutesCategories(ID);
                if (dt.Rows.Count > 0)
                {
                    ddlCat.DataTextField = "CatWithCount";
                    ddlCat.DataValueField = "ID";
                    ddlCat.DataSource = dt;
                    ddlCat.DataBind();
                    ddlCat.Items.Insert(0, new ListItem("All", "0"));
                }
                
                
            }
            catch { }

        }
        public void GetStatuesGroup(int ID)
        {
            try
            {

                DataTable dt = objstat.GetStatutesGroup(ID);
                if (dt.Rows.Count > 0)
                {
                    ddlGroup.DataTextField = "Statutes_Category_Group";
                    ddlGroup.DataValueField = "ID";
                    ddlGroup.DataSource = dt;
                    ddlGroup.DataBind();
                    ddlGroup.Items.Insert(0, new ListItem("All", "0"));
                }


            }
            catch { }

        }
        public void GetStatuesSubGroupByGroup(int GroupID)
        {
            try
            {

                DataTable dt = objstat.GetStatutesSubGroupByGroup(GroupID);
                if (dt.Rows.Count > 0)
                {
                    ddlSubGroup.DataTextField = "Statutes_Category_SubGroup";
                    ddlSubGroup.DataValueField = "ID";
                    ddlSubGroup.DataSource = dt;
                    ddlSubGroup.DataBind();
                    ddlSubGroup.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                    ddlSubGroup.Items.Insert(0, new ListItem("All", "0"));


            }
            catch { }

        }

        void GetStatutes(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objstat.GetStatutes(ID);
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
                ExceptionHandling.SendErrorReport("CasesMigrationList.aspx", "GetCases", e.Message);
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (Session["StateSearch"] != null)
                {
                    if (Session["StateSearch"].ToString() == "Yes")
                    {
                        gv.PageIndex = e.NewPageIndex;
                        StatutesSearch();
                    }
                }
                else
                {
                    gv.PageIndex = e.NewPageIndex;
                    GetStatutes(0);
                }
               
                   
                
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageWebScapStatutes.aspx", "gv_PageIndexChanging", ex.Message);
            }
        }

        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gv.Rows[e.RowIndex];


                if ((row != null))
                {
                    //hd = (HiddenField)row.FindControl("hdID");
                    //int ID = Convert.ToInt32(hd.Value);
                    int ID = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values[0].ToString());
                    int chk = objstat.DeleteStatutes(ID, int.Parse(Session["UserID"].ToString()));
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
                GetStatutes(0);

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageWebScapStatutes.aspx", "gv_RowDeleting", ex.Message);
            }
        }

        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                //gv.EditIndex = e.NewEditIndex;
                //GetStatutes(0);

                    GridViewRow row = gv.Rows[e.NewEditIndex];
                    HiddenField hd = default(HiddenField);

                    if ((row != null))
                    {
                        hd = (HiddenField)row.FindControl("hdID");
                        int ID = Convert.ToInt32(hd.Value);

                        Response.Redirect("AddStatute.aspx?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));



                    }
                

                
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageWebScapStatutes.aspx", "gv_RowEditing", ex.Message);
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                DataTable dtCats = objstat.GetActiveStatutesCategories();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Label lblCat= (Label)e.Row.FindControl("lblCat");
                    //if (lblCat != null)
                    //{
                    //    int catId = Convert.ToInt32(lblCat.Text);
                    //    lblType.Text = (string)contactType.GetTypeById(catId);
                    //}
                    DropDownList ddlEditCat = (DropDownList)e.Row.FindControl("ddlEditCat");
                    if (ddlEditCat != null)
                    {
                        ddlEditCat.DataSource = dtCats;
                        ddlEditCat.DataTextField = "CatName";
                        ddlEditCat.DataValueField = "ID";
                        ddlEditCat.DataBind();
                        ddlEditCat.SelectedValue = gv.DataKeys[e.Row.RowIndex].Values[1].ToString();
                    }
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    DropDownList ddlCatNew = (DropDownList)e.Row.FindControl("ddlNewCat");
                    ddlCatNew.DataSource = dtCats;
                    ddlCatNew.DataBind();
                }

                ImageButton imgBtn = default(ImageButton);
                string script = null;
                script = "";

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    imgBtn = (ImageButton)e.Row.Controls[0].FindControl("ibtnDelete");
                    script = "javascript:return(confirm_delete())";
                    imgBtn.Attributes.Add("onclick", script);
                }

                
                Button btnMakePublicLink = default(Button);
                Button btnDisablePublicLink = default(Button);
                HiddenField hdPublicDisplay = default(HiddenField);

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   
                    hdPublicDisplay = (HiddenField)e.Row.Controls[0].FindControl("hdPublicDisplay");

                    btnMakePublicLink = (Button)e.Row.Controls[0].FindControl("btnMakePublicLink");
                    btnDisablePublicLink = (Button)e.Row.Controls[0].FindControl("btnDisablePublicLink");

                  

                    if (hdPublicDisplay.Value.ToString() == "1")
                    {
                        btnMakePublicLink.Visible = false;
                        btnDisablePublicLink.Visible = true;
                    }
                    else
                    {
                        btnMakePublicLink.Visible = true;
                        btnDisablePublicLink.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageWebScapStatutes.aspx", "gv_RowDataBound", ex.Message);
            }
        }
        protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv.EditIndex = -1;
            GetStatutes(0);
        }
        protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // ContactTableAdapter contact = new ContactTableAdapter();
            // bool flag = false;
            DropDownList ddlCat = (DropDownList)gv.Rows[e.RowIndex].FindControl("ddlEditCat");
            TextBox Title = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditTitle");
            TextBox Date = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditDate");
            TextBox Act = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditAct");
            TextBox TitleVariation1 = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditTitleVariation1");
            TextBox TitleVariation2 = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditTitleVariation2");
            TextBox TitleVariation3 = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditTitleVariation3");
            CheckBox chkActive = (CheckBox)gv.Rows[e.RowIndex].FindControl("chkActive");
            HiddenField hdID = (HiddenField)gv.Rows[e.RowIndex].FindControl("hdID");

            objstat.CatID = int.Parse(ddlCat.SelectedValue);
            objstat.Title = Title.Text.Trim();
            objstat.Date = Date.Text.Trim();
            objstat.Act = Act.Text.Trim();
            if (!string.IsNullOrEmpty(TitleVariation1.Text.Trim()))
                objstat.TitleVariation1 = TitleVariation1.Text.Trim();
            //else
            //    objstat.TitleVariation1 = DBNull.Value.ToString();

            if (!string.IsNullOrEmpty(TitleVariation2.Text.Trim()))
                objstat.TitleVariation2 = TitleVariation2.Text.Trim();
            //else
            //    objstat.TitleVariation2 = DBNull.Value.ToString();

            if (!string.IsNullOrEmpty(TitleVariation3.Text.Trim()))
                objstat.TitleVariation3 = TitleVariation3.Text.Trim();
            //else
            //    objstat.TitleVariation3 = DBNull.Value.ToString();
            if (chkActive.Checked == true)
                objstat.Active = 1;
            else
                objstat.Active = 0;
            objstat.ModifiedBy = int.Parse(Session["UserID"].ToString());
            int chk = objstat.EditStatutes(int.Parse(hdID.Value.ToString()));

            gv.EditIndex = -1;
            GetStatutes(0);

        }
        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ViewIndex")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    //string url = "StatutesIndex.aspx?dis=" + hdnField.Value;
                    string url = "StatutesIndex.aspx?dis=" + val.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( '" + url + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                }
                if (e.CommandName == "EditStatutes")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    
                    Response.Redirect("AddStatute.aspx?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }
                if (e.CommandName == "ManageIndex")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    Response.Redirect("ManageStatutesIndex.aspx?dis=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }
                if (e.CommandName.Equals("Insert"))
                {
                    DropDownList ddlCat = (DropDownList)gv.FooterRow.FindControl("ddlNewCat");
                    TextBox Title = (TextBox)gv.FooterRow.FindControl("txtNewTitle");
                    TextBox Date = (TextBox)gv.FooterRow.FindControl("txtNewDate");
                    TextBox Act = (TextBox)gv.FooterRow.FindControl("txtNewAct");
                    TextBox TitleVariation1 = (TextBox)gv.FooterRow.FindControl("txtNewTitleVariation1");
                    TextBox TitleVariation2 = (TextBox)gv.FooterRow.FindControl("txtNewTitleVariation2");
                    TextBox TitleVariation3 = (TextBox)gv.FooterRow.FindControl("txtNewTitleVariation3");
                    CheckBox chkActive = (CheckBox)gv.FooterRow.FindControl("chkNewActive");

                    objstat.CatID = int.Parse(ddlCat.SelectedValue);
                    objstat.Title = Title.Text.Trim();
                    objstat.Date = Date.Text.Trim();
                    objstat.Act = Act.Text.Trim();
                    if (!string.IsNullOrEmpty(TitleVariation1.Text.Trim()))
                        objstat.TitleVariation1 = TitleVariation1.Text.Trim();
                    else
                        objstat.TitleVariation1 = DBNull.Value.ToString();

                    if (!string.IsNullOrEmpty(TitleVariation2.Text.Trim()))
                        objstat.TitleVariation2 = TitleVariation2.Text.Trim();
                    else
                        objstat.TitleVariation2 = DBNull.Value.ToString();
                    if (!string.IsNullOrEmpty(TitleVariation3.Text.Trim()))
                        objstat.TitleVariation3 = TitleVariation3.Text.Trim();
                    else
                        objstat.TitleVariation3 = DBNull.Value.ToString();
                    if (chkActive.Checked == true)
                        objstat.Active = 1;
                    else
                        objstat.Active = 0;
                    objstat.CreatedBy = int.Parse(Session["UserID"].ToString());
                    int chk = objstat.InsertStatutes();
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

                    GetStatutes(0);
                }

                if (e.CommandName == "MakePublic")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    objstat.UpdateStatutesPublicEnable_Disable(int.Parse(val), 1, int.Parse(Session["UserID"].ToString()));
                    DataTable dt = new DataTable();

                    dt = objstat.GetStatutes(int.Parse(val));
                    lblLnk.Text = "https://eastlaw.pk/public-statutes/" + clsUtilities.RemoveSpecialChars(dt.Rows[0]["Title"].ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["ID"].ToString());
                    lblLnk.Visible = true;



                }
                if (e.CommandName == "DisableMakePublic")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    objstat.UpdateStatutesPublicEnable_Disable(int.Parse(val), 0, int.Parse(Session["UserID"].ToString()));
                    lblLnk.Text = "Case Disabled from Public View";
                    lblLnk.Visible = true;

                }
            }
            catch(Exception ex)
            {

            }
        }

        protected void btnStatutesTaggingWithCases_Click(object sender, EventArgs e)
        {
            try
            {
                int chk = objstat.InsertStatutesTaggingCases();
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";

                }
            }
            catch (Exception ex)
            {

            }
        }
        void StatutesSearch()
        {
            try
            {
                string cri = "Where A.IsDeleted=0";

                if (!string.IsNullOrEmpty(txtID.Text.Trim()))
                    cri = cri + " AND A.ID='" + txtID.Text.Trim() + "'";

                
                if (!string.IsNullOrEmpty(txtTitle.Text.Trim()))
                    cri = cri + " AND CONTAINS(A.Title,'\"" + txtTitle.Text + "\"')";

                if (ddlCat.SelectedValue != "0")
                    cri = cri + " AND A.CatID='" + ddlCat.SelectedValue + "'";

                if (ddlGroup.SelectedValue != "0")
                    cri = cri + " AND A.GroupID='" + ddlGroup.SelectedValue + "'";

                if (ddlSubGroup.SelectedValue != "0")
                    cri = cri + " AND A.SubGroupID='" + ddlSubGroup.SelectedValue + "'";
                if (Request.QueryString["sc"] != null)
                {
                    cri = cri + " AND A.Title like  '" + Request.QueryString["alp"].ToString() + "%'";
                }

                DataTable dt = new DataTable();
                dt = objstat.GetStatutesSearch(cri);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("strActive");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["Active"].ToString() == "1")
                                dt.Rows[a]["strActive"] = "Yes";
                            else
                                dt.Rows[a]["strActive"] = "No";
                        }
                        Session["StateSearch"] = "Yes";
                        lblCount.Text = dt.Rows.Count.ToString();
                        gv.DataSource = dt;
                        gv.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetStatuesSubGroupByGroup(int.Parse(ddlGroup.SelectedValue));
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StatutesSearch();
        }

        protected void btnAll_Click1(object sender, EventArgs e)
        {
            GetStatutes(0);
        }
    }
}