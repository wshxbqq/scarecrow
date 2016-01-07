using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Handler_setTaskStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

        long taskId = Convert.ToInt64(Request.QueryString["taskId"]);

        if (!File.Exists("D:/scarecrow/tpl/"+ taskId+".png")) {
            Response.Write("0");
            return;
        }

        Dao.TbTasks daoTask = new Dao.TbTasks();
        var entityTask = daoTask.Get(taskId);


        if (Request.QueryString["Task_enable"]!=null) {
            entityTask.TaskEnable = Convert.ToBoolean(Request.QueryString["Task_enable"]);
        }

        if (Request.QueryString["Task_alert"] != null)
        {
            entityTask.TaskAlert = Convert.ToBoolean(Request.QueryString["Task_alert"]);
        }

         

        daoTask.AddOrUpdate(entityTask);
        Response.Write("1");

    }
}