using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;


public partial class Handler_indexList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Dao.TbTasks daoTask = new Dao.TbTasks();
        var list = daoTask.GetAll();

        //StringBuilder sb = new StringBuilder();
        //LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);

        //jw.WriteArrayStart();

        //foreach (var ent in list) {


        //}

        //jw.WriteArrayEnd();


       string JSON= LitJson.JsonMapper.ToJson(list);
        Response.Write(JSON);
    }
}