using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NhibernateDataAccess;

namespace Dao
{
    public partial class TbError : NhibernateDataAccess.DataAccess<Entity.TbError, int>
    {
        public TbError()
        {
            TableName = "TbError";
       

        }

    }
}
