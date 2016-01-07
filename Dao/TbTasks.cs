using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NhibernateDataAccess;

namespace Dao
{
    public partial class TbTasks : NhibernateDataAccess.DataAccess<Entity.TbTasks, int>
    {
        public TbTasks()
        {
            TableName = "TbTasks";
       

        }

    }
}
