using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Handler_upLoadImg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        Request.Files[0].SaveAs(SeleniumHelper.MyWebDriver.TPL_IMG_PATH+id+".png");
        Response.Write("1");
        
    }
}