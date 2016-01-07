using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Handler_addTask : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = Request["browser_url"];
        string browserType= Request["browser_type"];
        string script = Request["browser_script"];

        int width = Convert.ToInt32(Request["browser_width"]);
        int height = Convert.ToInt32(Request["browser_height"]);
        


        Dao.TbTasks daoTask = new Dao.TbTasks();
        Entity.TbTasks entityTask = new Entity.TbTasks();

        entityTask.TaskTime = DateTime.Now;
        entityTask.TaskUrl = url;
        entityTask.TaskType = browserType;
        entityTask.TaskBeforeScript = script;

        entityTask.TaskWidth = width;
        entityTask.TaskHeight = height;

        daoTask.Add(entityTask);

        SeleniumHelper.MyWebDriver.getShootImg(entityTask);


        StringBuilder sb = new StringBuilder();
        LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
        jw.WriteObjectStart();

        jw.WritePropertyName("taskId");
        jw.Write(entityTask.TaskId);

        jw.WritePropertyName("path");
        jw.Write("/Handler/downLoadImg.aspx?path=" + SeleniumHelper.MyWebDriver.SHOOT_IMG_PATH+ entityTask.TaskId+".png");

        jw.WriteObjectEnd();

        Response.Write(sb.ToString());

    }
}