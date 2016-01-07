using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScarecrowCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Dao.TbTasks dao_Task = new Dao.TbTasks();
            var listAll = dao_Task.GetAll();

            int loop = 0;
            while (true) {

                Entity.TbTasks entity = listAll[loop% listAll.Count];
                if (entity.TaskEnable)
                {
                    SeleniumHelper.MyWebDriver.getShootImg(entity);
                    SeleniumHelper.MyWebDriver.handleImg(entity);
                    System.Threading.Thread.Sleep(1000);
                }
                loop++;

            }

          
        }
    }
}
