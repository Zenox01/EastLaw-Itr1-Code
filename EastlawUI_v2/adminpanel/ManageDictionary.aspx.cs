using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SQLite;
using System.IO;


namespace EastlawUI_v2.adminpanel
{
    public partial class ManageDictionary : System.Web.UI.Page
    {
        EastLawBL.Dictionary objDic = new EastLawBL.Dictionary();
        private SQLiteConnection sqlite;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("default.aspx");
                }
                if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                    Response.Redirect("NotAuthorize.aspx");
               // InsertFromFile();
               
                GetDictionary(0);
                
            }
        }
        public void GetDictionary(int ID)
        {
            try
            {

                DataTable dt = objDic.GetDictionaryWord(ID);
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
                    dt.AcceptChanges();
                    grdContact.DataSource = dt;
                    grdContact.DataBind();
                }
                else
                {
                    dt.Rows.Add(dt.NewRow());
                    grdContact.DataSource = dt;
                    grdContact.DataBind();

                    int TotalColumns = grdContact.Rows[0].Cells.Count;
                    grdContact.Rows[0].Cells.Clear();
                    grdContact.Rows[0].Cells.Add(new TableCell());
                    grdContact.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                    grdContact.Rows[0].Cells[0].Text = "No Record Found";
                }
            }
            catch { }

        }
        void InsertFromFile()
        {
            DataTable dt = ExtractDataFromUploadedFile("dictionary.sqlite", "Select * from data4");
            for(int a=0;a < dt.Rows.Count;a++)
            {
                if(!string.IsNullOrEmpty(dt.Rows[a]["word"].ToString()) && !string.IsNullOrEmpty(dt.Rows[a]["mean"].ToString()))
                {
                    objDic.Word = dt.Rows[a]["word"].ToString().Trim();
                    objDic.Meaning = dt.Rows[a]["mean"].ToString().Trim();
                    
                        objDic.Active = 1;

                        objDic.CreatedBy = 0;
                    int chk = objDic.InsertDictionaryWord();
                }
            }
        }
        DataTable ExtractDataFromUploadedFile(string FileName, string Query)
        {
            string dbString = "Data Source=" + Server.MapPath("dbfilesothers/" + FileName + "");
            sqlite = new SQLiteConnection(dbString);

            DataTable dt = new DataTable();
            //dt = selectQuery("Select * from data");
            dt = selectQuery(Query);

          
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
        protected void grdContact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //ContactTypeTableAdapter contactType = new ContactTypeTableAdapter();
            //DataTable contactTypes = contactType.GetData();
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblType = (Label)e.Row.FindControl("lblType");
            //    if (lblType != null)
            //    {
            //        int typeId = Convert.ToInt32(lblType.Text);
            //        lblType.Text = (string)contactType.GetTypeById(typeId);
            //    }
            //    DropDownList cmbType = (DropDownList)e.Row.FindControl("cmbType");
            //    if (cmbType != null)
            //    {
            //        cmbType.DataSource = contactTypes;
            //        cmbType.DataTextField = "TypeName";
            //        cmbType.DataValueField = "Id";
            //        cmbType.DataBind();
            //        cmbType.SelectedValue = grdContact.DataKeys[e.Row.RowIndex].Values[1].ToString();
            //    }
            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    DropDownList cmbNewType = (DropDownList)e.Row.FindControl("cmbNewType");
            //    cmbNewType.DataSource = contactTypes;
            //    cmbNewType.DataBind();
            //}
        }
        protected void grdContact_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdContact.EditIndex = -1;
            GetDictionary(0);
        }
        protected void grdContact_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // ContactTableAdapter contact = new ContactTableAdapter();
            // bool flag = false;

            TextBox txtEditWord = (TextBox)grdContact.Rows[e.RowIndex].FindControl("txtEditWord");
            TextBox txtEditMeaning = (TextBox)grdContact.Rows[e.RowIndex].FindControl("txtEditMeaning");
            CheckBox chkActive = (CheckBox)grdContact.Rows[e.RowIndex].FindControl("chkActive");
            HiddenField hdID = (HiddenField)grdContact.Rows[e.RowIndex].FindControl("hdID");

            //objkey.ID = int.Parse(hdID.Value.ToString());
            objDic.Word = txtEditWord.Text.Trim();
            objDic.Meaning = txtEditMeaning.Text.Trim();
            if (chkActive.Checked == true)
                objDic.Active = 1;
            else
                objDic.Active = 0;
            objDic.ModifiedBy = int.Parse(Session["UserID"].ToString());
            int chk = objDic.EditDictionaryWord(int.Parse(hdID.Value.ToString()));

            grdContact.EditIndex = -1;
            GetDictionary(0);

        }
        protected void grdContact_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //ContactTableAdapter contact = new ContactTableAdapter();
            //int id = Convert.ToInt32(grdContact.DataKeys[e.RowIndex].Values[0].ToString());
            //int chk = obj
            //contact.Delete(id);
            //FillGrid();


            try
            {
                GridViewRow row = grdContact.Rows[e.RowIndex];


                if ((row != null))
                {
                    //hd = (HiddenField)row.FindControl("hdID");
                    //int ID = Convert.ToInt32(hd.Value);
                    int ID = Convert.ToInt32(grdContact.DataKeys[e.RowIndex].Values[0].ToString());
                    int chk = objDic.DeleteDictionaryWord(ID, int.Parse(Session["UserID"].ToString()));
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
                GetDictionary(0);

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "gv_RowDeleting", ex.Message);
            }
        }
        protected void grdContact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //ContactTableAdapter contact = new ContactTableAdapter();
            //bool flag = false;
            if (e.CommandName.Equals("Insert"))
            {
                TextBox txtNewWord = (TextBox)grdContact.FooterRow.FindControl("txtNewWord");
                TextBox txtNewMeaning = (TextBox)grdContact.FooterRow.FindControl("txtNewMeaning");
                CheckBox chkNewActive = (CheckBox)grdContact.FooterRow.FindControl("chkNewActive");

                objDic.Word= txtNewWord.Text.Trim();
                objDic.Meaning = txtNewMeaning.Text.Trim();
                if (chkNewActive.Checked == true)
                    objDic.Active = 1;
                else
                    objDic.Active = 0;
                objDic.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objDic.InsertDictionaryWord();
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

                GetDictionary(0);
            }
        }
        protected void grdContact_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdContact.EditIndex = e.NewEditIndex;
            GetDictionary(0);
        }

        //protected void btnKeywordTagging_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int chk = objkey.UpdateKeywords();
        //        if (chk > 0)
        //        {
        //            divSuccess.Style["Display"] = "";
        //            divError.Style["Display"] = "none";

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}