<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        Application["UsersLoggedIn"] = new System.Collections.Generic.List<string>();
        // Code that runs on application startup
        RegisterRoutes(System.Web.Routing.RouteTable.Routes);

    }
    private void RegisterRoutes(System.Web.Routing.RouteCollection routes)
    {
        //RouteCollection provides a collection of Routes for ASP.NET Routing
        //MapPageRoute(string RouteName,string routeUrl, string physicalFile)
        //routes.MapPageRoute("Browse-Items", "Items/{name}", "~/Page.aspx")

        //routes.Ignore("{*allaspx}", new {allaspx=@".*\.aspx(/.*)?"})
        //routes.Ignore("{*favicon}", new {favicon=@"(.*/)?favicon.ico(/.*)?"})

        routes.Ignore("{file}.js");
        routes.Ignore("{file}.png");
        routes.Ignore("adminpanel/*");

        routes.Ignore("css/{*pathInfo}");
        routes.Ignore("images/{*pathInfo}");
        routes.Ignore("media/{*pathInfo}");
        routes.Ignore("js/{*pathInfo}");

        routes.Ignore("{resource}.axd/{*pathInfo}");
        routes.Ignore("Telerik.Web.UI.DialogHandler.aspx");
        routes.Ignore("WebResource.axd");
        //routes.Ignore("SearchFill.asmx/{*pathInfo}");

        //routes.Ignore("images/*")
        //routes.Ignore("media/*")
        //routes.Ignore("css/*")
        //routes.Ignore("js/*")



        //routes.Add("Default", new System.Web.Routing.Route("{Page1}", new PropertyRewrite()));

        routes.Add("BecomeMember", new System.Web.Routing.Route("member/{BecomeMember}", new PropertyRewrite()));
        routes.Add("Dept", new System.Web.Routing.Route("departments/{Dept}", new PropertyRewrite()));
        routes.Add("DeptDT", new System.Web.Routing.Route("departments/{Dept1}/{DeptGroup}", new PropertyRewrite()));
        routes.Add("Citations", new System.Web.Routing.Route("cases/{Citations}", new PropertyRewrite()));
        routes.Add("PCitations", new System.Web.Routing.Route("public-cases/{PCitations}", new PropertyRewrite()));
        routes.Add("Pstatutes", new System.Web.Routing.Route("public-statutes/{Pstatutes}", new PropertyRewrite()));
        routes.Add("Statutes", new System.Web.Routing.Route("statutes/{Statutes}", new PropertyRewrite()));
        routes.Add("StatutesIndex", new System.Web.Routing.Route("statutes/{Statutes}/{StatutesIndex}", new PropertyRewrite()));
        //routes.Add("StatutesIndexFull", new System.Web.Routing.Route("Statutes/{Statutes}/Full-Content/{StatutesIndex}", new PropertyRewrite()));

        routes.Add("Search", new System.Web.Routing.Route("search/{SearchResult}", new PropertyRewrite()));

        routes.Add("SearchCitation", new System.Web.Routing.Route("search/Citation/{SearchCitation}", new PropertyRewrite()));
        routes.Add("ByPages", new System.Web.Routing.Route("en/{page}", new PropertyRewrite()));
        routes.Add("ByDic", new System.Web.Routing.Route("dictionary/{word}", new PropertyRewrite()));
        routes.Add("General", new System.Web.Routing.Route("{General}", new PropertyRewrite()));
        routes.Add("PA", new System.Web.Routing.Route("practice-area/{PA}", new PropertyRewrite()));
        routes.Add("News", new System.Web.Routing.Route("news/{News}", new PropertyRewrite()));
        routes.Add("PASearch", new System.Web.Routing.Route("practice-area/search/{PASearchResult}", new PropertyRewrite()));
        routes.Add("Restricted", new System.Web.Routing.Route("restricted/{RestrictedRegion}", new PropertyRewrite()));
        routes.Add("Corporate", new System.Web.Routing.Route("corporate/{CorporateEntity}", new PropertyRewrite()));
        routes.Add("ebook", new System.Web.Routing.Route("ebook/{ebookcat}/{ebookdetails}", new PropertyRewrite()));

        //routes.Add("MMember", new System.Web.Routing.Route("m/member/{MemberElements}", new PropertyRewrite()));
        //routes.Add("MCitations", new System.Web.Routing.Route("m/cases/{MCitations}", new PropertyRewrite()));
        //routes.Add("MSearch", new System.Web.Routing.Route("m/search/{MSearchResult}", new PropertyRewrite()));
        //routes.Add("MGeneral", new System.Web.Routing.Route("m/{MGeneral}", new PropertyRewrite()));
        //routes.Add("MPA", new System.Web.Routing.Route("m/practice-area/{MPA}", new PropertyRewrite()));
        //routes.Add("MDept", new System.Web.Routing.Route("m/departments/{MDept}", new PropertyRewrite()));
        //routes.Add("MDeptDT", new System.Web.Routing.Route("m/departments/{MDept1}/{MDeptGroup}", new PropertyRewrite()));
        //routes.Add("MPASearch", new System.Web.Routing.Route("m/practice-Area/Search/{MPASearchResult}", new PropertyRewrite()));
        //routes.Add("MStatutes", new System.Web.Routing.Route("m/statutes/{MStatutes}", new PropertyRewrite()));
        //routes.Add("MStatutesIndex", new System.Web.Routing.Route("m/statutes/{MStatutes}/{MStatutesIndex}", new PropertyRewrite()));
        //routes.Add("MByPages", new System.Web.Routing.Route("m/en/{Mpage}", new PropertyRewrite()));

        routes.Add("MMember", new System.Web.Routing.Route("m/Member/{MemberElements}", new PropertyRewrite()));
        routes.Add("MCitations", new System.Web.Routing.Route("m/Cases/{MCitations}", new PropertyRewrite()));
        routes.Add("MSearch", new System.Web.Routing.Route("m/Search/{MSearchResult}", new PropertyRewrite()));
        routes.Add("MGeneral", new System.Web.Routing.Route("m/{MGeneral}", new PropertyRewrite()));
        routes.Add("MPA", new System.Web.Routing.Route("m/Practice-Area/{MPA}", new PropertyRewrite()));
        routes.Add("MDept", new System.Web.Routing.Route("m/Departments/{MDept}", new PropertyRewrite()));
        routes.Add("MDeptDT", new System.Web.Routing.Route("m/Departments/{MDept1}/{MDeptGroup}", new PropertyRewrite()));
        routes.Add("MPASearch", new System.Web.Routing.Route("m/Practice-Area/Search/{MPASearchResult}", new PropertyRewrite()));
        routes.Add("MStatutes", new System.Web.Routing.Route("m/Statutes/{MStatutes}", new PropertyRewrite()));
        routes.Add("MStatutesIndex", new System.Web.Routing.Route("m/Statutes/{MStatutes}/{MStatutesIndex}", new PropertyRewrite()));
        routes.Add("MByPages", new System.Web.Routing.Route("m/en/{Mpage}", new PropertyRewrite()));





        //routes.Add("ByLatestPromotion", new System.Web.Routing.Route("Promotions/{latestpromotion}", new PropertyRewrite()));
        //routes.Add("ByCatalogPromotion", new System.Web.Routing.Route("Catalog/{TopCatalog}", new PropertyRewrite()));




        //routes.Ignore("{resource}.axd/{*pathInfo}")
        //routes.Ignore("Telerik.Web.UI.DialogHandler.aspx")


        //routes.MapPageRoute("", "Languages/{Paer}/{SectionName}", "~/Page.aspx", True, New System.Web.Routing.RouteValueDictionary() From {{"SectionName", "A"}, {"Paper", "Computer"}})


        //routes.MapPageRoute("abc", "Country/{locale}/{year}", "~/WorldPage.aspx", True, New System.Web.Routing.RouteValueDictionary() From {{"locale", "[a-z]{2}-[a-z]{2}"}, {"year", "\d{4}"}})

        //routes.MapPageRoute("Browse-Subjects", "Subjects/{name}", "~/ShowPage.aspx")

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        try
        {
            //  Exception exc = Server.GetLastError();
            // Code that runs when an unhandled error occurs
            //HttpRequest httpRequest = HttpContext.Current.Request;
            //if (httpRequest.Browser.IsMobileDevice)
            //{
            //    string path = httpRequest.Url.PathAndQuery;
            //    bool isOnMobilePage = path.StartsWith("/Mobile/",
            //                           StringComparison.OrdinalIgnoreCase);
            //    if (!isOnMobilePage)
            //    {
            //        string redirectTo = "~/Mobile/";

            //         Could also add special logic to redirect from certain 
            //         recognized pages to the mobile equivalents of those 
            //         pages (where they exist). For example,
            //         if (HttpContext.Current.Handler is UserRegistration)
            //             redirectTo = "~/Mobile/Register.aspx";

            //        HttpContext.Current.Response.Redirect(redirectTo);
            //    }
            //}
            //var url = "";
            //var page = "";
            //if (HttpContext.Current != null)
            //{
            //    url = HttpContext.Current.Request.Url.ToString();
            //    // page = HttpContext.Current.Handler as System.Web.UI.Page;
            //}
            //  Email.SendMail("umar.mughal83@gmail.com", "Application Error <br> Error Type:" + exc.GetType() + "<br>URL:" + url.ToString(), "Eastlaw Application Error", "eastlaw.pk", "");
            // Response.Redirect("/");
        }
        catch { }

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        //HttpRequest httpRequest = HttpContext.Current.Request;
        //if (httpRequest.Browser.IsMobileDevice)
        //{
        //    string path = httpRequest.Url.PathAndQuery;
        //    bool isOnMobilePage = path.StartsWith("/Mobile/",
        //                           StringComparison.OrdinalIgnoreCase);
        //    if (!isOnMobilePage)
        //    {
        //        string redirectTo = "~/m/";

        //        // Could also add special logic to redirect from certain 
        //        // recognized pages to the mobile equivalents of those 
        //        // pages (where they exist). For example,
        //        // if (HttpContext.Current.Handler is UserRegistration)
        //        //     redirectTo = "~/Mobile/Register.aspx";

        //        HttpContext.Current.Response.Redirect(redirectTo);
        //    }
        //}
    }

    void Session_End(object sender, EventArgs e)
    {
        try
        {
            string UserName = "umar";
            if (Session["UserLogged"] != null)
            {
                HttpContext.Current.Application.Remove("usr_" + Session["UserLogged"].ToString());
            }
            // NOTE: you might want to call this from the .Logout() method - aswell -, to speed things up
            //string userLoggedIn = Session["UserLoggedIn"] == null ? string.Empty ? (string)Session["UserLoggedIn"];
            //string userLoggedIn = null;

            //if (Session["UserLoggedIn"] != null)
            //    userLoggedIn = Session["UserLoggedIn"].ToString();

            //if (userLoggedIn.Length > 0)
            //{
            //    System.Collections.Generic.List<string> d = Application["UsersLoggedIn"]
            //        as System.Collections.Generic.List<string>;
            //    if (d != null)
            //    {
            //        lock (d)
            //        {
            //            d.Remove(userLoggedIn);
            //        }
            //    }
            //}
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
        catch { }
    }

</script>
