using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Handler_clearError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        long taskId = Convert.ToInt64(Request.QueryString["taskId"]);
        Dao.TbTasks daoTask = new Dao.TbTasks();
        var entity= daoTask.Get(taskId);
        entity.TaskErrorCount = 0;
        daoTask.AddOrUpdate(entity);
        Response.Write("1");

    }
}