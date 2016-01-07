using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Type;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.IO;

namespace NhibernateDataAccess
{
    public  class DataAccess<T, IDT>  
    {

        public static string localPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/";

        public static ISessionFactory sessionFactory_Default = new Configuration().Configure(localPath+"default.cfg.xml").BuildSessionFactory();

        public ISessionFactory sessionFactory { get { return sessionFactory_Default; } }
        public string TableName { get; set; }
    

        protected DataAccess()
        {
            
        }

   

        public virtual object Add(T model)
        {

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                   
                    try
                    {
                        //Persistent
                        object obj=session.Save(model);

                        //保存记录后修改数据，观察数据库中数据的变化
                        //product.SellPrice = 12M;

                        tran.Commit();
                        return obj;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public virtual void Add(List<T> modelList)
        {

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {

                    try
                    {
                        foreach (T t in modelList) {
                            object obj = session.Save(t);
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
        }




        public virtual void AddOrUpdate(T model)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {

                    try
                    {
                        session.SaveOrUpdate(model);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public T Get(int id) {
            using (ISession session = sessionFactory.OpenSession()){
               T model= session.Get<T>(id);
               return model;
            }
        }

        public T Get(long id)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                T model = session.Get<T>(id);
                return model;
            }
        }




        public T Get(string where, List<object> args) {
            using (ISession session = sessionFactory.OpenSession())
            {
                string str = string.Format("from {0} where {1}", this.TableName,where);
                IQuery iq = session.CreateQuery(str);
                for(int i=0;i<args.Count;i++){
                    iq.SetParameter(i, args[i]);
                }
                return iq.UniqueResult<T>();
            }
        }

        public List<T> GetAll() {
            return GetList("1=1", new List<object> { });
        }

        

        public List<T> GetList(string where, List<object> args)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                string str = string.Format("from {0} where {1}", this.TableName, where);
                IQuery iq = session.CreateQuery(str);
                for (int i = 0; i < args.Count; i++)
                {
                    iq.SetParameter(i, args[i]);
                }
                IList<T> collection = iq.List<T>();
                return new List<T>(collection);
            }
        
        }


        public int GetCount(string where, List<object> args)
        {

            using (ISession session = sessionFactory.OpenSession())
            {
                string str = string.Format("select count(*) from {0} where {1} ", this.TableName, where);
                
                IQuery iq = session.CreateQuery(str);
                for (int i = 0; i < args.Count; i++)
                {
                    iq.SetParameter(i, args[i]);
                }

                int count=  Convert.ToInt32(iq.UniqueResult());
                return count;
            }
        
        
        }

        public List<T> GetPagedList(string where, List<object> args, int pageSize, int pageIndex)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                string str = string.Format("from {0} where {1}", this.TableName, where);
                IQuery iq = session.CreateQuery(str);
                iq.SetFirstResult(pageSize * (pageIndex - 1));
                iq.SetMaxResults(pageSize);
                for (int i = 0; i < args.Count; i++)
                {
                    iq.SetParameter(i, args[i]);
                }
                IList<T> collection = iq.List<T>();
                return new List<T>(collection);
            }
        }

        public List<T> GetListByDate(DateTime dt,string dateColumu) {
            using (ISession session = sessionFactory.OpenSession())
            {
                string str = string.Format("from {0} where " + dateColumu + " between ? and ? ", this.TableName);
                var args=new List<object>{dateColumu,dt.Date,dt.Date.AddDays(1)};
                IQuery iq = session.CreateQuery(str);
                iq.SetDateTime(0, dt.Date) .SetDateTime(1, dt.Date.AddDays(1));
                IList<T> collection = iq.List<T>();
                return new List<T>(collection);
            }
        }

        public List<T> GetListToday(string dateColumu)
        {
           return GetListByDate(DateTime.Now, dateColumu);
        }

        public virtual bool Exist(string where, List<object> list)
        {
            try
            {
                return (GetCount(where, list) > 0);
            }
            finally
            {
            }
        }


        public virtual ISession GetSession()
        {
            return sessionFactory.OpenSession();
        }


        //public virtual int Delete(string where)
        //{
        //    return this.NHibernateSession.Delete(string.Format("from {0} where {1}", this.TableName, where));
        //}

        //public virtual void Delete(IDT id)
        //{
        //    this.NHibernateSession.Delete(this.GetModel(id));
        //}

        //public virtual bool Exist(string where)
        //{
        //    try
        //    {
        //        return (this.GetCount(where) > 0);
        //    }
        //    finally
        //    {
        //    }
        //}

        //public virtual int GetCount(string where)
        //{
        //    return this.GetCount(this.TableName, where);
        //}

        //public virtual int GetCount(string tablename, string where)
        //{
        //    try
        //    {
        //        string queryString = string.Format("select count(*) from {0}", tablename);
        //        if (!string.IsNullOrEmpty(where.Trim()))
        //        {
        //            queryString = queryString + string.Format(" where {0}", where);
        //        }
        //        return Convert.ToInt32(this.NHibernateSession.CreateQuery(queryString).UniqueResult());
        //    }
        //    finally
        //    {
        //    }
        //}

        //public virtual int GetCountByHQL(string hql)
        //{
        //    try
        //    {
        //        return Convert.ToInt32(this.NHibernateSession.CreateQuery(hql).UniqueResult());
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //}

        //public virtual T GetModel(IDT id)
        //{
        //    try
        //    {
        //        return this.NHibernateSession.Get<T>(id);
        //    }
        //    finally
        //    {
        //    }
        //}

        //public virtual IList GetModelList(int pageIndex, int pageSize, string sql)
        //{
        //    try
        //    {
        //        string queryString = string.Format("from {0} ", this.TableName);
        //        if (!string.IsNullOrEmpty(sql))
        //        {
        //            queryString = string.Format(" {0}", sql);
        //        }
        //        IQuery query = this.NHibernateSession.CreateQuery(queryString);
        //        query.SetFirstResult(pageSize * (pageIndex - 1));
        //        query.SetMaxResults(pageSize);
        //        return query.List();
        //    }
        //    finally
        //    {
        //    }
        //}

        //public virtual List<T> GetPagedModelList(int pageIndex, int pageSize, string whereClause, string orderClause, out int total)
        //{
        //    try
        //    {
        //        string queryString = string.Format("from {0} ", this.TableName);
        //        if (!string.IsNullOrEmpty(whereClause.Trim()))
        //        {
        //            queryString = queryString + string.Format(" where {0}", whereClause);
        //        }
        //        if (!string.IsNullOrEmpty(orderClause))
        //        {
        //            queryString = queryString + string.Format(" order by {0}", orderClause);
        //        }
        //        IQuery query = this.NHibernateSession.CreateQuery(queryString);
        //        query.SetFirstResult(pageSize * (pageIndex - 1));
        //        query.SetMaxResults(pageSize);
        //        IList<T> collection = query.List<T>();
        //        total = this.GetCount(whereClause);
        //        return new List<T>(collection);
        //    }
        //    finally
        //    {
        //    }
        //}

        //public virtual T GetSingle(string where)
        //{
        //    try
        //    {
        //        string str = string.Format("from {0}", this.TableName);
        //        if (!string.IsNullOrEmpty(where))
        //        {
        //            str = string.Format("{0} where {1}", str, where);
        //        }
        //        return this.NHibernateSession.CreateQuery(str).UniqueResult<T>();
        //    }
        //    finally
        //    {
        //    }
        //}

        //public virtual object GetSingleByHql(string hql)
        //{
        //    try
        //    {
        //        IQuery query = this.NHibernateSession.CreateQuery(hql);
        //        if (query.List().Count > 0)
        //        {
        //            return query.List()[0];
        //        }
        //        return null;
        //    }
        //    finally
        //    {
        //    }
        //}




 

       
    }
}
