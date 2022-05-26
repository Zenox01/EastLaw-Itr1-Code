using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GemBox.Document;
using GemBox.Document.Drawing;
using System.Text;
using System.IO;
using System.Data;

namespace EastlawUI_v2.adminpanel.printjournals
{
    public partial class CreatePrintableJournal : System.Web.UI.Page
    {
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        EastLawBL.Cases objcases = new EastLawBL.Cases();
        EastLawBL.PrintJournal objpj = new EastLawBL.PrintJournal();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["UserID"] == null)
                    {
                        Response.Redirect("default.aspx");
                    }
                    if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                        Response.Redirect("NotAuthorize.aspx");
                    RemoveSessions();
                    GetJournals();
                    
                    if ((Request.QueryString["param"] != null))
                        GetPrintJournal(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGlossary.aspx", "Page_Load", ex.Message);
            }
        }
        void GetPrintJournal(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objpj.GetPrintJournal(ID);
                if (dt.Rows.Count > 0)
                {


                    txtTitle.Text = dt.Rows[0]["JournalTitle"].ToString();
                    txtRefno.Text = dt.Rows[0]["RefNo"].ToString();
                    txtPwd.Text = dt.Rows[0]["Pwd"].ToString();
                   
                    //editorContent.Content = dt.Rows[0]["FileContent"].ToString();
                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;

                    GetPrintJournalItems(ID);
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageDepartmentFile.aspx", "GetDeptFileDetails", e.Message);
            }
        }
        void GetPrintJournalItems(int JournalID)
        {
            try
            {
                DataTable dtKeywords = new DataTable();
                dtKeywords = objpj.GetJournalItemsByJournalID(JournalID);
                gvCitations.DataSource = dtKeywords;
                gvCitations.DataBind();

            }
            catch { }
        }
        void GetJournals()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJo.GetActiveJournals();

                ddlJournals.DataValueField = "ID";
                ddlJournals.DataTextField = "JournalName";
                ddlJournals.DataSource = dt;
                ddlJournals.DataBind();
                ddlJournals.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("MemberHome.aspx", "GetJournals", e.Message);
            }
        }
        void RemoveSessions()
        {
            Session.Remove("ID");
            Session.Remove("Name");
            Session.Remove("CID");
            Session.Remove("CName");
            Session.Remove("LCID");
            Session.Remove("LCName");
        }
        protected void btnAddCitation_Click(object sender, EventArgs e)
        {

            try
            {
                string cri = "Where A.Citation is not null";

                if (!string.IsNullOrEmpty(txtCitationYear.Text.Trim()))
                    cri = cri + " AND A.Year='" + txtCitationYear.Text.Trim() + "'";

                if (ddlJournals.SelectedValue != "0")
                    cri = cri + " AND A.JournalID='" + ddlJournals.SelectedValue + "'";

                //if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                //    cri = cri + " AND A.Citation like '%"+txtCitationNumber.Text.Trim()+"%'";

                if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                    cri = cri + " AND  CONTAINS (A.Citation, '" + txtCitationNumber.Text.Trim() + "' )";

                DataTable dt = new DataTable();
                dt = objcases.GetCasesSearch(cri, 0, 30);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if ((Request.QueryString["param"] != null))
                        {
                            int chk = objpj.InsertPrintJournalItem(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())),"Case", int.Parse(dt.Rows[0]["ID"].ToString()),1,int.Parse(Session["UserID"].ToString()));

                            DataTable dtCitation = new DataTable();
                            dtCitation = objpj.GetJournalItemsByJournalID(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                            gvCitations.DataSource = dtCitation;
                            gvCitations.DataBind();

                        }
                        else
                        {
                            Session["CID"] += dt.Rows[0]["ID"].ToString() + "|";
                            Session["CName"] += dt.Rows[0]["Citation"].ToString() + " " + dt.Rows[0]["Appeallant"].ToString() + "|";
                            CreateTableValuesCitation();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }




        }
        public void CreateTableValuesCitation()
        {
            try
            {

                string[] sa = Session["CID"].ToString().Split('|');
                string[] sb = Session["CName"].ToString().Split('|');

                int recordnum = sa.Length;

                DataTable dt = new DataTable("tblCitation");
                dt.Columns.Add("ItemID");
                dt.Columns.Add("Citation");
                dt.Columns.Add("ID");
                for (int j = 0; j < recordnum - 1; j++)
                {
                    if (!string.IsNullOrEmpty(sa[j].ToString()))
                    {
                        DataRow dr = dt.NewRow();
                        dr["ItemID"] = sa[j].ToString();
                        dr["Citation"] = sb[j].ToString();
                        dr["ID"] = "0";
                        dt.Rows.Add(dr);
                    }

                }
                ViewState["dtCitation"] = dt;
                DataTable dt1 = (DataTable)ViewState["dtCitation"];
                dt1 = dt.DefaultView.ToTable(true, "ItemID", "Citation", "ID");
                gvCitations.DataSource = dt1;
                gvCitations.DataBind();



                //gvPracticeAreaSubCat.DataSource = dt.DefaultView;
                //gvPracticeAreaSubCat.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "CreateTableValuesPracticeArea", ex.Message);
            }



        }
        void SaveRecord()
        {
            try
            {

                objpj.RefNo = txtRefno.Text.Trim();
                objpj.JournalTitle = txtTitle.Text.Trim();
                objpj.Pwd = txtPwd.Text.Trim();
                
                if (chkActive.Checked == true)
                    objpj.Active = 1;
                else
                    objpj.Active = 0;
                // objkey.CreatedBy = 0;
                objpj.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objpj.InsertPrintJournal();
                if (chk > 0)
                {
                   
                    GetGridItemsCitationsAndSave(chk);
                   
                    //GetGridItemsDocumentsAndSave(chk);
                    GenerateJournal(chk, txtTitle.Text.Trim().Replace(" ", ""));
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();
                    // ClearFields();
                    

                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("AddGlossary.aspx", "SaveRecord", e.Message);
            }
        }
        void EditRecord(int ID)
        {
            try
            {
                objpj.RefNo = txtRefno.Text.Trim();
                objpj.JournalTitle = txtTitle.Text.Trim();
                objpj.Pwd = txtPwd.Text.Trim();

                if (chkActive.Checked == true)
                    objpj.Active = 1;
                else
                    objpj.Active = 0;

                objpj.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objpj.EditPrintJournal(ID);
                if (chk > 0)
                {

                    //GetGridItemsDocumentsAndSave(chk);
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    GenerateJournal(ID, txtTitle.Text.Trim().Replace(" ", ""));
                    ClearFields();
                    // ClearFields();
                    

                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("AddGlossary.aspx", "SaveRecord", e.Message);
            }
        }
        void ClearFields()
        {
            
            txtTitle.Text = "";
            txtRefno.Text = "";
            txtPwd.Text = "";
            
            chkActive.Checked = false;
            
            gvCitations.DataSource = null;
            gvCitations.DataBind();

        }
        void GetGridItemsCitationsAndSave(int JournalID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);

                foreach (GridViewRow row in gvCitations.Rows)
                {
                    if ((row != null))
                    {

                        hdID = (HiddenField)row.FindControl("hdItemID");

                        int chk = objpj.InsertPrintJournalItem(JournalID, "Case", int.Parse(hdID.Value), 1, int.Parse(Session["UserID"].ToString()));


                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Response.Write(ddlKeywords.SelectedValue);
            try
            {
                if (Request.QueryString["param"] == null)
                    SaveRecord();
                else
                    EditRecord(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "btnSave_Click", ex.Message);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        void GenerateJournal(int JournalID, string JournalName )
        {
            try
            {

                DataTable dt = new DataTable();
                dt = objpj.GetJournalItemsByJournalID(JournalID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //string text = System.IO.File.ReadAllText(Server.MapPath("testdt.txt"));

                        ComponentInfo.SetLicense("DAAN-ECJU-1F8U-002M");

                        DocumentModel document = new DocumentModel();



                        document.Sections.Add(
                      new Section(document,
                          new Paragraph(document, dt.Rows[0]["JournalTitle"].ToString())
                          {
                              ParagraphFormat = new ParagraphFormat
                              {
                                  Alignment = HorizontalAlignment.Center
                              }
                          },
                          new Paragraph(document, "This is sample Area")
                          {
                              ParagraphFormat = new ParagraphFormat
                              {
                                  LeftIndentation = 10,
                                  RightIndentation = 10,
                                  //SpecialIndentation = 20,
                                  LineSpacing = 20,
                                  LineSpacingRule = LineSpacingRule.Exactly,
                                  SpaceBefore = 20,
                                  SpaceAfter = 20
                              }
                          }
                          ));

                        int heading1Count = 3;
                        // int heading2Count = 5;

                        // Create and add Heading 1 style.
                        ParagraphStyle heading1Style = (ParagraphStyle)GemBox.Document.Style.CreateStyle(StyleTemplateType.Heading1, document);
                        document.Styles.Add(heading1Style);

                        //// Create and add Heading 2 style.
                        //ParagraphStyle heading2Style = (ParagraphStyle)GemBox.Document.Style.CreateStyle(StyleTemplateType.Heading2, document);
                        //document.Styles.Add(heading2Style);

                        // Create and add TOC style.
                        ParagraphStyle tocHeading = (ParagraphStyle)GemBox.Document.Style.CreateStyle(StyleTemplateType.Heading1, document);
                        tocHeading.Name = "toc";
                        tocHeading.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;
                        document.Styles.Add(tocHeading);

                        Section section = new Section(document);
                        document.Sections.Add(section);

                        // Add TOC title.
                        section.Blocks.Add(
                            new Paragraph(document, "Table of Contents")
                            {
                                ParagraphFormat =
                                {
                                    Style = tocHeading
                                }
                            });

                        // Create and add new TOC.
                        section.Blocks.Add(
                            new TableOfEntries(document, FieldType.TOC));

                        section.Blocks.Add(
                            new Paragraph(document,
                                new SpecialCharacter(document, SpecialCharacterType.PageBreak)));

                        // Add document content.
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            // Heading 1
                            section.Blocks.Add(
                                new Paragraph(document, dt.Rows[i]["Citation"].ToString())
                                {
                                    ParagraphFormat =
                                    {
                                        Style = heading1Style
                                    }
                                });


                            //// Heading 2 content.
                            //section.Blocks.Add(
                            //    new Paragraph(document,
                            //        "GemBox.Document is a .NET component that enables developers to read, write, convert and print document files(DOCX, DOC, PDF, HTML, XPS, RTF and TXT) from .NET applications in a simple and efficient way."));

                            //// Heading 2 content.
                            section.Blocks.Add(new Paragraph(document,
                              new Run(document, "Court: " + dt.Rows[i]["Court"].ToString())
                              {
                                  CharacterFormat = new CharacterFormat()
                                  {
                                      Bold = true
                                  }
                              }
                               ));
                            section.Blocks.Add(new Paragraph(document,
                               new Run(document, "Judge (s): " + dt.Rows[i]["JudgeName"].ToString())
                               {
                                   CharacterFormat = new CharacterFormat()
                                   {
                                       Bold = true
                                   }
                               }
                               ));

                            section.Blocks.Add(new Paragraph(document,
                                 new Run(document, EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[i]["Appeallant"].ToString()) + " VS " + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[i]["Respondent"].ToString()))
                                 {
                                     CharacterFormat = new CharacterFormat()
                                     {
                                         Bold = true
                                     }
                                 }
                                 ));

                            section.Blocks.Add(new Paragraph(document,
                                new Run(document, "Appeal: " + dt.Rows[i]["Appeal"].ToString())
                                {
                                    CharacterFormat = new CharacterFormat()
                                    {
                                        Bold = true
                                    }
                                }
                                ));

                            section.Blocks.Add(new Paragraph(document,
                                new Run(document, "Judgment Date: " + dt.Rows[i]["JDate"].ToString())
                                {
                                    CharacterFormat = new CharacterFormat()
                                    {
                                        Bold = true
                                    }
                                }
                                ));

                            section.Blocks.Add(new Paragraph(document,
                                new Run(document, "Citation No: " + dt.Rows[i]["Citation"].ToString())
                                {
                                    CharacterFormat = new CharacterFormat()
                                    {
                                        Bold = true
                                    }
                                }
                                ));

                            section.Blocks.Add(new Paragraph(document,
                                new Run(document, "Result: " + dt.Rows[i]["Result"].ToString())
                                {
                                    CharacterFormat = new CharacterFormat()
                                    {
                                        Bold = true
                                    }
                                }
                                ));
                            if (!string.IsNullOrEmpty(dt.Rows[i]["CaseSummary"].ToString()))
                            {
                                section.Blocks.Add(new Paragraph(document,
                                    new Run(document, "Summary: ")
                                    {
                                        CharacterFormat = new CharacterFormat()
                                        {
                                            Bold = true
                                        }
                                    }
                                    ));
                                section.Blocks.Add(new Paragraph(document,
                                   new Run(document, dt.Rows[i]["CaseSummary"].ToString())
                                   {
                                       CharacterFormat = new CharacterFormat()
                                       {
                                           Bold = false
                                       }
                                   }
                                   ));
                            }
                            //if(i==0)
                            //section.Content.End.LoadText("<p style='font-family:Calibri;font-size:11pt;text-align: justify;'>" + EastlawUI_v2.CommonClass.GetWords(dt.Rows[i]["Judgment"].ToString(),300) + "</p>", LoadOptions.HtmlDefault);
                            section.Content.End.LoadText("<p style='font-family:Calibri;font-size:10pt;text-align: justify;'>" + dt.Rows[i]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("<table border=\"0\"", "<table border=\"1\"") + "</p>", LoadOptions.HtmlDefault);

                            // Set content using HTML tags
                            //document.Sections[0].Blocks[4].Content.LoadText("Paragraph 5 <b>(part of this paragraph is bold)</b>", LoadOptions.HtmlDefault);

                            //section.Blocks.Add( new Paragraph(document,"<p>This is new Para</p>",));

                            //section.Blocks.Content.LoadText("Paragraph 5 <b>(part of this paragraph is bold)</b>", LoadOptions.HtmlDefault);




                            //for (int j = 0; j < heading2Count; j++)
                            //{
                            //    // Heading 2
                            //    section.Blocks.Add(
                            //        new Paragraph(document, String.Format("Heading 2 ({0}-{1})", i + 1, j + 1))
                            //        {
                            //            ParagraphFormat =
                            //            {
                            //                Style = heading2Style
                            //            }
                            //        });

                            //    // Heading 2 content.
                            //    section.Blocks.Add(
                            //        new Paragraph(document,
                            //            "GemBox.Document is a .NET component that enables developers to read, write, convert and print document files(DOCX, DOC, PDF, HTML, XPS, RTF and TXT) from .NET applications in a simple and efficient way."));
                            //}
                        }

                        // Update TOC (TOC can be updated only after all document content is added).
                        var toc = (TableOfEntries)document.GetChildElements(true, ElementType.TableOfEntries).First();
                        toc.Update();

                        // Update TOC's page numbers.
                        // NOTE: This is not necessary when printing and saving to PDF, XPS or an image format.
                        // Page numbers are automatically updated in that case.
                        document.GetPaginator(new PaginatorOptions() { UpdateFields = true });
                        document.Protection.StartEnforcingProtection(EditingRestrictionType.NoChanges, null);
                        string outputPassword = dt.Rows[0]["Pwd"].ToString();

                        if (string.IsNullOrEmpty(outputPassword))

                            document.Save(Server.MapPath("/adminpanel/printjournals/journals/" + JournalID.ToString() + "_" + JournalName + ".docx"));
                        else
                            document.Save(Server.MapPath("/adminpanel/printjournals/journals/" + JournalID.ToString() + "_" + JournalName + ".docx"), new DocxSaveOptions() { Password = outputPassword });
                    }
                }
            }
            catch(Exception ex) {
                Response.Write(ex.Message);
            }
        }
    }
}