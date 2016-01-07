using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace scarecrow
{
    class Program
    {
        static void Main(string[] args)
        {
 
            Dao.TbTasks dao_Task = new Dao.TbTasks();
            var listAll = dao_Task.GetAll();

            foreach (Entity.TbTasks entity in listAll) {
                SeleniumHelper.getShootImg(entity);
                Warden.handleImg(entity);

            }

            


        }
    }
}
