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
    public partial class ManagePrintableJournals : System.Web.UI.Page
    {
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
                    GetJournal(0);
                    //GetGlossory(0);

                }
            }
            catch { }
        }
        void GetJournal(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objpj.GetPrintJournal(ID);
                if (ID == 0)
                {
                    dt.Columns.Add("strActive");
                    dt.Columns.Add("FileName");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["Active"].ToString() == "1")
                            dt.Rows[a]["strActive"] = "Yes";
                        else
                            dt.Rows[a]["strActive"] = "No";
                        dt.Rows[a]["FileName"] = dt.Rows[a]["ID"].ToString() + "_" + dt.Rows[a]["JournalTitle"].ToString().Replace(" ","")+".docx";
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
                GetJournal(0);
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

                    int chk = objpj.DeletePrintJournal(ID, int.Parse(Session["UserID"].ToString()));
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
                GetJournal(0);

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

                    Response.Redirect("CreatePrintableJournal.aspx?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));



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
        void GenerateJournal(int JournalID)
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
                                    CharacterFormat =new CharacterFormat()
                                    {
                                        Bold=true
                                    }
                               }
                               ));
                            section.Blocks.Add(new Paragraph(document,
                               new Run(document, "Judge (s): " + dt.Rows[i]["JudgeName"].ToString())
                               {
                                    CharacterFormat =new CharacterFormat()
                                    {
                                        Bold=true
                                    }
                               }
                               ));

                            section.Blocks.Add(new Paragraph(document, 
                                 new Run(document,EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[i]["Appeallant"].ToString()) + " VS " + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[i]["Respondent"].ToString()))
                                 {
                                     CharacterFormat = new CharacterFormat()
                                     {
                                         Bold = true
                                     }
                                 }
                                 ));

                            section.Blocks.Add(new Paragraph(document,
                                new Run(document,"Appeal: " + dt.Rows[i]["Appeal"].ToString())
                                 {
                                     CharacterFormat = new CharacterFormat()
                                     {
                                         Bold = true
                                     }
                                 }
                                ));

                            section.Blocks.Add(new Paragraph(document,
                                new Run(document,"Judgment Date: " + dt.Rows[i]["JDate"].ToString())
                                 {
                                     CharacterFormat = new CharacterFormat()
                                     {
                                         Bold = true
                                     }
                                 }
                                ));

                            section.Blocks.Add(new Paragraph(document, 
                                new Run(document,"Citation No: " + dt.Rows[i]["Citation"].ToString())
                                    {
                                     CharacterFormat = new CharacterFormat()
                                     {
                                         Bold = true
                                     }
                                 }
                                ));

                            section.Blocks.Add(new Paragraph(document, 
                                new Run(document,"Result: " + dt.Rows[i]["Result"].ToString())
                                {
                                     CharacterFormat = new CharacterFormat()
                                     {
                                         Bold = true
                                     }
                                 }
                                ));

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

                            document.Save(Server.MapPath(dt.Rows[0]["JournalTitle"].ToString() + ".docx"));
                        else
                            document.Save(Server.MapPath(dt.Rows[0]["JournalTitle"].ToString() + ".docx"), new DocxSaveOptions() { Password = outputPassword });
                    }
                }
            }
            catch { }
        }

    }
}