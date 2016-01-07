using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Handler_downLoadImg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "image/png";

        System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(Request.Params["path"]);
        bitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
        
        bitmap.Dispose();

    }
}